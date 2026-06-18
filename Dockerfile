FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DevFreela.API/DevFreela.API.csproj", "DevFreela.API/"]
COPY ["DevFreela.Application/DevFreela.Application.csproj", "DevFreela.Application/"]
COPY ["DevFreela.Core/DevFreela.Core.csproj", "DevFreela.Core/"]
COPY ["DevFreela.Infrastructure/DevFreela.Infrastructure.csproj", "DevFreela.Infrastructure/"]
RUN dotnet restore "DevFreela.API/DevFreela.API.csproj"

COPY . .
RUN dotnet publish "DevFreela.API/DevFreela.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevFreela.API.dll"]
