# LoanAPI

LoanAPI is a .NET 8 web API for managing user authentication, authorization, and loan data. This project demonstrates the use of ASP.NET Core, Entity Framework Core, and JWT for secure user management.

## Features

- User Registration
- User Login with JWT Authentication
- Role-based Authorization
- Loan Management

## Technologies Used

- .NET 8
- Entity Framework Core
- JWT (JSON Web Tokens)
- BCrypt for password hashing
- FluentValidation for validation

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or any other compatible database

### Installation

1. Clone the repository:

   ```
   git clone https://github.com/yourusername/LoanAPI.git

   cd LoanAPI
   ```
2. Update the database connection string in `appsettings.json`:
    ```
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=your_server;Database=LoanAPI;User Id=your_user;Password=your_password;"
        },
        "Jwt": {
            "Key": "YourSuperSecretKey",
            "Issuer": "YourIssuer",
            "Audience": "YourAudience"
        }
    }
    ```
3. Apply migrations and update the database:
    ```
    dotnet ef database update
    ```
4. Run the application:
    ```
    dotnet run
    ```

### Usage

#### Register a new user

- **Endpoint**: `POST /api/auth/register`
- **Request Body**:
    ```
    {
        "FirstName": "John",
        "LastName": "Doe",
        "Username": "johndoe",
        "Email": "johndoe@example.com",
        "Age": 30,
        "MonthlyIncome": 5000,
        "Password": "Password123!"
    }
    ```   

#### Login

- **Endpoint**: `POST /api/auth/login`
- **Request Body**:
    ```
    {
        "Username": "johndoe",
        "Password": "Password123!"
    }
    ```   

- **Response**:
    ```
    {
        "Token": "your_jwt_token"
    }
    ```

#### Get All Loans (Accountant Role Required)

- **Endpoint**: `GET /api/loans/all`
- **Headers**:
    ```
    Authorization: Bearer your_jwt_token
    ```

## Project Structure

- **Controllers**: Contains API controllers for handling HTTP requests.
- **Services**: Contains business logic and service classes.
- **Models**: Contains data models and entities.
- **DTOs**: Contains Data Transfer Objects for request and response models.
- **Helpers**: Contains helper classes, such as JWT token generation.
- **Validators**: Contains User and Loan validation.
- **Migrations**: Contains Entity configurations.
