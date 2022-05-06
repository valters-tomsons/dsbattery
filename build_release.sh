#!/bin/sh
ARCH="linux-x64"

dotnet publish -c Release -r $ARCH --self-contained -o release_build
cp modules release_build/ -r
cp README.md release_build/
echo "dsbattery release created for $ARCH"
