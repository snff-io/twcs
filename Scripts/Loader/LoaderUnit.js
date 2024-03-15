"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoaderUnit = void 0;
class LoaderUnit {
    displayType() {
        return this.displayTypeString;
    }
    constructor(firstName, lastName, displayTypeString) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.displayTypeString = "";
        this.id = (firstName + "_" + lastName).replace(" ", "-");
        this.displayTypeString = displayTypeString;
    }
    get_hash(length) {
        throw new Error("Method not implemented.");
    }
}
exports.LoaderUnit = LoaderUnit;
//# sourceMappingURL=LoaderUnit.js.map