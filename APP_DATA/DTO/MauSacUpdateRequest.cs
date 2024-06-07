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

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên màu từ 2 đến 50 ký tự !")]
        public string? TenMauSac { get; set; }

        [StringLength(256, ErrorMessage = "Mô tả quá dài (dưới 256 ký tự)")]
        public string? MoTa { get; set; }

        [StringLength(50)]
        public string? TrangThai { get; set; }
        public MauSac ToMauSac()
        {
            return new MauSac()
            {
                ID_MauSac = ID_MauSac,
                TenMauSac = TenMauSac,
                MoTa = MoTa,
                TrangThai = TrangThai
            };
        }
    }
}
