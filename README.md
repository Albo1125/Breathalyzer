# Breathalyzer
Breathalyzer is a UK-based resource for FiveM by Albo1125 that provides breath testing functionality for drivers.

## Installation & Usage
1. Download the latest release.
2. Unzip the Breathalyzer folder into your resources folder on your FiveM server.
3. Add the following to your server.cfg file:
```text
ensure Breathalyzer
```
4. Optionally, customise the commands and alcohol limit in `sv_Breathalyzer.lua`.
5. Optionally, enable and customise the whitelist in `vars.lua`.

## Commands & Controls
* /breatha ID - Breathalyzes the player with ID. Aliases /breathalyze /breathalyse
* /breath READING - Gives a breath test of READING if currently being breathalyzed.
* /failprovide - Fail to provide a proper sample if currently being breathalyzed.

## Improvements & Licencing
Please view the license. Improvements and new feature additions are very welcome, please feel free to create a pull request. As a guideline, please do not release separate versions with minor modifications, but contribute to this repository directly. However, if you really do wish to release modified versions of my work, proper credit is always required and you should always link back to this original source and respect the licence.

## Libraries used (many thanks to their authors)
* [CitizenFX.Core.Client](https://www.nuget.org/packages/CitizenFX.Core.Client)
