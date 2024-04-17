# User CRUD API

This is a simple CRUD API for managing users using ASP.NET Core MVC with Entity Framework Core.

## Setup

1. Clone or download this repository.
2. Update the database connection string in the `appsettings.json` file.
3. Run the database migrations to create the database schema.

## Endpoints

- `GET /api/users`: Get all users.
- `GET /api/users/{id}`: Get a user by ID.
- `POST /api/users`: Create a new user.
- `PUT /api/users/{id}`: Update an existing user.
- `DELETE /api/users/{id}`: Delete a user by ID.

## Usage

1. Compile and run the application.
2. Use an API testing tool like Postman to test the endpoints.

## Configuration

- The database connection string is specified in the `appsettings.json` file.
- Logging configuration is also available in the `appsettings.json` file.

## Dependencies

- Microsoft.AspNetCore.Mvc
- Microsoft.EntityFrameworkCore
- Microsoft.Extensions.Logging
- Npgsql.EntityFrameworkCore.PostgreSQL

## Notes

- Ensure that the database is running and accessible before running the application.
- This API does not include authentication and authorization. Implement them as needed for your application.
