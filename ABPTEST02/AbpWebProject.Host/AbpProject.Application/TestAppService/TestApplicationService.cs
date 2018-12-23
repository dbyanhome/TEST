using Abp.Application.Services;
using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpProject.Application.TestAppService
{
    public class TestApplicationService:ApplicationService,ITestApplicationService
    {
        [DontWrapResult]
        public Task<string> GetTestString()
        {
            return Task.FromResult("ApplicationService Test  OK");
        }

        public async Task<string> GetSearchData()
        {

            return await Task.Run(() =>
            {
            //返回结果
            return "Task ApplicationService";
                
            });
        }

        public async Task<string> CreateBill()
        {

            return await Task.Run(() =>
            {
                //返回结果
                return "CreateBill ApplicationService";

            });
        }

        public async Task<string> UpdateBill()
        {

            return await Task.Run(() =>
            {
                //返回结果
                return "UpdateBill ApplicationService";

            });
        }

        public async Task<string> DeleteBill()
        {

            return await Task.Run(() =>
            {
                //返回结果
                return "DeleteBill ApplicationService";

            });
        }


    }
}
