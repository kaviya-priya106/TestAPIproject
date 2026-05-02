# Employee Management Web API

## 📌 Overview
This project is a RESTful Web API built using ASP.NET Core for managing employee data. It supports CRUD operations, authentication, and efficient data handling.

---

## 🚀 Features
- CRUD operations for employees
- JWT Authentication & Authorization
- Global Exception Handling (Middleware)
- Pagination for large datasets
- Optimized queries using AsNoTracking()
- Layered architecture (Controller → Service → Repository)

---

## 🛠 Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

---

## 📂 Project Structure
- Controllers → Handles HTTP requests
- Services → Business logic
- Repositories → Data access layer
- Models → Data models
- Middleware → Global exception handling

---

## 🔐 Authentication
Uses JWT token-based authentication.  
Include token in header:

Authorization: Bearer <token>

---

## 📌 Sample APIs

### Get All Employees
GET /api/employees?pageNumber=1&pageSize=10

### Get Employee by Id
GET /api/employees/{id}

### Create Employee
POST /api/employees

### Update Employee
PUT /api/employees/{id}

### Delete Employee
DELETE /api/employees/{id}

---

## ⚡ How to Run
1. Clone the repository
2. Update connection string in appsettings.json
3. Run the project
4. Use Swagger to test APIs

---

## 🗄️ Database Setup

1. Update connection string in appsettings.json
2. Run the following commands:

dotnet ef migrations add InitialCreate
dotnet ef database update

---

## 👩‍💻 Author
Kaviya Priya
