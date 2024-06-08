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
    public class ChuongTrinhKhuyenMaiResponse
    {
        public Guid ID_ChuongTrinhKhuyenMai { get; set; }
        public string? TenChuongTrinhKhuyenMai { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? TrangThai { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != typeof(ChuongTrinhKhuyenMaiResponse)) return false;
            ChuongTrinhKhuyenMaiResponse chuongTrinhKhuyenMai = (ChuongTrinhKhuyenMaiResponse)obj;
            return ID_ChuongTrinhKhuyenMai == chuongTrinhKhuyenMai.ID_ChuongTrinhKhuyenMai && TenChuongTrinhKhuyenMai == chuongTrinhKhuyenMai.TenChuongTrinhKhuyenMai && NgayBatDau == chuongTrinhKhuyenMai.NgayBatDau && NgayKetThuc == chuongTrinhKhuyenMai.NgayKetThuc && TrangThai == chuongTrinhKhuyenMai.TrangThai;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ChuongTrinhKhuyenMaiUpdateRequest ToChuongTrinhKhuyenMaiUpdateRequest()
        {
            return new ChuongTrinhKhuyenMaiUpdateRequest()
            {
                ID_ChuongTrinhKhuyenMai = ID_ChuongTrinhKhuyenMai,
                TenChuongTrinhKhuyenMai = TenChuongTrinhKhuyenMai,
                NgayBatDau = NgayBatDau,
                NgayKetThuc = NgayKetThuc,
                TrangThai = (StatusOptions)Enum.Parse(typeof(StatusOptions), TrangThai)
            };
        }
    }

    public static class ChuongTrinhKhuyenMaiExtensions
    {
        public static ChuongTrinhKhuyenMaiResponse ToChuongTrinhKhuyenMaiResponse(this ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            return new ChuongTrinhKhuyenMaiResponse()
            {
                ID_ChuongTrinhKhuyenMai = chuongTrinhKhuyenMai.ID_ChuongTrinhKhuyenMai,
                TenChuongTrinhKhuyenMai = chuongTrinhKhuyenMai.TenChuongTrinhKhuyenMai,
                NgayBatDau = chuongTrinhKhuyenMai.NgayBatDau,
                NgayKetThuc = chuongTrinhKhuyenMai.NgayKetThuc,
                TrangThai = chuongTrinhKhuyenMai.TrangThai.ToString()
            };
        }
    }
}
