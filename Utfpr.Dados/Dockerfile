FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj, Utfpr.Dados.Worker/
RUN dotnet restore "Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj"
COPY . .
WORKDIR "/src/Utfpr.Dados.Worker"
RUN dotnet publish "Utfpr.Dados.Worker.csproj" -c Release -o /app

FROM base AS publish
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Utfpr.Dados.Worker.dll"]