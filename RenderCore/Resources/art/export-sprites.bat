@echo off

rem call :exportmap sample_tree_map
rem call :exportmap square

for /r %%v in (*.aseprite) do (
	call :exportmap "%%~nv"
)

goto :eof

:exportmap

set map=%1

echo Processing %map%
aseprite -b %map%.aseprite --save-as %map%-{layer}.png --data %map%.json --list-layers --format json-array --sheet %map%.png

goto :eof