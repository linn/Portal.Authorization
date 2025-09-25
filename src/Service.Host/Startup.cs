namespace Linn.Portal.Authorization.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;
    
    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Logging;
    using Linn.Common.Service;
    using Linn.Common.Service.Extensions;
    using Linn.Portal.Authorization.IoC;
    using Linn.Portal.Authorization.Service.Host.Negotiators;
    using Linn.Portal.Authorization.Service.Models;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddCors();
            services.AddSingleton<IViewLoader, ViewLoader>();
            services.AddSingleton<IResponseNegotiator, HtmlNegotiator>();
            services.AddSingleton<IResponseNegotiator, UniversalResponseNegotiator>();

            services.AddDefaultAWSOptions(this.configuration.GetAWSOptions());

            services.AddLogging(builder =>
                {
                    builder.ClearProviders();           
                    builder.AddConsole();               
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                    builder.AddFilter("System", LogLevel.Warning);
                    builder.AddFilter("Linn", LogLevel.Information);
                });

            services.AddLog();

            services.AddServices();
            services.AddFacadeServices();
            services.AddBuilders();
            services.AddPersistence();  
            services.AddHandlers();

            var appSettings = ApplicationSettings.Get();
            var authority = appSettings.AuthorityUri;
            
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                    {
                        options.Authority = authority;

                        options.TokenValidationParameters = new TokenValidationParameters
                                                                {
                                                                    ValidateIssuer = true,
                                                                    ValidIssuer = authority,
                                                                    ValidateAudience = false,
                                                                    ValidAudience = "64fbgrkkslt1choig1e8km1g45",
                                                                    ValidateLifetime = true,
                                                                    ValidateIssuerSigningKey = true
                                                                };

                        options.Events = new JwtBearerEvents
                                             {
                                                 OnAuthenticationFailed = context =>
                                                     {
                                                         if (context.Exception is SecurityTokenExpiredException)
                                                         {
                                                             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                                             context.Response.ContentType = "text/plain";
                                                             return context.Response.WriteAsync("Token expired");
                                                         }

                                                         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                                         context.Response.ContentType = "text/plain";
                                                         return context.Response.WriteAsync("Authentication failed");
                                                     }
                                             };
                    });
            
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();

            app.UseBearerTokenAuthentication();
            app.Use(
                (context, next) =>
                    {
                        context.Response.Headers.Append("Vary", "Accept");
                        return next.Invoke();
                    });
            app.UseExceptionHandler(
                c => c.Run(async context =>
                    {
                        var exception = context.Features
                            .Get<IExceptionHandlerPathFeature>()
                            ?.Error;

                        var log = app.ApplicationServices.GetService<ILog>();
                        log.Error(exception?.Message, exception);

                        var response = new { error = $"{exception?.Message}  -  {exception?.StackTrace}" };
                        await context.Response.WriteAsJsonAsync(response);
                    }));
            app.UseRouting();
            app.UseEndpoints(builder => { builder.MapEndpoints(); });
        }
    }
}
