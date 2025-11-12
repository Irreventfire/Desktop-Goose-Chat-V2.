# Build Scripts Usage Guide

## Overview
This repository now includes automated build scripts that make compiling the Desktop Goose Chat V2 mod simple and straightforward.

## Quick Start

### Windows Users
Simply double-click `build.bat` or run from command prompt:
```cmd
build.bat
```

### Linux/Mac Users
Make the script executable (first time only) and run:
```bash
chmod +x build.sh
./build.sh
```

## What the Scripts Do

Both scripts perform the same operations:

1. **Check for Build Tools**
   - Windows: Checks for MSBuild
   - Linux/Mac: Checks for msbuild or xbuild (Mono)
   - Provides helpful error messages with installation instructions if tools are missing

2. **Clean Previous Build**
   - Removes old compiled files
   - Ensures fresh compilation

3. **Build in Release Mode**
   - Compiles the solution with optimizations
   - Creates production-ready DLL

4. **Show Results**
   - Displays build status (success/failure)
   - Shows exact location of compiled DLL
   - Provides installation instructions

## Output

After successful compilation, you'll find:
```
GooseMod_DefaultSolution/DefaultMod/bin/Release/DefaultMod.dll
```

## Installation

1. Copy `DefaultMod.dll` from the build output folder
2. Navigate to your Desktop Goose mods folder:
   - Windows: `%AppData%\SamPerson\DesktopGoose\Mods\ChatbotMod\`
   - Create the `ChatbotMod` folder if it doesn't exist
3. Paste the DLL file
4. Restart Desktop Goose

## Troubleshooting

### Windows: "MSBuild not found"
**Solution:** Install Visual Studio or Visual Studio Build Tools
- Download: https://visualstudio.microsoft.com/downloads/
- Install the ".NET desktop development" workload

### Linux/Mac: "Neither msbuild nor xbuild found"
**Solution:** Install Mono

**Ubuntu/Debian:**
```bash
sudo apt-get update
sudo apt-get install mono-complete
```

**macOS:**
```bash
brew install mono
```

**Other distributions:**
Visit: https://www.mono-project.com/download/stable/

### Build Fails with Errors
1. Make sure all prerequisites are installed
2. Check that you have .NET Framework 4.5.2 or higher
3. Ensure the repository is fully cloned (not just downloaded as ZIP without git)
4. Try cleaning the solution manually and rebuilding:
   ```
   cd GooseMod_DefaultSolution
   # Windows:
   msbuild GooseMod.sln /t:Clean
   msbuild GooseMod.sln /t:Build /p:Configuration=Release
   
   # Linux/Mac:
   msbuild GooseMod.sln /t:Clean
   msbuild GooseMod.sln /t:Build /p:Configuration=Release
   ```

## Script Features

### Error Handling
- Both scripts check for prerequisites before building
- Provide clear error messages if something goes wrong
- Exit cleanly without breaking your system

### User-Friendly Output
- Clear section headers
- Progress indicators
- Success/failure messages
- Installation instructions

### Cross-Platform Support
- Windows batch script for cmd.exe
- Unix shell script for bash
- Compatible with PowerShell (via cmd call)
- Works with various Linux distributions and macOS

## Advanced Usage

### Custom Build Configuration
To build in Debug mode instead, edit the script:

**Windows (build.bat):**
Change:
```batch
msbuild GooseMod.sln /t:Build /p:Configuration=Release /v:minimal
```
To:
```batch
msbuild GooseMod.sln /t:Build /p:Configuration=Debug /v:minimal
```

**Linux/Mac (build.sh):**
Change:
```bash
$BUILD_CMD GooseMod.sln /t:Build /p:Configuration=Release /v:minimal
```
To:
```bash
$BUILD_CMD GooseMod.sln /t:Build /p:Configuration=Debug /v:minimal
```

### Verbose Output
For detailed build logs, change `/v:minimal` to `/v:detailed` in the build commands.

## Benefits Over Manual Building

1. **Consistency**: Same process every time
2. **Speed**: One command vs multiple steps
3. **Error Detection**: Checks prerequisites automatically
4. **Documentation**: Clear output guides you through issues
5. **Cross-Platform**: Works on Windows, Linux, and macOS

## Support

If you encounter issues not covered in this guide:
1. Check the main README.md for detailed compilation instructions
2. Review FIX_SUMMARY.md for recent changes
3. Open an issue on GitHub with your build output

---

Happy building! 🦢
