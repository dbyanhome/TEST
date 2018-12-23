using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Castle.Facilities.Logging;
using Abp.Castle.Logging.Log4Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using AbpWebProject.Host.SwaggerFilters;
using Microsoft.Extensions.Configuration;
using AbpProject.Core;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Abp.Extensions;

namespace AbpWebProject.Host
{
    //public class Startup
    //{
   
    //    public IServiceProvider ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvc();
    //        ConfigureSwaggerUI(services);
    //        return services.AddAbp<AbpWebProjectHostModule>(options =>
    //        options.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseAbpLog4Net().WithConfig("Log4Net.config")));
    //    }


    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        app.UseAbp();
    //        app.UseMvc();
    //    }
   
    //}


    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = AppConfigurer.GetConfigurationRoot(_hostingEnvironment.ContentRootPath);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(new CorsAuthorizationFilterFactory("Cors")); });

            ConfigureCors(services);
            ConfigureJWTAuthentication(services);
            ConfigureSwaggerUI(services);
            return ConfigureAbpService(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAbp();
            app.UseCors("Cors");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(options => options.MapRoute("default", "{controller=Home}/{action=Index}/{Id?}"));
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // 加载自定义资源
                options.InjectOnCompleteJavaScript("/swagger/ui/abp.js");
                options.InjectOnCompleteJavaScript("/swagger/ui/on-complete.js");
                // 加载中文包
                options.InjectOnCompleteJavaScript("/swagger/ui/zh_CN.js");
                options.InjectStylesheet("/swagger/ui/zh_CN.css");

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TEST");
            });
        }

        /// <summary>
        /// 配置 CORS 跨域服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors",
                policyOptions =>
                {
                    policyOptions
                        .WithOrigins(_configuration["App:Cors"].Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(z => z.RemovePreFix("/")).ToArray()).AllowAnyHeader().AllowAnyMethod();
                }));
        }

        /// <summary>
        /// 配置 SwaggerUI
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureSwaggerUI(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(z => z.FullName);
                options.SwaggerDoc("v1", new Info { Title = "TEST", Version = "v1" });
                options.DocInclusionPredicate((s, b) => true);
                options.IncludeXmlComments(Path.Combine(_hostingEnvironment.ContentRootPath, "XMLDocument", "TEST.Application.xml"));

                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme
                {
                    Description = "TOKEN 授权",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                options.DocumentFilter<SwaggerCustomUIFilter>();
                options.OperationFilter<SwaggerAuthorizeFilter>();
            });
        }

        /// <summary>
        /// 配置 JWT 授权验证
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureJWTAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = _configuration["Authorize:ApiName"];
                    options.Authority = _configuration["Authorize:Address"];
                    options.RequireHttpsMetadata = false;
                });
        }

        /// <summary>
        /// 配置 ABP 框架服务
        /// </summary>
        /// <param name="services"></param>
        private IServiceProvider ConfigureAbpService(IServiceCollection services)
        {
            return services.AddAbp<AbpWebProjectHostModule>(options =>
            {
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
                        f.UseAbpLog4Net().WithConfig("log4net.config"));
            });
        }
    }
}
