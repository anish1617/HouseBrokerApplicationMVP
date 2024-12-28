# House Broker Application Setup Guide

This document outlines the steps to set up and run the HouseBrokerApplicationMVP project.

### Technology Used:

.NET 8, MSSQL, ORM(EF Core 8.0)

## Prerequisites

- Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed (version 8.0.0).
- SQL Server installed and running.

## Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/anish1617/HouseBrokerApplicationMVP.git
   ```

2. **Navigate to the Project Directory**

   ```bash
   cd HouseBrokerApplicationMVP
   ```

3. **Restore Dependencies**

   ```bash
   dotnet restore
   ```

4. **Build the Project**

   ```bash
   dotnet build
   ```

5. **Update Database Connection Settings**
   - Open the `appSettings.json` file located in the `HouseBrokerApplication.WebApi` project.
   - Update the `DefaultConnection` string under `ConnectionStrings` to match your SQL Server configuration:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=yourlocalserver;Database=HouseBrokerApplicationDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

## Database Setup

### Option 1: Using Powershell

6. **Install EF Core Tool (if not already installed)**

   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.0
   ```

7. **Navigate to the Project Folder**

   ```bash
   cd .\HouseBrokerApplication\
   ```

8. **Add Initial Migration**

   ```bash
   dotnet ef migrations add InitialMigration -p .\HouseBrokerApplication.Infrastructure -s .\HouseBrokerApplication.WebApi\
   ```

9. **Update the Database**
   ```bash
   dotnet ef database update -p .\HouseBrokerApplication.Infrastructure -s .\HouseBrokerApplication.WebApi
   ```

### Option 2: Using Package Manager Console in Visual Studio

**Add Initial Migration**

```bash
Add-Migration InitialMigration -Project         \HouseBrokerApplication.Infrastructure -StartupProject  \HouseBrokerApplication.WebApi
```

**Update The Database**

```bash
Update-Database -Project HouseBrokerApplication.Infrastructure -StartupProject HouseBrokerApplication.WebApi
```

4. **To Run the Unit Test**
   ```bash
    dotnet test .\HouseBrokerApplication.Tests\
   ```

## Run the Application

10. **Start the Project**
    ```bash
    dotnet run --project .\HouseBrokerApplication.WebApi\
    ```

The application should now be running. Access it using the configured endpoint.

## Troubleshooting

- **Error: Unable to connect to the database**

  - Ensure SQL Server is running and the connection string in `appSettings.json` is correctly configured.

- **Command 'dotnet-ef' not found**

  - Ensure the EF Core tools are installed globally using:
    ```bash
    dotnet tool install --global dotnet-ef --version 8.0.0
    ```
  - Verify the installation with:
    ```bash
    dotnet ef --version
    ```

- **Migration Issues**

  - If migrations fail, ensure the correct paths for the `Infrastructure` and `WebApi` projects are specified.
  - Clean and rebuild the project using:
    ```bash
    dotnet clean
    dotnet build
    ```

- **Also make sure dotnet sdk 8.0 is installed**

