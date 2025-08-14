using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StatTrackerGlobal.Client;
using StatTrackerGlobal.Client.Store;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        CommonServices.ConfigureServices(builder.Services);

        //builder.Services.AddScoped<ViewState>();

        await builder.Build().RunAsync();
    }
}