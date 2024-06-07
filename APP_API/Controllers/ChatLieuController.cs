using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
using ASM_NET105_BanTui.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        AppDbContext _db;

        public ChatLieuController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-chat-lieu")]
        public ActionResult GetAllChatLieu()
        {
            return Ok(_db.ChatLieu.ToList());
        }

        [HttpGet("get-chat-lieu-by-id")]
        public ActionResult GetChatLieuById(Guid id)
        {
            return Ok(_db.ChatLieu.FirstOrDefault(temp => temp.ID_ChatLieu == id));
        }

        [HttpPost("create-chat-lieu")]
        public ActionResult CreateChatLieu(ChatLieuAddRequest chatLieuAddRequest)
        {
            try
            {
                ChatLieu? chatLieu = new ChatLieu()
                {
                    ID_ChatLieu = Guid.NewGuid(),
                    TenChatLieu = chatLieuAddRequest.TenChatLieu,
                    MoTa = chatLieuAddRequest.MoTa,
                    TrangThai = chatLieuAddRequest.TrangThai.ToString()
                };
                _db.ChatLieu.Add(chatLieu);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-chat-lieu")]
        public ActionResult UpdateChatLieu(ChatLieuUpdateRequest chatLieuUpdateRequest)
        {
            try
            {
                ChatLieu? matchingChatLieu = _db.ChatLieu.FirstOrDefault(temp => temp.ID_ChatLieu == chatLieuUpdateRequest.ID_ChatLieu);

                if (matchingChatLieu == null)
                    throw new ArgumentNullException(nameof(matchingChatLieu));

                matchingChatLieu.TenChatLieu = chatLieuUpdateRequest.TenChatLieu;
                matchingChatLieu.MoTa = chatLieuUpdateRequest.MoTa;
                matchingChatLieu.TrangThai = chatLieuUpdateRequest.TrangThai.ToString();

                _db.ChatLieu.Update(matchingChatLieu);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-chat-lieu")]
        public ActionResult DeleteChatLieu(Guid id)
        {
            try
            {
                ChatLieu? matchingChatLieu = _db.ChatLieu.FirstOrDefault(temp => temp.ID_ChatLieu == id);
                if (matchingChatLieu == null)
                    throw new ArgumentNullException(nameof(matchingChatLieu));

                _db.ChatLieu.Remove(matchingChatLieu);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
