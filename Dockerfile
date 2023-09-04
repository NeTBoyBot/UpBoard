#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UpBoard.Host.Api/UpBoard.Host.Api.csproj", "UpBoard.Host.Api/"]
COPY ["UpBoard.Infrastructure.Registrar/UpBoard.Infrastructure.Registrar.csproj", "UpBoard.Infrastructure.Registrar/"]
COPY ["UpBoard.Infrastructure.DataAccess/UpBoard.Infrastructure.DataAccess.csproj", "UpBoard.Infrastructure.DataAccess/"]
COPY ["UpBoard.Application.AppData/UpBoard.Application.AppData.csproj", "UpBoard.Application.AppData/"]
COPY ["UpBoard.Contracts/UpBoard.Contracts.csproj", "UpBoard.Contracts/"]
COPY ["UpBoard.Domain/UpBoard.Domain.csproj", "UpBoard.Domain/"]
COPY ["UpBoard.Infrastructure/UpBoard.Infrastructure.csproj", "UpBoard.Infrastructure/"]
RUN dotnet restore "UpBoard.Host.Api/UpBoard.Host.Api.csproj"
COPY . .
WORKDIR "/src/UpBoard.Host.Api"
RUN dotnet build "UpBoard.Host.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UpBoard.Host.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpBoard.Host.Api.dll"]