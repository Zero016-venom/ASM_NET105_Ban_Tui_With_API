﻿using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
using ASM_NET105_BanTui.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSanPhamController : ControllerBase
    {
        AppDbContext _db;

        public LoaiSanPhamController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-loai-san-pham")]
        public ActionResult GetAllLoaiSanPham()
        {
            return Ok(_db.LoaiSP.ToList());
        }

        [HttpGet("get-loai-san-pham-by-id")]
        public ActionResult GetLoaiSanPhamById(Guid id)
        {
            return Ok(_db.LoaiSP.FirstOrDefault(temp => temp.ID_LoaiSP == id));
        }

        [HttpPost("create-loai-san-pham")]
        public ActionResult CreateLoaiSanPham(LoaiSanPhamAddRequest loaiSanPhamAddRequest)
        {
            try
            {
                LoaiSP loaiSanPham = new LoaiSP()
                {
                    ID_LoaiSP = Guid.NewGuid(),
                    TenLoaiSP = loaiSanPhamAddRequest.TenLoaiSP,
                    MoTa = loaiSanPhamAddRequest.MoTa,
                    TrangThai = loaiSanPhamAddRequest.TrangThai.ToString()
                };

                _db.LoaiSP.Add(loaiSanPham);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-loai-san-pham")]
        public ActionResult UpdateLoaiSanPham(LoaiSanPhamUpdateRequest loaiSanPhamUpdateRequest)
        {
            try
            {
                LoaiSP? mathingLoaiSanPham = _db.LoaiSP.FirstOrDefault(temp => temp.ID_LoaiSP == loaiSanPhamUpdateRequest.ID_LoaiSP);
                if (mathingLoaiSanPham == null)
                    throw new ArgumentNullException(nameof(mathingLoaiSanPham));

                mathingLoaiSanPham.TenLoaiSP = loaiSanPhamUpdateRequest.TenLoaiSP;
                mathingLoaiSanPham.MoTa = loaiSanPhamUpdateRequest.MoTa;
                mathingLoaiSanPham.TrangThai = loaiSanPhamUpdateRequest.TrangThai.ToString();

                _db.LoaiSP.Update(mathingLoaiSanPham);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-loai-san-pham")]
        public ActionResult DeleteLoaiSanPham(Guid id)
        {
            try
            {
                LoaiSP? mathingLoaiSanPham = _db.LoaiSP.FirstOrDefault(temp => temp.ID_LoaiSP == id);
                if (mathingLoaiSanPham == null)
                    throw new ArgumentNullException(nameof(mathingLoaiSanPham));

                _db.LoaiSP.Remove(mathingLoaiSanPham);
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
