using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Consultation.Data;
using Consultation.Models;
using Microsoft.AspNetCore.Identity;

namespace Consultation.Pages.Counsel
{
    public class IndexByIdModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexByIdModel(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (_context.Products != null || user!=null)
            {
                Product = await _context.Products.Where(O=>O.OwnerId==user.Id)
                .Include(o => o.Owner).Include(p => p.Price).ToListAsync();
            }
        }
    }
}
