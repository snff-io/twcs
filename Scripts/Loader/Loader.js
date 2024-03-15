"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __asyncValues = (this && this.__asyncValues) || function (o) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var m = o[Symbol.asyncIterator], i;
    return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
    function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
    function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
};
Object.defineProperty(exports, "__esModule", { value: true });
const aws_sdk_1 = require("aws-sdk"); // Import DynamoDB from the AWS SDK
const LoaderUnit_1 = require("./LoaderUnit");
const fs = __importStar(require("fs"));
const readline = __importStar(require("readline"));
// Create a new DynamoDB instance
const dynamoDB = new aws_sdk_1.DynamoDB({ region: 'us-east-2' }); // Specify the region
// Function to insert a unit into DynamoDB
function addUnit(unit) {
    return __awaiter(this, void 0, void 0, function* () {
        const params = {
            TableName: unit.displayType(), // Specify the table name based on the unit type
            Item: {
                'Id': { S: unit.id }, // Assuming id is a string attribute
                'FirstName': { S: unit.firstName },
                'LastName': { S: unit.lastName }
                // Add additional attributes as needed
            }
        };
        yield dynamoDB.putItem(params).promise();
        console.log('Unit inserted:', unit);
    });
}
// Function to read names from a file line by line
function readNamesFromFile(filename) {
    var _a, e_1, _b, _c;
    return __awaiter(this, void 0, void 0, function* () {
        const names = [];
        const fileStream = fs.createReadStream(filename, { encoding: 'utf8' });
        const rl = readline.createInterface({
            input: fileStream,
            crlfDelay: Infinity // Preserve newlines in output
        });
        try {
            for (var _d = true, rl_1 = __asyncValues(rl), rl_1_1; rl_1_1 = yield rl_1.next(), _a = rl_1_1.done, !_a; _d = true) {
                _c = rl_1_1.value;
                _d = false;
                const line = _c;
                names.push(line);
            }
        }
        catch (e_1_1) { e_1 = { error: e_1_1 }; }
        finally {
            try {
                if (!_d && !_a && (_b = rl_1.return)) yield _b.call(rl_1);
            }
            finally { if (e_1) throw e_1.error; }
        }
        return names;
    });
}
// Main function to insert units into DynamoDB
function main() {
    return __awaiter(this, void 0, void 0, function* () {
        const collection_name = process.argv[2]; // Get the first command-line argument as file name
        const file_name = process.argv[3]; // Get the second command-line argument as collection name
        try {
            const names = yield readNamesFromFile(file_name);
            for (const n of names) {
                const splitName = n.split(',');
                if (splitName.length !== 2) {
                    console.error(n + " has comma issue!");
                    continue; // Skip invalid names
                }
                const unit = new LoaderUnit_1.LoaderUnit(splitName[0].trim(), // Trim to remove leading/trailing spaces
                splitName[1].trim(), collection_name);
                yield addUnit(unit);
            }
            console.log('All units added successfully.');
        }
        catch (error) {
            console.error('Error:', error);
        }
    });
}
// Call the main function
main().catch(console.error);
//# sourceMappingURL=Loader.js.map