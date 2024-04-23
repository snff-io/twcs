#!/bin/bash

# Function to process each subdirectory
process_directory() {
    local dir="$1"
    local index_file="$2"

    cd "$dir" || exit 1

    # Check if description.md exists
    if [[ -f "description.md" ]]; then
        content=$(sed -n '6p' "description.md")
        echo "$dir: $content" >> "../$index_file"
    fi

    cd ..
}

# Main function to iterate through subdirectories
main() {
    local index_file="index.txt"
    > "$index_file" # Clearing index file if it exists

    for dir in */; do
        process_directory "$dir" "$index_file"
    done

    echo "Indexing complete. Results saved in $index_file"
}

main