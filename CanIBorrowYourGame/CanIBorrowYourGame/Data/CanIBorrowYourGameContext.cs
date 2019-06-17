using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanIBorrowYourGame.Model;

namespace CanIBorrowYourGame.Models
{
    public class CanIBorrowYourGameContext : DbContext
    {
        public CanIBorrowYourGameContext (DbContextOptions<CanIBorrowYourGameContext> options)
            : base(options)
        {
        }

        public DbSet<CanIBorrowYourGame.Model.Games> Games { get; set; }
    }
}
