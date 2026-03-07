FROM microsoft/dotnet-sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet publish IOT_Shopping.csproj -c Release -o /app/publish

FROM microsoft/dotnet-aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "IOT_Shopping.dll"]
