# 🍔 Food Ordering API – Backend

A robust and secure **RESTful API** built with .NET 9, Entity Framework Core, and SQL.
This API powers the food ordering application, handling authentication, business logic, and data persistence.

---

## 🚀 Features

* 🔐 Authentication with Microsoft Identity + JWT
* 👤 Role-based Authorization
* 📦 RESTful API architecture
* 🗄️ Entity Framework Core with SQL database
* 🐳 Dockerized database setup (MacOS)
* 🧩 Clean architecture & separation of concerns
* ⚠️ Global error handling

---

## 🛠️ Tech Stack

* **.NET 9 (ASP.NET Core Web API)**
* **Entity Framework Core**
* **SQL Server**
* **Microsoft Identity**
* **JWT Authentication**
* **Docker**

---

## 📂 Project Structure

```
/Controllers        # API endpoints
/Services           # Business logic
/Repositories       # Data access layer
/Models             # Domain models
/DTOs               # Data transfer objects
/Data               # DbContext & configurations
```

---

## ⚙️ Setup & Installation

```bash
# Clone the repository
git clone https://github.com/AwaisVU/GrillFusion_API.git

cd grillfusion_api

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

---

## 🐳 Database Setup (Docker)

Make sure Docker is running:

```bash
docker-compose up -d
```

This will spin up your SQL Server instance.

---

## 🔐 Authentication

* Uses **Microsoft Identity**
* JWT-based authentication
* Secure endpoints with role-based access control

---

## 🔑 Environment Configuration

Update `appsettings.json`:

NOTE: For secret keys and env variables, you may contact me at following! 

---

## 📬 API Endpoints (Sample)

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| POST   | /api/auth/login    | User login        |
| POST   | /api/auth/register | User registration |
| GET    | /api/products      | Get all products  |
| POST   | /api/orders        | Create new order  |

---

## 🚧 Upcoming Features

* 📧 Email service for password reset
* 🔄 Refresh tokens
* 📊 Logging & monitoring improvements

---

## 🔗 Frontend Repository

👉 https://github.com/AwaisVU/GrillFusion_React

---

## 🤝 Contributing

Pull requests are welcome!

---

## ⭐ Support

If you found this useful, give it a ⭐ on GitHub!
