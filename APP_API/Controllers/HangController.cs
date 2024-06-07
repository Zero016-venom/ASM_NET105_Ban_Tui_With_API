using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
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
        public ActionResult CreateHang(HangAddRequest hangAddRequest)
        {
            try
            {
                Hang? hang = new Hang()
                {
                    ID_Hang = Guid.NewGuid(),
                    TenHang = hangAddRequest.TenHang,
                    MoTa = hangAddRequest.MoTa,
                    TrangThai = hangAddRequest.TrangThai.ToString()
                };
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
        public ActionResult UpdateHang(HangUpdateRequest hangUpdateRequest)
        {
            try
            {
                Hang? matchingHang = _db.Hang.FirstOrDefault(temp => temp.ID_Hang == hangUpdateRequest.ID_Hang);

                if (matchingHang == null)
                    throw new ArgumentNullException(nameof(matchingHang));

                matchingHang.TenHang = hangUpdateRequest.TenHang;
                matchingHang.MoTa = hangUpdateRequest.MoTa;
                matchingHang.TrangThai = hangUpdateRequest.TrangThai.ToString();

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
