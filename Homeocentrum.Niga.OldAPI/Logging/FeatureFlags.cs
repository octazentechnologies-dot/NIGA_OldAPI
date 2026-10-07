using Microsoft.Extensions.Configuration;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>
    /// The FeatureFlags section of appsettings.json, bound once at startup. appsettings.json is loaded without
    /// reload, so a changed flag needs an app restart. Missing keys keep the behaviour the API had before flags existed.
    /// </summary>
    public sealed class FeatureFlags
    {
        public const string SectionName = "FeatureFlags";
        public const string DefaultMaintenanceMessage = "The service is under maintenance. Please try again shortly.";

        public static FeatureFlags Current { get; private set; } = new FeatureFlags();

        public bool EnableSwagger { get; set; } = true;
        public bool ExposeExceptionDetails { get; set; }
        public bool EnableDetailedLogging { get; set; }
        public bool EnableRequestLogging { get; set; } = true;
        public bool EnableResponseLogging { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
        public bool EnableAuditLogging { get; set; } = true;
        /// <summary>The Old API has no refresh-token endpoint, so this flag currently changes nothing.</summary>
        public bool EnableRefreshToken { get; set; } = true;
        public bool EnableRateLimiting { get; set; } = true;
        public bool EnableCors { get; set; } = true;
        public bool EnableSecurityHeaders { get; set; } = true;
        public bool EnableMaintenanceMode { get; set; }
        public string MaintenanceMessage { get; set; } = DefaultMaintenanceMessage;
        public bool EnableResponseCompression { get; set; }
        public bool EnableBackgroundJobs { get; set; } = true;

        public static FeatureFlags Load(IConfiguration configuration)
        {
            var flags = new FeatureFlags();
            if (configuration != null)
            {
                configuration.GetSection(SectionName).Bind(flags);
            }
            if (string.IsNullOrWhiteSpace(flags.MaintenanceMessage))
                flags.MaintenanceMessage = DefaultMaintenanceMessage;
            Current = flags;
            return flags;
        }
    }
}
