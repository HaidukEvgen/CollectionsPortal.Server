using CollectionsPortal.Server.Api.Attributes;
using CollectionsPortal.Server.Api.Extensions;
using CollectionsPortal.Server.Api.Middlewares;
using CollectionsPortal.Server.BusinessLayer.Settings;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.Configure<AzureBlobServiceOptions>(builder.Configuration.GetSection("AzureBlobServiceOptions"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .ConfigureCors(builder.Configuration)
    .ConfigureSqlServer(builder.Configuration)
    .ConfigureBusinessServices()
    .ConfigureRepositories()
    .ConfigureAutoMapper()
    .ConfigureIdentity(builder.Configuration)
    .ConfigureJwtAuthentication(builder.Configuration)
    .ConfigureSwagger()
    .AddAuthorization(options =>
    {
        options.AddPolicy(nameof(CollectionAccessForAdminOrCreatorRequirement), policy =>
            policy.Requirements.Add(new CollectionAccessForAdminOrCreatorRequirement()));
    })
    .AddControllers();

builder.Services.AddTransient<IAuthorizationHandler, CollectionAccessHandler>();

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<BanCheckMiddleware>();

app.MapControllers();

app.ApplyMigrations(app.Services);

app.Run();