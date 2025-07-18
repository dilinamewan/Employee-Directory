# Employee Directory

A modern, professional web application for managing employee information built with ASP.NET Core MVC.

## 🚀 Features

- **User Authentication**: Secure login and registration system
- **Employee Management**: Create, read, update, and delete employee records
- **Professional Dashboard**: Clean interface with key statistics and quick actions
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **Search Functionality**: Easily find employees by name or department
- **Data Validation**: Client-side and server-side validation for data integrity

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: Entity Framework Core with SQL Server
- **Frontend**: Bootstrap 5, FontAwesome icons, Custom CSS
- **Authentication**: ASP.NET Core Identity
- **Languages**: C#, HTML, CSS, JavaScript

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/dilinamewan/Employee-Directory.git
cd Employee-Directory
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Update Database Connection String
Update the connection string in `appsettings.json` if needed:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeDirectoryDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 4. Apply Database Migrations
```bash
dotnet ef database update
```

### 5. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:7XXX` or `http://localhost:5XXX`.

## 📁 Project Structure

```
Employee Directory/
├── Controllers/           # MVC Controllers
│   ├── AccountController.cs
│   ├── EmployeeController.cs
│   └── HomeController.cs
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Models/               # Data Models and ViewModels
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
└── Migrations/           # EF Core Migrations
```

## 💡 Usage

### Dashboard
- View total employee and department statistics
- Quick access to common actions (Add Employee, View All, Search)

### Employee Management
- **Add Employee**: Create new employee records with all necessary details
- **View Employees**: Browse all employees in a clean, organized list
- **Edit Employee**: Update existing employee information
- **Delete Employee**: Remove employee records (with confirmation)
- **Search**: Find employees quickly using the search functionality

### User Authentication
- **Register**: Create new user accounts
- **Login**: Secure access to the application
- **Logout**: Safe session termination

## 🎨 UI Features

- **Modern Design**: Clean, professional interface
- **Responsive Layout**: Optimized for all screen sizes
- **Interactive Elements**: Hover effects and smooth transitions
- **Validation Feedback**: Real-time form validation with helpful error messages
- **Loading States**: Visual feedback during form submissions

## 🗃️ Database Schema

### Employee Table
- Id (Primary Key)
- FirstName
- LastName
- Email
- PhoneNumber
- Department
- Position
- HireDate

### AspNetUsers Table (Identity)
- User authentication and authorization data

## 🔧 Configuration

### Development Environment
- Uses SQL Server LocalDB by default
- Development-specific settings in `appsettings.Development.json`
- Detailed error pages enabled

### Production Deployment
- Update connection strings for production database
- Configure appropriate logging levels
- Enable HTTPS enforcement
- Set up proper error handling

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Dilina Mewan**
- GitHub: [@dilinamewan](https://github.com/dilinamewan)

## 🐛 Known Issues

- None currently reported

## 📞 Support

If you encounter any issues or have questions, please:
1. Check the existing issues on GitHub
2. Create a new issue with detailed information
3. Contact the maintainer

## 🔄 Version History

- **v1.0.0** - Initial release with core functionality
  - User authentication system
  - Employee CRUD operations
  - Professional dashboard UI
  - Responsive design

---

⭐ If you find this project helpful, please give it a star on GitHub!