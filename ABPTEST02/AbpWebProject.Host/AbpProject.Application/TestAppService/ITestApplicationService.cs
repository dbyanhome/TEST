using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpProject.Application.TestAppService
{
    public interface ITestApplicationService:IApplicationService
    {
        Task<string> GetTestString();

        Task<string> GetSearchData();
        Task<string> CreateBill();
        Task<string> UpdateBill();
        Task<string> DeleteBill();

    }
}
