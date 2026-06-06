using System;

namespace LibraryBookLendingSystem.Models
{
    // Book inherits from LibraryItem - demonstrates INHERITANCE
    public class Book : LibraryItem
    {
        public string Genre { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public Book(int id, string title, string author, string isbn, int year, string genre, int totalCopies)
            : base(id, title, author, isbn, year)
        {
            Genre = genre;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
            IsAvailable = totalCopies > 0;
        }

        // Override abstract method - demonstrates POLYMORPHISM
        public override string GetItemType()
        {
            return "Book";
        }

        // Override virtual method - demonstrates POLYMORPHISM
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Genre: {Genre} | Copies: {AvailableCopies}/{TotalCopies}";
        }
    }
}