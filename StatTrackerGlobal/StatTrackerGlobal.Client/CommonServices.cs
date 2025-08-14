using Fluxor;
using StatTrackerGlobal.App;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.Data;

namespace StatTrackerGlobal.Client
{
    public static class CommonServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var currentAssembly = typeof(Program).Assembly;
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IPersistence, JsonPersistenceService>();
            services.AddFluxor(options => options.ScanAssemblies(currentAssembly));
        }
    }
}

