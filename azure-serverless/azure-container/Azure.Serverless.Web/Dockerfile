FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Parameters
ARG Environment=Docker

# System set-up
RUN mkdir -p /home/azure-serverless/web && \
 mkdir -p /var/www/web

# Compile
WORKDIR /home/azure-serverless/web

COPY . .

RUN dotnet publish Azure.Serverless.Web.csproj -o /var/www/web -c Release -f netcoreapp3.1 -r linux-x64 --self-contained

# Preparation
WORKDIR /var/www/web

# Environment variables
ENV ASPNETCORE_ENVIRONMENT=$Environment
ENV ASPNETCORE_URLS="http://+:5001"

EXPOSE 5001

# cleaning
RUN rm -rf /home/azure-serverless/web

# --cpus="0.5"
# --memory=1024m
CMD ["dotnet", "Azure.Serverless.Web.dll"]