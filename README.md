# AvaTradeApp

AvaTradeApp is a web application offering secure authentication, robust news management, and JWT-based session handling, tailored for financial and trading platforms.

---

## Features

- **Authentication**: User registration and login with secure JWT tokens.
- **News Management**:
  - Retrieve all news articles.
  - Filter news by keywords, instruments, or specific days.
  - Fetch the latest news for trading insights.
- **Secure API**: Endpoints are protected with JWT-based authentication.

---

---

## Core Entities

- **User**: Registered users of the platform.
- **News**: Represents news articles, including publisher and keywords.
- **Publisher**: Entities publishing the news.
- **RefreshToken**: Manages JWT expiration.

---

## API Endpoints

### **Authentication**
- `POST /api/Auth/Login`  
- `POST /api/Auth/Register`  

### **News**
- `GET /api/News/GetAllNews`
- `GET /api/News/GetAllNewsWithGivingDay`
- `GET /api/News/GetAllNewsPerInstrument`
- `GET /api/News/GetAllNewsPerInstrumentWithLimit`
- `GET /api/News/GetLatestNews`

---

## How to Run

### Prerequisites
- .NET SDK  
- SQL Server (update the connection string in `appsettings.json`)

### Steps
1. Clone the repository.
2. Set up the database.
3. Build the solution.
4. Run the application.
5. Open Swagger in your browser.

### Notes
For JWT tokens in Swagger, **do not include "Bearer"**. Just copy and paste the JWT token.

---

## Technologies Used

- **Framework**: ASP.NET Core 9.0  
- **Authentication**: JWT (JSON Web Tokens)  
- **ORM**: Entity Framework Core  
- **Database**: SQL Server  
- **Documentation**: Swagger  
