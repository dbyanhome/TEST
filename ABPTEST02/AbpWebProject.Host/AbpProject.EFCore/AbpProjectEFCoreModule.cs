using System;
using Abp;
using Abp.Modules;
using AbpProject.Core;

namespace AbpProject.EFCore
{
    [DependsOn(typeof(AbpProjectCoreModule))]
    public class AbpProjectEFCoreModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpProjectEFCoreModule).Assembly);
        }

    }
}
