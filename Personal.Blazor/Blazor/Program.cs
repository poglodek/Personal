using Blazor.Clients;
using Blazor.Components;
using Blazor.Services.UserService;
using Blazored.LocalStorage;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient()
    .AddRefitClient<IPersonalClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"));

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();