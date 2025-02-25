# Invoice System API

The Invoice System API is a .NET-based web application designed to manage companies, users, and invoices. It provides a RESTful API for creating, retrieving, and managing these entities. The system is built using a layered architecture, including an API layer, application layer, domain layer, and infrastructure layer. It also includes unit tests to ensure the reliability of the codebase.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Company Endpoints](#company-endpoints)
  - [Invoice Endpoints](#invoice-endpoints)
  - [User Endpoints](#user-endpoints)
- [Testing](#testing)
- [Docker Support](#docker-support)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Company Management**: Create and retrieve companies.
- **Invoice Management**: Create and retrieve invoices, including filtering by issuer and counterparty companies.
- **User Management**: Create and retrieve users, including user authentication.
- **Validation**: Input validation using FluentValidation.
- **Pagination**: Support for paginated results when retrieving lists of companies or users.
- **Docker Support**: Easily deploy the application using Docker.

## Technologies

- **.NET 9.0**: The application is built using the latest .NET framework.
- **Entity Framework Core**: Used for database interactions.
- **PostgreSQL**: The database used for storing application data.
- **AutoMapper**: For mapping between DTOs and domain entities.
- **FluentValidation**: For validating input data.
- **Swagger**: API documentation and testing.
- **Docker**: Containerization for easy deployment.

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/) (optional, if not using Docker)

### Installation

1. Clone the repository:
   ```bash
   git clone [https://github.com/your-repo/invoice-system-api.git](https://github.com/valthial/InvoiceSystem.git)
   cd InvoiceSystem
2. Restore the dependencies:
   ```bash
    dotnet restore
   ```
3. Set up the database:
  - If using Docker, the database will be automatically set up when you run the application.
  - If not using Docker, ensure you have PostgreSQL installed and update the connection string in appsettings.json.

### Running the Application
1. Using Docker:
   ```bash
    docker-compose up --build
   ```
   The API will be available at http://localhost:5000 and https://localhost:5001.
2. Without Docker:
   ```bash
   dotnet run --project InvoiceSystem.Api
   ```
   The API will be available at http://localhost:5000 and https://localhost:5001.
### API Endpoints
Company Endpoints

  - Create Company: POST /api/companies/create
  - Get Company by ID: GET /api/companies/{id}
  - Get All Companies: GET /api/companies/getAll

Invoice Endpoints

  - Create Invoice: POST /api/invoices/create
  - Get Invoice by ID: GET /api/invoices/{id}
  - Get Sent Invoices: GET /api/invoices/sent?companyId={companyId}
  - Get Received Invoices: GET /api/invoices/received?companyId={companyId}

User Endpoints

  - Create User: POST /api/users
  - Validate User Credentials: POST /api/users/validate
  - Get User by Email: GET /api/users/{email}
  - Get All Users: GET /api/users

### Testing
  The project includes unit tests for the API controllers and services. To run the tests, use the following command:
    ```bash
    dotnet test
    ```
### Docker Support
The application is Dockerized, making it easy to deploy in any environment that supports Docker. The docker-compose.yml file includes configurations for both the API and the PostgreSQL database.

 
