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
    public class SanPhamResponse
    {
        public Guid ID_SanPham { get; set; }
        public string? TenSanPham { get; set; }
        public decimal GiaNiemYet { get; set; }
        public int SoLuongTon { get; set; }
        public Guid? ID_Hang { get; set; }
        public string? Hang { get; set; }
        public Guid? ID_MauSac { get; set; }
        public string? MauSac { get; set; }
        public Guid? ID_ChatLieu { get; set; }
        public string? ChatLieu { get; set; }   
        public Guid? ID_LoaiSP { get; set; }
        public string? LoaiSP { get; set; }
        public string? Img { get; set; }
        public string? TrangThai { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SanPhamResponse)) return false;
            SanPhamResponse sanPham = (SanPhamResponse)obj;
            return ID_SanPham == sanPham.ID_SanPham && TenSanPham == sanPham.TenSanPham && ID_Hang == sanPham.ID_Hang && SoLuongTon == sanPham.SoLuongTon && ID_MauSac == sanPham.ID_MauSac && GiaNiemYet == sanPham.GiaNiemYet && ID_ChatLieu == sanPham.ID_ChatLieu && ID_LoaiSP == sanPham.ID_LoaiSP && Img == sanPham.Img && TrangThai == sanPham.TrangThai;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public SanPhamUpdateRequest ToSanPhamUpdateRequest()
        {
            return new SanPhamUpdateRequest()
            {
                ID_SanPham = ID_SanPham,
                TenSanPham = TenSanPham,
                ID_Hang = ID_Hang,
                SoLuongTon = SoLuongTon,
                ID_MauSac = ID_MauSac,
                GiaNiemYet = GiaNiemYet,
                ID_ChatLieu = ID_ChatLieu,
                ID_LoaiSP = ID_LoaiSP,
                Img = Img,
                TrangThai = (StatusOptions)Enum.Parse(typeof(StatusOptions), TrangThai)
            };
        }
    }

    public static class SanPhamResponseExtensions

    {
        public static SanPhamResponse ToSanPhamResponse(this SanPham sanPham)
        {
            return new SanPhamResponse()
            {
                ID_SanPham = sanPham.ID_SanPham,
                TenSanPham = sanPham.TenSanPham,
                ID_ChatLieu = sanPham.ID_ChatLieu,
                ID_Hang = sanPham.ID_Hang,
                ID_LoaiSP = sanPham.ID_LoaiSP,
                ID_MauSac = sanPham.ID_MauSac,
                SoLuongTon = sanPham.SoLuongTon,
                GiaNiemYet = sanPham.GiaNiemYet,
                Img = sanPham.Img,
                TrangThai = sanPham.TrangThai,
                Hang = sanPham.Hang?.TenHang,
                MauSac = sanPham.MauSac?.TenMauSac,
                ChatLieu = sanPham.ChatLieu?.TenChatLieu,
                LoaiSP = sanPham.LoaiSP?.TenLoaiSP
            };
        }
    }
}
