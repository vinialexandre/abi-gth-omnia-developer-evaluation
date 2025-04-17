@ECHO OFF
SETLOCAL

dotnet tool install --global coverlet.console --version 6.0.4 --ignore-failed-sources
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.4.5 --ignore-failed-sources

SET PATH=%USERPROFILE%\.dotnet\tools;%PATH%

dotnet restore Abi.DeveloperEvaluation.sln
dotnet build Abi.DeveloperEvaluation.sln --configuration Release --no-restore

REM 
coverlet ./src/Abi.DeveloperEvaluation.Unit/bin/Release/net9.0/Abi.DeveloperEvaluation.Unit.dll ^
  --target "dotnet" ^
  --targetargs "test src/Abi.DeveloperEvaluation.Unit/Abi.DeveloperEvaluation.Unit.csproj --no-build --configuration Release" ^
  --format cobertura ^
  --output ./../docs/coverage/coverage.cobertura.xml ^
  --exclude "[*]*.Program" "[*]*.Startup" "[*]*.Migrations.*"

reportgenerator ^
  -reports:./../docs/coverage/coverage.cobertura.xml ^
  -targetdir:./../docs ^
  -reporttypes:Html

rmdir /s /q bin 2>nul
rmdir /s /q obj 2>nul

echo.
echo ✅ Relatório gerado em: /docs/index.html
pause
