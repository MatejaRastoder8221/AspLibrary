# Library Management System

## Overview

The Library Management System is a web application designed to manage library resources. It allows users to perform various operations such as adding, updating, and deleting categories, books, and other resources. The application also includes user authentication and authorization features.

## Features

- **User Authentication and Authorization**: Secure login and role-based access control.
- **Entity Management**: Add, update, delete, and retrieve entities.
- **Book Reservations: Reserve books for future borrowing.
- **Book Borrowing: Borrow books and track due dates.
- **Swagger Documentation**: Interactive API documentation using Swagger.
- **Error Handling**: Comprehensive error handling and validation.
- **JWT Authentication**: Secure API endpoints with JSON Web Tokens (JWT).

## Technologies Used

- **.NET 6**
- **Entity Framework Core**
- **ASP.NET Core Web API**
- **FluentValidation**
- **Swagger**
- **JWT**
- **SQL Server**

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/library-management-system.git
   cd library-management-system
   ```

2. **Set up the database**
   - Update the `appsettings.json` file with your SQL Server connection string.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=LibraryDb;User Id=your_user;Password=your_password;"
     },
     "AppSettings": {
       "Jwt": {
         "Issuer": "LibraryApp",
         "SecretKey": "your_secret_key",
         "Seconds": 3600
       }
     }
   }
   ```

   - Run the database migrations to create the database schema.

   ```bash
   dotnet ef database update
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

   The application will start on `http://localhost:5000` by default.

4. **Access Swagger UI**
   Open your browser and navigate to `http://localhost:5000/swagger` to explore the API endpoints.

### Usage

#### Authentication

- **Register a new user**: POST `/api/users/register`
- **Login**: POST `/api/users/login`

#### Categories

- **Get all categories**: GET `/api/categories`
- **Get a category by ID**: GET `/api/categories/{id}`
- **Create a new category**: POST `/api/categories` (requires authorization)
- **Delete a category**: DELETE `/api/categories/{id}`

#### Example Requests

- **Create a new category**

  ```http
  POST /api/categories
  Authorization: Bearer {your_jwt_token}
  Content-Type: application/json

  {
    "name": "Science Fiction",
    "description": "Books that are set in the future or in outer space."
  }
  ```

- **Get all categories**

  ```http
  GET /api/categories
  ```
## Similar workflow for most of the tables, check the swagger UI for instructions
.
.
.

## Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.

## Acknowledgements

- Thanks to the open-source community for providing various tools and libraries used in this project.

## Contact

For any questions or feedback, please open an issue or contact [mateja.r8@gmail.com](mailto:your_email@example.com).
