# Desktop-Goose-Chat-V2.
Improved Version of the Desktop Goose mod chat expansion

# Desktop Goose Chat V2

Improved Version of the Desktop Goose mod chat expansion

## Quick Compilation (Easy Method)

We provide automated build scripts for easy compilation:

### Windows
```cmd
build.bat
```

### Linux/Mac
```bash
./build.sh
```

The scripts will automatically:
- Clean previous builds
- Compile the mod in Release mode
- Show the output DLL location
- Display installation instructions

## Manual Compilation Instructions

### Prerequisites

Before compiling this mod, ensure you have the following installed:

1. **Windows:** Visual Studio 2019 or later (Community Edition or higher) or MSBuild
   - Download from: https://visualstudio.microsoft.com/downloads/
   
2. **Linux/Mac:** Mono (with msbuild or xbuild)
   - Ubuntu/Debian: `sudo apt-get install mono-complete`
   - macOS: `brew install mono`
   
3. **Required Workloads (Windows):**
   - .NET desktop development
   
4. **Desktop Goose** installed on your system
   - Download from: https://samperson.itch.io/desktop-goose

### Step-by-Step Compilation Guide

#### 1. Clone the Repository

```bash
git clone https://github.com/Irreventfire/Desktop-Goose-Chat-V2.git
cd Desktop-Goose-Chat-V2
```

#### 2. Build Using Scripts (Recommended)

Use the provided build scripts (see Quick Compilation section above).

#### 3. Or Open the Project in Visual Studio (Manual Method)

- Launch Visual Studio
- Click **File** → **Open** → **Project/Solution**
- Navigate to the cloned repository folder: `GooseMod_DefaultSolution`
- Select `GooseMod.sln` and click **Open**

#### 4. Restore NuGet Packages

- Visual Studio should automatically restore any required NuGet packages
- If not, right-click on the solution in **Solution Explorer** and select **Restore NuGet Packages**

#### 5. Add Desktop Goose References (if needed)

The project should already have references configured, but if you see build errors about missing references:

- Right-click on **References** or **Dependencies** in the Solution Explorer
- Select **Add Reference**
- Click **Browse** and navigate to your Desktop Goose installation folder
- Add references to the required DLLs if missing

#### 6. Configure Build Settings

- In the toolbar, select your build configuration:
  - **Debug** - for development and testing
  - **Release** - for final distribution
- Select the platform (typically **Any CPU** or **x64**)

#### 7. Build the Project

**Option A: Build via Menu**
- Click **Build** → **Build Solution** (or press `Ctrl+Shift+B`)

**Option B: Build via Solution Explorer**
- Right-click on the project name in Solution Explorer
- Select **Build**

**Option C: Build via Command Line**
```cmd
cd GooseMod_DefaultSolution
msbuild GooseMod.sln /p:Configuration=Release
```

#### 8. Locate the Compiled Files

After a successful build, the compiled DLL will be in:
- **Debug builds:** `GooseMod_DefaultSolution\DefaultMod\bin\Debug\DefaultMod.dll`
- **Release builds:** `GooseMod_DefaultSolution\DefaultMod\bin\Release\DefaultMod.dll`

### Installation

1. Copy the compiled `.dll` file from the build output folder
2. Navigate to your Desktop Goose mods folder:
   - Default location: `%AppData%\SamPerson\DesktopGoose\Mods\`
   - Or: `C:\Users\[YourUsername]\AppData\Roaming\SamPerson\DesktopGoose\Mods\`
3. Create a new folder for this mod (e.g., `DesktopGooseChat`)
4. Paste the compiled DLL into this folder
5. Restart Desktop Goose

### Troubleshooting

**Build Errors:**
- Ensure all Desktop Goose references are correctly added
- Check that you're using a compatible .NET Framework version
- Verify that all NuGet packages are restored

**Missing References:**
- If you see errors about missing assemblies, double-check the Desktop Goose installation path
- Make sure Desktop Goose is properly installed

**Runtime Errors:**
- Ensure the mod is placed in the correct mods folder
- Check Desktop Goose logs for error messages
- Verify compatibility with your Desktop Goose version

### Development Tips

- Use **Debug** configuration during development for easier debugging
- Set **Desktop Goose** as the startup program for easier testing:
  - Right-click project → **Properties** → **Debug**
  - Set the path to `DesktopGoose.exe`
- Use breakpoints to debug your mod while Desktop Goose is running

### Building for Distribution

When creating a release:

1. Switch to **Release** configuration
2. Build the solution
3. Copy the DLL from `bin\Release\`
4. Create a distributable package with:
   - The compiled DLL
   - Any required configuration files
   - Installation instructions
   - README with usage information

## License

[Add your license information here]

## Contributing

[Add contribution guidelines here]
```
