
using Microsoft.EntityFrameworkCore;
using ProductAPI.Context;
using ProductAPI.Helper;
using ProductAPI.Interface;

namespace ProductAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ProductAPI.Context.MyDbContext>();
        builder.Services.AddTransient<IServiceCallHelper, ServiceCallHelper>();
        builder.Services.AddCap(options =>
        {
            options.UseEntityFramework<MyDbContext>();
            options.UsePostgreSql("Server=localhost;Port=6432;Database=etrade2;User Id=root;Password=12345;");
            options.UseRabbitMQ(roptions =>
            roptions.ConnectionFactoryOptions = foptions =>
            {
                foptions.Ssl.Enabled = false;
                foptions.HostName = "localhost";
                foptions.UserName = "guest";
                foptions.Password = "guest";
                foptions.Port = 5672;

            }
            );
        });
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
