# Azure Serverless API #

ASP.NET Core WebApi sample application.

It is designed as simple monolithic application due to simplify the understanding using the different layers instead of using a micro-service application design that implies the other external knowledges which are not in the scope and purpose of this example.

## During the development ## 

Using docker to start a new instance of PostgreSQL 12, or install a database server on local PC.

Run the database server: `docker run -p 5432:5432 -e POSTGRES_PASSWORD=MyPassword postgres:12`

**Note**: In this example the database hasn't the persistent volume, so all the times it start fresh. If you want add the persistent storage attached with a local folder, please follow the instruction on [PostgreSQL Docker Hub page](https://hub.docker.com/_/postgres).

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

Point missing:
 * application should wait that the database is up and running
 * custom script or command line to wait the database before start the application

## Deployment ##