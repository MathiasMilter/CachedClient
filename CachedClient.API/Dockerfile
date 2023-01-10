FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CachedClient/CachedClient.csproj", "CachedClient/"]
RUN dotnet restore "CachedClient/CachedClient.csproj"
COPY . .
WORKDIR "/src/CachedClient"
RUN dotnet build "CachedClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CachedClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CachedClient.dll"]
