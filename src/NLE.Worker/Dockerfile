FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/NLE.Worker/NLE.Worker.csproj", "src/NLE.Worker/"]
RUN dotnet restore "src/NLE.Worker/NLE.Worker.csproj"
COPY . .
WORKDIR "/src/src/NLE.Worker"
RUN dotnet build "NLE.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NLE.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NLE.Worker.dll"]
