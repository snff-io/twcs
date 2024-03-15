export class LoaderUnit implements IUnit {
    displayType(): string {
        return this.displayTypeString;
    }

    id: string;    
    displayTypeString = ""

    constructor(
        public firstName: string,
        public lastName: string,
        
        displayTypeString: string
    ) {
        this.id = (firstName + "_" + lastName).replace(" ", "-")
        this.displayTypeString = displayTypeString;

    }

    get_hash(length: number): string {
        throw new Error("Method not implemented.");
    }

}