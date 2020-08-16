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

    `az group create --location southeastasia --name MyContainerAppResourceGroup`

2. Create Azure Container Registry, where push the application image.

    `az acr create --name MyPersonalACR --resource-group MyContainerAppResourceGroup --sku Basic`

3. Push the docker image created with the description above

    `docker tag <YOUR_IMAGE_TAG> ...`

4. Create the Azure SQL Server

5. Create the Azure SQL Database


6. Create App Service Plan. We start with Linux App Service Plan

    `az appservice plan create --name MyLinuxAppServicePlan `
        ` --resource-group MyContainerAppResourceGroup`
        ` --is-linux --sku FREE `

7. Create WebApp for container using the previous `MyLinuxAppServicePlan`. The name of the webapp is `MyTestWebApiContainerApp`

    `az webapp create --resource-group MyContainerAppResourceGroup `
        `--plan MyLinuxAppServicePlan `
        `--name MyTestWebApiContainerApp `
        `--deployment-container-image-name myregistry.azurecr.io/docker-image:tag`



Create App Service Plan. Window$ here