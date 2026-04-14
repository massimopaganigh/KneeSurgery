@echo off

setlocal enabledelayedexpansion

set /p version=<..\VERSION
set "output_dir=..\out"
set "release_dir=%output_dir%\release"

echo Cleaning directories...

for %%d in ("%output_dir%") do (
    if exist "%%d" (
        echo Cleaning %%d...
        rmdir /s /q "%%d"
    )
)

echo Cleaning .cr, .vs, bin and obj directories...

for /r "..\src\KneeSurgery" %%p in (.cr .vs bin obj) do (
    if exist "%%~p" (
        echo Cleaning "%%~p"...
        rd /s /q "%%~p"
    )
)

echo Restoring KneeSurgery.slnx...

dotnet restore ..\src\KneeSurgery.slnx

if %ERRORLEVEL% neq 0 (
    echo Restore of KneeSurgery.slnx failed.
    exit /b %ERRORLEVEL%
)

echo Checking for outdated packages...

powershell -command "$output = dotnet list ..\src\KneeSurgery.slnx package --outdated --format json 2>$null | ConvertFrom-Json -ErrorAction SilentlyContinue; if ($output.projects.frameworks.topLevelPackages.Count -gt 0) { Write-Host 'Outdated packages found.' -ForegroundColor Red; exit 1 } else { Write-Host 'No outdated packages found.' -ForegroundColor Green }"

if %ERRORLEVEL% neq 0 (
    exit /b %ERRORLEVEL%
)

@REM echo Testing KneeSurgery...

@REM dotnet test ..\src\KneeSurgery.Tests\KneeSurgery.Tests.csproj

@REM if %ERRORLEVEL% neq 0 (
@REM     echo Test of KneeSurgery failed.
@REM     exit /b %ERRORLEVEL%
@REM )

set "frameworks=net6.0 net7.0 net8.0 net9.0 net10.0"

for %%f in (%frameworks%) do (
    set "knee_surgery_publish_dir=%output_dir%\KneeSurgery_%%f"
    
    echo Building KneeSurgery_%%f...
    
    for %%i in ("%output_dir%") do set "abs_output_dir=%%~fi"

    set "abs_publish_dir=!abs_output_dir!\KneeSurgery_%%f"
    
    dotnet publish ..\src\KneeSurgery\KneeSurgery.csproj -f %%f -p:PublishDir="!abs_publish_dir!" -p:Version=%version% -c Release
    
    if !ERRORLEVEL! neq 0 (
        echo Build of KneeSurgery_%%f failed.
        exit /b !ERRORLEVEL!
    )
    
    echo Cleaning PDB files for %%f...

    del /f /q "!knee_surgery_publish_dir!\*.pdb" 2>nul
    
    echo Archiving KneeSurgery_%%f...
    
    powershell Compress-Archive -Path "!knee_surgery_publish_dir!\*" -DestinationPath "!knee_surgery_publish_dir!_%version%.zip" -Force
    
    if !ERRORLEVEL! neq 0 (
        echo Archiving of KneeSurgery_%%f failed.
        exit /b !ERRORLEVEL!
    )
    
    if not exist "%release_dir%" mkdir "%release_dir%"
    
    move /y "!knee_surgery_publish_dir!_%version%.zip" "%release_dir%\KneeSurgery_%%f_%version%.zip"
)

endlocal