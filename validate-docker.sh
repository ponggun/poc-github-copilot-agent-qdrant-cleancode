#!/bin/bash

# Docker Compose Validation Script
# This script validates the Docker Compose configuration files

set -e

echo "🔧 Validating Docker Compose configuration..."

# Check if docker compose is available
if ! command -v docker &> /dev/null; then
    echo "❌ Docker is not installed or not in PATH"
    exit 1
fi

# Check if docker compose command is available
if ! docker compose version &> /dev/null; then
    echo "❌ Docker Compose is not available"
    exit 1
fi

echo "✅ Docker and Docker Compose are available"

# Validate main docker-compose.yml
echo "🔍 Validating docker-compose.yml..."
if docker compose -f docker-compose.yml config --quiet; then
    echo "✅ docker-compose.yml is valid"
else
    echo "❌ docker-compose.yml has validation errors"
    exit 1
fi

# Validate development configuration
echo "🔍 Validating development configuration..."
if docker compose -f docker-compose.yml -f docker-compose.override.yml config --quiet; then
    echo "✅ Development configuration is valid"
else
    echo "❌ Development configuration has validation errors"
    exit 1
fi

# Validate production configuration
echo "🔍 Validating production configuration..."
if docker compose -f docker-compose.yml -f docker-compose.prod.yml config --quiet; then
    echo "✅ Production configuration is valid"
else
    echo "❌ Production configuration has validation errors"
    exit 1
fi

# Check if required Dockerfiles exist
echo "🔍 Checking Dockerfile existence..."
DOCKERFILES=(
    "src/Presentation/WebApi/Dockerfile"
    "src/Presentation/WebApp/Dockerfile"
    "src/Presentation/Evaluator/Dockerfile"
)

for dockerfile in "${DOCKERFILES[@]}"; do
    if [[ -f "$dockerfile" ]]; then
        echo "✅ $dockerfile exists"
    else
        echo "❌ $dockerfile is missing"
        exit 1
    fi
done

# Check if .dockerignore exists
if [[ -f ".dockerignore" ]]; then
    echo "✅ .dockerignore exists"
else
    echo "❌ .dockerignore is missing"
    exit 1
fi

# Check if environment example exists
if [[ -f ".env.example" ]]; then
    echo "✅ .env.example exists"
else
    echo "❌ .env.example is missing"
    exit 1
fi

echo ""
echo "🎉 All Docker Compose validations passed!"
echo ""
echo "📋 Next Steps:"
echo "1. Copy .env.example to .env and set your actual values"
echo "2. Run 'docker compose up -d' to start the services"
echo "3. Access the applications:"
echo "   - Web API: http://localhost:5030"
echo "   - Web App: http://localhost:5032"
echo "   - Qdrant Dashboard: http://localhost:6333/dashboard"
echo ""