#!/bin/bash
set -ev

source ./scripts/install.sh

# c# tests
dotnet test ./tests/Unit/Domain.Tests/Domain.Tests.csproj
dotnet test ./tests/Unit/Facade.Tests/Facade.Tests.csproj
dotnet test ./tests/Integration/Integration.Tests/Integration.Tests.csproj

echo $?
if [ $? -eq 1 ]; then
  echo dotnet test fail
  exit 1
fi

# javascript tests
cd ./src/Service.Host
./node_modules/.bin/jest
echo $?
result=$?
cd ../..

exit $result
