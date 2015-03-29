@echo off
setlocal
set ProgRoot=%ProgramFiles%
if not "%ProgramFiles(x86)%" == "" set ProgRoot=%ProgramFiles(x86)%

FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "HKEY_USERS\.DEFAULT\Software\Microsoft\VisualStudio\12.0_Config" /v ShellFolder') DO SET vs12dir=%%B
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "HKEY_USERS\.DEFAULT\Software\Microsoft\WDExpress\12.0_Config" /v ShellFolder') DO SET vs12dir=%%B
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "HKEY_USERS\.DEFAULT\Software\Microsoft\VisualStudio\11.0_Config" /v ShellFolder') DO SET vs11dir=%%B
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "HKEY_USERS\.DEFAULT\Software\Microsoft\WDExpress\11.0_Config" /v ShellFolder') DO SET vs11dir=%%B
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "HKEY_USERS\.DEFAULT\Software\Microsoft\VisualStudio\10.0_Config" /v ShellFolder') DO SET vs10dir=%%B

if EXIST "%vs12dir%\vc\vcvarsall.bat" goto vs2012
if EXIST "%ProgRoot%\Microsoft Visual Studio 12.0\vc\vcvarsall.bat" goto vs2012pf
if EXIST "%vs11dir%\vc\vcvarsall.bat" goto vs2011
if EXIST "%ProgRoot%\Microsoft Visual Studio 11.0\vc\vcvarsall.bat" goto vs2011pf
if EXIST "%vs10dir%\vc\vcvarsall.bat" goto vs2010
if EXIST "%ProgRoot%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat" goto vs2010pf
goto error_msg

:vs2012pf
  SET vs12dir = %ProgRoot%\Microsoft Visual Studio 12.0\
:vs2012
  echo Visual Studio 2013 build environment
  call "%vs12dir%\vc\vcvarsall.bat" x86
  SET ENV=VS2013
  goto start_nant

:vs2011pf
  SET vs11dir = %ProgRoot%\Microsoft Visual Studio 11.0\
:vs2011
  echo Visual Studio 2012 build environment
  call "%vs11dir%\vc\vcvarsall.bat" x86
  SET ENV=VS2012
  goto start_nant

:vs2010pf
  SET vs10dir = %ProgRoot%\Microsoft Visual Studio 10.0\
:vs2010
  echo Visual Studio 2010 build environment
  call "%vs10dir%\vc\vcvarsall.bat" x86
  SET ENV=VS2010
  goto start_nant

:vs2008pf
  SET vs9dir = %ProgRoot%\Microsoft Visual Studio 9.0\
:vs2010
  echo Visual Studio 2008 build environment
  call "%vs9dir%\vc\vcvarsall.bat" x86
  SET ENV=VS2008
  goto start_nant
  goto pause

:start_nant
if NOT EXIST "../build" goto error_msg_working_dir
set PATH=../lib/nant-0.91/bin;../Tools/bin;%PATH%

if "%1"=="" (
  set FIRST_TARGET=build
) ELSE (
  set FIRST_TARGET=%1
)

echo calling nant with %ENV% %FIRST_TARGET% %2 %3 %4 %5 %6
nant -buildfile:ANX.Framework.extended.build %ENV% %FIRST_TARGET% %2 %3 %4 %5 %6
goto pause

:error_msg
echo Couldn't find Visual Studio 2010 or 2012 or 2013. Exiting.
goto pause

:error_msg_working_dir
echo Please start MakeAnxFramework from the build directory. (use CD to change directory)
goto pause

:pause
pause