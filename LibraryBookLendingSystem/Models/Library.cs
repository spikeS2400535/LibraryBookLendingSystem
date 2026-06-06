using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBookLendingSystem.Models
{
    // Main library manager class - implements IReportable interface
    public class Library : IReportable
    {
        private List<LibraryItem> _items;
        private List<Member> _members;
        private List<Loan> _loans;
        private int _nextItemId = 1;
        private int _nextMemberId = 1;
        private int _nextLoanId = 1;

        public Library()
        {
            _items = new List<LibraryItem>();
            _members = new List<Member>();
            _loans = new List<Loan>();
        }

        // --- BOOK METHODS ---
        public void AddItem(LibraryItem item)
        {
            item.Id = _nextItemId++;
            _items.Add(item);
        }

        public List<LibraryItem> GetAllItems()
        {
            return _items;
        }

        public List<LibraryItem> SearchItems(string keyword)
        {
            keyword = keyword.ToLower();
            return _items.Where(i =>
                i.Title.ToLower().Contains(keyword) ||
                i.Author.ToLower().Contains(keyword) ||
                i.ISBN.ToLower().Contains(keyword)).ToList();
        }

        public bool RemoveItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;
            _items.Remove(item);
            return true;
        }

        // --- MEMBER METHODS ---
        public void AddMember(Member member)
        {
            member.Id = _nextMemberId++;
            _members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return _members;
        }

        public Member GetMemberById(int id)
        {
            return _members.FirstOrDefault(m => m.Id == id);
        }

        public bool RemoveMember(int id)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;
            _members.Remove(member);
            return true;
        }

        // --- LOAN METHODS ---
        public string BorrowItem(int memberId, int itemId)
        {
            try
            {
                var member = _members.FirstOrDefault(m => m.Id == memberId);
                if (member == null) throw new Exception("Member not found.");
                if (!member.IsActive) throw new Exception("Member is not active.");

                var item = _items.FirstOrDefault(i => i.Id == itemId);
                if (item == null) throw new Exception("Item not found.");
                if (!item.IsAvailable) throw new Exception("Item is not available.");

                // Check if member already has this item
                var existing = _loans.FirstOrDefault(l =>
                    l.Member.Id == memberId && l.Item.Id == itemId && !l.IsReturned);
                if (existing != null) throw new Exception("Member already has this item on loan.");

                var loan = new Loan(_nextLoanId++, member, item);
                _loans.Add(loan);

                // Update availability
                if (item is Book book)
                {
                    book.AvailableCopies--;
                    if (book.AvailableCopies <= 0)
                        book.IsAvailable = false;
                }
                else
                {
                    item.IsAvailable = false;
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ReturnItem(int loanId)
        {
            try
            {
                var loan = _loans.FirstOrDefault(l => l.Id == loanId);
                if (loan == null) throw new Exception("Loan not found.");
                if (loan.IsReturned) throw new Exception("Item already returned.");

                loan.IsReturned = true;
                loan.ReturnDate = DateTime.Now;

                // Update availability
                if (loan.Item is Book book)
                {
                    book.AvailableCopies++;
                    book.IsAvailable = true;
                }
                else
                {
                    loan.Item.IsAvailable = true;
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Loan> GetAllLoans()
        {
            return _loans;
        }

        public List<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned).ToList();
        }

        public List<Loan> GetOverdueLoans()
        {
            return _loans.Where(l => l.IsOverdue()).ToList();
        }

        // --- INTERFACE METHOD ---
        public string GenerateReport()
        {
            return $"Library Report\n" +
                   $"Total Items: {_items.Count}\n" +
                   $"Total Members: {_members.Count}\n" +
                   $"Active Loans: {GetActiveLoans().Count}\n" +
                   $"Overdue Loans: {GetOverdueLoans().Count}";
        }
    }
}