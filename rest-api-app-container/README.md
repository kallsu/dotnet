# Azure Serverless API #

ASP.NET Core WebApi sample application.

It is designed as simple monolithic application due to simplify the understanding using the different layers instead of using a micro-service application design that implies the other external knowledges which are not in the scope and purpose of this example.

## During the development ## 

Using docker to start a new instance of PostgreSQL 12, or install a database server on local PC.

Run the database server: `docker run -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=MyPassword123' mcr.microsoft.com/mssql/server:2019-latest`

**Note**: In this example the database hasn't the persistent volume, so all the times it start fresh. If you want add the persistent storage attached with a local folder, please follow the instruction on [Microsoft SQL Server Docker Hub page](https://hub.docker.com/_/microsoft-mssql-server).

After the database, please check the `ASPNETCORE_ENVIROMENT` variables and update, if it is required, the connection string.

### Create the container ###

The docker build need 1 parameter:

 * `Environment`, that is the value of `ASPNETCORE_ENVIROMENT` variable above

So the final instruction to build the container will be:

 `docker build -f Dockerfile -t <YOUR_TAG> --build-arg Environment=<YOUR_ENV> .`

### To run the container ###

The container can run locally with the following command :

 `docker run -p 80:80 <YOUR_TAG> or <IMAGE_ID>`

### docker-compose ###

`docker-compose.yaml` file contains the base configuration to start the application and the database.

## Deployment ##

There is no script, due to the possible customization of every single environment. Instead of propose immediately the solution, according my point of view, it is better explain the single steps to gain own knowledge and awareness about the operations to-do.

Cloud provider choosen:
 * Azure

Default parameter map:

| Parameter Name | Parameter Default Value |
|----------------|-------------------------|
| Location | `southeastasia` |



### Azure CLI ###

Due to the Cloud Shell and the Azure CLI, the phase of the login and select the right solution is skipped.

1. Create the Resource Group, in this example `MyContainerAppResourceGroup`

    ```
    az group create --location southeastasia --name MyContainerAppResourceGroup
    ```

2. Create the Azure SQL Server

    ```
    az sql server create --name my-container-app-sql-server 
        --resource-group MyContainerAppResourceGroup 
        --admin-password MyPassword123 
        --admin-user sa01 
        --minimal-tls-version 1.2
    ```

    Enable the access to other Azure Services

    ```
    az sql server firewall-rule create --resource-group MyContainerAppResourceGroup  
        --server my-container-app-sql-server 
        --name AllowOtherAzureService 
        --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
    ```

3. Create the Azure SQL Database

    ```
    az sql db create --name my-container-app-sql-db
        --resource-group MyContainerAppResourceGroup
        --server my-container-app-sql-server
        --service-objective Basic
        --zone-redundant false
    ```

4. Create Azure Container Registry, where push the application image.

    ```
    az acr create --name MyPersonalAcr --resource-group MyContainerAppResourceGroup --sku Basic --admin-enabled true
    ```

5. For this example, use the admin credentials to push the image. For distributed and secure solutions, it is required a centralized user and permission management. 

   Configure the `appsettings.Development.json` file, build and push the docker image.

    ```
    docker build -t my-web-app-container -f Dockerfile --build-arg Environment=Development .

    docker login mypersonalacr.azurecr.io

    docker tag my-web-app-container mypersonalacr.azurecr.io/my-web-app-container

    docker push mypersonalacr.azurecr.io/my-web-app-container
    ```

6. Create App Service Plan. We start with Linux App Service Plan

    ```
    az appservice plan create --name MyLinuxAppServicePlan 
        --resource-group MyContainerAppResourceGroup 
        --is-linux 
        --sku FREE 
    ```

7. Create WebApp for container using the previous `MyLinuxAppServicePlan`. The name of the webapp is `MyTestWebApiContainerApp`

    ```
    az webapp create --resource-group MyContainerAppResourceGroup 
        --plan MyLinuxAppServicePlan 
        --name MyTestWebApiContainerApp 
        --deployment-container-image-name mypersonalacr.azurecr.io/my-web-app-container:latest
    ```

8. Setup the right connection string (again)

    ```
    az webapp config connection-string set --resource-group MyContainerAppResourceGroup  \
        --connection-string-type SQLAzure \
        --name MyTestWebApiContainerApp \ 
        --settings DefaultConnection='Server=tcp:my-container-app-sql-server.database.windows.net,1433;Initial Catalog=my-container-app-sql-db;Persist Security Info=False;User ID=sa01;Password=MyPassword123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
    ```
9. The result should be showed [here](https://mytestwebapicontainerapp.azurewebsites.net/swagger/index.html)

**It is not finishe yet !**

10. Create the application insight associated to the application, that keeps the log for 30 days.

    ```
    az monitor app-insights component create --app MyTestWebApiContainerApp 
        --location southeastasia 
        --resource-group MyContainerAppResourceGroup 
        --kind web 
        --application-type web 
        --retention-time 30
    ```

