@echo off
setlocal
set ProgRoot=%ProgramFiles%
if not "%ProgramFiles(x86)%" == "" set ProgRoot=%ProgramFiles(x86)%

if EXIST "%ProgRoot%\Microsoft Visual Studio 11.0\vc\vcvarsall.bat" goto vs2011
if EXIST "%ProgRoot%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat" goto vs2010
goto error_msg

:vs2011
  echo Visual Studio 2011 build environment
  call "%ProgRoot%\Microsoft Visual Studio 11.0\vc\vcvarsall.bat" x86
  SET ENV=VS2011
  goto start_nant

:vs2010
  echo Visual Studio 2010 build environment
  call "%ProgRoot%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat" x86
  SET ENV=VS2010
  goto start_nant

:start_nant
set PATH=../lib/nant-0.91/bin;../Tools/bin;%PATH%

if "%1"=="" (
  set FIRST_TARGET=build
) ELSE (
  set FIRST_TARGET=%1
)

echo calling nant with %ENV% %FIRST_TARGET% %2 %3 %4 %5 %6
nant %ENV% %FIRST_TARGET% %2 %3 %4 %5 %6
goto pause

:error_msg
echo Couldn't find Visual Studio 2010 or 2011. Exiting.

:pause
pause