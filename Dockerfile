FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish \
    -c release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["./Api.Gear"]