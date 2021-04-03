# dsbattery
Small linux utility to display connected DualShock 4 battery indicators in status bars.

![polybar-preview](https://i.imgur.com/9r9o1hP.png)

![waybar-preview](https://i.imgur.com/GXa37M9.png)

![charging-icon](https://i.imgur.com/62s66R7.png)

## Features

* Show battery percentage of all connected Sony controllers
* Provide indicator for charging devices
* Disconnect all sony bluetooth controllers (by passing `-d` argument)

## Usage

1. Install dsbattery to `$PATH`
2. Add [provided modules](./modules/) to your statusbar configuration

## Dependencies

* `bt-device` - for disconnect functionality

## Building

Requires `.NET 5.0 SDK`
