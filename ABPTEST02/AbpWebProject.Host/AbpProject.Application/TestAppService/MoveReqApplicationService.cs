using Abp.Application.Services;
using Abp.Web.Models;
using AbpProject.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpProject.Application.TestAppService
{
    [DontWrapResult]
    public class MoveReqApplicationService : ApplicationService, IMoveReqApplicationService
    {
        public async Task<MoveReqDTO> GetMoveReqData(MoveReqSearch input)
        {
            return await Task.Run(() =>
            {
                MoveReqDTO mdto = new MoveReqDTO()
                {
                    ID = 5,
                    CompanyID = "HK",
                    BillDate = System.DateTime.Now,
                    BillNo = "IM2018061300001",
                    OperatorID = 1,
                    CheckerID = 3,
                    CheckerDate = System.DateTime.Now,
                    ModifyDTM = System.DateTime.Now
                };

                mdto.DetailData = new List<MoveReqDetailDto>();

                MoveReqDetailDto detailDTO = new MoveReqDetailDto()
                {
                    ID = 1,
                    MasterID = 5,
                    SkuID = 9001,
                    SkuCode = "97051YM",
                    ColorName = "蓝色",
                    SizeName = "M",
                    Price = 299,
                    Qty = 2,
                    Amount = 598,
                    ModifyDTM = System.DateTime.Now

                };
                mdto.DetailData.Add(detailDTO);

                detailDTO = new MoveReqDetailDto()
                {
                    ID = 2,
                    MasterID = 5,
                    SkuID = 9002,
                    SkuCode = "97052YM",
                    ColorName = "红色色",
                    SizeName = "XL",
                    Price = 299,
                    Qty = 1,
                    Amount = 299,
                    ModifyDTM = System.DateTime.Now

                };
                mdto.DetailData.Add(detailDTO);
                //返回结果
                return mdto;

            });
        }

        public async Task<string> GetMoveReqDetailData()
        {
            return await Task.Run(() =>
            {
                //返回结果
                return "Task GetMoveReqDetailData";

            });
        }

        public async Task<string> CreateMoveReqData()
        {
            return await Task.Run(() =>
            {
                //返回结果
                return "Task CreateMoveReqData";

            });
        }

        public async Task<string> UpdateMoveReqData()
        {
            return await Task.Run(() =>
            {
                //返回结果
                return "Task UpdateMoveReqData";

            });
        }

        public async Task<string> DeleteMoveReqData()
        {
            return await Task.Run(() =>
            {
                //返回结果
                return "Task DeleteMoveReqData";

            });
        }
    }
}
