#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserService/UserApi/UserApi.csproj", "src/UserService/UserApi/"]
COPY ["src/UserService/UserApplication/UserApplication.csproj", "src/UserService/UserApplication/"]
COPY ["src/UserService/UserDomain/UserDomain.csproj", "src/UserService/UserDomain/"]
COPY ["src/UserService/UserInfra/UserInfra.csproj", "src/UserService/UserInfra/"]
COPY ["src/Lib/Contracts.csproj", "src/Lib/"]
RUN dotnet restore "./src/UserService/UserApi/./UserApi.csproj"
COPY . .
WORKDIR "/src/src/UserService/UserApi"
RUN dotnet build "./UserApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserApi.dll"]