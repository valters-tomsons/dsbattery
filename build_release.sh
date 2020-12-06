#!/bin/sh
dotnet publish -c Release --self-contained -o release
echo "dsbattery release created"