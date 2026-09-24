using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Homeocentrum.Niga.OldAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }
        public string ContentRootPath { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Unable resources sharing
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            var sqlOnly = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
            var defaultConnection = sqlOnly.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found in appsettings.json.");
            }
            services.AddDbContext<NIGACentrumContext>(options =>
                options.UseSqlServer(defaultConnection, sql => sql.CommandTimeout(90)));
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            ////up to 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AppFileLog.Initialize(env.ContentRootPath, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

            app.UseAuthentication();
            app.UseMiddleware<AppDiagnosticsMiddleware>();
            app.UseMiddleware<SimpleRateLimitMiddleware>();
            app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                ResponseWriter = (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"success\":true,\"status\":\"" + report.Status + "\",\"api\":\"Old API\"}");
                }
            });
            //app.UseCors(builder => builder.AllowAnyOrigin()
            //                    .AllowAnyMethod()
            //                    .WithHeaders("authorization", "accept", "content-type", "origin"));

            app.UseCors("AllowAllOrigins");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Resources", "NewsImages")),
                RequestPath = "/NewsImages",
                ServeUnknownFileTypes = true,
                DefaultContentType = "image"
            });


            app.UseMiddleware<SwaggerGateMiddleware>();
            app.UseMvc();

            ////comment below part at the time host
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Homeocentrum Old API");
                c.DocumentTitle = "Homeocentrum Old API";
            });
            ////up to
        }
    }
}
