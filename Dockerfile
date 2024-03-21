#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Test/API.Test/API.Test.csproj", "Test/API.Test/"]
RUN dotnet restore "./API/./API.csproj"
RUN dotnet restore "./Domain/./Domain.csproj"
RUN dotnet restore "./Infra/./Infra.csproj"
RUN dotnet restore "./Application/./Application.csproj"
RUN dotnet restore "./Test/API.Test/API.Test.csproj" 
COPY . .
WORKDIR "/src/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/Domain"
RUN dotnet build "./Domain.csproj" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/Infra"
RUN dotnet build "./Infra.csproj" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/Application"
RUN dotnet build "./Application.csproj" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/Test/API.Test"
RUN dotnet build "./API.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test --no-restore --verbosity normal

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/API"
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
WORKDIR "/src/Domain"
RUN dotnet publish "./Domain.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
WORKDIR "/src/Infra"
RUN dotnet publish "./Infra.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
WORKDIR "/src/Application"
RUN dotnet publish "./Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
