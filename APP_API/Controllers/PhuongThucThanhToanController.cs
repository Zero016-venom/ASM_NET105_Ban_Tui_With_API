using APP_DATA.DatabaseContext;
using APP_DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongThucThanhToanController : ControllerBase
    {
        AppDbContext _db;

        public PhuongThucThanhToanController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-phuong-thuc-thanh-toan")]
        public ActionResult GetAllPhuongThucThanhToan()
        {
            return Ok(_db.PTTT.ToList());
        }

        [HttpGet("get-phuong-thuc-thanh-toan-by-id")]
        public ActionResult GetPhuongThucThanhToanById(Guid id)
        {
            return Ok(_db.PTTT.FirstOrDefault(temp=>temp.ID_PTTT == id));
        }

        [HttpPost("create-phuong-thuc-thanh-toan-by")]
        public ActionResult CreatePhuongThucThanhToan(PTTT phuongThucThanhToan)
        {
            try
            {
                phuongThucThanhToan.ID_PTTT = Guid.NewGuid();
                _db.PTTT.Add(phuongThucThanhToan);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-phuong-thuc-thanh-toan-by")]
        public ActionResult UpdatePhuongThucThanhToan(PTTT phuongThucThanhToan)
        {
            try
            {
                PTTT? matchingPhuongThucThanhToan = _db.PTTT.FirstOrDefault(temp => temp.ID_PTTT == phuongThucThanhToan.ID_PTTT);
                if(matchingPhuongThucThanhToan == null)
                    throw new ArgumentNullException(nameof(PTTT));

                matchingPhuongThucThanhToan.TenPTTT = phuongThucThanhToan.TenPTTT;
                matchingPhuongThucThanhToan.MoTa = phuongThucThanhToan.MoTa;
                matchingPhuongThucThanhToan.TrangThai = phuongThucThanhToan.TrangThai;

                _db.PTTT.Update(matchingPhuongThucThanhToan);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-phuong-thuc-thanh-toan-by")]
        public ActionResult DeletePhuongThucThanhToan(Guid id)
        {
            try
            {
                PTTT? matchingPhuongThucThanhToan = _db.PTTT.FirstOrDefault(temp => temp.ID_PTTT == id);
                if (matchingPhuongThucThanhToan == null)
                    throw new ArgumentNullException(nameof(PTTT));

                _db.PTTT.Remove(matchingPhuongThucThanhToan);
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
