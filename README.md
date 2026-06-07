# Library Book Lending System

## Project Description
A Windows Forms desktop application built with C# and .NET Framework that manages a library's book lending operations. The system allows librarians to manage books, register members, process loans and track overdue books.

## Developer
- **Name:** Rupak Balami
- **GitHub:** spikeS2400535
- **Unit:** ITS203 Object-Oriented Design and Programming
- **Assessment:** B - Individual Assignment

## Features
- Book Management - Add, delete, view and search books
- Member Management - Register and manage library members
- Loan Management - Borrow and return books with due dates
- Overdue Tracking - View all overdue loans with days overdue
- Dashboard - Overview of library statistics

## OOP Principles Demonstrated
- **Abstraction** - Abstract classes Person and LibraryItem, and interface IReportable
- **Inheritance** - Member and Librarian inherit from Person, Book and Magazine inherit from LibraryItem
- **Polymorphism** - GetRole(), GetItemType() and GetInfo() are overridden in subclasses
- **Encapsulation** - Private fields with public properties and validation in Person and LibraryItem
- **Exception Handling** - Try-catch blocks used throughout all forms and the Library class

## How to Run
1. Clone this repository
2. Open LibraryBookLendingSystem.sln in Visual Studio 2022
3. Press F5 or click Start to run the application
4. No additional setup required

## References and Tools Used
- Microsoft Learn - Windows Forms: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/
- Microsoft Learn - C# OOP: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop
- Microsoft Learn - Exception Handling: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/
- Claude AI (Anthropic) - Used for guidance, code structure suggestions and debugging support during development
