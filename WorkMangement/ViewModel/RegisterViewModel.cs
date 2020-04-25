using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class RegisterViewModel
    {
        // Họ và tên
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; }
        // Tên đăng nhập
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string UserName { get; set; }
        // Mật khẩu
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="Mật khẩu tối thiểu 6 kí tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
        // Mã
        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên")]
        public string EmployeeCode { get; set; }
        // Tuổi
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        public int Age { get; set; }
        // Giới tính
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public EnumGender Gender { get; set; }
        // Phòng ban
        [Required(ErrorMessage = "Vui lòng nhập tên phòng ban")]
        public string Department { get; set; }
    }
}
