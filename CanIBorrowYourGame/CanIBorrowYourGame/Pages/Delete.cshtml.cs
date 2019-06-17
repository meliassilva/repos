using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CanIBorrowYourGame.Model;
using CanIBorrowYourGame.Models;

namespace CanIBorrowYourGame.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CanIBorrowYourGame.Models.CanIBorrowYourGameContext _context;

        public DeleteModel(CanIBorrowYourGame.Models.CanIBorrowYourGameContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Games Games { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Games = await _context.Games.FirstOrDefaultAsync(m => m.ID == id);

            if (Games == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Games = await _context.Games.FindAsync(id);

            if (Games != null)
            {
                _context.Games.Remove(Games);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
