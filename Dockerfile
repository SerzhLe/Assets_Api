# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source
# Copy all files wrom project directory (where solution is located) to source folder
COPY . .
RUN dotnet restore "./ApplicantAssets/ApplicantAssets.Api.csproj"
RUN dotnet publish "./ApplicantAssets/ApplicantAssets.Api.csproj" -c Release -o /app --no-restore

#Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
# we copy all published files from app folder in build stage to current (app) folder in serve stage (./)
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "ApplicantAssets.Api.dll"]
