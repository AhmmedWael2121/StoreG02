using Store.Domain.Contracts;
using Store.Web.MiddleWare;
using Store.Web.Extensions;
namespace Store.Web;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        // Contains All Services before building App like (DB / AddScoped)
        builder.Services.AddAllServices(builder.Configuration);
       
        var app = builder.Build();
        await  app.AddConfigurationMiddleware();
        app.Run();

    }
}
