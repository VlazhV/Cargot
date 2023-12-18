# build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "Identity.Presentation" 
RUN dotnet publish "Identity.Presentation" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as serve
WORKDIR /app
COPY --from=build /app ./

COPY Identity.Presentation/.env /app/.env

EXPOSE 80
ENTRYPOINT [ "dotnet", "Identity.Presentation.dll" ]