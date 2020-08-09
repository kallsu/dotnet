# Azure Serverless API #

ASP.NET Core WebApi sample application.

It is designed as simple monolithic application due to simplify the understanding using the different layers instead of using a micro-service application design that implies the other external knowledges which are not in the scope and purpose of this example.

## Run Locally ## 

Using docker to start a new instance of PostgreSQL 12, or install a database server on local PC.

Run the database server: `docker run -p 5432:5432 -e POSTGRES_PASSWORD=MyPassword postgres:12`

**Note**: In this example the database hasn't the persistent volume, so all the times it start fresh. If you want add the persistent storage attached with a local folder, please follow the instruction on [PostgreSQL Docker Hub page](https://hub.docker.com/_/postgres).

After the database, please check the `ASPNETCORE_ENVIROMENT` variables and update, if it is required, the connection string.