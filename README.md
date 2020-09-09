# FPS-Counter
A Beat Saber mod to show your fps in-game.

## Installation

Below, you can find the few mods that this mods requires. You'll also find the versions that I used to test.

 - BeatSaberMarkupLanguage v1.3.4 or higher
 - SiraUtil v1.0.2 or higher
 - Counters+ v1.9.1 (Optional)  
   Because version 2.0.0 of Counters+ is still in development, FPS Counter currently can't integrate with that version and above for now. However, it's still possible to use both simultaneously. 

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
