# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Копіюємо проект і відновлюємо залежності
COPY ["ATB_serverWebApi/ATB_serverWebApi.csproj", "ATB_serverWebApi/"]
RUN dotnet restore "ATB_serverWebApi/ATB_serverWebApi.csproj"

# Копіюємо всі файли і будуємо додаток
COPY . .
WORKDIR /source/ATB_serverWebApi
RUN dotnet publish -c Release -o /app

# Stage 2: Final image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Встановлюємо необхідні пакети для підключення до PostgreSQL
WORKDIR /app

# Копіюємо додаток з етапу побудови
COPY --from=build /app .

# Запускаємо додаток
ENTRYPOINT ["dotnet", "ATB_serverWebApi.dll"]
