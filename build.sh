#!/bin/bash
# Build script for Desktop Goose Chat V2 Mod (Linux/Mac)
# This script compiles the mod using msbuild or xbuild

echo "========================================"
echo "Desktop Goose Chat V2 - Build Script"
echo "========================================"
echo ""

# Get the script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
SOLUTION_DIR="$SCRIPT_DIR/GooseMod_DefaultSolution"

# Check if msbuild is available (preferred)
if command -v msbuild &> /dev/null; then
    BUILD_CMD="msbuild"
    echo "Using msbuild..."
# Check if xbuild is available (fallback for older Mono)
elif command -v xbuild &> /dev/null; then
    BUILD_CMD="xbuild"
    echo "Using xbuild..."
else
    echo "ERROR: Neither msbuild nor xbuild found!"
    echo "Please install Mono or .NET SDK."
    echo ""
    echo "For Ubuntu/Debian:"
    echo "  sudo apt-get install mono-complete"
    echo ""
    echo "For macOS:"
    echo "  brew install mono"
    echo ""
    echo "For .NET SDK:"
    echo "  https://dotnet.microsoft.com/download"
    echo ""
    exit 1
fi

# Navigate to solution directory
cd "$SOLUTION_DIR"

# Clean previous build
echo "Cleaning previous build..."
$BUILD_CMD GooseMod.sln /t:Clean /p:Configuration=Release /v:minimal
if [ $? -ne 0 ]; then
    echo ""
    echo "ERROR: Clean failed!"
    exit 1
fi
echo ""

# Build the solution
echo "Building solution in Release mode..."
$BUILD_CMD GooseMod.sln /t:Build /p:Configuration=Release /v:minimal
if [ $? -ne 0 ]; then
    echo ""
    echo "ERROR: Build failed!"
    echo "Please check the error messages above."
    exit 1
fi

echo ""
echo "========================================"
echo "Build completed successfully!"
echo "========================================"
echo ""
echo "Output file location:"
echo "$SOLUTION_DIR/DefaultMod/bin/Release/DefaultMod.dll"
echo ""
echo "To install:"
echo "1. Copy DefaultMod.dll to your Desktop Goose mods folder"
echo "2. Mods folder location varies by platform"
echo "3. Restart Desktop Goose"
echo ""
