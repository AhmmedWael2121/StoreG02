using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Service.Abstractions;
using Store.Service.Mapper.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.ServiceExtensions
{
    public static class AppServiceRegistrations
    {
        public static IServiceCollection AddAppService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));

            return services;

        }
    }
}
