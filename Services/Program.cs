using Domain.Entities;
using Services;

internal class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost().Build().Run();
    }

    public static IHostBuilder BuildWebHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>(); 
            });
    }
}
 