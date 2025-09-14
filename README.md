Create, read, update, and delete patient notes.

Create
-----
POST /api/Notes

Read
-----
GET /api/Notes/{patientId}

Update
------
PUT /api/Notes/{id}

Delete
------
DELETE /api/Notes/{id}

Requires API key authorization via request headers.
  API KEY : 3EAHealth

Supports multi-tenancy using X-Tenant-Id header.

Swagger UI integration for testing endpoints. Should open browser automatically. If not --> localhost:7228/swagger/index.html

Testing Swagger
---------------
Run dotnet run

Go to https://localhost:5001/swagger

Use the Authorize button → enter 3EAHealth as API key

Add X-Tenant-Id where required

Execute requests directly from Swagger UI

Tech
-----
Visual Studio 2022
.NET 9.0
Entity framework 9.0.9
Sql Lite

**Set up**

git clone https://github.com/TarekAkkawi89/3EA_Health.git
or
download zip file



