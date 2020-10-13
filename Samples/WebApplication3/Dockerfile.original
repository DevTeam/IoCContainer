#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Samples/WebApplication3/WebApplication3.csproj", "Samples/WebApplication3/"]
COPY ["IoC.AspNetCore/IoC.AspNetCore.csproj", "IoC.AspNetCore/"]
COPY ["IoC/IoC.csproj", "IoC/"]
COPY ["Samples/Clock/Clock.csproj", "Samples/Clock/"]
RUN dotnet restore "Samples/WebApplication3/WebApplication3.csproj"
COPY . .
WORKDIR "/src/Samples/WebApplication3"
RUN dotnet build "WebApplication3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication3.dll"]