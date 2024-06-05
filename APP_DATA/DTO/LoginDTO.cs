using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email không được để trống !")]
        [EmailAddress(ErrorMessage = "Email phải hợp lệ !")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống !")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
