using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
namespace webapp.Pages.Prawa
{
    public class PracChangeModel : PageModel
    {
        private  ShopContext _context;

        public PracChangeModel(ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            if (id == null)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
            if(Account.role==0)
            Account.role = 1;
            else if (Account.role == 1)
                Account.role = 0;
            await _context.SaveChangesAsync();

            if (Account.role == 0)
                return RedirectToPage("Pracownicy");
            else
                return RedirectToPage("Index");
        }
    }
}
