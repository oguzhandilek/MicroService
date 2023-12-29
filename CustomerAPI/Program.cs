
using CustomerAPI.Context;
using CustomerAPI.Helper;
using CustomerAPI.Interface;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MyDbContext>();
        builder.Services.AddTransient<IServiceCallHelper, ServiceCallHelper>();

        builder.Services.AddCap(options =>
        { options.UseEntityFramework<MyDbContext>();
            options.UsePostgreSql("Host = localhost; Port = 6432; Database =etrade; User Id = root; Password = 12345;");
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
