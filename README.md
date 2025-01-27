AvaTradeApp
AvaTradeApp is a web application offering secure authentication, robust news management, and JWT-based session handling, tailored for financial and trading platforms.

Features
  Authentication: User registration and login with secure JWT tokens.
  News Management:
  Retrieve all news articles.
  Filter news by keywords, instruments, or specific days.
  Fetch the latest news for trading insights.
  Secure API: Endpoints are protected with JWT-based authentication.

Project Structure

src/
├── Application/    # DTOs, validation, and application logic
├── Domain/         # Core entities and domain logic
├── Infrastructure/ # Repositories, services, and database logic
├── WebApi/         # API controllers and configuration

Core Entities
  User: Registered users of the platform.
  News: Represents news articles, including publisher and keywords.
  Publisher: Entities publishing the news.
  RefreshToken: Manages JWT expiration.

API Endpoints
Authentication
/api/Auth/Login	--- POST
/api/Auth/Register --- POST

/api/News/GetAllNews --- GET
/api/News/GetAllNewsWithGivingDay ---	GET
/api/News/GetAllNewsPerInstrument ---	GET
/api/News/GetAllNewsPerInstrumentWithLimit --- GET
/api/News/GetLatestNews --- GET

How to Run
Prerequisites
.NET SDK
SQL Server (update connection string in appsettings.json)

Clone the repository.
Set up the database.
Build the solution.
Run the application.
Open Swagger in your browser.

For JWT token in swagger not need to type 'Bearer' . Just copy and paste JWT token. 

Technologies Used
  Framework: ASP.NET Core 9.0
  Authentication: JWT (JSON Web Tokens)
  ORM: Entity Framework Core
  Database: SQL Server
  Documentation: Swagger

