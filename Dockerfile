FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

# copy project csproj file and restore it in docker directory
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Candidates.Backend.Api/Candidates.Backend.Api.csproj", "Candidates.Backend.Api"]
COPY ["Candidates.Backend.Application/Candidates.Backend.Application.csproj", "Candidates.Backend.Application"]
COPY ["Candidates.Backend.Domain/Candidates.Backend.Domain.csproj", "Candidates.Backend.Domain"]
COPY ["Candidates.Backend.Infrastructure/Candidates.Backend.Infrastructure.csproj", "Candidates.Backend.Infrastructure"]
COPY ["Candidates.Backend.TestApi/Candidates.Backend.TestApi.csproj", "Candidates.Backend.TestApi"]
# COPY ["Candidates.Backend.Api.Tests/Candidates.Backend.Api.Tests.csproj", "Candidates.Backend.Api.Tests"]
RUN dotnet restore "Candidates.Backend.Api/Candidates.Backend.Api.csproj"
COPY . .
WORKDIR "/src/Candidates.Backend.Api"
RUN dotnet build "Candidates.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Copy from de build to PROduction
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Candidates.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Build runtime final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Candidates.Backend.Api.dll"]