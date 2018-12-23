using Abp.Application.Services;
using AbpProject.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpProject.Application.TestAppService
{
    public interface IMoveReqApplicationService : IApplicationService
    {
        Task<MoveReqDTO> GetMoveReqData(MoveReqSearch input);

        Task<string> GetMoveReqDetailData();

        Task<string> CreateMoveReqData();

        Task<string> UpdateMoveReqData();

        Task<string> DeleteMoveReqData();
    }
}
