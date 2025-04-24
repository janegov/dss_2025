# Todo List Application

A web-based Todo list application with user authentication and management capabilities.

## Features

- User registration and authentication
- JWT-based authentication
- Todo list management
- Task management within todo lists
- RESTful API endpoints
- Swagger documentation

## Prerequisites

- .NET 6.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or Visual Studio Code

## Setup

1. Clone the repository
2. Update the connection string in `appsettings.json` if needed
3. Run the following commands in the project directory:

```bash
dotnet restore
dotnet build
dotnet run
```

## API Documentation

Once the application is running, you can access the Swagger documentation at:
```
https://localhost:5001/swagger
```

## API Endpoints

### Authentication

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token

### Todo Lists

- `GET /api/todo` - Get all todo lists for the current user
- `GET /api/todo/{id}` - Get a specific todo list
- `POST /api/todo` - Create a new todo list
- `PUT /api/todo/{id}` - Update a todo list
- `DELETE /api/todo/{id}` - Delete a todo list

### Tasks

- `POST /api/todo/{todoId}/tasks` - Add a task to a todo list
- `PUT /api/todo/{todoId}/tasks/{taskId}` - Update a task
- `DELETE /api/todo/{todoId}/tasks/{taskId}` - Delete a task

## Security

- JWT authentication is required for all endpoints except registration and login
- Passwords are hashed using SHA256 with salt
- HTTPS is enforced in production

## Architecture

The application follows a clean architecture pattern with the following layers:

- Domain: Core business entities and interfaces
- Application: Business logic
- Infrastructure: Data access and external services
- Web.Api: API endpoints and presentation

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.
