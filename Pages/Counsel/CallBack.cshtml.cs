using Consultation.Data;
using Consultation.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace Consultation.Pages.Counsel
{
    public class CallBackModel : PageModel
    {
        private readonly ApplicationDbContext _context;

		public CallBackModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public  void OnGet(ResponseDto response)
        {
            if (response == null)
            {
                ViewData["Title"] = "Failed";
            }
            else
            {
              var productConfirmation = _context.ProductsConfirmation.SingleOrDefault(p=> p.order == response.order.ToString() );
              var product = _context.Products.SingleOrDefault(p => p.order == response.order.ToString());

                if(product!=null)
				{
					if (response.success)
					{
                        product.IsBlocked = false;
                        _context.SaveChangesAsync();
                        ViewData["Title"] = "Success, you can give your counsel";
                    }
                    else
					{
                        _context.Products.Remove(product);
                        _context.SaveChangesAsync();
                        ViewData["Title"] = "Failed, you can't give your counsel";
                    }
                }

               else if (productConfirmation != null && response.success)
				{
                    productConfirmation.Expired = false;
                     _context.SaveChangesAsync();
                    ViewData["Title"] = "Success, you can message counsel owner";
                }
                else

				{
                    _context.ProductsConfirmation.RemoveRange(
                            _context.ProductsConfirmation.Where(p => p.Id <= productConfirmation.Id && p.Expired == true)
                        );
                     _context.SaveChangesAsync();

                    ViewData["Title"] = "Failed, try again";
                }
				


            }
            /*
            string[] HMACStringKeys = new string[20] { "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction"
              , "id","integration_id","is_3d_secure","is_auth","is_capture","is_refunded","is_standalone_payment","is_voided"
              ,"order","owner","pending","source_data_pan","source_data_sub_type","source_data_type","success" };

            string HMACStringconcatenates = "", HMAC_secret = "C052EACA1C43FDFCD23FA8D9BFE98113";

            foreach (var propertyName in HMACStringKeys)
            {
                try
                {
                    HMACStringconcatenates += response.GetType().GetProperty(propertyName).GetValue(response, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            var HashedHMAC = SHA512_ComputeHash(HMACStringconcatenates, HMAC_secret);

            if (string.Equals(response.hmac, HashedHMAC))
            {
                return View();
            }
            return View("failed");
            */
        }
        public static string SHA512_ComputeHash(string text, string secretKey)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
