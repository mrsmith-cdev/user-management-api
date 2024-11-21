# **User Management API**

This is a simple **User Management API** built with **ASP.NET Core**. It provides endpoints to perform CRUD operations on users, including creating, updating, retrieving, and deleting user data.

---

## **Features**
- Create new users.
- Update existing user details.
- Retrieve a list of users or specific user details.
- Delete a user by their ID.

---

## **Endpoints**

### **Base URL:** `/api/users`

| **HTTP Method** | **Endpoint**     | **Description**               |
|------------------|------------------|-------------------------------|
| `POST`          | `/`              | Create a new user.            |
| `PUT`           | `/`              | Update an existing user.      |
| `GET`           | `/search`        | Retrieve a list of users.     |
| `GET`           | `/{id}`          | Retrieve details of a user.   |
| `DELETE`        | `/{id}`          | Delete a user by ID.          |

---

## **Setup Instructions**

### **Prerequisites**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or any other supported database configured in the app)
- Visual Studio or any C# IDE (optional for development)

### **Steps to Run the Application**
1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
2. **dotnet restore**
3. **Add Json for App Config and connection string**
4. **dotnet run**