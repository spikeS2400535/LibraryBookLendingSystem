using System;

namespace LibraryBookLendingSystem.Models
{
    // Loan class - links Member and LibraryItem together
    public class Loan
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public LibraryItem Item { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public Loan(int id, Member member, LibraryItem item)
        {
            Id = id;
            Member = member;
            Item = item;
            BorrowDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(14); // 2 weeks loan period
            IsReturned = false;
            ReturnDate = null;
        }

        // Check if loan is overdue
        public bool IsOverdue()
        {
            if (IsReturned) return false;
            return DateTime.Now > DueDate;
        }

        // Get number of days overdue
        public int DaysOverdue()
        {
            if (!IsOverdue()) return 0;
            return (DateTime.Now - DueDate).Days;
        }

        public string GetInfo()
        {
            string status = IsReturned ? "Returned" : (IsOverdue() ? $"OVERDUE by {DaysOverdue()} days" : "Active");
            return $"Loan ID: {Id} | Member: {Member.Name} | Item: {Item.Title} | Borrowed: {BorrowDate.ToShortDateString()} | Due: {DueDate.ToShortDateString()} | Status: {status}";
        }
    }
}