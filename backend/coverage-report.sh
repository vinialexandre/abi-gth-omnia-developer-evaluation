#!/bin/bash

echo "🛠️ Installing tools if not present..."
dotnet tool install --global coverlet.console --version 6.0.4 --ignore-failed-sources
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.4.5 --ignore-failed-sources

export PATH="$HOME/.dotnet/tools:$PATH"

echo "🧹 Cleaning and building solution..."
dotnet restore Abi.DeveloperEvaluation.sln
dotnet build Abi.DeveloperEvaluation.sln --configuration Release --no-restore

echo "✅ Running tests with coverage..."
coverlet ./src/Abi.DeveloperEvaluation.Unit/bin/Release/net9.0/Abi.DeveloperEvaluation.Unit.dll \
  --target "dotnet" \
  --targetargs "test src/Abi.DeveloperEvaluation.Unit/Abi.DeveloperEvaluation.Unit.csproj --no-build --configuration Release" \
  --format cobertura \
  --output ./src/Abi.DeveloperEvaluation.Unit/TestResults/coverage.cobertura.xml \
  --exclude "[*]*.Program" "[*]*.Startup" "[*]*.Migrations.*"

echo "📊 Generating coverage report..."
reportgenerator \
  -reports:./src/Abi.DeveloperEvaluation.Unit/TestResults/coverage.cobertura.xml \
  -targetdir:./src/Abi.DeveloperEvaluation.Unit/TestResults/CoverageReport \
  -reporttypes:Html

echo "🧽 Cleaning temporary files..."
rm -rf bin obj

echo ""
echo "✅ Relatório gerado em: /Abi.DeveloperEvaluation.Unit/TestResults/CoverageReport/index.html"
read -p "Pressione Enter para continuar..."
