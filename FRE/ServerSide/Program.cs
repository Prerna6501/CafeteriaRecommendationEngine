using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;
using ServerSide.Data;
using ServerSide.Data.Extentions;

namespace ServerSide
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
           .Build();
            
            var serviceProvider = new ServiceCollection();

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();

            var services = new ServiceCollection();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<CafeteriaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("REDatabase")));
            services.RegisterRepositories();
            services.RegisterServices();

            var app = services.BuildServiceProvider();
            var logger = app.GetService<ILogger<Program>>();
            logger.LogInformation("In program.cs server side");
           
            using (var scope = app.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    logger.LogInformation("Running the DbInilizer....");
                    var context = service.GetRequiredService<CafeteriaDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Exception occured in creating the db.");
                }
            }

            await Server.StartServer(app);
        }
    }
}

