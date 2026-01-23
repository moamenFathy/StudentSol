# 🎓 Student Management System

A web-based **Student Management System** built with **ASP.NET Core MVC** and **Entity Framework Core**.  
The system helps manage students, courses, and enrollments with clean UI, validation, and filtering.

---

## 🔗 Live Demo

👉 **Live Application:**  
[(https://moamen.runasp.net/)](https://moamen.runasp.net/)

*(Hosted on MonsterASP)*

---

## 🛠️ Tech Stack

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **Razor Views**
- **Bootstrap** (for UI)
- **LINQ**

---

## ✨ Features

- 📋 Manage students (Create / Edit / Delete)
- 📚 Manage courses
- 🔗 Student–Course enrollment (Many-to-Many)
- 🔍 Search & filter enrollments
- ✅ Server-side & client-side validation
- 🧾 Validation summary for form errors
- 🎨 Clean and responsive UI
- ⚡ Optimized queries using `AsNoTracking`

---

## 🗂️ Project Structure

```text
├── Controllers
├── Models
├── ViewModels
├── Data
│   └── ApplicationDbContext
├── Views
│   ├── Students
│   ├── Courses
│   └── Enrollments
└── wwwroot
