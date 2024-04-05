#!/bin/bash

# Target directory to lowercase directories within
target_directory="."

# Iterate over each item in the target directory
for dir in "$target_directory"/*; do
  # Check if the item is a directory
  if [ -d "$dir" ]; then
    # Extract basename and lowercase it
    dir_name=$(basename "$dir")
    lower_dir_name=$(echo "$dir_name" | tr '[:upper:]' '[:lower:]')
    # Check if the conversion is necessary
    if [ "$dir_name" != "$lower_dir_name" ]; then
      # Move (rename) the directory to its lowercase version
      mv "$dir" "$(dirname "$dir")/$lower_dir_name"
    fi
  fi
done