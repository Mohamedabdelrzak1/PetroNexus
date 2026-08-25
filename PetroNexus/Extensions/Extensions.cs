using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Data;
using PetroNexus.Middleware;
using Service;
using Shared.Dto.Auth;
using Shared.Common;
using Shared.ErrorModels;
using System.Text;

namespace PetroNexus.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService(configuration);
            services.AddSwaggerService();

            // ✅ CORS آمن (React Local + Production)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins(
                            "https://petrovix.vercel.app",
                            "http://localhost:3000"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;

                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);

            // ✅ إجبار كل API على تسجيل الدخول (FallbackPolicy)
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy =
                    new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
            });

            services.ConfigurService();

            return services;
        }

        private static IServiceCollection AddBuiltInService(
         this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddControllers();

            #region SignalR
            services.AddSignalR();
            #endregion


            #region Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                options.User.RequireUniqueEmail = true;
            })
   .AddEntityFrameworkStores<PetroNexusDbContext>()
   .AddDefaultTokenProviders();
            #endregion


            #region JWT

            var jwtOptions =
                configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    // 🔥 مهم جدًا لSignalR
                    OnMessageReceived = context =>
                    {
                        var accessToken =
                            context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken)
                            && path.StartsWithSegments("/hubs/notifications"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                    context.HandleResponse();

                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        var result =
                            System.Text.Json.JsonSerializer.Serialize(
                                ApiResponse.FailureResult("Unauthorized access.", "Authentication required."));

                        return context.Response.WriteAsync(result);
                    }
                };
            });

            #endregion

            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
     new OpenApiInfo
     {
         Title = "PetroVix ERP API",
         Version = "v1",
         Description = "A comprehensive ERP solution for managing Tenders, CRM, Procurement, Logistics, Finance, QA/QC, Engineering Services, Document Control, and Business Analytics."
     });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter Bearer token"
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
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        private static IServiceCollection ConfigurService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError()
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                        });

                    var errorMessages = errors
                        .SelectMany(e => e.Errors)
                        .ToList();

                    var response = ApiResponse.FailureResult("Validation failed.", errorMessages);

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static async Task<WebApplication> ConfigurMiddelwares(this WebApplication app)
        {
           

            //// ✅ Swagger فقط في Development
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseGlobalErrorHandling();

            // 🔥 تحويل الـ root إلى swagger
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                    return;
                }
                await next();
            });

            // ✅ Security headers ضد XSS و clickjacking
            app.Use(async (context, next) =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
                context.Response.Headers["Referrer-Policy"] = "no-referrer";
                await next();
            });

            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.MapHub<NotificationHub>("/hubs/notifications");
            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseMiddleware<UpdateLastSeenMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

  

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
            return app;
        }
    }
}
