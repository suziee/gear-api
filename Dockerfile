FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
COPY . .
RUN dotnet restore "./Api.Gear.csproj"
RUN dotnet build "Api.Gear.csproj" \
    -c Release \
    --no-restore \
    --runtime alpine-x64 \
    --self-contained true \
    -p:PublishSingleFile=true

FROM build AS publish
RUN dotnet publish "Api.Gear.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    --no-build \
    --runtime alpine-x64 \
    --self-contained true \
    -p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["./Api.Gear"]