using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        AppDbContext _db;

        public SanPhamController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("get-all-san-pham")]
        public ActionResult GetAllSanPham()
        {
            return Ok(_db.SanPham.ToList());
        }

        [HttpGet("get-san-pham-by-id")]
        public ActionResult GetSanPhamById(Guid id)
        {
            return Ok(_db.SanPham.FirstOrDefault(temp => temp.ID_SanPham == id));
        }

        [HttpPost("create-san-pham")]
        public ActionResult CreateSanPham(SanPhamAddRequest sanPhamAddRequest)
        {
            try
            {
                SanPham sanPham = new SanPham()
                {
                    ID_SanPham = Guid.NewGuid(),
                    TenSanPham = sanPhamAddRequest.TenSanPham,
                    ID_Hang = sanPhamAddRequest.ID_Hang,
                    SoLuongTon = sanPhamAddRequest.SoLuongTon,
                    ID_MauSac = sanPhamAddRequest.ID_MauSac,
                    GiaNiemYet = sanPhamAddRequest.GiaNiemYet,
                    ID_ChatLieu= sanPhamAddRequest.ID_ChatLieu,
                    ID_LoaiSP = sanPhamAddRequest.ID_LoaiSP,
                    TrangThai = sanPhamAddRequest.TrangThai.ToString()
                };
                _db.SanPham.Add(sanPham);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-san-pham")]
        public ActionResult UpdateSanPham(SanPhamUpdateRequest sanPhamUpdateRequest)
        {
            try
            {
                SanPham? matchingSanPham = _db.SanPham.FirstOrDefault(temp => temp.ID_SanPham == sanPhamUpdateRequest.ID_SanPham);
                if (matchingSanPham == null)
                    throw new ArgumentNullException(nameof(matchingSanPham));

                matchingSanPham.TenSanPham = sanPhamUpdateRequest.TenSanPham;
                matchingSanPham.ID_Hang = sanPhamUpdateRequest.ID_Hang;
                matchingSanPham.SoLuongTon = sanPhamUpdateRequest.SoLuongTon;
                matchingSanPham.ID_MauSac = sanPhamUpdateRequest.ID_MauSac;
                matchingSanPham.GiaNiemYet = sanPhamUpdateRequest.GiaNiemYet;
                matchingSanPham.ID_ChatLieu = sanPhamUpdateRequest.ID_ChatLieu;
                matchingSanPham.ID_LoaiSP = sanPhamUpdateRequest.ID_LoaiSP;
                matchingSanPham.Img = sanPhamUpdateRequest.Img;
                matchingSanPham.TrangThai = sanPhamUpdateRequest.TrangThai.ToString();

                _db.SanPham.Update(matchingSanPham);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-san-pham")]
        public ActionResult DeleteSanPham(Guid id)
        {
            try
            {
                SanPham? matchingSanPham = _db.SanPham.FirstOrDefault(temp => temp.ID_SanPham == id);
                if(matchingSanPham == null)
                    throw new ArgumentException(nameof(matchingSanPham));
                _db.Remove(matchingSanPham);
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
