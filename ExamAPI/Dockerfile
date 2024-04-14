# Используем базовый образ ASP.NET среды выполнения
#FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 8080

# Устанавливаем PostgreSQL
RUN apt-get update && \
    apt-get install -y postgresql postgresql-contrib && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Добавляем конфигурацию для PostgreSQL
RUN echo "host all all 0.0.0.0/0 md5" >> /etc/postgresql/15/main/pg_hba.conf && \
    echo "listen_addresses = '*'" >> /etc/postgresql/15/main/postgresql.conf && \
    echo "port = 5432" >> /etc/postgresql/15/main/postgresql.conf

# Задаем пароль для пользователя postgres
RUN service postgresql start && \
    su - postgres -c "psql -c \"ALTER USER postgres WITH PASSWORD '1';\"" && \
    su - postgres -c "psql -c \"CREATE DATABASE testd32;\"" && \
    su - postgres -c "psql -c \"GRANT ALL PRIVILEGES ON DATABASE testd32 TO postgres;\""



FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExamAPI/ExamAPI.csproj", "ExamAPI/"]
RUN dotnet restore "./ExamAPI/./ExamAPI.csproj"
COPY . .
WORKDIR "/src/ExamAPI"
RUN dotnet build "./ExamAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExamAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamAPI.dll"]
