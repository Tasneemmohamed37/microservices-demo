# -----------------------------------------------------------
# ----------------------- 1 Build Stage -----------------------

#  the image we will pull from docker hub and code is compiled using a large SDK image
FROM mcr.microsoft.com/dotnet/sdk:10.0 as build-env
# our working dir will be app
WORKDIR /app  


# copy csproj files and restore dependencies to the working dir (app)
COPY *.csproj ./
RUN dotnet restore


# Copy everything else and build release to the working dir (app)
COPY . ./
RUN dotnet publish -c Release -o out

# -----------------------------------------------------------
# ----------------------- 2 Run Stage -----------------------

# Build runtime image where only the compiled code is placed into a small, lightweight image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app
ENTRYPOINT ["dotnet", "PlatformService.dll"]