#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/HumanResources/HumanResources.Api/HumanResources.Api.csproj", "Services/HumanResources/HumanResources.Api/"]
COPY ["Services/HumanResources/HumanResources.Infrastructure/HumanResources.Infrastructure.csproj", "Services/HumanResources/HumanResources.Infrastructure/"]
COPY ["Services/HumanResources/HumanResources.AppLogic/HumanResources.AppLogic.csproj", "Services/HumanResources/HumanResources.AppLogic/"]
COPY ["Services/HumanResources/HumanResources.Domain/HumanResources.Domain.csproj", "Services/HumanResources/HumanResources.Domain/"]
RUN dotnet restore "Services/HumanResources/HumanResources.Api/HumanResources.Api.csproj"
COPY . .
WORKDIR "/src/Services/HumanResources/HumanResources.Api"
RUN dotnet build "HumanResources.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HumanResources.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HumanResources.Api.dll"]