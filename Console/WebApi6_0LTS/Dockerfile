#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi6_0LTS/WebApi6_0LTS.csproj", "WebApi6_0LTS/"]
RUN dotnet restore "WebApi6_0LTS/WebApi6_0LTS.csproj"
COPY . .
WORKDIR "/src/WebApi6_0LTS"
RUN dotnet build "WebApi6_0LTS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi6_0LTS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi6_0LTS.dll"]