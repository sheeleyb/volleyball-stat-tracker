using StatTrackerGlobal.App;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Client;
using StatTrackerGlobal.Client.Pages;
using StatTrackerGlobal.Client.Store;
using StatTrackerGlobal.Components;
using StatTrackerGlobal.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();




CommonServices.ConfigureServices(builder.Services);

////builder.Services.AddScoped<ViewState>();
//builder.Services.AddScoped<IPersistence, InMemoryPersistenceService>();
//builder.Services.AddScoped<IApplicationService, ApplicationService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(StatTrackerGlobal.Client._Imports).Assembly);

app.Run();
