using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WorkMangement
{
    public class ApplicationUser:IdentityUser
    {
        // Tên đầy đủ
        public string FullName { get; set; }
        // Mã
        public string EmployeeCode { get; set; }
        // Tuổi
        public int Age { get; set; }
        // Giới tính
        public bool IsMale { get; set; }
        // Phòng ban
        public string Department { get; set; }
    }
}
