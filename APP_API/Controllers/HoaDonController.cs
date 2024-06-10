using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Enums;
using APP_DATA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        AppDbContext _db;
        public HoaDonController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-hoadon")]
        public ActionResult GetAll()
        {
            return Ok(_db.HoaDon.ToList());
        }

        [HttpGet("get-hoadon-by-id")]
        public ActionResult GetHoaDonById(Guid id)
        {
            var detailedInvoices = _db.HoaDonCT
                .Where(x => x.ID_HoaDon == id)
                .Include(x => x.SanPham)
                .ToList();

            var detailedInvoicesWithProducts = detailedInvoices.Select(d => new
            {
                d.ID_HoaDonCT,
                d.ID_HoaDon,
                d.ID_SanPham,
                d.SoLuong,
                d.SanPham.TenSanPham,
                d.SanPham.GiaNiemYet
            }).ToList();

            return Ok(detailedInvoicesWithProducts);
        }

        [HttpDelete("delete-hoadon")]
        public ActionResult DeleteHoaDon(Guid id)
        {
            try
            {
                HoaDon? matchingHoaDon = _db.HoaDon.FirstOrDefault(temp => temp.ID_HoaDon == id);

                if (matchingHoaDon == null)
                {
                    return NotFound(new { message = "Không tìm thấy hoá đơn." });
                }

                if (matchingHoaDon.TrangThai != StatusOfBIllOptions.Canceled.ToString())
                {
                    matchingHoaDon.TrangThai = StatusOfBIllOptions.Canceled.ToString();

                    var hoaDonCTs = _db.HoaDonCT.Where(x => x.ID_HoaDon == id).ToList();

                    foreach (var item in hoaDonCTs)
                    {
                        var product = _db.SanPham.FirstOrDefault(p => p.ID_SanPham == item.ID_SanPham);

                        if (product != null)
                        {
                            product.SoLuongTon += item.SoLuong;
                        }
                    }

                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
