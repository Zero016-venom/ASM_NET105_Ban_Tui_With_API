﻿using APP_DATA.DatabaseContext;
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

        [HttpGet("get-hat-lieu-by-id")]
        public ActionResult GetChatLieuById(Guid id)
        {
            return Ok(_db.ChatLieu.FirstOrDefault(temp => temp.ID_ChatLieu == id));
        }

        [HttpPost("create-chat-lieu")]
        public ActionResult CreateChatLieu(ChatLieu chatLieu)
        {
            try
            {
                chatLieu.ID_ChatLieu = Guid.NewGuid();
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
        public ActionResult UpdateChatLieu(ChatLieu chatLieu)
        {
            try
            {
                ChatLieu? matchingChatLieu = _db.ChatLieu.FirstOrDefault(temp => temp.ID_ChatLieu == chatLieu.ID_ChatLieu);

                matchingChatLieu.TenChatLieu = chatLieu.TenChatLieu;
                matchingChatLieu.MoTa = chatLieu.MoTa;
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
