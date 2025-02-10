# Unity Visual Studio Code Opener

Allows the ability to explicitly specify which file types to open in Visual Studio Code, while keeping another default code editor configured in Preferences -> External Tools.

Tested in Unity 2018.4.36 and Unity 6. It should work in Unity 2018.4.36 and up. It may work in earlier versions, however it has not been tested, so no guarantee.

Works on Windows, macOS, and Linux.
<br><br>

## Table of Contents
- [Installation](#installation)
- [Initial Setup](#initial-setup)
<br><br>

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
<br><br>
### Install via .unitypackage

Import asset package(`VisualStudioCodeOpener.*.*.*.unitypackage`) available in [UnityVisualStudioCodeOpener/releases](https://github.com/nathanAjacobs/UnityVisualStudioCodeOpener/releases).
<br><br>

Initial Setup
---

1. Open Preferences -> Visual Studio Code Opener

2. Browse for Visual Studio Code executable, these are common Visual Studio Code installation paths:<br><br>
   &nbsp;&nbsp;&nbsp;Windows:<br>
   &nbsp;&nbsp;&nbsp;`C:\Users\{user}\AppData\Local\Programs\Microsoft VS Code\Code.exe`

   &nbsp;&nbsp;&nbsp;Linux:<br>
   &nbsp;&nbsp;&nbsp;`/usr/share/code/code`

   &nbsp;&nbsp;&nbsp;macOS:<br>
   &nbsp;&nbsp;&nbsp;`/Applications/Visual Studio Code.app`<br><br>

3. Add desired file extensions to be opened with Visual Studio Code.<br><br>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="https://github.com/user-attachments/assets/3a1288f6-4f09-4c13-834c-601db8e134b7" width="60%"/>
