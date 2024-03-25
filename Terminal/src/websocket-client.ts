const socket = new WebSocket('ws://100.115.92.204:5000'); // Adjust URL as per your server configuration

socket.onopen = (event) => {
    console.log('WebSocket connected');
};

socket.onerror = (error) => {
    console.error('WebSocket error:', error);
};

socket.onclose = (event) => {
    console.log('WebSocket closed:', event);
};



function sendMessage() {
    const inputTextarea = document.getElementById('ta_input') as HTMLTextAreaElement;
    const message = inputTextarea.value;
    socket.send(message);
    
}
