# EmployeeManagement

Minimal .NET 8 microservices skeleton demonstrating Clean Architecture and Modular Monolith patterns.

## Architecture

| Service | Pattern | Database |
|---------|---------|----------|
| **API Gateway** | YARP Reverse Proxy | N/A |
| **UserManagement API** | Clean Architecture (independent service) | `usermanagement_db` |
| **SystemManagement API** | Modular Monolith (3 modules) | `accesscard_db`, `leave_db`, `report_db` |
| **Web** | Razor Pages frontend | N/A |

### Traffic Flow

```
Browser → Web (5000) → API Gateway (5003) → UserManagement API (5001)
                                           → SystemManagement API (5002)
```

## Prerequisites

- Docker & Docker Compose

## Run

```bash
docker compose up --build
```

## URLs

| Service | URL |
|---------|-----|
| Web (Home) | http://localhost:5000 |
| Web (Users page) | http://localhost:5000/Users |
| Web (System page) | http://localhost:5000/System |
| API Gateway Swagger (aggregated) | http://localhost:5003/swagger |
| API Gateway Health | http://localhost:5003/health |
| UserManagement API Swagger (direct) | http://localhost:5001/swagger |
| SystemManagement API Swagger (direct) | http://localhost:5002/swagger |

## API Endpoints

### API Gateway (port 5003)

The gateway proxies all requests to downstream services with path transforms:

```
GET  http://localhost:5003/health                    (aggregated health)
GET  http://localhost:5003/api/users                 → UserManagement GET /users
POST http://localhost:5003/api/users                 → UserManagement POST /users
GET  http://localhost:5003/api/system/accesscards    → SystemManagement GET /accesscards
POST http://localhost:5003/api/system/accesscards    → SystemManagement POST /accesscards
GET  http://localhost:5003/api/system/leaves         → SystemManagement GET /leaves
POST http://localhost:5003/api/system/leaves         → SystemManagement POST /leaves
GET  http://localhost:5003/api/system/reports        → SystemManagement GET /reports
POST http://localhost:5003/api/system/reports        → SystemManagement POST /reports
```

### UserManagement API (port 5001, direct access)

```
GET  http://localhost:5001/health
GET  http://localhost:5001/users
POST http://localhost:5001/users
     Body: { "fullName": "John Doe", "email": "john@example.com" }
```

### SystemManagement API (port 5002, direct access)

```
GET  http://localhost:5002/health
GET  http://localhost:5002/accesscards
POST http://localhost:5002/accesscards
     Body: { "cardNumber": "AC-001", "employeeName": "Jane Doe" }
GET  http://localhost:5002/leaves
POST http://localhost:5002/leaves
     Body: { "employeeName": "Jane Doe", "reason": "Vacation" }
GET  http://localhost:5002/reports
POST http://localhost:5002/reports
     Body: { "title": "Monthly Report", "description": "January summary" }
```

## Test Quick Start

After `docker compose up --build`, create sample data via the gateway:

```bash
curl -X POST http://localhost:5003/api/users -H "Content-Type: application/json" -d '{"fullName":"Alice Smith","email":"alice@example.com"}'

curl -X POST http://localhost:5003/api/system/accesscards -H "Content-Type: application/json" -d '{"cardNumber":"AC-100","employeeName":"Alice Smith"}'
```

Then visit:
- http://localhost:5000/Users to see the user list
- http://localhost:5000/System to see the access cards list
