# controllerbattery
simple script that displays selected wireless controller battery to be used as polybar module

![](https://i.imgur.com/9r9o1hP.png)

# Usage
Copy contents of `polybar.module` to your polybar config

Change your controller path name in `Program.cs`, line 11 (see `upower -e`)

Change your controller device name in module.

# Dependencies
.NET Core 2.2

upower

bt-device

# Building
Install `.NET Core SDK 2.2`

Build project with `-c Release`
