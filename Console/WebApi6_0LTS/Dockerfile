#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApi6_0.csproj", "src/"]
RUN dotnet restore "src/WebApi6_0.csproj"
COPY . .
WORKDIR "/src/WebApi6_0"
RUN dotnet build "WebApi6_0.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi6_0.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi6_0.dll"]