---

Debug Method Plugin for BepInEx IL2CPP

This plugin is designed for IL2CPP games using BepInEx. It automatically logs method calls, records the last executed method before a game crash or closure, and tracks the addresses of these methods. Useful for debugging issues related to game crashes or sudden closures by identifying methods that were executed right before the event.

Features

Automatic Method Tracking: Hooks into game methods and logs each execution with its memory address.

Crash and Closure Logging: Captures and logs the last executed method in case of a crash or process exit.

File Logging: Saves all method execution logs, including addresses, to a DebugMethodLog.txt file for easy analysis.


Requirements

BepInEx IL2CPP: This plugin is built for IL2CPP games running under BepInEx.

Harmony: This plugin uses Harmony to patch methods.


Installation

1. Install BepInEx for IL2CPP:

Download and install BepInEx for IL2CPP in your game’s directory.



2. Download the Plugin:

Clone this repository or download the latest release as a .dll file.



3. Place the Plugin:

Copy the compiled .dll file into the BepInEx/plugins folder in your game directory.




Usage

Once the plugin is installed, it will automatically start logging method executions each time the game runs. Logs are saved in a DebugMethodLog.txt file, located in the same directory as the plugin.

Log File Location

The log file DebugMethodLog.txt is located in the BepInEx/plugins directory and will contain:

Method Execution Logs: Each method execution is logged with the method name and memory address.

Process Exit Logs: Logs the last executed method if the game closes normally.

Crash Logs: Logs the last executed method and exception details (if available) in case of a crash.



Example Log Output

Example entries in DebugMethodLog.txt:

Debug Method Plugin Log

Method executed: StartGame, Address: 0x7ff3a5d3e610
Method executed: LoadLevel, Address: 0x7ff3a5d3f870
Last executed method before exit: SaveProgress, Address: 0x7ff3a5d3a120

Unhandled exception occurred.
Last executed method before crash: RenderGraphics, Address: 0x7ff3a5d3b330
Exception: NullReferenceException
Stack Trace: at Game.RenderGraphics()

Contributing

Contributions are welcome! If you encounter issues or have ideas for improvement, please open an issue or submit a pull request.

License

This project is licensed under the MIT License.


---

This README provides all essential information for users to understand the plugin's purpose, installation, usage, and example output. Let me know if you'd like to add or change any sections!

