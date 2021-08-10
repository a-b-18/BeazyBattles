#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
EXPOSE 443
EXPOSE 80
COPY ["BeazyBattles/Server/BeazyBattles.Server.csproj", "BeazyBattles/Server/"]
COPY ["BeazyBattles/Shared/BeazyBattles.Shared.csproj", "BeazyBattles/Shared/"]
COPY ["BeazyBattles/Client/BeazyBattles.Client.csproj", "BeazyBattles/Client/"]
RUN dotnet restore "BeazyBattles/Server/BeazyBattles.Server.csproj"
COPY . .
WORKDIR "/src/BeazyBattles/Server"
RUN dotnet build "BeazyBattles.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeazyBattles.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeazyBattles.Server.dll"]