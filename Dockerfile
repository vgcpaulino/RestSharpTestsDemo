FROM mcr.microsoft.com/dotnet/sdk:8.0

# Change the working dir folder:
WORKDIR /application

# Copy Solution and Project files:
COPY RestSharpTestsDemo.sln RestSharpTestsDemo.sln
COPY RestSharpTests/RestSharpTests.csproj RestSharpTests/RestSharpTests.csproj

# Restore packages:
RUN  dotnet restore 

# Copy source files:
COPY . .

# Build Solution:
RUN dotnet build
    
# Run tests:
CMD [ "dotnet", "test", "--logger", "\"trx\"" ]