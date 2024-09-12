FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY *.sln .
COPY Tsu.IndividualPlan.WebApi/*.csproj ./Tsu.IndividualPlan.WebApi/
COPY Tsu.IndividualPlan.Tests/*.csproj ./Tsu.IndividualPlan.Tests/
COPY Tsu.IndividualPlan.Domain/*.csproj ./Tsu.IndividualPlan.Domain/
COPY Tsu.IndividualPlan.Data/*.csproj ./Tsu.IndividualPlan.Data/
COPY Tsu.IndividualPlan.Transfer/*.csproj ./Tsu.IndividualPlan.Transfer/

RUN dotnet restore

COPY . .

WORKDIR /src/Tsu.IndividualPlan.WebApi/
RUN dotnet build "Tsu.IndividualPlan.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tsu.IndividualPlan.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsu.IndividualPlan.WebApi.dll"]
