#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim 
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "apiSupplier.dll"]