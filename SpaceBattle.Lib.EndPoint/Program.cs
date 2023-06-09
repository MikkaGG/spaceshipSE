using CoreWCF.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace SpaceBattle.Lib.EndPoint
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true;
            });

            builder.Services.AddServiceModelWebServices();
            builder.Services.AddSingleton(new SwaggerOptions());

            var app = builder.Build();

            app.UseServiceModel(builder =>
            {
                builder.AddService<EndPoint>();
                builder.AddServiceWebEndpoint<EndPoint, IWebApi>("api", behavior =>
                {
                    behavior.HelpEnabled = true;
                });
            });

            app.UseMiddleware<SwaggerMiddleware>();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}
