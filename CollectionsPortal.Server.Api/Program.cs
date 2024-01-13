using CollectionsPortal.Server.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .ConfigureCors(builder.Configuration)
    .ConfigureSqlServer(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations(app.Services);

app.Run();