#!/bin/sh
ARCH="linux-x64"

dotnet publish -c Release -r $ARCH --self-contained -o release_build
echo "dsbattery release created for $ARCH"