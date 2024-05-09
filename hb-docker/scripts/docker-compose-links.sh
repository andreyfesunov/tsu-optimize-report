#!/bin/bash

# Set the locale to Russian
export LC_ALL=ru_RU.UTF-8

# Get the absolute path of the script's directory
parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" && pwd -P )

backend_folder="$parent_path/../../hb-back"
solution_file="./BackendBase.sln"
echo "$backend_folder"

cd $backend_folder

sudo sh -c "ln -s ../hb-docker/docker-compose.yml docker-compose.yml; ln -s ../hb-docker/docker-compose.override.yml docker-compose.override.yml"