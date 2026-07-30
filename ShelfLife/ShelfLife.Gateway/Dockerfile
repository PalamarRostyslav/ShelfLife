FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ShelfLife.Gateway/ShelfLife.Gateway.csproj", "ShelfLife.Gateway/"]
RUN dotnet restore "ShelfLife.Gateway/ShelfLife.Gateway.csproj"

COPY . .
WORKDIR "/src/ShelfLife.Gateway"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ShelfLife.Gateway.dll"]
