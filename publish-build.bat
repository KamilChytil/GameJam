@echo off

rem Prompt the user to enter the version

set /p version=Enter the version: 

rem Compress the directory into a zip file with the specified version

powershell Compress-Archive "Build\*" "Build_publish\Paradox_Agency_v%version%.zip" -Force

butler push "Build_publish\Paradox_Agency_v%version%.zip" nhykuu/paradox-agency:windows --userversion %version%