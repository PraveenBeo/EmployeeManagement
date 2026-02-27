# EmployeeManagement

Minimal .NET 8 microservices skeleton demonstrating Clean Architecture, Modular Monolith patterns, and inter-service messaging with RabbitMQ.

## Architecture

| Service | Pattern | Database |
|---------|---------|----------|
| **API Gateway** | YARP Reverse Proxy | N/A |
| **UserManagement API** | Clean Architecture (independent service) | `usermanagement_db` |
| **SystemManagement API** | Modular Monolith (3 modules) | `accesscard_db`, `leave_db`, `report_db` |
| **Web** | Razor Pages frontend | N/A |
| **RabbitMQ** | Message Broker | N/A |

### Traffic Flow

```
Browser → Web (:5000) → API Gateway (:5003) → UserManagement API (internal only)
                                              → SystemManagement API (internal only)
```

> **Note:** UserManagement API and SystemManagement API have no direct external access.
> All external traffic must route through the API Gateway on port 5003.

### Inter-Service Events (RabbitMQ + MassTransit)

```
UserManagement API ──[UserCreatedIntegrationEvent]──► RabbitMQ ──► SystemManagement API
                                                                    └─ AccessCard auto-created
```

When a new user is created via `POST /api/users`, the UserManagement service publishes a
`UserCreatedIntegrationEvent`. The SystemManagement service consumes this event and automatically
creates an AccessCard with card number `AC-{first8charsOfUserId}`.

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
| RabbitMQ Management UI | http://localhost:15672 (guest/guest) |

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

## Test Quick Start

After `docker compose up --build`, create a user via the gateway:

```bash
curl -X POST http://localhost:5003/api/users -H "Content-Type: application/json" -d '{"fullName":"Alice Smith","email":"alice@example.com"}'
```

This single command triggers the full event flow:
1. UserManagement API saves the user to `usermanagement_db`
2. A `UserCreatedIntegrationEvent` is published to RabbitMQ
3. SystemManagement API consumes the event and auto-creates an AccessCard in `accesscard_db`

Verify the auto-created access card:

```bash
curl http://localhost:5003/api/system/accesscards
```

Or visit:
- http://localhost:5000/Users to see the user list
- http://localhost:5000/System to see the auto-created access card
