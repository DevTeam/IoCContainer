#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Samples/WebApplication2/WebApplication2.csproj", "Samples/WebApplication2/"]
COPY ["IoC.AspNetCore/IoC.AspNetCore.csproj", "IoC.AspNetCore/"]
COPY ["IoC/IoC.csproj", "IoC/"]
COPY ["Samples/Clock/Clock.csproj", "Samples/Clock/"]
RUN dotnet restore "Samples/WebApplication2/WebApplication2.csproj"
COPY . .
WORKDIR "/src/Samples/WebApplication2"
RUN dotnet build "WebApplication2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication2.dll"]