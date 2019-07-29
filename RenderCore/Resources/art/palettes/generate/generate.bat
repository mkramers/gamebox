@echo off

call :writeFile grey16.gpl 16
call :writeFile grey8.gpl 8

:goto eof

:writeFile

SETLOCAL EnableDelayedExpansion

set outputFile=%1
set levels=%2

echo GIMP Palette > %outputFile%
echo. >> %outputFile%

set /a max=255
set /a step=(%max% + 1) / %levels%

FOR /L %%i IN (%max%,-%step%,0) DO (
	ECHO %%i %%i %%i Untitled >> %outputFile%
)

ENDLOCAL

goto :eof