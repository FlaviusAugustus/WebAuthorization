FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /App

WORKDIR /Shared
COPY Shared ./
RUN dotnet restore Shared.csproj
RUN dotnet publish -c Release -o /App/out

WORKDIR /Client
COPY Client ./
RUN dotnet restore Client.csproj
RUN dotnet publish -c Release -o /App/out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env App/out .
ENTRYPOINT ["dotnet", "Client.dll", "--server.urls", "http://0.0.0.0:80"]

EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80
