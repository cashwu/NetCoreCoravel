FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NetCoreCoravel/NetCoreCoravel.csproj", "NetCoreCoravel/"]
RUN dotnet restore "NetCoreCoravel/NetCoreCoravel.csproj"
COPY . .
WORKDIR "/src/NetCoreCoravel"
RUN dotnet build "NetCoreCoravel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreCoravel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreCoravel.dll"]
