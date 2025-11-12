# Desktop-Goose-Chat-V2.
Improved Version of the Desktop Goose mod chat expansion

# Desktop Goose Chat V2

Improved Version of the Desktop Goose mod chat expansion

## Compilation Instructions

### Prerequisites

Before compiling this mod, ensure you have the following installed:

1. **Visual Studio 2019 or later** (Community Edition or higher)
   - Download from: https://visualstudio.microsoft.com/downloads/
   
2. **Required Workloads:**
   - .NET desktop development
   
3. **Desktop Goose** installed on your system
   - Download from: https://samperson.itch.io/desktop-goose

### Step-by-Step Compilation Guide

#### 1. Clone the Repository

```bash
git clone https://github.com/Irreventfire/Desktop-Goose-Chat-V2.git
cd Desktop-Goose-Chat-V2
```

#### 2. Open the Project in Visual Studio

- Launch Visual Studio
- Click **File** → **Open** → **Project/Solution**
- Navigate to the cloned repository folder
- Select the `.sln` (solution) or `.csproj` (project) file and click **Open**

#### 3. Restore NuGet Packages

- Visual Studio should automatically restore any required NuGet packages
- If not, right-click on the solution in **Solution Explorer** and select **Restore NuGet Packages**

#### 4. Add Desktop Goose References

You'll need to reference the Desktop Goose assemblies:

- Right-click on **References** or **Dependencies** in the Solution Explorer
- Select **Add Reference**
- Click **Browse** and navigate to your Desktop Goose installation folder (typically `C:\Program Files\Desktop Goose\`)
- Add references to the required DLLs (e.g., `DesktopGoose.exe` or any mod API DLLs)

#### 5. Configure Build Settings

- In the toolbar, select your build configuration:
  - **Debug** - for development and testing
  - **Release** - for final distribution
- Select the platform (typically **Any CPU** or **x64**)

#### 6. Build the Project

**Option A: Build via Menu**
- Click **Build** → **Build Solution** (or press `Ctrl+Shift+B`)

**Option B: Build via Solution Explorer**
- Right-click on the project name in Solution Explorer
- Select **Build**

#### 7. Locate the Compiled Files

After a successful build, the compiled DLL will be in:
- **Debug builds:** `bin\Debug\`
- **Release builds:** `bin\Release\`

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
