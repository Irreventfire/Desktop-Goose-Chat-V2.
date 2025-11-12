@echo off
setlocal enabledelayedexpansion

echo ========================================
echo Desktop Goose Chat V2 - Build Script
echo ========================================
echo.

REM Try to locate MSBuild using vswhere (VS 2017 and later)
set "vswhere=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

if exist "%vswhere%" (
    echo Locating Visual Studio installation...
    for /f "usebackq tokens=*" %%i in (`"%vswhere%" -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
        set "VSINSTALLDIR=%%i"
    )
    
    if defined VSINSTALLDIR (
        echo Found Visual Studio at: !VSINSTALLDIR!
        
        REM Try Current\Bin location (VS 2019+)
        if exist "!VSINSTALLDIR!\MSBuild\Current\Bin\MSBuild.exe" (
            set "MSBUILD=!VSINSTALLDIR!\MSBuild\Current\Bin\MSBuild.exe"
        ) else if exist "!VSINSTALLDIR!\MSBuild\15.0\Bin\MSBuild.exe" (
            set "MSBUILD=!VSINSTALLDIR!\MSBuild\15.0\Bin\MSBuild.exe"
        )
    )
)

REM Fallback: Check common installation paths
if not defined MSBUILD (
    echo Checking fallback locations...
    
    if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
    )
)

REM Check if MSBuild was found
if not defined MSBUILD (
    echo.
    echo ERROR: MSBuild could not be found!
    echo.
    echo Please ensure you have Visual Studio installed with:
    echo   - .NET desktop development workload
    echo   - MSBuild component
    echo.
    echo Alternatively, run this script from:
    echo   - Developer Command Prompt for VS
    echo   - Developer PowerShell for VS
    echo.
    pause
    exit /b 1
)

echo Using MSBuild: !MSBUILD!
echo.

REM Find the solution file
for %%f in (*.sln) do set "SOLUTION=%%f"

if not defined SOLUTION (
    echo ERROR: No solution file (.sln) found in current directory!
    pause
    exit /b 1
)

echo Building solution: !SOLUTION!
echo Configuration: Release
echo.

REM Build the solution
"!MSBUILD!" "!SOLUTION!" /p:Configuration=Release /p:Platform="Any CPU" /m /v:minimal

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo BUILD FAILED!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo ========================================
echo BUILD SUCCESSFUL!
echo ========================================
pause
