FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# COPY PROJECT TO:
COPY . /application

# APPLICATION:
WORKDIR /application

# EXECUTE:
RUN cd /application

CMD [ "ls" ]
CMD [ "dotnet", "build" ]
CMD [ "dotnet", "restore" ]
CMD [ "dotnet", "test", "./RestSharpTests/RestSharpTests.csproj" ]