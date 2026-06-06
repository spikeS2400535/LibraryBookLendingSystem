using System;

namespace LibraryBookLendingSystem.Models
{
    // Abstract base class for library items - demonstrates ABSTRACTION
    public abstract class LibraryItem
    {
        private string _title;
        private string _author;

        public int Id { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty.");
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be empty.");
                _author = value;
            }
        }

        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public int Year { get; set; }

        // Constructor
        public LibraryItem(int id, string title, string author, string isbn, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            Year = year;
            IsAvailable = true;
        }

        // Abstract method - subclasses must implement
        public abstract string GetItemType();

        // Virtual method - can be overridden
        public virtual string GetInfo()
        {
            return $"ID: {Id} | {GetItemType()} | Title: {Title} | Author: {Author} | ISBN: {ISBN} | Year: {Year} | Available: {IsAvailable}";
        }
    }
}