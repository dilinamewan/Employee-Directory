# Employee Directory - Software Developer Intern Assignment

**Assignment Title**: Employee Directory with Authentication  
**Deadline**: 7/22/2025  
**Developer**: Dilina Mewan

A secure employee directory web application built with ASP.NET Core MVC and Entity Framework Core, implementing authentication and CRUD operations on employee records.

## 📋 Assignment Requirements Coverage

### ✅ **User Authentication** (Required)
- ✅ ASP.NET Core Identity implementation
- ✅ Register page with validation
- ✅ Login page with validation  
- ✅ Logout functionality
- ✅ Redirect to employee list after login
- ✅ Access control for authenticated users only

### ✅ **Employee Management** (Required)
**Employee Model Fields:**
- ✅ EmployeeId (Primary Key, auto-generated) → `Id`
- ✅ FullName (string) → `FirstName` + `LastName`
- ✅ Email (string)
- ✅ Position (string)
- ✅ Department (string)
- ✅ Phone (string) → `PhoneNumber`
- ✅ HireDate (DateTime)

**CRUD Operations:**
- ✅ **Create**: Add new employee with validation
- ✅ **Read**: List all employees in table format + individual details view
- ✅ **Update**: Edit existing employee information
- ✅ **Delete**: Remove employee with confirmation

### ✅ **UI Requirements** (Required)
- ✅ Bootstrap styling implementation
- ✅ Navigation bar with login/logout links
- ✅ Employee list in professional table format
- ✅ Forms with proper input validation messages
- ✅ Logged-in user's information in navigation

### ✅ **Access Control** (Required)
- ✅ Protected employee management pages (login required)
- ✅ Non-authenticated users redirected to login/register
- ✅ Secure authentication flow

### 🎁 **Bonus Features Implemented**
- ✅ **Search functionality**: Filter employees by name or department
- ✅ **Professional Dashboard**: Statistics and quick actions
- ✅ **Responsive Design**: Mobile-friendly interface
- ✅ **Advanced Validation**: Client-side and server-side validation

## 🚀 Features

- **User Authentication**: Secure login and registration system
- **Employee Management**: Create, read, update, and delete employee records
- **Professional Dashboard**: Clean interface with key statistics and quick actions
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **Search Functionality**: Easily find employees by name or department
- **Data Validation**: Client-side and server-side validation for data integrity

## 🛠️ Technology Stack (Assignment Requirements)

- **Backend**: ASP.NET Core 8.0 MVC (.NET 6+ requirement ✅)
- **Database**: Entity Framework Core ✅
- **Authentication**: ASP.NET Core Identity ✅
- **Database Server**: Microsoft SQL Server ✅
- **Views**: Razor Views ✅
- **Styling**: Bootstrap 5 ✅
- **Additional**: FontAwesome icons, Custom CSS

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
Update the connection string in `appsettings.json` with your SQL Server Management Studio server name:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SQL_SERVER_NAME;Database=EmployeeDirectoryDb;Trusted_Connection=true;TrustServerCertificate=Yes"
  }
}
```

**Note**: Replace `YOUR_SQL_SERVER_NAME` with your actual SQL Server Management Studio server name (e.g., `DESKTOP-60VHJM3`, `localhost`, `.\SQLEXPRESS`, etc.).

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

## 🗃️ Database Schema (Assignment Compliance)

### Employee Table
- **Id** (Primary Key, auto-generated) ✅ *matches EmployeeId requirement*
- **FirstName + LastName** ✅ *implements FullName requirement*
- **Email** (string) ✅
- **Position** (string) ✅
- **Department** (string) ✅
- **PhoneNumber** (string) ✅ *matches Phone requirement*
- **HireDate** (DateTime) ✅

### AspNetUsers Table (Identity)
- User authentication and authorization data ✅

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

## 📝 Assignment Submission

**Repository**: [Employee-Directory](https://github.com/dilinamewan/Employee-Directory)  
**Submitted by**: Dilina Mewan  
**GitHub**: [@dilinamewan](https://github.com/dilinamewan)  
**Submission Date**: July 18, 2025

### 🎯 Assignment Status: **COMPLETE** ✅

All functional requirements have been implemented and tested:
- ✅ User Authentication System
- ✅ Employee CRUD Operations  
- ✅ UI with Bootstrap Styling
- ✅ Access Control Implementation
- ✅ Bonus: Search Functionality
- ✅ Additional Features: Professional Dashboard

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