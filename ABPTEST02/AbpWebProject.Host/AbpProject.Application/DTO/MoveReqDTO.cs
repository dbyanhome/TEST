using System;
using System.Collections.Generic;
using System.Text;

namespace AbpProject.Application.DTO
{
    public class MoveReqDTO
    {
        public int ID { get; set; }
        public string CompanyID { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }

        public int OperatorID { get; set; }

        public int CheckerID { get; set; }

        public DateTime CheckerDate { get; set; }

        public DateTime ModifyDTM { get; set; }

        public List<MoveReqDetailDto> DetailData { get; set; }
    }

    public class MoveReqDetailDto
    {

        public int ID { get; set; }

        public int MasterID { get; set; }

        public int SkuID { get; set; }

        public string SkuCode { get; set; }
        public string ColorName { get; set; }

        public string SizeName { get; set; }

        public decimal Price { get; set; }

        public decimal Qty { get; set; }

        public decimal Amount { get; set; }

        public DateTime ModifyDTM { get; set; }
    }

    public class MoveReqSearch
    {
        public int? ID { get; set; }
        public string CompanyID { get; set; }
        public string BillNo { get; set; }
        public DateTime? SBillDate { get; set; }
        public DateTime? EBillDate { get; set; }

        public int? OutStockID { get; set; }

        public int? InStockID { get; set; }

    }
}