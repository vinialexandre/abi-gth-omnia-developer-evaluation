#!/bin/bash

echo "Install tools if not present"
dotnet tool install --global coverlet.console
dotnet tool install --global dotnet-reportgenerator-globaltool

echo "Clean and build solution"
dotnet restore
dotnet build  Ambev.DeveloperEvaluation.sln --configuration Release --no-restore

echo "Run tests with coverage"
dotnet test  Ambev.DeveloperEvaluation.sln --no-restore --verbosity normal \
/p:CollectCoverage=true \
/p:CoverletOutputFormat=cobertura \
/p:CoverletOutput=./TestResults/coverage.cobertura.xml \
/p:Exclude="[*]*.Program,[*]*.Startup,[*]*.Migrations.*"

echo "Generate coverage report"
reportgenerator \
-reports:"./tests/**/TestResults/coverage.cobertura.xml" \
-targetdir:"./TestResults/CoverageReport" \
-reporttypes:Html

echo "Removing temporary files"
rm -rf bin obj

echo ""
echo "Coverage report generated at TestResults/CoverageReport/index.html"
pause
