using Abp.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AbpWebProject.Host.SwaggerFilters
{
    /// <summary>
    /// Swagger 授权过滤器
    /// </summary>
    public class SwaggerAuthorizeFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            // 获得控制器上的所有权限
            var controllerPermissions = context.ApiDescription
                .ControllerAttributes()
                .OfType<AbpAuthorizeAttribute>()
                .Select(attr => attr.Permissions);

            // 获得 Action 方法上的所有权限
            var actionPermissions = context.ApiDescription
                .ActionAttributes()
                .OfType<AbpAuthorizeAttribute>()
                .Select(attr => attr.Permissions);

            // 连接并且去重获得所有权限集合
            var allPermission = controllerPermissions.Union(actionPermissions).Distinct().SelectMany(z => z).ToList();

            if (allPermission.Any())
            {
                operation.Responses.Add("401", new Response { Description = "没有登录系统" });
                operation.Responses.Add("403", new Response { Description = "你没有权限访问" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>{ {"bearerAuth" ,allPermission}}
                };
            }
        }
    }
}
