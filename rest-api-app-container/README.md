# Azure Serverless API #

ASP.NET Core WebApi sample application.

It is designed as simple monolithic application due to simplify the understanding using the different layers instead of using a micro-service application design that implies the other external knowledges which are not in the scope and purpose of this example.

## During the development ## 

Using docker to start a new instance of Microsoft Sql Server 2019, or install a database server on local PC.

Run the database server: `docker run -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=MyPassword123' mcr.microsoft.com/mssql/server:2019-latest`

**Note**: In this example the database hasn't the persistent volume, so all the times it start fresh. If you want add the persistent storage attached with a local folder, please follow the instruction on [Microsoft SQL Server Docker Hub page](https://hub.docker.com/_/microsoft-mssql-server).

After the database, please check the `ASPNETCORE_ENVIROMENT` variables and update, if it is required, the connection string.

## Infrastructure ##

Azure ARM template.

## Deploy ##

Parameters used for this example and reused below.

| Parameter Name | Parameter Value |
|----------------|-----------------|
| Resource group Name | MyAppResourceGroup |
| Azure SQL Server Name | my-web-app-sql-server |
| Azure SQL Database Name | my-web-app-sql-db |
| App Service Plan Name | MyWebAppServicePlan |
| WebApp Name | MyTestWebApiApp |


## Application configuration ##

Need to configure the web application

```
az webapp config connection-string set --resource-group MyAppResourceGroup --connection-string-type SQLAzure --name MyTestWebApiApp --settings DefaultConnection='Server=tcp:my-web-app-sql-server.database.windows.net,1433;Initial Catalog=my-app-sql-db;Persist Security Info=False;User ID=sa01;Password=MyPassword123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'

az monitor app-insights component create --app MyTestWebApiApp --location southeastasia --resource-group MyAppResourceGroup --kind web --application-type web --retention-time 30

az webapp config set --name MyTestWebApiApp --resource-group MyAppResourceGroup --generic-configurations '{"APPINSIGHTS_INSTRUMENTATIONKEY": "COPY_HERE_THE_APPINSIGHT_KEY"}'

az webapp update --https-only true --name MyTestWebApiApp --resource-group MyAppResourceGroup

az webapp config set --name MyTestWebApiApp --resource-group MyAppResourceGroup --ftps-state FtpsOnly

az webapp deployment user set --user-name MyTestWebApiAppDeployUserName --password MyPassword123

az webapp deployment source config-local-git --resource-group MyAppResourceGroup --name MyTestWebApiApp
```

## Deploy ##

Publish the project into a separated folder of local disk

`dotnet publish src/Azure.Web.Api -c Release -f netcoreapp3.1 -o <LOCAL_FOLDER_PATH>`

Configure the Web Application to deploy by Kudu.

```
git remote add azure https://MyDeployUserName@MyTestWebApiApp.scm.azurewebsites.net/MyTestWebApiApp.git

git add .

git commit -m "Deploy"

git push
```