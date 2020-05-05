#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# TO BUILD DOCKER IMAGE FROM THE SOURCE CODE
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./", "src/"]
RUN dotnet restore "src/Roamler.LocationSearch.sln"
COPY . .
WORKDIR /src
RUN dotnet build "Roamler.LocationSearch.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/src/Roamler.LocationSearch/Roamler.LocationSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roamler.LocationSearch.dll"]