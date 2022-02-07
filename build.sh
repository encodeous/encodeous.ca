#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 6.0 -InstallDir ./dotnet-dir
./dotnet-dir/dotnet --version
./dotnet-dir/dotnet publish -c Release -o output