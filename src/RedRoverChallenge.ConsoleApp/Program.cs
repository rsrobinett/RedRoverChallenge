using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedRoverChallenge.SolveService.SolveHandler;

namespace RedRoverChallenge.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = host.Services.GetRequiredService<App>();
        app.Run();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                // Get the assembly where ISolveHandler is defined
                var assembly = typeof(ISolveHandler).Assembly;

                // Find all types that implement ISolveHandler
                var solverTypes = assembly.GetTypes()
                    .Where(t => typeof(ISolveHandler).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

                // Register each solver type
                foreach (var solverType in solverTypes)
                {
                    services.AddTransient(typeof(ISolveHandler), solverType);
                }

                // Register the App class
                services.AddTransient<App>();
            });
}