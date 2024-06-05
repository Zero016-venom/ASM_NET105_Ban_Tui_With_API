using APP_DATA.DatabaseContext;
using APP_DATA.Models;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuongTrinhKhuyenMaiController : ControllerBase
    {
        AppDbContext _db;

        public ChuongTrinhKhuyenMaiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-chuong-trinh-khuyen-mai")]
        public ActionResult GetAllChuongTrinhKhuyenMai()
        {
            return Ok(_db.ChuongTrinhKhuyenMai.ToList());
        }

        [HttpGet("get-chuong-trinh-khuyen-mai-by-id")]
        public ActionResult GetAllChuongTrinhKhuyenMaiById(Guid id)
        {
            return Ok(_db.ChuongTrinhKhuyenMai.FirstOrDefault(temp=>temp.ID_ChuongTrinhKhuyenMai == id));
        }

        [HttpPost("create-chuong-trinh-khuyen-mai")]
        public ActionResult CreateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            try
            {
                chuongTrinhKhuyenMai.ID_ChuongTrinhKhuyenMai = Guid.NewGuid();
                _db.ChuongTrinhKhuyenMai.Add(chuongTrinhKhuyenMai);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-chuong-trinh-khuyen-mai")]
        public ActionResult UpdateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            try
            {
                ChuongTrinhKhuyenMai? matchingChuongTrinhKhuyenMai = _db.ChuongTrinhKhuyenMai.FirstOrDefault(temp => temp.ID_ChuongTrinhKhuyenMai == chuongTrinhKhuyenMai.ID_ChuongTrinhKhuyenMai);
                if (matchingChuongTrinhKhuyenMai == null)
                    throw new ArgumentNullException(nameof(ChuongTrinhKhuyenMai));

                matchingChuongTrinhKhuyenMai.TenChuongTrinhKhuyenMai = chuongTrinhKhuyenMai.TenChuongTrinhKhuyenMai;
                matchingChuongTrinhKhuyenMai.NgayBatDau = chuongTrinhKhuyenMai.NgayBatDau;
                matchingChuongTrinhKhuyenMai.NgayKetThuc = chuongTrinhKhuyenMai.NgayKetThuc;
                matchingChuongTrinhKhuyenMai.TrangThai = chuongTrinhKhuyenMai.TrangThai;

                _db.ChuongTrinhKhuyenMai.Update(matchingChuongTrinhKhuyenMai);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("delete-chuong-trinh-khuyen-mai")]
        public ActionResult DeleteChuongTrinhKhuyenMai(Guid id)
        {
            try
            {
                ChuongTrinhKhuyenMai? matchingChuongTrinhKhuyenMai = _db.ChuongTrinhKhuyenMai.FirstOrDefault(temp => temp.ID_ChuongTrinhKhuyenMai == id);
                if (matchingChuongTrinhKhuyenMai == null)
                    throw new ArgumentNullException(nameof(ChuongTrinhKhuyenMai));

                _db.ChuongTrinhKhuyenMai.Remove(matchingChuongTrinhKhuyenMai);
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
