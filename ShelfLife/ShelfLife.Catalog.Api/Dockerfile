FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ShelfLife.Catalog.Api/ShelfLife.Catalog.Api.csproj", "ShelfLife.Catalog.Api/"]
COPY ["ShelfLife.Catalog.Application/ShelfLife.Catalog.Application.csproj", "ShelfLife.Catalog.Application/"]
COPY ["ShelfLife.Catalog.Domain/ShelfLife.Catalog.Domain.csproj", "ShelfLife.Catalog.Domain/"]
COPY ["ShelfLife.Catalog.Infrastructure/ShelfLife.Catalog.Infrastructure.csproj", "ShelfLife.Catalog.Infrastructure/"]
COPY ["ShelfLife.SharedKernel/ShelfLife.SharedKernel.csproj", "ShelfLife.SharedKernel/"]
RUN dotnet restore "ShelfLife.Catalog.Api/ShelfLife.Catalog.Api.csproj"

COPY . .
WORKDIR /src/ShelfLife.Catalog.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShelfLife.Catalog.Api.dll"]
