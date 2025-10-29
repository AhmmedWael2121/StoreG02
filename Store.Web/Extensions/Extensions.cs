using Microsoft.AspNetCore.Mvc;
using Store.Domain.Contracts;
using Store.Persistence.persistenceExtensions;
using Store.Service.ServiceExtensions;
using Store.Shared.DTOs.ErrorModels;
using Store.Web.MiddleWare;
using System.Threading.Tasks;
namespace Store.Web.Extensions
{
    public static class Extensions
    {
        #region Service

        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddInfrastructureService(configuration);
            services.AddAppService(configuration);
            services.AddWebServices();
            services.ConfigureApiBehaviorOptions();


            return services;

        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            // update validation problem details behavior
            // to return custom validation error response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // customize the validation error response
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    // create a custom validation error response
                    var errors = ActionContext.ModelState
                        .Where(e => e.Value.Errors.Any())
                        // project the errors into a list of ValidationError objects
                        .Select(e => new ValidationError
                        {
                            Field = e.Key,
                            Error = e.Value.Errors.Select(er => er.ErrorMessage)
                        }).ToList();
                    // create the ValidationErrorResponse object
                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }


        #endregion


        #region Web Collections
        public static async Task<WebApplication> AddConfigurationMiddleware(this WebApplication app)
        {
           app.UseGlobalMiddleware();
            //D:\Route\Back-End\C#\WebAPI\Store\Store.Web\wwwroot\images\products\
            app.UseStaticFiles();
            await app.DataSeed();

            // Global Error Handling Middleware
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }

        private static WebApplication UseGlobalMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

        private static async Task<WebApplication> DataSeed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }
            return app;
        }

        #endregion

    }
}
