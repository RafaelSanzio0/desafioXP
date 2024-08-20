FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Portfolio.Management.API/Portfolio.Management.API.csproj", "./"]
RUN dotnet restore "./Portfolio.Management.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Portfolio.Management.API/Portfolio.Management.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portfolio.Management.API/Portfolio.Management.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Management.API.dll"]
