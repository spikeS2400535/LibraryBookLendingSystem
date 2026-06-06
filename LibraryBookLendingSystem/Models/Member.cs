using System;

namespace LibraryBookLendingSystem.Models
{
    // Member inherits from Person - demonstrates INHERITANCE
    public class Member : Person
    {
        public DateTime MembershipDate { get; set; }
        public bool IsActive { get; set; }

        public Member(int id, string name, string email, string phone)
            : base(id, name, email, phone)
        {
            MembershipDate = DateTime.Now;
            IsActive = true;
        }

        // Override abstract method - demonstrates POLYMORPHISM
        public override string GetRole()
        {
            return "Member";
        }

        // Override virtual method - demonstrates POLYMORPHISM
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Role: Member | Active: {IsActive} | Joined: {MembershipDate.ToShortDateString()}";
        }
    }
}