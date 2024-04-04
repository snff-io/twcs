import os
import sys
from PIL import Image
import qrcode

# Function to generate and save QR code image
def generate_qr_code(data, filename, size=24):
    qr = qrcode.QRCode(
        version=1,
        error_correction=qrcode.constants.ERROR_CORRECT_L,
        box_size=10,
        border=4,
    )
    qr.add_data(data)
    qr.make(fit=True)
    img = qr.make_image(fill_color="black", back_color="white")
    img = img.resize((size, size))
    img.save(filename)

# Function to overlay watermark on image
def overlay_watermark(image_filename, qr_code_filename, output_filename):
    # Open the original image
    image = Image.open(image_filename)

    # Open the QR code image
    qr_code = Image.open(qr_code_filename)

    # Resize QR code to desired size
    qr_code = qr_code.resize((24, 24))

    # Paste QR code onto original image at upper left corner
    image.paste(qr_code, (0, 0))

    # Save the resulting image with watermark (overwriting the original)
    image.save(output_filename)

# Function to process single file
def process_single_file(file_path):
    # Generate QR code
    generate_qr_code("worldcomputer.info", "qr_code.png")
    # Overlay watermark on the single image
    overlay_watermark(file_path, "qr_code.png", file_path)

# Function to process directory
def process_directory(directory):
    # Generate QR code
    generate_qr_code("worldcomputer.info", "qr_code.png")
    # Iterate over files in the directory
    for file_name in os.listdir(directory):
        if file_name.endswith(".png") or file_name.endswith(".jpg") or file_name.endswith(".jpeg"):
            file_path = os.path.join(directory, file_name)
            # Overlay watermark on each image
            overlay_watermark(file_path, "qr_code.png", file_path)

# Main function
def main():
    if len(sys.argv) < 2:
        print("Usage: python watermark.py <image_file_or_directory>")
        sys.exit(1)

    path = sys.argv[1]

    if os.path.isfile(path):
        process_single_file(path)
    elif os.path.isdir(path):
        process_directory(path)
    else:
        print("Invalid path.")
        sys.exit(1)

if __name__ == "__main__":
    main()
