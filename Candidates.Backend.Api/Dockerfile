#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Candidates.Backend.Api/Candidates.Backend.Api.csproj", "Candidates.Backend.Api/"]
COPY ["Candidates.Backend.Application/Candidates.Backend.Application.csproj", "Candidates.Backend.Application/"]
COPY ["Candidates.Backend.Domain/Candidates.Backend.Domain.csproj", "Candidates.Backend.Domain/"]
COPY ["Candidates.Backend.Infrastructure/Candidates.Backend.Infrastructure.csproj", "Candidates.Backend.Infrastructure/"]
RUN dotnet restore "./Candidates.Backend.Api/./Candidates.Backend.Api.csproj"
COPY . .
WORKDIR "/src/Candidates.Backend.Api"
RUN dotnet build "./Candidates.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Candidates.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Candidates.Backend.Api.dll"]