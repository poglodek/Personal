using ApiShared;
using Auth;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallModules();


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

