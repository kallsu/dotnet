FROM mcr.microsoft.com/dotnet/core/sdk:3.1

ARG Environment
ARG Runtime

# System configuration
RUN mkdir -p /home/web-api && \
    mkdir /home/web-api-build && \
    mkdir -p /var/www-root

# Copy
WORKDIR /home/web-api

COPY . .

# compile
RUN dotnet restore src/Azure.Web.Api && \
    dotnet publish src/Azure.Web.Api -f netcoreapp3.1 -c Release -r linux-x64 --self-contained -o /home/web-api-build

# Deploy
WORKDIR /var/www-root

RUN cp -rv /home/web-api-build/* . && \ 
    rm -rf /home/web-api-build && \
    rm -rf /home/web-api

# Setup
ENV ASPNETCORE_ENVIRONMENT = $Environment
ENV ENV ASPNETCORE_URLS="https://+:80"

EXPOSE 80

CMD ["dotnet", "Azure.Web.Api.dll"]