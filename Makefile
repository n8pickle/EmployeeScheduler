
up:
	dotnet watch run

db:
	docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1!" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name root --hostname sqldb -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

test:
	curl -X GET https://localhost:5000/health
