using System.Text;
using InstantCmsApi.Auth;
using InstantCmsApi.DataAccess;
using InstantCmsApi.Repository;
using InstantCmsApi.Service;
using InstantCmsApi.Service.Authentication;
using InstantCmsApi.Service.Background;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace InstantCmsApi.Helpers;

public static class StartupHelper
{
    public static void HandleServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IJwtManager, JwtManager>();

        // for background service worker
        services.AddSingleton<IPageWorker, PageWorker>();
        services.AddSingleton<IDataAccess, DataAccess.DataAccess>();

        services.AddSingleton<IImageGalleryService, ImageGalleryService>();
        services.AddSingleton<IImageGalleryTypeService, ImageGalleryTypeService>();
        services.AddSingleton<ICdnTokenService, CdnTokenService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<IPageTypeService, PageTypeService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ISettingService, SettingService>();
        services.AddSingleton<IDataService, DataService>();

        services.AddSingleton<IImageGalleryRepository, ImageGalleryRepository>();
        services.AddSingleton<IImageGalleryTypeRepository, ImageGalleryTypeRepository>();
        services.AddSingleton<ICdnTokenRepository, CdnTokenRepository>();
        services.AddSingleton<IPageRepository, PageRepository>();
        services.AddSingleton<IPageTypeRepository, PageTypeRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IDomainRepository, DomainRepository>();
        services.AddSingleton<ISettingRepository, SettingRepository>();
        services.AddSingleton<IDataRepository, DataRepository>();

        services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();

        services.AddSingleton<BasicAuthenticationHandler>(); // handles auth
        services.AddSingleton<UserClaimsHandler>();

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "InstantCMS API",
                Description = ".NET Core Web API"
            });

            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please type 'Bearer' followed by the token into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
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

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var Key = Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidAudience = configuration["Jwt:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key)
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy(
                name: configuration["Origins"],
                builder => builder
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://localhost:4200",
                        "http://89.187.103.53",
                        "http://craftsfo.instantcms.dk",
                        "http://beautify-by-h.instantcms.dk",
                        "http://p-grillen.instantcms.dk",
                        "http://kageogform.instantcms.dk"
                    )
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });

        services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
            hubOptions.ClientTimeoutInterval = new System.TimeSpan(0, 0, 10);
        });
    }
}

