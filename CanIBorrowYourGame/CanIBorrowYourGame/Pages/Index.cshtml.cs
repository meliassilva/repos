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
    public class IndexModel : PageModel
    {
        private readonly CanIBorrowYourGame.Models.CanIBorrowYourGameContext _context;

        public IndexModel(CanIBorrowYourGame.Models.CanIBorrowYourGameContext context)
        {
            _context = context;
        }

        public IList<Games> Games { get;set; }

        public async Task OnGetAsync()
        {
            Games = await _context.Games.ToListAsync();
        }
    }
}
