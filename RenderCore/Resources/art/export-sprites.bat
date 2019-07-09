@echo off

 call :exportmap sample_tree_map
 call :exportmap square

goto :eof

:exportmap

set map=%1

aseprite -b %map%.aseprite --save-as %map%-{layer}.png

goto :eof