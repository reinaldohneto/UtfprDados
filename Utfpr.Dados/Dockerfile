FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Utfpr.Dados.API/Utfpr.Dados.API.csproj", "Utfpr.Dados.API/"]
COPY ["Utfpr.Dados.Messages/Utfpr.Dados.Messages.csproj", "Utfpr.Dados.Messages/"]
RUN dotnet restore "Utfpr.Dados.API/Utfpr.Dados.API.csproj"

COPY . .
WORKDIR "/src/Utfpr.Dados.API"
RUN dotnet build "Utfpr.Dados.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Utfpr.Dados.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Utfpr.Dados.API.dll