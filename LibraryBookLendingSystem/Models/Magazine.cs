using System;

namespace LibraryBookLendingSystem.Models
{
    // Magazine inherits from LibraryItem - demonstrates INHERITANCE and POLYMORPHISM
    public class Magazine : LibraryItem
    {
        public string Issue { get; set; }
        public string Category { get; set; }

        public Magazine(int id, string title, string author, string isbn, int year, string issue, string category)
            : base(id, title, author, isbn, year)
        {
            Issue = issue;
            Category = category;
        }

        // Override abstract method - demonstrates POLYMORPHISM
        public override string GetItemType()
        {
            return "Magazine";
        }

        // Override virtual method - demonstrates POLYMORPHISM
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Issue: {Issue} | Category: {Category}";
        }
    }
}