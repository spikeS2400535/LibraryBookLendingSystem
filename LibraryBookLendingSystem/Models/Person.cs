using System;

namespace LibraryBookLendingSystem.Models
{
    // Abstract base class - demonstrates ABSTRACTION and ENCAPSULATION
    public abstract class Person
    {
        // Private fields - ENCAPSULATION
        private string _name;
        private string _email;
        private string _phone;

        // Properties - controlled access to private fields
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty.");
                _email = value;
            }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public int Id { get; set; }

        // Constructor
        public Person(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        // Abstract method - must be implemented by subclasses (POLYMORPHISM)
        public abstract string GetRole();

        // Virtual method - can be overridden
        public virtual string GetInfo()
        {
            return $"ID: {Id} | Name: {Name} | Email: {Email} | Phone: {Phone}";
        }
    }
}
