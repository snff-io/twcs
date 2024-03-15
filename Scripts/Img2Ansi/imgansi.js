"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
function processImage(inputFile, canvas, rows = 32, columns = 64) {
    return __awaiter(this, void 0, void 0, function* () {
        const img = new Image();
        img.src = inputFile;
        yield img.decode(); // Ensure the image is fully loaded before processing
        const width = img.width;
        const height = img.height;
        const chunkWidth = Math.floor(width / columns);
        const chunkHeight = Math.floor(height / rows);
        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
        for (let y = 0; y < height; y += chunkHeight) {
            for (let x = 0; x < width; x += chunkWidth) {
                const chunk = ctx.getImageData(x, y, chunkWidth, chunkHeight);
                let r_bg = 0, g_bg = 0, b_bg = 0;
                let r_fg = 0, g_fg = 0, b_fg = 0;
                for (let i = 0; i < chunk.data.length; i += 4) {
                    if (i < chunk.data.length / 2) {
                        r_bg += chunk.data[i];
                        g_bg += chunk.data[i + 1];
                        b_bg += chunk.data[i + 2];
                    }
                    else {
                        r_fg += chunk.data[i];
                        g_fg += chunk.data[i + 1];
                        b_fg += chunk.data[i + 2];
                    }
                }
                r_bg = Math.floor(r_bg / (chunk.data.length / 2));
                g_bg = Math.floor(g_bg / (chunk.data.length / 2));
                b_bg = Math.floor(b_bg / (chunk.data.length / 2));
                r_fg = Math.floor(r_fg / (chunk.data.length / 2));
                g_fg = Math.floor(g_fg / (chunk.data.length / 2));
                b_fg = Math.floor(b_fg / (chunk.data.length / 2));
                ctx.fillStyle = `rgb(${r_fg},${g_fg},${b_fg})`;
                ctx.fillRect(x / chunkWidth, y / chunkHeight, 1, 1);
            }
        }
    });
}
function main() {
    return __awaiter(this, void 0, void 0, function* () {
        const inputFile = 'path/to/image.jpg'; // Specify the path to your image file
        const canvas = document.createElement('canvas');
        document.body.appendChild(canvas); // Append the canvas to the DOM
        const rows = 24;
        const columns = 68;
        yield processImage(inputFile, canvas, rows, columns);
    });
}
main();
//# sourceMappingURL=imgansi.js.map