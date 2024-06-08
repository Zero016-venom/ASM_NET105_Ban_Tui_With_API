using APP_DATA.Enums;
using APP_DATA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.DTO
{
    public class MauSacUpdateRequest
    {
        public Guid ID_MauSac { get; set; }

        [Required(ErrorMessage = "Tên màu không được để trống")]
        public string? TenMauSac { get; set; }

        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public StatusOptions? TrangThai { get; set; }

        public MauSac ToMauSac()
        {
            return new MauSac()
            {
                ID_MauSac = ID_MauSac,
                TenMauSac = TenMauSac,
                MoTa = MoTa,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
