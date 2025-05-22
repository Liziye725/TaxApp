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


### v1.4 - Testing Improvements & Bug Fixes
- Fixed test data contamination: Changed `GetService()` method to use a unique in-memory database name per test by using the caller member name, avoiding cross-test interference.
- Modified `GetTax` to return `null` instead of throwing exception when no matching tax record is found. This improves API usability and makes tests simpler.
- Added test `GetTax_ReturnsNull_WhenNoMatchFound` to verify correct behavior for non-existing data.
---


## Known Issues and Solutions

- **.NET SDK Version Mismatch**  
  If you have multiple .NET SDK versions installed, builds can fail or behave inconsistently.  
  **Solution:** Add a `global.json` file to the project root specifying .NET 6 SDK version to ensure consistent builds.

- **SQLite Issues in Tests (DLLs or `.deps.json` Errors)**  
  Running tests with SQLite sometimes causes missing DLL errors or corrupted build outputs.  
  **Solution:** For testing, switch to using only the in-memory database provider instead of SQLite to avoid these problems and speed up tests.

- **In-memory Database Data Contamination Between Tests**  
  Using the same in-memory database name in multiple tests caused data to leak across tests, causing flaky results.  
  **Solution:** Modify `GetService()` to use unique database names per test (using `[CallerMemberName]`), isolating test data.

- **`GetTax` Throws Exception When No Data Found**  
  Originally, querying a tax rate for a non-existent city or date threw exceptions, making client code and tests complex.  
  **Solution:** Change `GetTax` to return `null` if no matching tax is found, improving usability and simplifying tests.

---

##  Notes

- The app currently uses **in-memory** database for unit tests to enable fast, isolated tests.
- Production or integration scenarios can use SQLite or SQL Server with proper connection strings.
- Returning `null` for no matching tax makes client code cleaner and avoids unexpected exceptions.
- Using unique in-memory DB per test prevents flaky tests caused by data contamination.


---

##  Questions?

Feel free to open an issue or send feedback!
