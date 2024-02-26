
docker-build:
	@docker-compose build dot_net_core_restsharp

docker-run:
	@docker-compose up dot_net_core_restsharp --abort-on-container-exit

docker-test:
	@make docker-build && make docker-run && make docker-test-copy-results

docker-test-copy-results:
	@docker cp dotNetCoreRestSharp:/application/RestSharpTests/TestResults ./RestSharpTests

restore:
	@dotnet restore

build:
	@dotnet build ./RestSharpTestsDemo.sln

test:
	@dotnet test ./RestSharpTests/RestSharpTests.csproj

