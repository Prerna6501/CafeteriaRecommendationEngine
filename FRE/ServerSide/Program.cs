using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerSide.Data;
using ServerSide.Data.Extentions;

namespace ServerSide
{
    class program
    {
        static async Task Main(string[] args)
        {          
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
           .Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<CafeteriaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("REDatabase")));
            services.RegisterRepositories();
            services.RegisterServices();

            var app = services.BuildServiceProvider();

            using (var scope = app.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<CafeteriaDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    //Add Logger
                }
            }

            await Server.StartServer(app);
        }
    }
}

