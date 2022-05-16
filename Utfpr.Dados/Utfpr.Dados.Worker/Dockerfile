FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj", "Utfpr.Dados.Worker/"]
RUN dotnet restore "Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj"
COPY . .
WORKDIR "/src/Utfpr.Dados.Worker"
RUN dotnet build "Utfpr.Dados.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Utfpr.Dados.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Utfpr.Dados.Worker.dll"]
