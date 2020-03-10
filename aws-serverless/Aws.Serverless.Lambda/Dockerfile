FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# System configuration
RUN apt-get update && \
 apt-get install -y zip && \ 
 useradd -u 111 -M -r -s /bin/bash jenkins
 

# Create home and CHROOT on that
RUN mkdir -p /home/dtac-ev/lambdas && \
 mkdir -p /home/dtac-ev/deploy
 
WORKDIR /home/dtac-ev/lambdas

# Copy files
COPY . .

# Compile
RUN export PATH="$PATH:/root/.dotnet/tools" && \
 dotnet tool install -g Amazon.Lambda.Tools && \
 dotnet lambda package -pl src/Dtac.Ev.Lambdas -o /home/dtac-ev/deploy/dtac-ev-lambdas.zip && \
 chown jenkins /home/dtac-ev/deploy/dtac-ev-lambdas.zip

CMD cp -v /home/dtac-ev/deploy/dtac-ev-lambdas.zip /mnt