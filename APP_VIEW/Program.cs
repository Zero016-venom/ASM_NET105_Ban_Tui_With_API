using APP_VIEW.IServices;
using APP_VIEW.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using APP_DATA.DatabaseContext;
using APP_DATA.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddTransient<IChatLieuService, ChatLieuService>();
builder.Services.AddTransient<IHangService,  HangService>();
builder.Services.AddTransient<ILoaiSanPhamService, LoaiSanPhamService>();
builder.Services.AddTransient<IChuongTrinhKhuyenMaiService, ChuongTrinhKhuyenMaiService>();
builder.Services.AddTransient<ISanPhamService,  SanPhamService>();
builder.Services.AddTransient<IUserService, UserService>();


//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//{
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireDigit = true;
//})
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders()
//    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
//    .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
