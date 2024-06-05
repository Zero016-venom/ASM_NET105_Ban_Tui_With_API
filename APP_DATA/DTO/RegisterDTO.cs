using APP_DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Tên không được để trống !")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email không được để trống !")]
        [EmailAddress(ErrorMessage = "Email phải hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống !")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ chứa số")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu không được để trống !")]
        [Compare("Password", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không giống nhau !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
