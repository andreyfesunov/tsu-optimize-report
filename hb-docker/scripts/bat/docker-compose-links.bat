@echo off

chcp 65001

set "parent_path=%~dp0"

set "backend_folder=%parent_path%\..\..\hb-back"
set "solution_file=BackendBase.sln"
echo %backend_folder%

cd /d "%backend_folder%"

REM Create symbolic links using mklink command
mklink docker-compose.yml "..\hb-docker\docker-compose.yml"
mklink docker-compose.override.yml "..\hb-docker\docker-compose.override.yml"