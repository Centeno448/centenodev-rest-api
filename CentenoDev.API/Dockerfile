#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CentenoDev.API/CentenoDev.API.csproj", "CentenoDev.API/"]
RUN dotnet restore "CentenoDev.API/CentenoDev.API.csproj"
COPY . .
WORKDIR "/src/CentenoDev.API"
RUN dotnet build "CentenoDev.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CentenoDev.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CentenoDev.API.dll"]