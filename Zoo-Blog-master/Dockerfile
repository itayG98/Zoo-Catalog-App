FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app
COPY *.sln .
COPY Zoo ./Zoo
COPY Model ./Model
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
EXPOSE 433
ENTRYPOINT ["dotnet","Zoo.dll"]
