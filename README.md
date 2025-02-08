# Unity Visual Studio Code Opener

This package allows you to explicitly specify which file types to open in Visual Studio Code, while keeping another default IDE configured in the External Tools settings.

Currently only works for Windows, but Linux and Mac support could come later.

<img src="https://github.com/user-attachments/assets/3a1288f6-4f09-4c13-834c-601db8e134b7" width="60%"/>

Installation
---
### Install via git URL

Requires a version of unity that supports path query parameter for git packages (Unity >= 2019.3.4f1, Unity >= 2020.1a21).

Add the following url to Package Manager:

`https://github.com/nathanAjacobs/UnityVisualStudioCodeOpener.git?path=src/VisualStudioCodeOpener/Assets/Plugins/VisualStudioCodeOpener`

![image](https://github.com/user-attachments/assets/1793d097-e196-44a2-b27e-476ff8fb72ad)

![image](https://github.com/user-attachments/assets/690fd6d8-30d3-419a-a08a-d0fe42f6b6a3)

Alternatively add the following line to `Packages/manifest.json`:

`"com.nathanajacobs.visual-studio-code-opener": "https://github.com/nathanAjacobs/UnityVisualStudioCodeOpener.git?path=src/VisualStudioCodeOpener/Assets/Plugins/VisualStudioCodeOpener"`

### Install via .unitypackage

Import asset package(`VisualStudioCodeOpener.*.*.*.unitypackage`) available in [UnityVisualStudioCodeOpener/releases](https://github.com/nathanAjacobs/UnityVisualStudioCodeOpener/releases).
