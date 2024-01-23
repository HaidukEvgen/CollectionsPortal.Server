using CollectionsPortal.Server.Api.Middlewares;
using CollectionsPortal.Server.BusinessLayer.Infrastructure.Mapper;
using CollectionsPortal.Server.BusinessLayer.Services.Implementations;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.BusinessLayer.Settings;
using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Implementations;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CollectionsPortal.Server.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins(config["ClientUrls:Localhost"], config["ClientUrls:VercelClient"])
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            return services;
        }

        public static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("CollectionsDbConnectionString");
            services.
                AddDbContext<AppDbContext>(
                    b => b.UseLazyLoadingProxies()
                          .UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAzureService, AzureSerivce>();
            services.AddScoped<ICollectionService, CollectionService>();

            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICollectionCategoryRepository, CollectionCategoryRepository>();
            services.AddScoped<IItemTagRepository, ItemTagRepository>();
            services.AddScoped<ICollectionItemRepository, CollectionItemRepository>();

            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtSection);
            var jwtOptions = jwtSection.Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)
                    ),
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Collections Portal API",
                    Version = "v1",
                    Description = "Collections Portal API"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static void ApplyMigrations(this IApplicationBuilder app, IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }
        }
    }
}