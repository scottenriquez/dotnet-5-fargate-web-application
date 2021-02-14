FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY WebApplication.csproj .
RUN dotnet restore -r linux-musl-x64 WebApplication.csproj

COPY * ./
WORKDIR /source
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine-amd64
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./aspnetapp"]