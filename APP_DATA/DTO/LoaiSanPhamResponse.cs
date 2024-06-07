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
    public class LoaiSanPhamResponse
    {
        public Guid ID_LoaiSP { get; set; }
        public string? TenLoaiSP { get; set; }
        public string? MoTa { get; set; }
        public string? TrangThai { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(LoaiSanPhamResponse)) return false;

            LoaiSanPhamResponse loaiSanPham = (LoaiSanPhamResponse)obj;
            return ID_LoaiSP == loaiSanPham.ID_LoaiSP && TenLoaiSP == loaiSanPham.TenLoaiSP && MoTa == loaiSanPham.MoTa && TrangThai == loaiSanPham.TrangThai;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public LoaiSanPhamUpdateRequest ToLoaiSanPhamUpdateRequest()
        {
            return new LoaiSanPhamUpdateRequest()
            {
                ID_LoaiSP = ID_LoaiSP,
                TenLoaiSP = TenLoaiSP,
                MoTa = MoTa,
                TrangThai = (StatusOptions)Enum.Parse(typeof(StatusOptions), TrangThai)
            };
        }
    }

    public static class LoaiSanPhamExtensions
    {
        public static LoaiSanPhamResponse ToLoaiSanPhamResponse(this LoaiSP loaiSanPham)
        {
            return new LoaiSanPhamResponse()
            {
                ID_LoaiSP = loaiSanPham.ID_LoaiSP,
                TenLoaiSP = loaiSanPham.TenLoaiSP,
                MoTa = loaiSanPham.MoTa,
                TrangThai = loaiSanPham.TrangThai
            };
        }
    }
}
