@echo off

echo This will push all *.nupkg files to my own feed
echo.
pause
echo.

nuget push *.nupkg -s http://to_be_determined/nuget/api/v2/package
echo.
pause