using ApiSegundoOAuth.Data;
using ApiSegundoOAuth.Helpers;
using ApiSegundoOAuth.Repositories;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
    (builder.Configuration.GetSection("KeyVault"));
});
SecretClient secretClient =
builder.Services.BuildServiceProvider().GetService<SecretClient>();


KeyVaultSecret secret =
    await secretClient.GetSecretAsync("SqlAzure");
KeyVaultSecret audienceKey = await secretClient.GetSecretAsync("Audience");
KeyVaultSecret issuerKey = await secretClient.GetSecretAsync("Issuer");
KeyVaultSecret secretKey = await secretClient.GetSecretAsync("SecretKey");


string connectionString = secret.Value;
string secretKeyValue = secretKey.Value;
string audience = audienceKey.Value;
string issuer = issuerKey.Value;

HelperActionServicesOAuth helper = new HelperActionServicesOAuth(secretKeyValue, audience, issuer);

builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
//builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);

builder.Services.AddAuthentication
    (helper.GetAuthenticateSchema())
    .AddJwtBearer(helper.GetJwtBearerOptions());


builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API OAuth Cubos",
        Description = "Api con token de seguridad. Segundo examen",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
                , name: "API OAuth Cubos");
            options.RoutePrefix = "";
        });

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
