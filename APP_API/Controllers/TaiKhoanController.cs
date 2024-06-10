using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Enums;
using APP_DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _db;

        public TaiKhoanController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.User.ToListAsync();
            return Ok(users);
        }

        [HttpGet("get-user-by-id")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _db.User.FirstOrDefault(temp => temp.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = registerDTO.Email,
                    PersonName = registerDTO.PersonName,
                    PhoneNumber = registerDTO.Phone,
                    UserName = registerDTO.Email,
                    Status = StatusOptions.Active.ToString()
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    if (registerDTO.UserType == UserTypeOptions.Admin)
                    {
                        if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                        {
                            ApplicationRole applicationRole = new ApplicationRole()
                            {
                                Name = UserTypeOptions.Admin.ToString()
                            };
                            await _roleManager.CreateAsync(applicationRole);
                        }

                        await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                    }
                    else
                    {
                        if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                        {
                            ApplicationRole applicationRole = new ApplicationRole()
                            {
                                Name = UserTypeOptions.User.ToString()
                            };
                            await _roleManager.CreateAsync(applicationRole);
                        }

                        await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                    }

                    var gioHang = new GioHang()
                    {
                        ID_User = user.Id,
                        TrangThai = StatusOptions.Active.ToString()
                    };

                    _db.GioHang.Add(gioHang);
                    await _db.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
