#!/bin/sh
dotnet publish -c Release --self-contained -o controllerbattery
echo "Release created in 'controllerbattery'"
