using System;
using Abp;
using Abp.Modules;

namespace AbpProject.Core
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpProjectCoreModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpProjectCoreModule).Assembly);
        }
    }
}
