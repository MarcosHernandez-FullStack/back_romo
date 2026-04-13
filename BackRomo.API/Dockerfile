# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["BackRomo.API/BackRomo.API.csproj", "BackRomo.API/"]
COPY ["BackRomo.Application/BackRomo.Application.csproj", "BackRomo.Application/"]
COPY ["BackRomo.Domain/BackRomo.Domain.csproj", "BackRomo.Domain/"]
COPY ["BackRomo.Infrastructure/BackRomo.Infrastructure.csproj", "BackRomo.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "BackRomo.API/BackRomo.API.csproj"

# Copiar todo el código
COPY . .

# Publicar
WORKDIR "/src/BackRomo.API"
RUN dotnet publish "BackRomo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "BackRomo.API.dll"]