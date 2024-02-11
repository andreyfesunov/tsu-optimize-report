#!/bin/bash

# Get the absolute path of the script's directory
parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" && pwd -P )

# Specify the source file and destination directory
source_file="$parent_path/pre-commit"
dst="$parent_path/../.git/hooks"

# Print information for debugging
echo "Source file: $source_file"
echo "Destination directory: $dst"

# Ensure the source file exists
if [ ! -e "$source_file" ]; then
  echo "Error: The source file '$source_file' does not exist."
  exit 1
fi

# Ensure the destination directory exists
mkdir -p "$dst"

# Create the symbolic link
ln -s -f "$source_file" "$dst"

echo "Success! Press any key to continue..."

# Optionally, you may remove the 'read' command if you don't need the pause
read