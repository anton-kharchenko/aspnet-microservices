#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Ordering.API/Ordering.API.csproj", "Services/Ordering.API/"]
COPY ["Services/Ordering.Application/Ordering.Application.csproj", "Services/Ordering.Application/"]
COPY ["Services/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering.Domain/"]
COPY ["Services/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering.Infrastructure/"]
RUN dotnet restore "Services/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]