#!/bin/sh
dotnet publish -c Release -r linux-x64 --self-contained -o release
echo "dsbattery release created"