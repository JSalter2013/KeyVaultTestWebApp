using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KeyVaultTestWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        var builtConfig = config.Build();

                        var keyVaultConfigBuilder = new ConfigurationBuilder();

                        keyVaultConfigBuilder.AddAzureKeyVault(
                            $"https://{builtConfig["Vault"]}.vault.azure.net/",
                            builtConfig["ClientId"],
                            builtConfig["ClientSecret"]);

                        var keyVaultConfig = keyVaultConfigBuilder.Build();

                        config.AddConfiguration(keyVaultConfig);
                    })
                .UseStartup<Startup>();
    }
}
