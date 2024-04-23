import argparse
import sys

# Define the I Ching mapping
iching_mapping = {
    '000000': '䷀', '000001': '䷁', '000010': '䷂', '000011': '䷃',
    '000100': '䷄', '000101': '䷅', '000110': '䷆', '000111': '䷇',
    '001000': '䷈', '001001': '䷉', '001010': '䷊', '001011': '䷋',
    '001100': '䷌', '001101': '䷍', '001110': '䷎', '001111': '䷏',
    '010000': '䷐', '010001': '䷑', '010010': '䷒', '010011': '䷓',
    '010100': '䷔', '010101': '䷕', '010110': '䷖', '010111': '䷗',
    '011000': '䷘', '011001': '䷙', '011010': '䷚', '011011': '䷛',
    '011100': '䷜', '011101': '䷝', '011110': '䷞', '011111': '䷟',
    '100000': '䷠', '100001': '䷡', '100010': '䷢', '100011': '䷣',
    '100100': '䷤', '100101': '䷥', '100110': '䷦', '100111': '䷧',
    '101000': '䷨', '101001': '䷩', '101010': '䷪', '101011': '䷫',
    '101100': '䷬', '101101': '䷭', '101110': '䷮', '101111': '䷯',
    '110000': '䷰', '110001': '䷱', '110010': '䷲', '110011': '䷳',
    '110100': '䷴', '110101': '䷵', '110110': '䷶', '110111': '䷷',
    '111000': '䷸', '111001': '䷹', '111010': '䷺', '111011': '䷻',
    '111100': '䷼', '111101': '䷽', '111110': '䷾', '111111': '䷿'
}

def ic_encode(data):
    encoded_data = ''
    for i in range(0, len(data), 6):
        chunk = data[i:i+6].zfill(6)
        encoded_data += iching_mapping[chunk]
    return encoded_data

def ic_decode(encoded_data):
    binary_data = ''
    for char in encoded_data:
        for key, value in iching_mapping.items():
            if value == char:
                binary_data += key
                break
    # Pad binary data with zeros to ensure it's a multiple of 8 bits
    padded_length = (len(binary_data) + 7) // 8 * 8
    binary_data = binary_data.ljust(padded_length, '0')
    # Convert binary string to bytes
    data_bytes = bytes(int(binary_data[i:i+8], 2) for i in range(0, len(binary_data), 8))
    # Decode bytes to string
    decoded_data = data_bytes.decode('utf-8')
    return decoded_data

def main():
    # Create argument parser
    parser = argparse.ArgumentParser(description='Encode or decode data using I Ching Unicode characters')

    # Add mode argument
    parser.add_argument('mode', choices=['encode', 'decode'], help='Mode: encode or decode')

    # Add argument for input file
    parser.add_argument('-f', '--file', help='Input file to encode or decode')

    # Parse command line arguments
    args = parser.parse_args()

    if args.mode == 'encode':
        if args.file:
            with open(args.file, 'rb') as f:
                data = f.read().decode('utf-8')
        else:
            data = sys.stdin.read().strip()

        # Convert input data to bytes
        data_bytes = data.encode('utf-8')
        # Convert bytes to binary string
        binary_data = ''.join(format(byte, '08b') for byte in data_bytes)
        # Encode binary data
        encoded_data = ic_encode(binary_data)
        # Print encoded data
        print(encoded_data)

    elif args.mode == 'decode':
        if args.file:
            with open(args.file, 'r', encoding='utf-8') as f:
                encoded_data = f.read().strip()
        else:
            encoded_data = sys.stdin.read().strip()

        # Decode encoded data
        decoded_data = ic_decode(encoded_data)
        # Print decoded data
        print(decoded_data)

if __name__ == '__main__':
    main()

