# User Management API

This is the coding challenge from One, Inc.

## Instructions
We want you to develop an API that exposes one RESTful endpoint. This endpoint should 
provide standard CRUD functionality for Users.
 
1. You must use .NET Core 8 and C# 12
2. The API should be an ASP.NET Core Web API project
3. The API should consume and return data as JSON
4. You do not need to consider any security implications. This includes using HTTPS or attempting to provide any Authorization/Authentication.
5. You can use any persistent storage. SQL Server or in-memory DB are sufficient but should be treated as production data stores.
6. You can use any NuGet package, but be prepared to justify its usage.
7. Please add logging to all the operations. Log all exceptions, and avoid logging sensitive information, such as emails, in plain text.
8. Please make sure the code is testable.


## Specification 
 
### Endpoints 
Users
```
POST /api/users
GET /api/users
    /api/users/{id}
DELETE /api/users/{id}
PUT /api/users/{id}
```
 
### Business Rules

| Field Name | Data Type | Required | Validation |
|------------|-----------|----------|------------|
| FirstName | string | true | max length 128 |
| LastName | string | false | max length 128 |
| Email | string | true | Valid | email address |
| DateOfBirth | DateTime | true | Valid date |
| Phone Number | number | true | Valid | phone with ten digits |
 
- Every user must have a unique email address
- The user must be 18 years or older. Validate the date in creation and updates. 
- When returning one user or a list of users, add the user‘s age and date of birth. 
 
## How we will assess your work
We seek production-quality code that appropriately utilizes design patterns and adheres to 
established best practices and principles, such as SOLID.
Please push your work to a repository on GitHub or a similar platform, and when you're ready, 
share the link with our recruitment team.

