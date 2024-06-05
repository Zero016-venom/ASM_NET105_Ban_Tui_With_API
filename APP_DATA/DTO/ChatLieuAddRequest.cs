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
    public class ChatLieuAddRequest
    {
        [Required(ErrorMessage = "Tên chất liệu không được để trống")]
        public string? TenChatLieu { get; set; }

        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public StatusOptions? TrangThai { get; set; }

        public ChatLieu ToChatLieu()
        {
            return new ChatLieu()
            {
                TenChatLieu = TenChatLieu,
                MoTa = MoTa,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
