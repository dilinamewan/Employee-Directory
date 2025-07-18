# Employee Directory

A modern employee management system built with ASP.NET Core MVC and Entity Framework Core. This application provides a clean and intuitive interface for managing employee records with authentication and authorization features.

## Features

- **User Authentication & Authorization**: Secure login/registration system using ASP.NET Core Identity
- **Employee Management**: Create, read, update, and delete employee records
- **Search & Pagination**: Advanced search functionality with paginated results
- **Dashboard Statistics**: Real-time employee count and department overview
- **Responsive Design**: Mobile-friendly interface using Bootstrap
- **Data Validation**: Comprehensive client and server-side validation
- **Professional UI**: Modern design with Font Awesome icons

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Icons**: Font Awesome
- **Fonts**: Google Fonts (Inter)

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full installation)
- Visual Studio 2022 or VS Code
- Git

## Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/dilinamewan/Employee-Directory.git
   cd Employee-Directory
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   
   Edit `appsettings.json` and update the connection string if needed:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=EmployeeDirectoryDb;Trusted_Connection=true;TrustServerCertificate=Yes"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   
   Open your browser and navigate to: `https://localhost:5001`

## Usage

### Getting Started

1. **Register a new account** or **login** with existing credentials
2. **Dashboard** shows employee statistics and recent hires
3. **Browse employees** to view all employee records
4. **Add new employees** using the create form
5. **Edit or delete** existing employee records

### Employee Fields

- **Full Name**: Employee's complete name (required)
- **Email**: Valid email address (required, unique)
- **Position**: Job title or role (required)
- **Department**: Department or division (required)
- **Phone**: 10-digit phone number (required)
- **Hire Date**: Date when employee was hired (required)

### Search & Filter

- Search employees by name or department
- Results are paginated for better performance
- Real-time search feedback

## Project Structure

```
Employee Directory/
├── Controllers/           # MVC Controllers
│   ├── AccountController.cs
│   ├── EmployeeController.cs
│   └── HomeController.cs
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Models/               # Data Models & ViewModels
│   ├── Employee.cs
│   ├── ApplicationUser.cs
│   └── ViewModels/
├── Views/                # Razor Views
│   ├── Account/
│   ├── Employee/
│   ├── Home/
│   └── Shared/
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── Migrations/           # EF Core Migrations
└── Program.cs           # Application Entry Point
```

## Sample Data

The application automatically seeds sample employee data on first run:
- Ajith (Software Engineer, IT Department)
- Mewan (Project Manager, IT Department)

## Security Features

- Password requirements: minimum 6 characters with at least one digit
- Anti-forgery token protection
- Authorization required for employee management
- Secure cookie configuration
- SQL injection protection via Entity Framework

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/dilinamewan/Employee-Directory/issues) on GitHub.

## Acknowledgments

- ASP.NET Core team for the excellent framework
- Bootstrap team for the responsive CSS framework
- Font Awesome for the beautiful icons
- Contributors and the open-source community