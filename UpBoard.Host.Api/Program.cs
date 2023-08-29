using AutoMapper;
using Board.Contracts.Ad;
using Board.Contracts.User;
using Board.Infrastucture.MapProfiles;
using Doska.AppServices.MapProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UpBoard.Infrastructure.Registrar;

namespace UpBoard.Host.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            builder.Services.AddServices();

            #region Authentication & Authorization

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                options =>
                {
                    var secretKey = builder.Configuration["Jwt:Key"];

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            builder.Services.AddAuthorization();

            #endregion

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "UpBoard Api", Version = "V1" });
                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
                    $"{typeof(InfoUserResponse).Assembly.GetName().Name}.xml")));
                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, "Documentation.xml")));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.  
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer secretKey'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
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
                },
                Scheme="oauth2",
                Name= "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
            });


            using var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();

            static MapperConfiguration GetMapperConfiguration()
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CategoryMapProfile>();
                    cfg.AddProfile<AdvertisementMapProfile>();
                    cfg.AddProfile<CategoryMapProfile>();
                    cfg.AddProfile<CommentMapProfile>();
                    cfg.AddProfile<FavoriteAdMapProfile>();
                    cfg.AddProfile<UserMapProfile>();
                    cfg.AddProfile<FileMapProfile>();
                });
                //configuration.AssertConfigurationIsValid();
                return configuration;
            }

        }
    }
}