# FPS-Counter
A Beat Saber mod to show your fps in-game.

## Installation

Below, you can find the few mods that this mods requires. You'll also find the versions that I used to test.
  - BSIPA v4.2.2 or higher
  - BeatSaberMarkupLanguage v1.6.3 or higher
  - SiraUtil v3.0.6 or higher
  - Counters+ v2.0.0 or higher (Optional, use v2.3.0 or higher for multiplayer integration)

Since version 3.0.0 of FPS Counter, support for Counters+ v1.x.x integration has been dropped because it was simply not possible.
This doesn't mean that both mods can't be used simultaneously, however all the extra features that come along with the Counters+ integration won't be available.

If you still insist on keeping Counters+ 1.x.x and want to preserve the FPS Counter integration, I recommend you to stay on version 2.2.6 of FPS Counter for a little while longer.
However, as my time still is limited, keep in mind that I won't be offering support for the older 2.x.x versions.

The installation itself is fairly simple.

 1) Grab the latest plugin release from [the releases page](https://github.com/ErisApps/FPS-Counter/releases)
 2) Drop the .dll file in the Plugins folder of your Beat Saber installation.
 3) Boot it up (or reboot)

## For developers

### Contributing to FPS-Counter
In order to build this project, please create the file `FPS Counter.csproj.user` and add your Beat Saber directory path to it in the project directory.
This file should not be uploaded to GitHub and is in the .gitignore.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Set "YOUR OWN" Beat Saber folder here to resolve most of the dependency paths! -->
    <BeatSaberDir>D:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
  </PropertyGroup>
</Project>
```

If you plan on adding any new dependencies which are located in the Beat Saber directory, it would be nice if you edited the paths to use `$(BeatSaberDir)` in `FPS Counter.csproj`

```xml
...
<Reference Include="BS_Utils">
  <HintPath>$(BeatSaberDir)\Plugins\BS_Utils.dll</HintPath>
</Reference>
<Reference Include="IPA.Loader">
  <HintPath>$(BeatSaberDir)\Beat Saber_Data\Managed\IPA.Loader.dll</HintPath>
</Reference>
...
```


## Credits

[**@DeadlyKitten**](https://github.com/DeadlyKitten) for creating the original mod

[**@Pespiri**](https://github.com/Pespiri) for updating the mod to the more recent BS 1.7.0 and making the Counters+ mod optional
