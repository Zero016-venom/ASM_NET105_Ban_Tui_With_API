using APP_DATA.DatabaseContext;
using ASM_NET105_BanTui.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangController : ControllerBase
    {
        AppDbContext _db;
        public HangController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-hang")]
        public ActionResult GetAllHang()
        {
            return Ok(_db.Hang.ToList());
        }

        [HttpGet("get-hang-by-id")]
        public ActionResult GetHangById(Guid id)
        {
            return Ok(_db.Hang.FirstOrDefault(temp => temp.ID_Hang == id));
        }

        [HttpPost("create-hang")]
        public ActionResult CreateHang(Hang hang)
        {
            try
            {
                hang.ID_Hang = Guid.NewGuid();
                _db.Hang.Add(hang);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-hang")]
        public ActionResult UpdateHang(Hang hang)
        {
            try
            {
                Hang? matchingHang = _db.Hang.FirstOrDefault(temp => temp.ID_Hang == hang.ID_Hang);

                if (matchingHang == null)
                    throw new ArgumentNullException(nameof(matchingHang));

                matchingHang.TenHang = hang.TenHang;
                matchingHang.MoTa = hang.MoTa;
                matchingHang.TrangThai = hang.TrangThai;

                _db.Hang.Update(matchingHang);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-hang")]
        public ActionResult DeleteHang(Guid id)
        {
            try
            {
                Hang? matchingHang = _db.Hang.FirstOrDefault(temp => temp.ID_Hang == id);

                if (matchingHang == null)
                    throw new ArgumentNullException(nameof(matchingHang));

                _db.Hang.Remove(matchingHang);
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
