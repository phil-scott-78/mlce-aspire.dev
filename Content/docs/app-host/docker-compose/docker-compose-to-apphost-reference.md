---
title: Docker Compose to Aspire AppHost
description: Quick reference for converting Docker Compose YAML syntax to Aspire AppHost API calls.
order: 54
---



This reference provides systematic mappings from Docker Compose YAML syntax to equivalent Aspire AppHost API calls. Use these tables as a quick reference when converting your existing Docker Compose files to Aspire application host configurations.

## Service definitions

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `services:` | `var builder = DistributedApplication.CreateBuilder(args)` | Root application builder used for adding and representing resources |
| `service_name:` | `builder.Add*("service_name")` | Service name becomes resource name |

## Images and builds

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `image: nginx:latest` | `builder.AddContainer("name", "nginx", "latest")` | Direct image reference |
| `build: .` | `builder.AddDockerfile("name", ".")` | Build from Dockerfile |
| `build: ./path` | `builder.AddDockerfile("name", "./path")` | Build from specific path |
| `build.context: ./app` | `builder.AddDockerfile("name", "./app")` | Build context |
| `build.dockerfile: Custom.dockerfile` | `builder.Add*("name").WithDockerfile("Custom.dockerfile")` | Custom Dockerfile name |

## Pull policy

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `pull_policy: always` | `.WithImagePullPolicy(ImagePullPolicy.Always)` | Always pull the image |
| `pull_policy: missing` | `.WithImagePullPolicy(ImagePullPolicy.Missing)` | Pull only if not present locally |
| `pull_policy: never` | `.WithImagePullPolicy(ImagePullPolicy.Never)` | Never pull from registry |

## .NET projects

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `build: ./MyApi` (for .NET) | `builder.AddProject<Projects.MyApi>("myapi")` | Direct .NET project reference |

## Port mappings

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `ports: ["8080:80"]` | `.WithHttpEndpoint(port: 8080, targetPort: 80)` | HTTP endpoint mapping. Ports are optional; dynamic ports are used if omitted |
| `ports: ["443:443"]` | `.WithHttpsEndpoint(port: 443, targetPort: 443)` | HTTPS endpoint mapping. Ports are optional; dynamic ports are used if omitted |
| `expose: ["8080"]` | `.WithEndpoint(port: 8080)` | Internal port exposure. Ports are optional; dynamic ports are used if omitted |

## Environment variables

| Docker Compose | Aspire / Notes |
|----------------|-------------|
| `environment: KEY=value` | `.WithEnvironment("KEY", "value")` <br/> Static environment variable |
| `environment: KEY=${HOST_VAR}` | `.WithEnvironment(context => context.EnvironmentVariables["KEY"] = hostVar)` <br/> Environment variable with callback context |
| `env_file: .env` | `.ConfigureEnvFile(env => { ... })` <br/> Environment file generation (available in 13.1+) |

## Volumes and storage

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `volumes: ["data:/app/data"]` | `.WithVolume("data", "/app/data")` | Named volume |
| `volumes: ["./host:/container"]` | `.WithBindMount("./host", "/container")` | Bind mount |
| `volumes: ["./config:/app:ro"]` | `.WithBindMount("./config", "/app", isReadOnly: true)` | Read-only bind mount |

## Dependencies and ordering

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `depends_on: [db]` | `.WithReference(db)` | Service dependency with connection string injection |
| `depends_on: db: condition: service_started` | `.WaitFor(db)` | Wait for service start |
| `depends_on: db: condition: service_healthy` | `.WaitForCompletion(db)` | Wait for health check to pass |

## Networks

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `networks: [backend]` | Automatic | Aspire handles networking automatically |
| Custom networks | Not needed | Service discovery handles inter-service communication |

## Resource limits

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `deploy.resources.limits.memory: 512m` | Not supported | Resource limits aren't supported in Aspire |
| `deploy.resources.limits.cpus: 0.5` | Not supported | Resource limits aren't supported in Aspire |

## Health checks

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `healthcheck.test: ["CMD", "curl", "http://localhost/health"]` | Built-in for integrations | Aspire integrations include health checks |
| `healthcheck.interval: 30s` | Configurable in integration | Health check configuration varies by resource type |

## Restart policies

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `restart: unless-stopped` | Not supported | Restart policies aren't supported in Aspire |
| `restart: always` | Not supported | Restart policies aren't supported in Aspire |
| `restart: no` | Default | No restart policy |

## Logging

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `logging.driver: json-file` | Built-in | Aspire provides integrated logging |
| `logging.options.max-size: 10m` | Dashboard configuration | Managed through Aspire dashboard |

## Database services

| Docker Compose | Aspire | Notes |
|----------------|-------------|-------|
| `image: postgres:15` | `builder.AddPostgres("name")` | PostgreSQL with automatic configuration |
| `image: mysql:8` | `builder.AddMySql("name")` | MySQL with automatic configuration |
| `image: redis:7` | `builder.AddRedis("name")` | Redis with automatic configuration |
| `image: mongo:latest` | `builder.AddMongoDB("name")` | MongoDB with automatic configuration |

## See also

- [Migrate from Docker Compose to Aspire](/docs/app-host/docker-compose/migrate-from-docker-compose)
- [AppHost overview](/docs/fundamentals/core-concepts/app-host)
- [WithDockerfile](/docs/app-host/resource-types/withdockerfile)
