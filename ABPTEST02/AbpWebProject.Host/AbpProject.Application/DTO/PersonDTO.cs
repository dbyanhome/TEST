using System;
using System.Collections.Generic;
using System.Text;

namespace AbpProject.Application.DTO
{
    public class PersonDTO
    {
        public int PersonID { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string DepartMentID { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

    }
}
