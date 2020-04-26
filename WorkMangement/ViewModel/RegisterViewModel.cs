using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class RegisterViewModel
    {
        // Họ và tên
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }
        // Tên đăng nhập
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        // Mật khẩu
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="Mật khẩu tối thiểu 6 kí tự")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage = "Mật khẩu không trùng khớp")]
        [DisplayName("Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        // Mã
        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên")]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        // Tuổi
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        [DisplayName("Tuổi")]
        public int Age { get; set; }
        // Giới tính
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [DisplayName("Giới tính")]
        public EnumGender Gender { get; set; }
        // Phòng ban
        [Required(ErrorMessage = "Vui lòng nhập tên phòng ban")]
        [DisplayName("Phòng ban")]
        public string Department { get; set; }
    }
}
