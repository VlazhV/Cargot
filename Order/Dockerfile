# build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "Order.WebApi" 
RUN dotnet publish "Order.WebApi" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as serve
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENTRYPOINT [ "dotnet", "Order.WebApi.dll" ]