# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj files and restore dependencies
COPY src/Domain/*.csproj ./src/Domain/
COPY src/Application/*.csproj ./src/Application/
COPY src/Infrastructure/*.csproj ./src/Infrastructure/
COPY src/Presentation/Evaluator/*.csproj ./src/Presentation/Evaluator/
RUN dotnet restore ./src/Presentation/Evaluator/

# Copy everything and build
COPY . ./
RUN dotnet publish src/Presentation/Evaluator -c Release -o out

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copy the published app
COPY --from=build-env /app/out .

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser
RUN chown -R appuser:appuser /app
USER appuser

# Set the entry point
ENTRYPOINT ["dotnet", "PocGithubCopilotAgentQdrantCleancode.Presentation.Evaluator.dll"]