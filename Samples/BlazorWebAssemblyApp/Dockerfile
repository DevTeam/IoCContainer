#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Samples/BlazorWebAssemblyApp/BlazorWebAssemblyApp.csproj", "Samples/BlazorWebAssemblyApp/"]
COPY ["IoC.AspNetCore/IoC.AspNetCore.csproj", "IoC.AspNetCore/"]
COPY ["IoC/IoC.csproj", "IoC/"]
COPY ["Samples/Clock/Clock.csproj", "Samples/Clock/"]
RUN dotnet restore "Samples/BlazorWebAssemblyApp/BlazorWebAssemblyApp.csproj"
COPY . .
WORKDIR "/src/Samples/BlazorWebAssemblyApp"
RUN dotnet build "BlazorWebAssemblyApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWebAssemblyApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorWebAssemblyApp.dll"]