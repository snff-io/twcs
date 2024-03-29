const WebSocket = require('ws');

const socket = new WebSocket('ws://100.115.92.204:5260'); // Adjust URL as per your server configuration

socket.on('open', () => {
    console.log('WebSocket connected');
});

socket.on('error', (error) => {
    console.error('WebSocket error:', error);
});

socket.on('close', (event) => {
    console.log('WebSocket closed:', event);
});

socket.on('message', (data) => {
    console.log("WebSocket Message", data);
    handleMessage(data);
});

function handleMessage(message) {
    console.log(message); // Modify this function according to your needs
}

// Function to send messages
function sendMessage(message) {
    socket.send(message);
}
