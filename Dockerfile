# Build aşaması için .NET SDK imajını kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Stock Control.csproj", "./"]
RUN dotnet restore "Stock Control.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Stock Control.csproj" -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish "Stock Control.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final çalışma zamanı imajı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/DataProtection-Keys
COPY ["DataProtection-Keys", "/app/DataProtection-Keys"]
COPY ["stockcontrol.db", "./"]
EXPOSE 4209
ENV ASPNETCORE_URLS=http://+:4209
ENTRYPOINT ["dotnet", "Stock Control.dll"]