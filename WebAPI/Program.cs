using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Castle.Core.Configuration;
using Core.Utilities.Security.Encryption;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Core.Utilities.Security.JWT;
using TokenOptions = Core.Utilities.Security.JWT.TokenOptions;
using Core.Extensions;
using Core.Utilities.Ioc;
using Core.DependencyResolvers;
using Microsoft.IdentityModel.Tokens;
using Autofac.Core;
using Microsoft.OpenApi.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Autofac, Ninject, CastleWindsor, Structuremap, LightInject, DryInject, --> Ioc Container
            
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

            builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

            var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(configureOptions =>
            //{
            //    configureOptions.TokenValidationParameters = new TokenValidationParameters
            //    {

            //        ValidIssuer = tokenOptions.Issuer,
            //        ValidAudience = tokenOptions.Audience,
            //        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

            //        ValidateIssuerSigningKey = true,
            //        ValidateAudience = true,
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        RequireExpirationTime = false,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}