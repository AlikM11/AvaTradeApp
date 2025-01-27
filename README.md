AvaTradeApp
AvaTradeApp is a web application that provides a robust news service, authentication, and JWT-based token management for financial and trading platforms.

Features
Authentication: User login and registration using JWT for secure session management.
News Management:
Retrieve all news.
Filter news by instruments or specific keywords.
Fetch news published within a given number of days.
Get the latest news for trading insights.
Secure API Endpoints: Endpoints are protected with JWT-based authentication.
Project Structure
The project is organized into the following layers:

bash
Copy
Edit
src/  
├── Application/  # Contains DTOs, validation rules, and other application logic.  
├── Domain/       # Core entities and domain logic.  
├── Infrastructure/  # Repositories, services, and database-related code.  
├── WebApi/       # API controllers and startup configuration.  
Entities
User: Represents a registered user.
News: Represents a news article with associated metadata like Publisher and Keywords.
Publisher: Represents the entity publishing the news.
RefreshToken: Used for managing JWT token expiration.
API Endpoints
Authentication
Endpoint	HTTP Method	Description
/api/Auth/Login	POST	Logs in a user.
/api/Auth/Register	POST	Registers a new user.
News
Endpoint	HTTP Method	Description
/api/News/GetAllNews	GET	Retrieves all news.
/api/News/GetAllNewsWithGivingDay	GET	Gets news from today - N days.
/api/News/GetAllNewsPerInstrumentWithLimit	GET	Gets news for a keyword, limited by N.
/api/News/GetAllNewsPerInstrument	GET	Gets news for a specific keyword.
/api/News/GetLatestNews	GET	Gets the latest news.
How to Run
Prerequisites
.NET SDK: Ensure you have the .NET SDK installed.
Database: A running SQL Server instance. Update the connection string in appsettings.json.
Steps
Clone the repository:

bash
Copy
Edit
git clone https://github.com/your-repo/avatradeapp.git  
cd avatradeapp  
Set up the database:

bash
Copy
Edit
dotnet ef database update  
Build the solution:

bash
Copy
Edit
dotnet build  
Run the application:

bash
Copy
Edit
dotnet run --project src/WebApi  
Access the application in your browser:

bash
Copy
Edit
http://localhost:5000/swagger  
Technologies Used
Framework: ASP.NET Core
Authentication: JWT (JSON Web Tokens)
ORM: Entity Framework Core
Database: SQL Server
Documentation: Swagger
Environment Configuration
Update the following in appsettings.json:

json
Copy
Edit
"ConnectionStrings": {  
  "DefaultConnection": "Server=your-server;Database=your-database;User Id=your-username;Password=your-password;"  
},  
"JWT": {  
  "Key": "your-secret-key",  
  "Issuer": "your-issuer",  
  "Audience": "your-audience",  
  "ExpiresInMinutes": 120  
}  
Testing
Run unit tests to ensure everything works correctly:

bash
Copy
Edit
dotnet test  
Contributing
Fork the repository.
Create a feature branch:
bash
Copy
Edit
git checkout -b feature-name  
Commit your changes:
bash
Copy
Edit
git commit -m "Description of changes"  
Push the branch:
bash
Copy
Edit
git push origin feature-name  
Open a pull request.
