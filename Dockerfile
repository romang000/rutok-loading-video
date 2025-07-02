FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем ТОЛЬКО файлы проектов (для ускорения сборки)
COPY ["Rutok.DownloadVideo/Rutok.DownloadVideo.csproj", "Rutok.DownloadVideo/"]
COPY ["Rutok.DownloadVideo.Infrastructure/Rutok.DownloadVideo.Infrastructure.csproj", "Rutok.DownloadVideo.Infrastructure/"]
COPY ["Rutok.DownloadVideo.Application/Rutok.DownloadVideo.Application.csproj", "Rutok.DownloadVideo.Application/"]
COPY ["Rutok.DownloadVideo.Domain/Rutok.DownloadVideo.Domain.csproj", "Rutok.DownloadVideo.Domain/"]
COPY ["Rutok.DownloadVideo.Infrastructure/Migrations/", "Rutok.DownloadVideo.Infrastructure/Migrations/"]

# Восстанавливаем зависимости основного проекта
RUN dotnet restore "Rutok.DownloadVideo/Rutok.DownloadVideo.csproj"

# Копируем ВСЕ остальные файлы
COPY . .

# Собираем проект
WORKDIR "/src/Rutok.DownloadVideo"
RUN dotnet build "Rutok.DownloadVideo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Устанавливаем dotnet-ef (если нужно)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Публикуем проект
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Rutok.DownloadVideo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "Rutok.DownloadVideo.dll"]