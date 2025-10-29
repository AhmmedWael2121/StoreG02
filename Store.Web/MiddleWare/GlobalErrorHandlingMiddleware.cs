using Store.Domain.Exception;
using Store.Shared.DTOs.ErrorModels;
namespace Store.Web.MiddleWare
{
    public class GlobalErrorHandlingMiddleware(RequestDelegate _next)
    {

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
              await  _next.Invoke(context);

                if(context.Response.StatusCode ==  StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json"; // Set the response content type
                    var errorResponse = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"EndPoint of {context.Request.Path} Doesn't Exist"
                    };


                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed:
                //-------------------------------------------
                context.Response.ContentType = "application/json"; // Set the response content type
                var errorResponse = new ErrorDetails()
                {
                    Message = ex.Message,
                };

                errorResponse.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound, // Not Found

                    _ => StatusCodes.Status500InternalServerError // Internal Server Error
                }; // Internal Server Error

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            // 1. 
            
        }
    }
}
