# Используем базовый образ с .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Устанавливаем рабочую директорию
WORKDIR /app

# Копирование исходного кода и восстановление пакетов, сборка приложения
COPY . ./
RUN dotnet restore && \
    dotnet publish -c Release -o out

# Создаем финальный образ на основе .NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Устанавливаем рабочую директорию
WORKDIR /app

RUN mkdir -p /app/Generated

# Копируем собранное приложение из образа сборки
COPY --from=build-env /app/out .

# Создаем пользователя с ограниченными правами
RUN useradd -m choco && \
    chown -R choco:choco /app
USER choco

# Открываем порт для приложения
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "Choco.dll"]