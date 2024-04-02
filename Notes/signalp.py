import math

def draw_sine_wave(freq):
    width = 80
    height = 24
    display_time = 0.1  # 100 milliseconds display time

    # Adjust the cycles based on frequency
    if freq <= 30:
        cycles = int(freq / 2) + 1
    else:
        cycles = int(freq * 0.3) + 5

    # Calculate scale factor based on frequency and cycles
    scale = (2 * math.pi * cycles) / width

    # Initialize empty grid
    grid = [[' ' for _ in range(width)] for _ in range(height)]

    # Draw sine wave
    for x in range(width):
        y = math.sin(x * scale)
        y_scaled = int((y + 1) * (height - 1) / 2)
        grid[y_scaled][x] = '\u2588'  # Half block character

    # Print the grid
    for row in grid:
        print(''.join(row))

# Example usage

for freqency in range(25, 46, 3):
    print(str(freqency))
    draw_sine_wave(freqency)
