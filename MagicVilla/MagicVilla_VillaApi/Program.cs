using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MagicVilla_VillaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers(op =>
                {
                    //op.ReturnHttpNotAcceptable = true;
                }).AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
            });
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<ILogging, LoggingV1>();


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}