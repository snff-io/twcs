
#!/bin/bash

# Set the output folder
output_folder="../../Library/images/landscapes"

# Find all urls.md files recursively
find . -name "urls.md" | while read -r url_file; do
    # Get the directory name
    directory=$(dirname "$url_file")
    # Read the URL from urls.md
    url=$(cat "$url_file")
    # Extract the filename from the URL
    filename=$(basename "$url")
    # Remove extension from the filename
    filename="${filename%.*}"
    # Download the image using wget
    wget -O "$output_folder/${directory##*/}_d2.jpeg" "$url"
done
