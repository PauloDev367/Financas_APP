FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY FinancasApp/*.csproj FinancasApp/
COPY FinancasAppTest/*.csproj FinancasAppTest/

RUN dotnet restore

COPY . .
WORKDIR /src/FinancasApp

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "FinancasApp.dll"]
