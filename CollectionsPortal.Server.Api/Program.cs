using CollectionsPortal.Server.Api.Extensions;
using CollectionsPortal.Server.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .ConfigureCors(builder.Configuration)
    .ConfigureSqlServer(builder.Configuration)
    .ConfigureBusinessServices()
    .ConfigureAutoMapper()
    .ConfigureIdentity(builder.Configuration)
    .ConfigureJwtAuthentication(builder.Configuration)
    .ConfigureSwagger()
    .AddControllers();

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