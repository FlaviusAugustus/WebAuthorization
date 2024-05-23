FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /App

WORKDIR /Shared
COPY Shared ./
RUN dotnet restore Shared.csproj
RUN dotnet publish -c Release -o /App/out

WORKDIR /Api
COPY Api ./
RUN dotnet restore Api.csproj
RUN dotnet publish -c Release -o /App/out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env App/out .
ENTRYPOINT ["dotnet", "Api.dll", "--server.urls", "http://0.0.0.0:8080"]

EXPOSE 8080
