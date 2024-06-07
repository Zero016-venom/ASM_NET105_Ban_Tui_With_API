﻿using APP_DATA.Enums;
using APP_DATA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.DTO
{
    public class HangAddRequest
    {
        [Required(ErrorMessage = "Tên hãng không được để trống ")]
        public string? TenHang { get; set; }
        
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống ")]
        public StatusOptions? TrangThai { get; set; }

        public Hang ToHang()
        {
            return new Hang()
            {
                TenHang = TenHang,
                MoTa = MoTa,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
