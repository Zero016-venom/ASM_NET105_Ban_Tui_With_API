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
    public class HoaDonResponce
    {
        public Guid ID_HoaDon { get; set; }
        public Guid? ID_User { get; set; }
        public decimal TongTien { get; set; }
        public string? TrangThai { get; set; }
        public Guid? ID_PTTT { get; set; }
        public DateTime NgayThanhToan { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            HoaDonResponce hoadon = (HoaDonResponce)obj;
            return ID_HoaDon.Equals(hoadon.ID_HoaDon) &&
                   ID_User.Equals(hoadon.ID_User) &&
                   TongTien.Equals(hoadon.TongTien) &&
                   TrangThai == hoadon.TrangThai &&
                   ID_PTTT.Equals(hoadon.ID_PTTT) &&
                   NgayThanhToan.Equals(hoadon.NgayThanhToan);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID_HoaDon, ID_User, TongTien, TrangThai, ID_PTTT, NgayThanhToan);
        }
    }
    public static class HoaDonExtensions
    {
        public static HoaDonResponce ToHoaDonResponse(this HoaDon hoaDon)
        {
            return new HoaDonResponce()
            {
                ID_HoaDon = hoaDon.ID_HoaDon,
                ID_PTTT = hoaDon.ID_PTTT,
                ID_User = hoaDon.ID_User,
                TrangThai = hoaDon.TrangThai,
                TongTien = hoaDon.TongTien,
                NgayThanhToan = hoaDon.NgayThanhToan
            };
        }
    }
}
