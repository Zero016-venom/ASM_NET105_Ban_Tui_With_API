using ASM_NET105_BanTui.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<HoaDon>? Hoadons { get; set; }
        public virtual GioHang? GioHang { get; set; }
    }
}
