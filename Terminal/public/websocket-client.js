"use strict";
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
socket.onmessage = (event) => {
    console.log("WebSocket Message", event.data);
    handleMessage(event.data);
};
function handleMessage(message) {
    const breadcrumbTextArea = document.getElementById("ta_breadcrumb");
    breadcrumbTextArea.value = message;
}
function sendMessage() {
    const inputTextarea = document.getElementById('ta_input');
    const message = inputTextarea.value;
    socket.send(message);
}
//# sourceMappingURL=websocket-client.js.map