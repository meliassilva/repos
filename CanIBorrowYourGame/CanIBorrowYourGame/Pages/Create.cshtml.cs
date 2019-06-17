using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CanIBorrowYourGame.Model;
using CanIBorrowYourGame.Models;

namespace CanIBorrowYourGame.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CanIBorrowYourGame.Models.CanIBorrowYourGameContext _context;

        public CreateModel(CanIBorrowYourGame.Models.CanIBorrowYourGameContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Games Games { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Games.Add(Games);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}