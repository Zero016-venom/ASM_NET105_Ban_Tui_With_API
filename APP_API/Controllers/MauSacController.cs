using APP_DATA.DatabaseContext;
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
        public ActionResult CreateMau(MauSac mauSac)
        {
            try
            {
                mauSac.ID_MauSac = Guid.NewGuid();
                _db.MauSac.Add(mauSac);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-mau")]
        public ActionResult UpdateMau(MauSac mauSac)
        {
            try
            {
                MauSac? matchingMau = _db.MauSac.FirstOrDefault(temp=>temp.ID_MauSac == mauSac.ID_MauSac);

                if (matchingMau == null)
                    throw new ArgumentNullException(nameof(matchingMau));
                matchingMau.TenMauSac = mauSac.TenMauSac;
                matchingMau.MoTa = mauSac.MoTa;
                matchingMau.TrangThai = mauSac.TrangThai;

                _db.MauSac.Update(mauSac);
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
