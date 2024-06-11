using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace APP_VIEW.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;
        private readonly IChatLieuService _chatLieuService;
        private readonly IHangService _hangService;
        private readonly IMauSacService _mauSacService;
        private readonly ILoaiSanPhamService _loaiSanPhamService;
        AppDbContext context;

        public SanPhamController(ISanPhamService sanPhamService, IChatLieuService chatLieuService, IHangService hangService, IMauSacService mauSacService, ILoaiSanPhamService loaiSanPhamService, AppDbContext context)
        {
            _sanPhamService = sanPhamService;
            _chatLieuService = chatLieuService;
            _hangService = hangService;
            _mauSacService = mauSacService;
            _loaiSanPhamService = loaiSanPhamService;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<SanPhamResponse> sanPhamResponses = await _sanPhamService.GetAllSanPhams();
            return View(sanPhamResponses);
        }
        public async Task<IActionResult> IndexKH()
        {
            List<SanPhamResponse> sanPhamResponses = await _sanPhamService.GetAllSanPhams();
            return View(sanPhamResponses);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            SanPhamResponse? sanPhamResponse = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhamResponse);
        }

        public async Task<IActionResult> DetailsKH(Guid id)
        {
            SanPhamResponse? sanPhamResponse = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhamResponse);
        }

        public async Task<IActionResult> Create()
        {
            List<ChatLieuResponse> chatLieus = await _chatLieuService.GetAllChatLieu();
            ViewBag.ChatLieus = chatLieus.Select(temp => new SelectListItem()
            {
                Text = temp.TenChatLieu,
                Value = temp.ID_ChatLieu.ToString()
            });

            List<HangResponse> hangs = await _hangService.GetAllHang();
            ViewBag.Hangs = hangs.Select(temp => new SelectListItem()
            {
                Text = temp.TenHang,
                Value = temp.ID_Hang.ToString()
            });

            List<MauSacResponse> mauSacs = await _mauSacService.GetAllMauSac();
            ViewBag.MauSacs = mauSacs.Select(temp => new SelectListItem()
            {
                Text = temp.TenMauSac,
                Value = temp.ID_MauSac.ToString()
            });

            List<LoaiSanPhamResponse> loaiSanPhams = await _loaiSanPhamService.GetAllLoaiSanPham();
            ViewBag.LoaiSanPhams = loaiSanPhams.Select(temp => new SelectListItem()
            {
                Text = temp.TenLoaiSP,
                Value = temp.ID_LoaiSP.ToString()
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SanPhamAddRequest sanPhamAddRequest, IFormFile imgFile)
        {
            if(imgFile != null && imgFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imgFile.FileName);
                var steam = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(steam);
                sanPhamAddRequest.Img = imgFile.FileName;
            }
            else
            {
                sanPhamAddRequest.Img = "";
            }

            SanPhamResponse sanPhamResponse = await _sanPhamService.AddSanPham(sanPhamAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            List<ChatLieuResponse> chatLieus = await _chatLieuService.GetAllChatLieu();
            ViewBag.ChatLieus = chatLieus.Select(temp => new SelectListItem()
            {
                Text = temp.TenChatLieu,
                Value = temp.ID_ChatLieu.ToString()
            });

            List<HangResponse> hangs = await _hangService.GetAllHang();
            ViewBag.Hangs = hangs.Select(temp => new SelectListItem()
            {
                Text = temp.TenHang,
                Value = temp.ID_Hang.ToString()
            });

            List<MauSacResponse> mauSacs = await _mauSacService.GetAllMauSac();
            ViewBag.MauSacs = mauSacs.Select(temp => new SelectListItem()
            {
                Text = temp.TenMauSac,
                Value = temp.ID_MauSac.ToString()
            });

            List<LoaiSanPhamResponse> loaiSanPhams = await _loaiSanPhamService.GetAllLoaiSanPham();
            ViewBag.LoaiSanPhams = loaiSanPhams.Select(temp => new SelectListItem()
            {
                Text = temp.TenLoaiSP,
                Value = temp.ID_LoaiSP.ToString()
            });

            SanPhamResponse? sanPhamResponse = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhamResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SanPhamUpdateRequest sanPhamUpdateRequest, IFormFile imgFile)
        {
            if (imgFile != null && imgFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imgFile.FileName);
                var steam = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(steam);
                sanPhamUpdateRequest.Img = imgFile.FileName;
            }
            else
            {
                sanPhamUpdateRequest.Img = "";
            }

            SanPhamResponse sanPhamResponse = await _sanPhamService.UpdateSanPham(sanPhamUpdateRequest);
            return RedirectToAction("Index");
        }

        public IActionResult AddToCart2(Guid id, int quantity)
        {
            var check = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(check, out Guid UserId))
            {
                if (string.IsNullOrEmpty(check))
                {
                    return RedirectToAction("Login", "TaiKhoan");
                }
                else
                {
                    var cartItem = context.GioHangCT.FirstOrDefault(x => x.ID_User == UserId && x.ID_SanPham == id);
                    var matchingSanPham = context.SanPham.Find(id);

                    if (cartItem == null)
                    {
                        if (matchingSanPham.SoLuongTon <= 0)
                        {
                            TempData["Message2"] = "Sản phẩm hết mất rồi!";
                        }
                        else
                        {
                            // Ktra sluong nhapạ vào 1
                            if (quantity > matchingSanPham.SoLuongTon)
                            {
                                quantity = matchingSanPham.SoLuongTon;
                                TempData["Message2"] = $"Số lượng nhập vào vượt quá số lượng còn lại. Đã điều chỉnh số lượng thành {quantity}.";
                            }

                            GioHangCT gioHangCT = new GioHangCT()
                            {
                                ID_GioHangCT = Guid.NewGuid(),
                                ID_SanPham = id,
                                SoLuong = quantity,
                                ID_User = UserId,
                            };
                            context.GioHangCT.Add(gioHangCT);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        var sanPham = context.SanPham.Find(id);

                        // Ktra sluong nhapạ vào 2
                        if (cartItem.SoLuong + quantity > sanPham.SoLuongTon)
                        {
                            quantity = sanPham.SoLuongTon - cartItem.SoLuong;
                            TempData["Message2"] = $"Số lượng nhập vào vượt quá số lượng còn lại. Đã điều chỉnh số lượng thành {quantity}.";
                        }

                        if (quantity > 0)
                        {
                            cartItem.SoLuong = cartItem.SoLuong + quantity;
                            context.GioHangCT.Add(cartItem);
                            context.SaveChanges();
                        }
                        else
                        {
                            TempData["Message2"] = "Không thể thêm số lượng bằng 0 hoặc âm!";
                        }
                    }
                }
            }
            return RedirectToAction("IndexKH", "SanPham");
        }
        public IActionResult AddToCartView(Guid id)
        {
            // Lấy thông tin sản phẩm từ ID và truyền vào view
            var product = context.SanPham.FirstOrDefault(p => p.ID_SanPham == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
