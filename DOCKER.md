# Docker Compose Setup for Qdrant Clean Code POC

This directory contains the Docker Compose configuration for running the Qdrant Clean Code POC with all its services containerized.

## Services

The Docker Compose setup includes the following services:

### 1. Qdrant (Vector Database)
- **Image**: `qdrant/qdrant:latest`
- **Ports**: 
  - `6333` - REST API
  - `6334` - gRPC API
- **Storage**: Persistent volume for data storage
- **Configuration**: On-premise deployment

### 2. Web API
- **Port**: `5030`
- **Purpose**: REST API for document management and search
- **Health Check**: Available at `/health`
- **Dependencies**: Qdrant service

### 3. Web App
- **Port**: `5032` (development)
- **Purpose**: Web interface for document management
- **Dependencies**: Web API and Qdrant services

### 4. Evaluator (Optional)
- **Purpose**: Console application for testing and evaluation
- **Profile**: `evaluator` (only starts when explicitly requested)
- **Dependencies**: Web API and Qdrant services

## Quick Start

### Prerequisites
- Docker and Docker Compose installed
- .NET 8.0 SDK (for building locally)

### Starting the Services

1. **Start basic services (Qdrant, WebApi, WebApp):**
   ```bash
   docker-compose up -d
   ```

2. **Start with the evaluator service:**
   ```bash
   docker-compose --profile evaluator up -d
   ```

3. **View logs:**
   ```bash
   docker-compose logs -f
   ```

4. **Stop services:**
   ```bash
   docker-compose down
   ```

### Environment Variables

The setup supports the following environment variables for configuration:

- `OPENAI_API_KEY` - OpenAI API key for embedding services
- `QDRANT_HOST` - Qdrant host (default: qdrant)
- `QDRANT_PORT` - Qdrant port (default: 6333)

Example:
```bash
export OPENAI_API_KEY="your_openai_api_key_here"
docker-compose up -d
```

## Service URLs

After starting the services, you can access:

- **Qdrant Dashboard**: http://localhost:6333/dashboard
- **Web API**: http://localhost:5030
- **Web API Swagger**: http://localhost:5030/swagger (in development)
- **Web App**: http://localhost:5032
- **Web API Health Check**: http://localhost:5030/health

## Development

### Building Images Locally

To rebuild the images after code changes:

```bash
docker-compose build
docker-compose up -d
```

### Development Configuration

The `docker-compose.override.yml` file contains development-specific configurations:
- Debug logging levels
- Additional ports for debugging
- Volume mounts for configuration files
- Evaluator service profile

### Logs and Storage

- **Application logs**: Stored in containers and accessible via `docker-compose logs`
- **Qdrant data**: Persisted in a Docker volume `qdrant_data`

## Architecture

The setup follows Clean Architecture principles and Twelve-Factor App methodology:

- **Config**: Environment variables for configuration
- **Backing Services**: Qdrant as an attached resource
- **Processes**: Stateless application design
- **Port Binding**: Each service binds to its own port
- **Dev/Prod Parity**: Consistent environments across development and production

## Troubleshooting

### Common Issues

1. **Port conflicts**: Ensure ports 5030, 5032, 6333, and 6334 are available
2. **Build failures**: Check Docker build context and ensure .dockerignore is properly configured
3. **Service dependencies**: Services start in order based on health checks

### Viewing Service Status

```bash
docker-compose ps
```

### Checking Service Health

```bash
curl http://localhost:5030/health
curl http://localhost:6333/health
```

### Rebuilding After Code Changes

```bash
docker-compose down
docker-compose build --no-cache
docker-compose up -d
```

## Deployment Options

### On-Premise Deployment (Default)

The default configuration uses a local Qdrant instance:

```bash
docker compose up -d
```

### Cloud Deployment (Qdrant Cloud)

For production with Qdrant Cloud, use the production override:

```bash
# Set required environment variables
export QDRANT_CLOUD_HOST="your-cluster.qdrant.tech"
export QDRANT_CLOUD_PORT="443"
export QDRANT_CLOUD_API_KEY="your-api-key"
export OPENAI_API_KEY="your-openai-key"
export WEBAPI_URL="https://your-api-domain.com/"

# Deploy without local Qdrant
docker compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```

### Hybrid Deployment

You can also run locally while connecting to Qdrant Cloud:

```bash
# Set Qdrant Cloud variables
export QDRANT_CLOUD_HOST="your-cluster.qdrant.tech"
export QDRANT_CLOUD_API_KEY="your-api-key"

# Override the Qdrant configuration
docker compose up -d --scale qdrant=0
```