FROM mcr.microsoft.com/dotnet/runtime AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /src
COPY ["Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj", "Utfpr.Dados.Worker/"]
COPY ["Utfpr.Dados.Messages/Utfpr.Dados.Messages.csproj", "Utfpr.Dados.Messages/"]
RUN dotnet restore "Utfpr.Dados.Worker/Utfpr.Dados.Worker.csproj"
COPY . .
WORKDIR "/src/Utfpr.Dados.Worker"
RUN dotnet publish "Utfpr.Dados.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Utfpr.Dados.Worker.dll"]