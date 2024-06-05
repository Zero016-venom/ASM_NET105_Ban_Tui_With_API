using APP_DATA.DatabaseContext;
using ASM_NET105_BanTui.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSanPhamCcontroller : ControllerBase
    {
        AppDbContext _db;

        public LoaiSanPhamCcontroller(AppDbContext db)
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
            return Ok(_db.LoaiSP.FirstOrDefault(temp=>temp.ID_LoaiSP == id));
        }

        [HttpPost("create-loai-san-pham")]
        public ActionResult CreateLoaiSanPham(LoaiSP loaiSanPham)
        {
            try
            {
                loaiSanPham.ID_LoaiSP = Guid.NewGuid();
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
        public ActionResult UpdateLoaiSanPham(LoaiSP loaiSanPham)
        {
            try
            {
                LoaiSP? mathingLoaiSanPham = _db.LoaiSP.FirstOrDefault(temp => temp.ID_LoaiSP == loaiSanPham.ID_LoaiSP);
                if (mathingLoaiSanPham == null)
                    throw new ArgumentNullException(nameof(mathingLoaiSanPham));

                mathingLoaiSanPham.TenLoaiSP = loaiSanPham.TenLoaiSP;
                mathingLoaiSanPham.MoTa = loaiSanPham.MoTa;
                mathingLoaiSanPham.TrangThai = loaiSanPham.TrangThai;

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
