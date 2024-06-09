
using Lantor.Data;
using Lantor.Data.Infrastructure;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Lantor.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Serilog;
using System;

namespace Lantor.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
            IdentityModelEventSource.ShowPII = true;

            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            LoggerService.Logger = new SeriLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddSerilog(lc => lc.ReadFrom.Configuration(builder.Configuration));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        builder.Configuration.Bind("AzureAd", options);
                        options.Events = new JwtBearerEvents();

                        options.Events.OnTokenValidated = context =>
                        {

                            string? clientappId = context?.Principal?.Claims
                                .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                            //Log.Information("ClientAppId: {clientappid}", clientappId);
                            return Task.CompletedTask;
                        };

                        options.Events.OnForbidden = context =>
                        {
                            Log.Warning("forbidden");
                            return Task.CompletedTask;
                        };

                    }, options => { builder.Configuration.Bind("AzureAd", options); }
                );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LantorContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("LantorDatabase")));
            builder.Services.AddScoped<IDomainUnitOfWork, DomainUnitOfWork>();
            builder.Services.AddScoped<IBasicCrudUnitOfWork, BasicCrudUnitOfWork>();
            builder.Services.AddScoped<ILanguageVectorBuilder, LanguageVectorBuilder>();
            builder.Services.AddScoped<ILanguageDetectorService, LanguageDetectorService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();

            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
            IdentityModelEventSource.ShowPII = true;

            using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LantorContext>();
                context?.Database.Migrate();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

