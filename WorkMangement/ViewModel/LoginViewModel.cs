using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập")]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Lưu đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
