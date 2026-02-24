FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /Crudman

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /Crudman
COPY --from=build-env /Crudman/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Crudman.dll"]