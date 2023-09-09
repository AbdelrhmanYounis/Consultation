using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Consultation.Data;
using Consultation.Models;
using Consultation.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Consultation.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Consultation.Pages.Counsel
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _service;
        public IndexModel(IPaymentService service, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _context = context;
            _userManager = userManager;
        }

		public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(o => o.Owner).Include(p=>p.Price).ToListAsync();
            }
        }
        public async Task<IActionResult> OnGetCheckoutProduct(int id)
        {
            var product=_context.Products.Include(p => p.Price).SingleOrDefault(p => p.Id == id);
            var user = await _userManager.GetUserAsync(User);
            if (product == null || user == null)
                return Page();
            try
            {
                var Tokenobj = await _service.GetToken();
                var objwithID = await _service.OrderRegistation(Tokenobj, product.ProductPrice * 100);
                var IframToken = await _service.PaymentKey(Tokenobj, objwithID, product.ProductPrice * 100);
                var ProductConfirmation = new ProductConfirmation
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    order = objwithID,
                    Expired = true
                };
                _context.ProductsConfirmation.Add(ProductConfirmation);
                await _context.SaveChangesAsync();
                return Redirect("https://accept.paymob.com/api/acceptance/iframes/392179?payment_token=" + IframToken);
            }
            catch (Exception ex)
			{
                return Page();
            }
        }
        public IActionResult OnGetBlockProduct(int id)
        {
            var result = false;
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            if (product != null)
            {
               product.IsBlocked = !product.IsBlocked;
                 _context.SaveChangesAsync();
                result = true;
            }
			return new JsonResult(result);
        }

    }
}
