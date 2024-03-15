import os
import argparse
from PIL import Image

def process_image(input_file, output_folder, rows=32, columns=64):
    img = Image.open(input_file)
    width, height = img.size
    
    chunk_width = width // columns
    chunk_height = height // rows
    
    filename = os.path.basename(input_file)
    output_file = os.path.join(output_folder, os.path.splitext(filename)[0] + ".ans")
    
    with open(output_file, "w") as f:
        for y in range(0, height, chunk_height):
            for x in range(0, width, chunk_width):
                chunk = img.crop((x, y, x + chunk_width, y + chunk_height))
                pixels = chunk.getdata()
                r_bg, g_bg, b_bg = 0, 0, 0
                r_fg, g_fg, b_fg = 0, 0, 0
                for i, pixel in enumerate(pixels):
                    if i < len(pixels) // 2:  # Top half for background
                        r_bg += pixel[0]
                        g_bg += pixel[1]
                        b_bg += pixel[2]
                    else:  # Bottom half for foreground
                        r_fg += pixel[0]
                        g_fg += pixel[1]
                        b_fg += pixel[2]
                r_bg //= len(pixels) // 2
                g_bg //= len(pixels) // 2
                b_bg //= len(pixels) // 2
                r_fg //= len(pixels) // 2
                g_fg //= len(pixels) // 2
                b_fg //= len(pixels) // 2
                f.write(f"\033[48;2;{r_bg};{g_bg};{b_bg}m\033[38;2;{r_fg};{g_fg};{b_fg}m▄")
            f.write("\033[0m\n")

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Process images")
    parser.add_argument("input", help="Input file or folder")
    parser.add_argument("output", help="Output folder")
    parser.add_argument("--height", type=int, default=24, help="Number of rows (height) for output images")
    parser.add_argument("--width", type=int, default=68, help="Number of columns (width) for output images")
    args = parser.parse_args()
    
    if os.path.isdir(args.input):
        for file in os.listdir(args.input):
            if file.endswith((".jpg", ".jpeg", ".png")):
                process_image(os.path.join(args.input, file), args.output, args.height, args.width)
    elif os.path.isfile(args.input):
        process_image(args.input, args.output, args.height, args.width)
    else:
        print("Invalid input")
