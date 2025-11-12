@echo off
REM Build script for Desktop Goose Chat V2 Mod (Windows)
REM This script compiles the mod using MSBuild

echo ========================================
echo Desktop Goose Chat V2 - Build Script
echo ========================================
echo.

REM Check if MSBuild is available
where msbuild >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: MSBuild not found!
    echo Please install Visual Studio or Visual Studio Build Tools.
    echo Download from: https://visualstudio.microsoft.com/downloads/
    echo.
    pause
    exit /b 1
)

REM Navigate to solution directory
cd /d "%~dp0GooseMod_DefaultSolution"

REM Clean previous build
echo Cleaning previous build...
msbuild GooseMod.sln /t:Clean /p:Configuration=Release /v:minimal
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Clean failed!
    pause
    exit /b 1
)
echo.

REM Build the solution
echo Building solution in Release mode...
msbuild GooseMod.sln /t:Build /p:Configuration=Release /v:minimal
if %ERRORLEVEL% NEQ 0 (
    echo.
    echo ERROR: Build failed!
    echo Please check the error messages above.
    pause
    exit /b 1
)

echo.
echo ========================================
echo Build completed successfully!
echo ========================================
echo.
echo Output file location:
echo %~dp0GooseMod_DefaultSolution\DefaultMod\bin\Release\DefaultMod.dll
echo.
echo To install:
echo 1. Copy DefaultMod.dll to your Desktop Goose mods folder
echo 2. Mods folder location: %%AppData%%\SamPerson\DesktopGoose\Mods\ChatbotMod\
echo 3. Restart Desktop Goose
echo.
pause
