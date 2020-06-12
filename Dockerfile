FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Copy local folder to the container:
COPY . /application

# Change the working dir folder:
WORKDIR /application

# CHANGE directory, restore the packages and build the project:
RUN cd /application \
    dotnet restore \
    dotnet build
    
# Run tests:
CMD [ "dotnet", "test", "--logger", "\"trx\"" ]