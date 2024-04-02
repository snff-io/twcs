//const WebSocket = require('ws');

const socket = new WebSocket('ws://100.115.92.204:5260'); // Adjust URL as per your server configuration

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
}

function handleMessage(message: string) {

    // Regular expression to match URLs ending with .jpeg
    const urlRegex: RegExp = /(https?:\/\/[^\s]+\.jpeg)/g;

    // Extract URLs from the message
    const urls: string[] = message.match(urlRegex) || [];

    // Get the div element
    const divMidLeft: HTMLDivElement | null = document.getElementById("div_mid_left") as HTMLDivElement | null;
    const divMidRight: HTMLDivElement | null = document.getElementById("div_mid_left") as HTMLDivElement | null;
    // If the div exists and at least one URL is found
    if (divMidLeft && urls.length === 1) {
        // Set the background image of the div to the first URL found
        divMidLeft.style.backgroundImage = `url(${urls[0]})`;

    }
    else if (divMidRight && urls.length === 2) {
        divMidRight.style.backgroundImage = `url(${urls[1]})`;

    }
    else {
        const outputTextArea = document.getElementById("ta_output") as HTMLTextAreaElement;

        //const breadcrumbTextArea = document.getElementById("ta_breadcrumb");
        outputTextArea.value += message;
    }
}

function handleInput() {

    var value = inputTextarea.value;

    if (value.startsWith("'")) {
        //send to chat
    }
    else {
        socket.send(value);
    }

}
const inputTextarea = document.getElementById('ta_input') as HTMLTextAreaElement;
inputTextarea.addEventListener('keydown', (event) => {
    // Check if the Enter key was pressed
    if (event.key === 'Enter') {
        // Trigger your event or action here
        handleInput();
        inputTextarea.value = "";
        // Prevent default behavior of textarea (adding new line)
        event.preventDefault();
    }
});
