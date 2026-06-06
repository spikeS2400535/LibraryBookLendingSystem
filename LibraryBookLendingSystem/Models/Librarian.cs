using System;

namespace LibraryBookLendingSystem.Models
{
    // Librarian inherits from Person - demonstrates INHERITANCE
    public class Librarian : Person
    {
        public string StaffId { get; set; }
        public string Department { get; set; }

        public Librarian(int id, string name, string email, string phone, string staffId, string department)
            : base(id, name, email, phone)
        {
            StaffId = staffId;
            Department = department;
        }

        // Override abstract method - demonstrates POLYMORPHISM
        public override string GetRole()
        {
            return "Librarian";
        }

        // Override virtual method - demonstrates POLYMORPHISM
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Role: Librarian | Staff ID: {StaffId} | Department: {Department}";
        }
    }
}