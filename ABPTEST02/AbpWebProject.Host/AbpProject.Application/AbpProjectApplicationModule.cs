using System;
using Abp;
using Abp.Modules;
using AbpProject.Core;

namespace AbpProject.Application
{
    [DependsOn(typeof(AbpProjectCoreModule))]
    public class AbpProjectApplicationModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpProjectApplicationModule).Assembly);
        }
    }
}
