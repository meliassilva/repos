using System;
using System.ComponentModel.DataAnnotations;

namespace CanIBorrowYourGame.Model
{
    public class Games
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Owner { get; set; }
        public string AvailableAt { get; set; }

    }
}
