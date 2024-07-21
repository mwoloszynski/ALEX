using COMMON.Models;

namespace ALEX_API.Helpers
{
    public class AppSettingsManager
    {
        public static void Setup(Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            ALEX_API.Helpers.ConfigurationManager.manager = configuration;

            var appSettingsSection = configuration.GetSection(nameof(AppSettings));

            AppSettings.ConnectionString = appSettingsSection.GetSection(nameof(AppSettings.ConnectionString)).Get<string>() ?? throw new Exception($"{nameof(AppSettings.ConnectionString)} in appsetings.json is invalid");
        }
    }

    public static class ConfigurationManager
    {
        public static IConfiguration manager { get; set; }


        public static string Get(string key)
        {
            if (manager == null)
                return string.Empty;
            return manager.GetSection(key)?.Value;
        }
    }
}
