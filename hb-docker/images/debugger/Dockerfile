FROM mcr.microsoft.com/dotnet/sdk:6.0 AS debugger
RUN apt-get update && apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg