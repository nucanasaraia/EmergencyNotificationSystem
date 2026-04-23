# 🚨 Emergency Notification & Response System

A **production-style backend API** built with **ASP.NET Core Web API** that helps emergency teams manage crisis events, notify nearby citizens, coordinate volunteers, and track critical resources in real time.

Designed to simulate how modern emergency management platforms operate during fires, floods, earthquakes, and other urgent situations.

---

# ✨ Key Features

- 🔐 JWT Authentication & Role-Based Authorization
- 🔄 Refresh Tokens via HttpOnly Cookies
- 📧 Email Verification & Password Reset
- 🚨 Emergency Event Lifecycle Management
- 📍 Geo-Based Notifications
- 🙋 Volunteer Assignment Workflow
- 📦 Resource Dispatch & Return Tracking
- ⚠️ Global Error Handling
- 📜 Structured Logging with Serilog
- 🧩 Clean Layered Architecture

---

# 💡 Why This Project Stands Out

This project goes beyond standard CRUD APIs by implementing **real-world coordination logic**:

* Notify only users inside an affected geographic radius
* Prevent unavailable volunteers from being assigned
* Prevent resources with active assignments from deletion
* Manage active/resolved emergency workflows
* Secure multi-role access for sensitive operations

It reflects practical backend engineering challenges involving **security, business logic, workflows, and scalability**.

---

# 👥 User Roles

## 🛡️ Admin

* Full system access
* Manage users and roles
* Delete emergency events
* Oversee platform operations

## 🚑 Emergency Service

* Create and manage emergency events
* Dispatch resources
* Assign volunteers

## 🙋 Volunteer

* Register as volunteer
* Set availability status
* Receive assignments
* Complete missions

## 🏠 Citizen

* Register account
* Receive nearby emergency alerts
* View personal notifications

---

# 🚨 Core Modules

## Emergency Events

Create and manage real-time incidents such as:

* Fire
* Flood
* Earthquake
* Landslide
* Medical Emergency
* Power Outage

Includes:

* Active / Resolved / Cancelled statuses
* Nearby search by coordinates
* Event updates

---

## 📍 Geo Notification System

When an emergency is created, the system automatically identifies users located within the affected radius and sends alerts.

Uses the **Haversine Formula** to calculate geographic distance between coordinates.

This simulates real emergency alert systems used in production environments.

---

## 🙋 Volunteer Coordination

The platform manages volunteer availability states:

* AVAILABLE
* UNAVAILABLE
* ON_MISSION

Only eligible volunteers can be assigned to emergencies.

---

## 📦 Resource Management

Track emergency resources such as:

* Vehicles
* Medical kits
* Rescue equipment
* Personnel units

Resources support:

* Assignment to active emergencies
* Return workflow
* Availability checks

---

# 🔐 Authentication Flow

A complete production-style authentication lifecycle:

1. Register account
2. Verify email address
3. Login and receive JWT access token
4. Refresh expired token via secure HttpOnly cookie
5. Password reset via secure email token

---

# 🛠️ Tech Stack

| Technology              | Purpose        |
| ----------------------- | -------------- |
| ASP.NET Core 8          | Backend API    |
| Entity Framework Core   | ORM            |
| SQL Server / PostgreSQL | Database       |
| JWT Bearer              | Authentication |
| AutoMapper              | DTO Mapping    |
| FluentValidation        | Validation     |
| Serilog                 | Logging        |
| Swagger / OpenAPI       | API Docs       |

---

# 🧠 Architecture Overview

The project follows a clean layered structure:

* **Controllers** → HTTP endpoints
* **Services** → Business logic
* **Data Layer** → EF Core database operations
* **DTOs** → Safe data transfer
* **Middleware** → Logging & exception handling

```text
Controllers
   ↓
Services
   ↓
Data Access
   ↓
Database
```

---

# ⚙️ Getting Started

## 1. Clone Repository

```bash
git clone https://github.com/yourusername/EmergencyNotificationSystem.git
cd EmergencyNotificationSystem
```

---

## 2. Configure Database

Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "your_connection_string_here"
}
```

---

## 3. Set User Secrets

```bash
dotnet user-secrets set "Jwt:Key" "your_super_secret_key"
dotnet user-secrets set "SMTP:SenderEmail" "your_email@gmail.com"
dotnet user-secrets set "SMTP:AppPassword" "your_app_password"
```

---

## 4. Apply Migrations

```bash
dotnet ef database update
```

---

## 5. Run Project

```bash
dotnet run
```

---

# 📬 API Documentation

After running locally:

```text
https://localhost:{port}/swagger
```

---

# 🌐 Live Demo

https://emergencynotif-dkaqbkebh0hbhuh5.westeurope-01.azurewebsites.net/swagger/index.html

Fully deployed on Azure App Service with Neon PostgreSQL database.
---

# 📸 Screenshots

## Swagger Overview

<img width="1800" height="957" alt="wholeEndpoints" src="https://github.com/user-attachments/assets/224d887c-8b48-469a-846e-2ac1e617c244" />

## Create Emergency Event

<img width="1370" height="459" alt="emev" src="https://github.com/user-attachments/assets/2feb6cab-056e-4f6f-a675-4fb6ef0a175e" />

## Notification Endpoints 

<img width="1799" height="485" alt="notif" src="https://github.com/user-attachments/assets/9f5b419b-1824-422f-a9e3-26ba26f3b4bf" />

## Resource Creation 

<img width="1742" height="449" alt="resource" src="https://github.com/user-attachments/assets/5124397f-7ec8-48dc-8beb-1edbc5ceac57" />

## Volunteer Assignment

<img width="1749" height="319" alt="val" src="https://github.com/user-attachments/assets/6439a595-16a8-4692-8458-8d600b86cfd3" />

---

# 🚀 Future Improvements

* Real-time alerts with SignalR
* Push notifications / SMS integration
* Background jobs with Hangfire
* Unit testing with xUnit + Moq
* Integration tests
* Admin analytics dashboard

---

# 📌 Project Status

* ✅ Fully Functional Backend API
* ✅ Secure Authentication System
* ✅ Multi-Role Authorization
* ✅ Real-World Workflow Logic
* ✅ Production-Style Architecture

---

# 💡 Final Notes

This project demonstrates practical backend engineering skills including:

* Secure authentication systems
* Real-world business rule implementation
* Geolocation-based logic
* Clean architecture principles
* Scalable API design

Built to reflect how serious backend systems are designed in production environments.
