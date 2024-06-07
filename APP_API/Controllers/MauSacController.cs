using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        AppDbContext _db;

        public MauSacController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-mau")]
        public ActionResult GetAllMau()
        {
            return Ok(_db.MauSac.ToList());
        }

        [HttpGet("get-mau-by-id")]
        public ActionResult GetMauById(Guid id)
        {
            return Ok(_db.MauSac.FirstOrDefault(temp => temp.ID_MauSac == id));
        }

        [HttpPost("create-mau")]
        public ActionResult CreateMau(MauSacAddRequest ms)
        {
            try
            {
                MauSac? mau = new MauSac()
                {
                    ID_MauSac = Guid.NewGuid(),
                    TenMauSac = ms.TenMauSac,
                    MoTa = ms.MoTa,
                    TrangThai = ms.TrangThai
                };
                _db.MauSac.Add(mau);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-mau")]
        public ActionResult UpdateMau(MauSacUpdateRequest msu)
        {
            try
            {
                MauSac? matchingMau = _db.MauSac.FirstOrDefault(t => t.ID_MauSac == msu.ID_MauSac);

                if (matchingMau == null)
                    throw new ArgumentNullException(nameof(matchingMau));
                matchingMau.TenMauSac = msu.TenMauSac;
                matchingMau.MoTa = msu.MoTa;
                matchingMau.TrangThai = msu.TrangThai;
                _db.MauSac.Update(matchingMau);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-mau")]
        public ActionResult DeleteMau(Guid id)
        {
            try
            {
                MauSac? matchingMau = _db.MauSac.FirstOrDefault(temp => temp.ID_MauSac == id);

                if (matchingMau == null)
                    throw new ArgumentNullException(nameof(matchingMau));

                _db.MauSac.Remove(matchingMau);
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
