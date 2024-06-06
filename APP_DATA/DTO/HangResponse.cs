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
    public class HangResponse
    {
        public Guid ID_Hang { get; set; }
        public string? TenHang { get; set; }
        public string? MoTa { get; set; }
        public string? TrangThai { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(HangResponse)) return false;
            HangResponse hangResponse = (HangResponse)obj;

            return ID_Hang == hangResponse.ID_Hang && TenHang == hangResponse.TenHang && MoTa == hangResponse.MoTa && TrangThai == hangResponse.TrangThai;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public HangUpdateRequest ToHangUpdateRequest()
        {
            return new HangUpdateRequest()
            {
                ID_Hang = ID_Hang,
                TenHang = TenHang,
                MoTa = MoTa,
                TrangThai = (StatusOptions)Enum.Parse(typeof(StatusOptions), TrangThai)
            };
        }
    }

    public static class HangExtensions
    {
        public static HangResponse ToHangResponse(this Hang hang)
        {
            return new HangResponse()
            {
                ID_Hang = hang.ID_Hang,
                TenHang = hang.TenHang,
                MoTa = hang.MoTa,
                TrangThai = hang.TrangThai
            };
        }
    }
}
