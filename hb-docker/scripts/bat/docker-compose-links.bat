@echo off
REM Set the code page to handle Cyrillic characters
chcp 1251 > nul

REM Get the absolute path of the script's directory
for %%I in ("%~dp0.") do set "parent_path=%%~fI"

set "backend_folder=%parent_path%\..\..\..\hb-back"
set "solution_file=BackendBase.sln"
echo %backend_folder%

cd /d %backend_folder%

mklink /h "%~dp0\hb-back\hb-docker\docker-compose.yml" "..\hb-docker\docker-compose.yml"