#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RAS.Bootcamp.mvc.csproj", "."]
RUN dotnet restore "./RAS.Bootcamp.mvc.csproj"

# untuk mengcopy semua root file yang ada di RAS.BOOTCAMP.MVC
COPY . . 
WORKDIR "/src/."
RUN dotnet build "RAS.Bootcamp.mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RAS.Bootcamp.mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RAS.Bootcamp.mvc.dll"]