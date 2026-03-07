FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY . .

RUN dotnet publish IOT_Shopping.csproj -c Release -o out

WORKDIR /app/out

ENTRYPOINT ["dotnet", "IOT_Shopping.dll"]
