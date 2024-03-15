import { DynamoDB } from 'aws-sdk'; // Import DynamoDB from the AWS SDK
import { LoaderUnit } from './LoaderUnit';
import * as fs from 'fs';
import * as readline from 'readline';

// Create a new DynamoDB instance
const dynamoDB = new DynamoDB({ region: 'us-east-2' }); // Specify the region

// Function to insert a unit into DynamoDB
async function addUnit(unit: LoaderUnit): Promise<void> {
  const params = {
    TableName: unit.displayType(), // Specify the table name based on the unit type
    Item: {
      'Id': { S: unit.id }, // Assuming id is a string attribute
      'FirstName': { S: unit.firstName },
      'LastName': { S: unit.lastName }
      // Add additional attributes as needed
    }
  };

  await dynamoDB.putItem(params).promise();
  console.log('Unit inserted:', unit);
}

// Function to read names from a file line by line
async function readNamesFromFile(filename: string): Promise<string[]> {
  const names: string[] = [];
  const fileStream = fs.createReadStream(filename, { encoding: 'utf8' });
  const rl = readline.createInterface({
    input: fileStream,
    crlfDelay: Infinity // Preserve newlines in output
  });

  for await (const line of rl) {
    names.push(line);
  }

  return names;
}

// Main function to insert units into DynamoDB
async function main() {

  const collection_name = process.argv[2]; // Get the first command-line argument as file name
  const file_name = process.argv[3]; // Get the second command-line argument as collection name

  
  try {
    const names = await readNamesFromFile(file_name);

    for (const n of names) {
      const splitName = n.split(',');
      if (splitName.length !== 2) {
        console.error(n + " has comma issue!");
        continue; // Skip invalid names
      }

      const unit = new LoaderUnit(
        splitName[0].trim(), // Trim to remove leading/trailing spaces
        splitName[1].trim(),
        collection_name
      );

      await addUnit(unit);
    }

    console.log('All units added successfully.');
  } catch (error) {
    console.error('Error:', error);
  }
}

// Call the main function
main().catch(console.error);
