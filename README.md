# Employee Management System

## Description
The Employee Management System is a solution for managing employee information. This project uses ASP.NET Core Web API and mssql Server for the backend and integrates Serilog for logging.
Stored procedures and API endpoints for managing employee data.

## Features
- Add, update, and retrieve employee addresses
- Get a list of employees reporting to a manager
- Retrieve the manager for a specific employee
- Exception handling with middleware and Serilog logging

## Technologies Used
- ASP.NET Core 9
- Entity Framework Core
- Serilog
- SQL Server

- ## Installation

## 1. **Clone the Repository**
   
    git clone https://github.com/kchoudhari127/Employee-Management.git
	
    cd Employee-Management\src\Employee-Management.API

## 2. Install Dependencies
   dotnet restore
  
## 3. Update Database Make sure your SQL Server instance is running and update the connection string in appsettings.json.

    {
         "ConnectionStrings": {
         "DefaultConnection": "data source=your_server_name;catalog=your_database_name;integrated security=true;encrypt=false"
        }
   }

## 4. Run Migrations

    dotnet ef database update
   
## 5. Run the Application

   dotnet run
