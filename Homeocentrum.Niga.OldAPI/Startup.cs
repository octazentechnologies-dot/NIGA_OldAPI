using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Homeocentrum.Niga.OldAPI.Logging;
using Homeocentrum.Niga.OldAPI.Business.Implementation;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Business.Interfaces;
using Homeocentrum.Niga.OldAPI.Business.Services;
using Homeocentrum.Niga.OldAPI.Common;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Homeocentrum.Niga.OldAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
            Flags = FeatureFlags.Load(configuration);
        }

        public IConfiguration Configuration { get; }
        public string ContentRootPath { get; }
        public FeatureFlags Flags { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Unable resources sharing
            //services.AddCors();
            // Any browser origin unless Cors:AllowedOrigins lists specific ones. Credentials are never allowed cross-origin.
            if (Flags.EnableCors)
            {
                var configuredOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[0];
                var allowedOrigins = new HashSet<string>(configuredOrigins, StringComparer.OrdinalIgnoreCase);
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAllOrigins",
                        builder =>
                        {
                            builder.SetIsOriginAllowed(origin => allowedOrigins.Count == 0 || allowedOrigins.Contains(origin.TrimEnd('/')))
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders("X-Trace-Id");
                        });
                });
            }
            if (Flags.EnableResponseCompression)
            {
                services.AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                    options.Providers.Add<GzipCompressionProvider>();
                });
                services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            }
            var defaultConnection = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found in appsettings.json.");
            }
            var sensitiveDataLogging = Flags.EnableSensitiveDataLogging;
            services.AddDbContext<NIGACentrumContext>(options =>
            {
                options.UseSqlServer(defaultConnection, sql => sql.CommandTimeout(90));
                if (sensitiveDataLogging)
                    options.EnableSensitiveDataLogging();
            });
            services.AddMemoryCache();
            services.AddMvc(options => options.Filters.Add(new SafeServerErrorResultFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiProblem.Validation;
            });
            services.AddHealthChecks();
            //services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<SmtpSettingsModel>(option => Configuration.GetSection("smtp").Bind(option));
            services.AddLogging(builder =>
            {
                builder.AddFilter<AppFileLoggerProvider>(null, LogLevel.Debug);
                builder.AddProvider(new AppFileLoggerProvider());
            });
            if (Flags.EnableBackgroundJobs)
                services.AddHostedService<DailyIssueMatrixEmailService>();
            services.Configure<ConfigurationModel>(option => Configuration.GetSection("ConfigurationModel").Bind(option));

            // configure jwt authentication
            var jwtSecret = Configuration["JWT:Secret"];
            var jwtIssuer = Configuration["JWT:Issuer"];
            var jwtAudience = Configuration["JWT:Audience"];
            
            var key = Encoding.UTF8.GetBytes(jwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5) // Allow 5 minutes clock skew
                };
            });

            // M02 W0 — Admin Portal policy for clinical masters mutate APIs (apply in W1+)
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.AdminPortal, policy =>
                    policy.RequireAuthenticatedUser()
                          .RequireAssertion(ctx =>
                              Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.IsAdminPortalUser(ctx.User)));
            });

            //Register all injecting interfaces with implemented class
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogDetailService, BlogDetailService>();
            services.AddScoped<IEnquiryDetailService, EnquiryDetailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IMastersAPIService, MastersAPIService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPathologyService, PathologyService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped <ICaseDetailsService, CaseDetailsService>();
            services.AddScoped<IQualificationService, QualificationService>();
            services.AddScoped<IDiagnosisGroupService, DiagnosisGroupService>();
            services.AddScoped<IDiagnosisSystemService, DiagnosisSystemService>();
            services.AddScoped<IDiagnosisService, DiagnosisService>();
            services.AddScoped<ILanguageMasterService, LanguageMasterService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ISubSectionService, SubSectionService>();
            services.AddScoped<IRemedyService, RemedyService>();
           services.AddScoped<IMateriaMedicaDetailService,MateriaMedicaDeatailsService>();
            services.AddScoped<IMateriaMedicaHeadMasterService, MateriaMedicaHeadService>();
            services.AddScoped<IMateriaMedicaMasterService, MateriaMedicaMasterService>();
            services.AddScoped<IMateriaMedicaRemediesDetails,MateriaMedicaRemediesDetailsService>();
            services.AddScoped<IIntensityService, IntensityService>();
            services.AddScoped<IRemedyGradeService, RemedyGradeService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IBodyPartService, BodyPartService>();
            services.AddScoped<IQuestionSectionService, QuestionSectionService>();
            services.AddScoped<IQuestionSubGroupService, QuestionSubGroupService>();
            services.AddScoped<IPartLocationService, PartLocationService>();
            services.AddScoped<IClinicalQuestionsService, ClinicalQuestionsService>();
            services.AddScoped<IClinicalQueKeywordService, ClinicalQueKeywordService>();
            services.AddScoped<IQuestionGroupService, QuestionGroupService>();
            services.AddScoped<IRubricRemedyDetailsService, RubricRemedyDetailsService>();
            services.AddScoped<IPatientLabOrderServices, PatientLabOrderServices>();
            services.AddScoped<IPatientLabEntryServices, PatientLabEntryServices>();
            services.AddScoped<ILabTestMasterServices, LabTestMasterServices>();
            services.AddScoped<IMenuMasterService, MenuMasterService>();
            services.AddScoped<IRoleMasterService, RoleMasterService>();
            services.AddScoped<IRoleDetailsService, RoleDetailsService>();
            services.AddScoped<IPatientAppointmentService, PatientAppointmentService>();
            services.AddScoped<IDoctorDashBoardService, DoctorDashBoardService>();
            services.AddScoped<IClipboardRubricsService, ClipboardRubricsService>();
            services.AddScoped<IMonoGramService,MonogramService>();
            services.AddScoped<INewsDetailService, NewsDetailService>();
            services.AddScoped<INewsCategoryService, NewsCategoryService>();
            services.AddScoped<IAllopathicDrugService, AllopathicDrugService>();
            services.AddScoped<IAdverseReactionService, AdverseReactionService>();
            services.AddScoped<IDrugGroupService, DrugGroupService>();
            services.AddScoped<IDrugSystemService, DrugSystemService>();
            services.AddScoped<IAdverseReactionService, AdverseReactionService>();
            services.AddScoped<IOtherSideEffectService, OtherSideEffectService>();
            services.AddScoped<ISeriousSideEffectService, SeriousSideEffectService>();
            services.AddScoped<IDiagnosisTherapeuticsDetailService, DiagnosisTherapeuticsDetailService>();
            services.AddScoped<IDropdownListService, DropdownListService>();
            services.AddScoped<IRepertorizationPageService, RepertorizationPageService>();
            services.AddScoped<IPatientLabTestService, PatientLabTestService>();
            services.AddScoped<IPaginationService, PaginationService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IAppointmentHistoryNoteService, AppointmentHistoryNoteService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ITokenService, TokenService>();
            ////comment below part at the time host
            //// Register the Swagger generator, defining 1 or more Swagger documents
            if (Flags.EnableSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Homeocentrum Old API", Version = "v1" });

                    var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", new string[] { }},
                    };

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });
                    c.AddSecurityRequirement(security);



                    var filePath = Path.Combine(AppContext.BaseDirectory, "Homeocentrum.Niga.OldAPI.xml");
                    c.IncludeXmlComments(filePath);

                });
            }
            ////up to 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AppFileLog.Initialize(env.ContentRootPath, Configuration);
            SecurityAudit.Initialize(Configuration);
            Homeocentrum.Niga.OldAPI.Security.PatientAccess.Initialize(Configuration);
            AppFileLog.SendDeployNotice("started");
            var lifetime = app.ApplicationServices.GetService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>();
            if (lifetime != null)
            {
                lifetime.ApplicationStarted.Register(() => AppFileLog.SendDeployNotice("ready"));
                lifetime.ApplicationStopping.Register(AppFileLog.SendStopNotice);
            }
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                AppFileLog.Write("errors", "CRITICAL", "Process", "Unhandled exception; process terminating=" + e.IsTerminating, ex, null, false);
                AppFileLog.SendOpsAlert("crash", "API crashed", new Dictionary<string, string>
                {
                    { "Exception", ex == null ? "unknown" : ex.GetType().FullName },
                    { "Message", ex == null ? "" : ex.Message },
                    { "Terminating", e.IsTerminating.ToString() },
                }, 0, true);
            };

            // IIS reverse-proxies to Kestrel on this machine; X-Forwarded-For is trusted only from loopback proxies (the default).
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });

            if (Flags.EnableResponseCompression)
                app.UseResponseCompression();

            if (Flags.EnableMaintenanceMode)
            {
                // CORS runs first so a browser on another origin can read the 503 body.
                if (Flags.EnableCors)
                    app.UseCors("AllowAllOrigins");
                var maintenanceJson = JsonConvert.SerializeObject(new { success = false, status = 503, message = Flags.MaintenanceMessage });
                app.Use(async (context, next) =>
                {
                    if ((context.Request.Path.Value ?? "").StartsWith("/health", StringComparison.OrdinalIgnoreCase))
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    context.Response.ContentType = "application/json";
                    context.Response.Headers["Retry-After"] = "300";
                    await context.Response.WriteAsync(maintenanceJson);
                });
            }

            // The developer exception page shows stack traces and source. It only runs for a local developer:
            // Development environment, not hosted by IIS, and only for loopback callers.
            var underIis = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APP_POOL_ID"))
                || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH"));
            if (env.IsDevelopment() && !underIis)
            {
                app.UseWhen(ctx => ctx.Connection.RemoteIpAddress != null && System.Net.IPAddress.IsLoopback(ctx.Connection.RemoteIpAddress),
                    local => local.UseDeveloperExceptionPage());
            }
            app.UseExceptionHandler(errorApp => errorApp.Run(async ctx =>
            {
                var feature = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                var body = SafeError.Capture(feature?.Error, ctx, "Unhandled");
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";
                await ctx.Response.WriteAsync(SafeError.ToJson(body));
            }));

            if (Flags.EnableSwagger)
            {
                app.Use(async (context, next) =>
                {
                    if (HttpMethods.IsGet(context.Request.Method)
                        && (context.Request.Path.Value == "/" || string.IsNullOrEmpty(context.Request.Path.Value)))
                    {
                        context.Response.Redirect("/swagger");
                        return;
                    }
                    await next();
                });
            }

            // Before security, rate-limit and health responses so browsers on any origin can read them (including 429).
            if (Flags.EnableCors && !Flags.EnableMaintenanceMode)
                app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseMiddleware<HostSecurityMiddleware>();
            app.UseMiddleware<AppDiagnosticsMiddleware>();
            if (Flags.EnableRateLimiting)
                app.UseMiddleware<SimpleRateLimitMiddleware>();
            app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                ResponseWriter = (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"success\":true,\"status\":\"" + report.Status + "\",\"api\":\"Old API\"}");
                }
            });
            app.UseHomeocentrumFavicon();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Resources", "NewsImages")),
                RequestPath = "/NewsImages",
                ServeUnknownFileTypes = true,
                DefaultContentType = "image"
            });


            if (Flags.EnableSwagger)
                app.UseMiddleware<SwaggerGateMiddleware>();
            app.UseMvc();

            ////comment below part at the time host
            if (Flags.EnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Homeocentrum Old API");
                    c.DocumentTitle = "Homeocentrum Old API";
                    c.HeadContent =
                        "<link rel=\"icon\" type=\"image/png\" href=\"/favicon.png\" />" +
                        "<link rel=\"shortcut icon\" href=\"/favicon.ico\" />" +
                        "<script>document.addEventListener('DOMContentLoaded',function(){" +
                        "document.querySelectorAll('link[rel*=\"icon\"]').forEach(function(el){el.parentNode.removeChild(el);});" +
                        "var l=document.createElement('link');l.rel='icon';l.type='image/png';l.href='/favicon.png?v=hc';document.head.appendChild(l);" +
                        "});</script>";
                });
            }
            ////up to
        }
    }
}
