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
    public class LoaiSanPhamAddRequest
    {

        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống")]
        public string? TenLoaiSP { get; set; }

        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public StatusOptions? TrangThai { get; set; }

        public LoaiSP ToLoaiSanPham()
        {
            return new LoaiSP()
            {
                TenLoaiSP = TenLoaiSP,
                MoTa = MoTa,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
