﻿using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;

namespace APP_VIEW.Controllers
{
    public class ChatLieuController : Controller
    {
        private readonly IChatLieuService _iChatLieuService;

        public ChatLieuController(IChatLieuService ichatLieuService)
        {
            _iChatLieuService = ichatLieuService;
        }

        public async Task<ActionResult> Index()
        {
            List<ChatLieuResponse> chatLieus = await _iChatLieuService.GetAllChatLieu();
            return View(chatLieus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatLieuAddRequest chatLieuAddRequest)
        {
            ChatLieuResponse chatLieuResponse = await _iChatLieuService.AddChatLieu(chatLieuAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ChatLieuResponse? chatLieuResponse = await _iChatLieuService.GetChatLieuById(id);
            if (chatLieuResponse == null)
                return RedirectToAction("Index");

            ChatLieuUpdateRequest chatLieuUpdateRequest  = chatLieuResponse.ToChatLieuUpdateRequest();

            List<ChatLieuResponse> chatLieus = await _iChatLieuService.GetAllChatLieu();

            return View(chatLieuUpdateRequest);
        }

        [HttpPost]
        public ActionResult EditChatLieu(ChatLieu chatLieu)
        {
            _iChatLieuService.UpdateChatLieu(chatLieu);
            return RedirectToAction("Index");
        }
    }
}