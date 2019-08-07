@echo off

set generateScript=%~dp0\generate.bat
pushd ".\bin\Debug\netcoreapp2.1"

call %generateScript%

popd

pause