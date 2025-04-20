#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Abi.DeveloperEvaluation.WebApi/Abi.DeveloperEvaluation.WebApi.csproj", "src/Abi.DeveloperEvaluation.WebApi/"]
COPY ["src/Abi.DeveloperEvaluation.IoC/Abi.DeveloperEvaluation.IoC.csproj", "src/Abi.DeveloperEvaluation.IoC/"]
COPY ["src/Abi.DeveloperEvaluation.Domain/Abi.DeveloperEvaluation.Domain.csproj", "src/Abi.DeveloperEvaluation.Domain/"]
COPY ["src/Abi.DeveloperEvaluation.Common/Abi.DeveloperEvaluation.Common.csproj", "src/Abi.DeveloperEvaluation.Common/"]
COPY ["src/Abi.DeveloperEvaluation.Application/Abi.DeveloperEvaluation.Application.csproj", "src/Abi.DeveloperEvaluation.Application/"]
COPY ["src/Abi.DeveloperEvaluation.Infra/Abi.DeveloperEvaluation.Infra.csproj", "src/Abi.DeveloperEvaluation.Infra/"]
RUN dotnet restore "./src/Abi.DeveloperEvaluation.WebApi/Abi.DeveloperEvaluation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Abi.DeveloperEvaluation.WebApi"
RUN dotnet build "./Abi.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Abi.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abi.DeveloperEvaluation.WebApi.dll"]