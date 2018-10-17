using System.Web.Http;
using WebActivatorEx;
using WebApiSegura;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace WebApiSegura
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WebApiSegura con soporte Token JWT");
                    c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                })
                .EnableSwaggerUi();
        }

        /// <summary>
        /// AuthorizationHeaderParameterOperationFilter para introducir JWT en dialogo Swagger
        /// </summary>
        public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();

                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "JWT Token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}