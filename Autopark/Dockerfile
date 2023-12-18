# build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "Autopark.Presentation" 
RUN dotnet publish "Autopark.Presentation" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as serve
WORKDIR /app
COPY --from=build /app ./

COPY Autopark.Presentation/.env /app/.env

EXPOSE 80
ENTRYPOINT [ "dotnet", "Autopark.Presentation.dll" ]