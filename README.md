# eBook Management System

## Project Overview

The eBook Management System is a comprehensive web application built with ASP.NET MVC that allows organizations to manage their book inventory efficiently. This system enables librarians or administrators to track books, manage borrowers, and maintain the status of each book in the collection.

## Features

- **Book Management**
  - Add new books to the collection
  - Update existing book information
  - Remove books from the collection
  - View detailed information about each book

- **Search Functionality**
  - Search books by name
  - Filter by book category
  - Filter by borrower
  - Filter by status (available, borrowed, etc.)

- **Book Information Tracking**
  - Book ID and title
  - Author information
  - Publisher details
  - Purchase date
  - Content summary
  - Borrowing status
  - Current borrower

## System Architecture

The application follows a layered architecture pattern:

- **eBook (Presentation Layer)**
  - MVC controllers and views
  - User interface components
  - Client-side functionality

- **eBook.Service (Business Logic Layer)**
  - Business rules and workflows
  - Service interfaces and implementations

- **eBook.Dao (Data Access Layer)**
  - Database communication
  - CRUD operations

- **eBook.Model (Domain Model)**
  - Entity classes
  - Data transfer objects

- **eBook.Common (Utility Layer)**
  - Shared functionality
  - Logging
  - Helper methods

- **eBookTest (Testing Layer)**
  - Unit tests
  - Integration tests

## Technologies Used

- **Backend**:
  - ASP.NET MVC Framework
  - C# Programming Language
  - Entity Framework (for data access)

- **Frontend**:
  - HTML5, CSS3
  - JavaScript/jQuery
  - Bootstrap (for responsive design)

- **Database**:
  - Microsoft SQL Server

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.5 or later
- SQL Server 2012 or later

### Installation

1. Clone the repository
2. Open the solution file `eBook.sln` in Visual Studio
3. Restore NuGet packages
4. Update the database connection string in the web.config file
5. Build the solution
6. Run the application

### Database Setup

1. Create a new database in SQL Server
2. Execute the SQL scripts located in the database scripts folder
3. Update the connection string in the application configuration

## Usage

### Book Management

1. **View Books**: Navigate to the home page to see all books
2. **Add a Book**: Click on "Add New Book" and fill in the required information
3. **Edit a Book**: Click on the edit button next to a book entry
4. **Delete a Book**: Click on the delete button and confirm your action

### Search and Filter

1. Use the search box to find books by title
2. Use dropdown filters to narrow results by category, borrower, or status

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Thanks to all contributors who have helped improve this system
- Special thanks to the development team for their dedication
