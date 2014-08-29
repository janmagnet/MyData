@echo off

if not exist backup mkdir backup
if exist *.nupkg move /Y *.nupkg backup

nuget pack -build MyData.csproj -properties Configuration=Release

pause