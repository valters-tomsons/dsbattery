# controllerbattery
simple script that displays selected wireless controller battery to be used as module for system bars

![](https://i.imgur.com/9r9o1hP.png)

# Usage
Change your controller path name in `Program.cs`, line 11 (see `upower -e`)

Change your controller device name in module.

If you use polybar or wayland, see their respective *.module file.

# Dependencies
.NET Core 2.2

upower

bt-device

# Building
Install `.NET Core SDK 2.2`

Build project with `-c Release`
