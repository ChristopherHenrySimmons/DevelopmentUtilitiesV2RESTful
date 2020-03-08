#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DevelopmentUtilitiesV2RESTful.csproj", ""]
RUN dotnet restore "./DevelopmentUtilitiesV2RESTful.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DevelopmentUtilitiesV2RESTful.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevelopmentUtilitiesV2RESTful.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevelopmentUtilitiesV2RESTful.dll"]