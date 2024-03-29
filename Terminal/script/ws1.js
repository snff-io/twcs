const WebSocket = require('ws');
const readline = require('readline');

// WebSocket server URL
const serverUrl = 'ws://100.115.92.204:5260';

// Create a WebSocket instance
const ws = new WebSocket(serverUrl);

// Event: WebSocket connection opened
ws.on('open', function open() {
  console.log('Connected to WebSocket server.');
  
  // Start the input loop
  startInputLoop();
});

// Event: Received a message from the WebSocket server
ws.on('message', function incoming(message) {
  console.log('Received message from server:', message.toString());
});

// Event: WebSocket connection closed
ws.on('close', function close() {
  console.log('WebSocket connection closed.');
});

// Function to start the input loop
function startInputLoop() {
  const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
  });

  // Start the input loop
  rl.setPrompt('Enter message to send (press Ctrl+C to exit): ');
  rl.prompt();

  // Continuous input loop
  rl.on('line', function(message) {
    // Send the message to the WebSocket server
    ws.send(message);

    // Prompt the user for the next input
    rl.prompt();
  });
}
