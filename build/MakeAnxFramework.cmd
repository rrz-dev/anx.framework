@echo off
setlocal
set ProgRoot=%ProgramFiles%
if not "%ProgramFiles(x86)%" == "" set ProgRoot=%ProgramFiles(x86)%
call "%ProgRoot%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat" x86
set PATH=../lib/nant-0.91/bin;../Tools/bin;%PATH%
nant %1 %2 %3 %4 %5 %6
pause