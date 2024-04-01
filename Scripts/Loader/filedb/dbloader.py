import os

# Output root directory
output_root = "/mnt/chromeos/removable/stf/names"

# Function to sanitize a string for use as a file/directory name
def sanitize_filename(name):
    # Replace any non-alphanumeric character with a plus sign
    sanitized_name = ''.join(c if c.isalnum() else '+' for c in name)
    # Remove leading and trailing plus signs
    sanitized_name = sanitized_name.strip('+')
    return sanitized_name

# Function to determine the folder name for a given name
def get_folder_name(name):
    # Get the first character of the name
    first_char = name[0].lower()
    # Check if it's alphabetic
    if first_char.isalpha():
        return first_char  # Return the first character if it's alphabetic
    else:
        return "other"  # Return "other" if the first character is not alphabetic

# Loop through files matching the pattern "names_<type>"
for filename in os.listdir("."):
    # Check if the file name starts with "names_"
    if filename.startswith("names_"):
        # Extract the type from the filename
        type = filename.split("_")[1]
        type = type.replace(".txt", "")

        # Create directory for the type if it doesn't exist
        os.makedirs(os.path.join(output_root, type), exist_ok=True)
        count = 0
        # Read each line in the file
        with open(filename, "r", encoding="utf-8") as file:
            
            for line in file:
                # Replace space with dash, comma with underscore, and / with dash
                formatted_line = line.strip().replace(" ", "-").replace(",", "_").replace("/", "-")
                
                                # Extract first and last names
                first_name, last_name = formatted_line.split("_")

                # Remove any non-alphanumeric characters from the name
                first_name = sanitize_filename(first_name)
                last_name = sanitize_filename(last_name)

                # Create YAML content
                yaml_content = f"firstName: {first_name}\nlastName: {last_name}\ntype: {type}"

                # Determine folder for sorting
                first_char = get_folder_name(first_name)
                folder = os.path.join(output_root, type, first_char)

                # Create directory if it doesn't exist
                os.makedirs(folder, exist_ok=True)

                count = count+1
                if count % 1000 == 0:
                    print("\r" + str(count))

                # Write YAML content to file in the appropriate directory
                with open(os.path.join(folder, f"{first_name}_{last_name}.yaml"), "w", encoding="utf-8") as yaml_file:
                    yaml_file.write(yaml_content)
