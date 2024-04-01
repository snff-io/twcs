#!/bin/bash

# Output root directory
output_root="/mnt/chromeos/removable/stf/names"

# Function to determine the folder name for a given name
get_folder_name() {
    first_char=$(echo "$1" | cut -c1 | tr '[:upper:]' '[:lower:]')
    if [[ "$first_char" =~ [a-z] ]]; then
        echo "$first_char"
    else
        echo "other"
    fi
}

# Loop through files matching the pattern "names_<type>"
for file in names_*; do
    # Extract the type from the filename
    type=$(echo "$file" | cut -d'_' -f2)

    # Create directory if it doesn't exist
    mkdir -p "$output_root/$type"

    # Read each line in the file
    while IFS= read -r line; do
        # Replace space with dash, comma with underscore, and / with dash
        formatted_line=$(echo "$line" | tr ' ' '-' | tr ',' '_' | tr '/' '-')

        # Extract first and last names
        first_name=$(echo "$formatted_line" | cut -d'_' -f1)
        last_name=$(echo "$formatted_line" | cut -d'_' -f2)

        # Create YAML content
        yaml_content="firstName: $first_name\nlastName: $last_name\ntype: $type"

        # Write YAML content to file
        echo -e "$yaml_content" > "$output_root/$type/${first_name}_${last_name}.yaml"

        # Determine folder for sorting
        first_char=$(get_folder_name "$first_name")
        folder="$output_root/$type/$first_char"
        
        # Create directory if it doesn't exist
        mkdir -p "$folder"

    done < "$file"
    
    # Move files to the appropriate folders
    for yaml_file in "$output_root/$type"/*.yaml; do
        if [ -f "$yaml_file" ]; then
            filename=$(basename "$yaml_file")
            first_char=$(get_folder_name "${filename%%_*}")
            mv "$yaml_file" "$output_root/$type/$first_char/"
        fi
    done
done
