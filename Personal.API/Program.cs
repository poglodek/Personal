using System.Reflection;
using ApiShared;
using Auth;
using Notification.Shared;
using User.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallModules(typeof(UserModule), typeof(WardModule.Shared.WardModule), typeof(WorkoutModule.Shared.WorkoutModule), typeof(NotificationModule));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => $"{DateTimeOffset.Now} - Personal API");

app.UseModules();

await app.RunAsync();

