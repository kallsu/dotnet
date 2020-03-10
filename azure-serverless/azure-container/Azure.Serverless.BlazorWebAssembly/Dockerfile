FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Parameters
ARG Environment=Docker

# System set-up
RUN mkdir -p /home/azure-serverless/blazor && \
 mkdir -p /var/www/blazor

# Compile
WORKDIR /home/azure-serverless/blazor

COPY . .

RUN dotnet publish Azure.Serverless.BlazorWebAssembly.csproj -c Release -f netstandard2.1 -r linux-x64 --self-contained

# Preparation
WORKDIR /var/www/blazor

RUN cp -rv /home/azure-serverless/blazor/bin/Release/netstandard2.1/linux-x64/* .

# Environment variables
ENV ASPNETCORE_ENVIRONMENT=$Environment
ENV ASPNETCORE_URLS="http://+:5002"

EXPOSE 5002

# cleaning
RUN rm -rf /home/azure-serverless/blazor

CMD ["dotnet", "Azure.Serverless.BlazorWebAssembly.dll"]