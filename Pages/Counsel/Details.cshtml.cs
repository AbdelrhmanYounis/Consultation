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

namespace Consultation.Pages.Counsel
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _service;
        public DetailsModel(IPaymentService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p=>p.Price).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
                var Tokenobj = await _service.GetToken();
                var objwithID = await _service.OrderRegistation(Tokenobj, product.Price.Price*100);
                var IframToken = await _service.PaymentKey(Tokenobj, objwithID, product.Price.Price*100);
                return Redirect("https://accept.paymob.com/api/acceptance/iframes/392179?payment_token=" + IframToken);
            }
            return Page();
        }
   
    }
}
