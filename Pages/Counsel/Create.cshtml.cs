using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Consultation.Data;
using Consultation.Models;
using Microsoft.AspNetCore.Identity;
using Consultation.Interfaces;

namespace Consultation.Pages.Counsel
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _service;

        public CreateModel(IPaymentService service, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _service = service;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Prices"] = _context.ProductPrice.ToList();
            return Page();
        }
        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var price = _context.ProductPrice.SingleOrDefault(p => p.Id == Product.PriceId);
            if ( Product == null || user==null || price==null)
            {
                return Page();
            }
            try
            {
                var Tokenobj = await _service.GetToken();
                var objwithID = await _service.OrderRegistation(Tokenobj, price.Price * 100);
                var IframToken = await _service.PaymentKey(Tokenobj, objwithID, price.Price * 100);

                Product.OwnerId = user.Id;
                Product.order = objwithID;
                Product.IsBlocked = true;
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();

                return Redirect("https://accept.paymob.com/api/acceptance/iframes/392179?payment_token=" + IframToken);
            }
            catch (Exception ex)
			{
                return Page();
            }
            }
    }
}
