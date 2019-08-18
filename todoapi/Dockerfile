FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /usr/src/app
RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
    
ENV DOTNET_USE_POLLING_FILE_WATCHER 1

COPY todos.csproj .
COPY Directory.Build.props .
RUN dotnet restore

ENTRYPOINT dotnet watch run --no-restore