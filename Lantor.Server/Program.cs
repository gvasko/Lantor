
using Lantor.Data;
using Lantor.Data.Infrastructure;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Lantor.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Lantor.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            LoggerService.Logger = new SeriLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
