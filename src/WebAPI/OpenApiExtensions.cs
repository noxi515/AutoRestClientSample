using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI
{
    public static class OpenApiExtensions
    {
        /// <summary>
        /// OpenAPIミドルウェアの設定を追加します。
        /// </summary>
        public static void AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebAPI",
                    Version = "v1.0.0"
                });
                options.OperationFilter<CustomOperationFilter>();
            });
        }

        /// <summary>
        /// OpenAPIミドルウェアを使用します。
        /// </summary>
        public static void UseOpenApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
        }
    }

    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controller)
            {
                operation.OperationId =
                    $"{controller.ControllerName}_{operation.OperationId ?? controller.ActionName}";
            }
        }
    }
}