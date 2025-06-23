@echo off

setlocal enabledelayedexpansion

set /p version=<..\VERSION
set "output_dir=..\out"
set "release_dir=%output_dir%\release"
set "knee_surgery_publish_dir=%output_dir%\KneeSurgery"

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

echo Restoring KneeSurgery.sln...

dotnet restore ..\src\KneeSurgery.sln

if %ERRORLEVEL% neq 0 (
    echo Restore of KneeSurgery.sln failed.
    exit /b %ERRORLEVEL%
)

echo Checking for outdated packages...

powershell -command "$output = dotnet list ..\src\KneeSurgery.sln package --outdated --format json 2>$null | ConvertFrom-Json -ErrorAction SilentlyContinue; if ($output.projects.frameworks.topLevelPackages.Count -gt 0) { Write-Host 'Outdated packages found.' -ForegroundColor Red; exit 1 } else { Write-Host 'No outdated packages found.' -ForegroundColor Green }"

if %ERRORLEVEL% neq 0 (
    exit /b %ERRORLEVEL%
)

@REM echo Testing KneeSurgery...

@REM dotnet test ..\src\KneeSurgery.Tests\KneeSurgery.Tests.csproj

@REM if %ERRORLEVEL% neq 0 (
@REM     echo Test of KneeSurgery failed.
@REM     exit /b %ERRORLEVEL%
@REM )

echo Building KneeSurgery...

dotnet publish ..\src\KneeSurgery\KneeSurgery.csproj -p:PublishProfile=FolderProfile -p:Version=%version%

if %ERRORLEVEL% neq 0 (
    echo Build of KneeSurgery failed.
    exit /b %ERRORLEVEL%
)

del /f /q "%knee_surgery_publish_dir%\*.pdb"

echo Archiving KneeSurgery...

powershell Compress-Archive -Path "%knee_surgery_publish_dir%\*" -DestinationPath "%knee_surgery_publish_dir%_%version%.zip" -Force

if %ERRORLEVEL% neq 0 (
    echo Archiving of KneeSurgery failed.
    exit /b %ERRORLEVEL%
)

mkdir "%release_dir%"

move /y "%knee_surgery_publish_dir%_%version%.zip" "%release_dir%\KneeSurgery_%version%.zip"

endlocal