import numpy as np
import os
import time
import random

# Parameters
sample_rate = 1000  # Hz
duration = 0.2  # seconds (window size)
n_samples = int(sample_rate * duration)
freq_range = (25, 46)  # Hz
num_buckets = 4

# Function to clear the terminal
def clear_terminal():
    os.system('cls' if os.name == 'nt' else 'clear')

# Generate a parametric signal
def generate_signal(signal_params):
    t = np.linspace(0, duration, n_samples, endpoint=False)
    signal = np.zeros(n_samples)
    for duration_ms, freq in signal_params:
        duration_samples = int((duration_ms / 1000) * sample_rate)
        signal[:duration_samples] += np.sin(2 * np.pi * freq * t[:duration_samples])
    return signal

# Generate frequency spectrum analyzer
def spectrum_analyzer(signal):
    spectrum = np.fft.fft(signal)
    freqs = np.fft.fftfreq(len(signal), 1 / sample_rate)
    
    freq_buckets = np.linspace(freq_range[0], freq_range[1], num_buckets + 1)
    freq_indices = [np.where((freqs >= freq_buckets[i]) & (freqs < freq_buckets[i+1]))[0] for i in range(num_buckets)]
    bucket_values = [np.abs(spectrum[indices]).mean() for indices in freq_indices]
    
    return freq_buckets[:-1], bucket_values

# Print frequency spectrum as ASCII
def print_spectrum(frequencies, amplitudes):
    max_amplitude = max(amplitudes) if any(amplitudes) else 1
    print("Max amplitude:", max_amplitude)
    for freq, amp in zip(frequencies, amplitudes):
        if not np.isnan(amp):
            scaled_amp = int(amp / max_amplitude * 20)  # Scale to fit within 20 characters
            print(f"{freq:.1f} Hz | {'#' * scaled_amp}")
        else:
            print(f"{freq:.1f} Hz | #")


# Main animation loop
while True:
    # Generate random signal parameters within the specified range
    num_components = random.randint(1, 3)
    signal_params = [(random.randint(50, 150), random.uniform(freq_range[0], freq_range[1])) for _ in range(num_components)]
    
    # Generate signal and analyze spectrum
    signal = generate_signal(signal_params)
    frequencies, amplitudes = spectrum_analyzer(signal)
    
    # Clear the terminal and print the spectrum
    clear_terminal()
    print_spectrum(frequencies, amplitudes)
    
    # Wait for the next frame
    time.sleep(duration)
