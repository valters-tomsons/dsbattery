# dsbattery
A controller utility for status bars that displays DUALSHOCK 4™ and DualSense™ controller battery levels.

![polybar-preview](https://i.imgur.com/9r9o1hP.png)

## Features

* Battery percentage of all connected controllers
* Indicators for charging devices
* Disconnect all controllers (with `-d` argument, or right-click in status bar)

## Usage

1. Install `dsbattery`
2. Add [provided modules](./modules/) to your statusbar configuration

## Install

*Note:* `bt-device` from `bluez-tools` is required for disconnect functionality

### Arch-based distributions

If you are using an Arch-based distribution, install [dsbattery](https://aur.archlinux.org/packages/dsbattery/) with your favorite AUR helper.

**Experimental:** Pacman binary releases are available at https://repo.tomsons.me/

### GitHub releases

Download the latest file under Releases for your architecture, extract and copy `dsbattery` to your desired location.

## Building

Requires `.NET 6.0 SDK`
