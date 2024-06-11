using APP_DATA.DatabaseContext;
using APP_DATA.Enums;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP_VIEW.Controllers
{
    public class HoaDonController : Controller
    {
        AppDbContext _db;

        public HoaDonController()
        {
            _db = new AppDbContext();
        }

        public IActionResult IndexAdmin()
        {
            var data = _db.HoaDon.Include(temp => temp.User).ToList();
            return View(data);
        }

        public IActionResult Index()
        {
            var check = HttpContext.Session.GetString("UserId");
            var data = _db.HoaDon.Where(temp => temp.ID_User.ToString() == check)
                .Include(x => x.User).Where(p => p.ID_User.ToString() == check).ToList();
            return View(data);
        }

        public ActionResult Details(Guid id)
        {
            var check = HttpContext.Session.GetString("UserId");

            var HoaDonId = HttpContext.Session.GetString("hoaDonId");
            if (string.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            else
            {
                var cartItem = _db.HoaDonCT
                .Where(x => x.ID_HoaDon == id)
                .Include(x => x.SanPham)
                .ToList();
                return View(cartItem);
            }
        }

        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var check = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            else
            {
                var hoaDon = _db.HoaDon.FirstOrDefault(x => x.ID_HoaDon == id);

                if (hoaDon != null && hoaDon.TrangThai != StatusOfBIllOptions.Canceled.ToString())
                {
                    hoaDon.TrangThai = StatusOfBIllOptions.Canceled.ToString();
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
                }

                return RedirectToAction("Index", "HoaDon");
            }
        }
        public ActionResult BuyAgain(Guid id)
        {
            var check = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            else
            {
                var hoaDon = _db.HoaDon.FirstOrDefault(x => x.ID_HoaDon == id);

                if (hoaDon != null)
                {
                    var hoaDonCTs = _db.HoaDonCT.Where(x => x.ID_HoaDon == id).ToList();
                    foreach (var item in hoaDonCTs)
                    {
                        var cartItem = _db.GioHangCT.FirstOrDefault(x => x.ID_User == Guid.Parse(check));
                        var matchingSanPham = _db.SanPham.FirstOrDefault(a => a.ID_SanPham == item.ID_SanPham);
                        if (cartItem == null)
                        {
                            if (item.SoLuong < matchingSanPham.SoLuongTon)
                            {
                                GioHangCT gioHangCT = new GioHangCT
                                {
                                    ID_GioHangCT = Guid.NewGuid(),
                                    ID_User = Guid.Parse(check),
                                    ID_SanPham = item.ID_SanPham,
                                    SoLuong = item.SoLuong
                                };
                                _db.GioHangCT.Add(gioHangCT);
                                TempData["Message5"] = "Thêm vào giỏ hàng thành công";
                            }
                            else
                            {
                                TempData["Message5"] = "Sản phẩm không đủ số lượng";
                                RedirectToAction("Index", "HoaDon");
                            }
                        }
                        else
                        {
                            if (matchingSanPham.SoLuongTon >= cartItem.SoLuong + item.SoLuong)
                            {
                                cartItem.SoLuong += item.SoLuong;
                                _db.GioHangCT.Update(cartItem);
                                TempData["Message5"] = "Thêm vào giỏ hàng thành công";
                            }
                            else
                            {
                                TempData["Message5"] = "Sản phẩm không đủ số lượng";
                                RedirectToAction("Index", "HoaDon");
                            }
                        }
                    }
                    _db.SaveChanges();
                }

                return RedirectToAction("Index", "HoaDon");
            }
        }
    }
}