FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["br.com.fiap.alert.api/br.com.fiap.alert.api.csproj", "br.com.fiap.alert.api/"]
RUN dotnet restore "./br.com.fiap.alert.api/br.com.fiap.alert.api.csproj"
COPY . .
WORKDIR "/src/br.com.fiap.alert.api"
RUN dotnet build "br.com.fiap.alert.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "br.com.fiap.alert.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "br.com.fiap.alert.api.dll"]
