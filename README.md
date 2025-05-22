# TaxApp

TaxApp is a simple .NET 6 application for managing city tax records by date range. It supports adding, editing, and retrieving tax rates. Unit tests are included to ensure the core logic is working as expected.


##  Features

-  Add tax rate for a city with start/end dates
-  Query tax rate by city and date
-  Update existing tax records
-  Simple in-memory + SQLite support
-  Unit testing with xUnit

---

##  Getting Started

### Requirements

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- IDE (like Visual Studio or VS Code)

---

##  Main Project Structure

```
TaxAppProject_Final/
│
├── TaxApp/              # Main project with tax logic
├── TaxApp.Tests/        # xUnit tests for validation
├── TaxApp.sln           # Solution file
```

---

##  How to Run the App

From the project root:

```bash
dotnet build       # Builds the app
dotnet run --project TaxApp   # Runs the main app
```

To run the tests:

```bash
dotnet test
```

---

###  If You Get Errors (like `.deps.json` or missing DLLs)

Sometimes the build gets corrupted. To clean and rebuild:

```bash
cd TaxApp.Tests
Remove-Item -Recurse -Force .\bin\
Remove-Item -Recurse -Force .\obj\
dotnet clean
dotnet restore
dotnet build
dotnet test
```

This usually fixes `*.deps.json` or `Microsoft.Data.Sqlite` not found errors.

---

##  Version History

### v1.0 -  Basic Query

- `GetTax(city, date)` function
- In-memory only

### v1.1 -  Edit & Update

- Added ability to modify existing tax entries

### v1.2 -  SQLite Integration

- Switched to using Microsoft.Data.Sqlite
- Database setup and dependency injection

### v1.3 -  Unit Testing Added

- Created test project using xUnit
- Covered major logic paths (add, get, edge cases)

---

##  Notes

- The app currently uses **in-memory** database in tests and can be extended to SQLite or SQL Server.
- If no tax is found for a city/date, the service may throw an exception (you can change this logic if needed).

---

##  Questions?

Feel free to open an issue or send feedback!
