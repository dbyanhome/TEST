using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Modules;
using Abp;
using AbpProject.Application;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore;

namespace AbpWebProject.Host
{
    [DependsOn(typeof(AbpProjectApplicationModule),typeof(AbpAspNetCoreModule))]
    public class AbpWebProjectHostModule:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(AbpProjectApplicationModule).Assembly);

        }
        
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpWebProjectHostModule).Assembly);
        }
    }
}
