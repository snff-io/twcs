using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using library.worldcomputer.info;

public class DynamoDb<T> : IDal<T>
    where T : IUnit
{
    private AmazonDynamoDBClient _client;

    public DynamoDb()
    {

        _client = new AmazonDynamoDBClient();

    }

    public async Task<T> Get(string table, string key)
    {
        var keyDict = new Dictionary<string, AttributeValue>
        {
            { "Id", new AttributeValue { S = key } } // Assuming key is of type string
        };

        var request = new GetItemRequest
        {
            TableName = table,
            Key = keyDict
        };

        var response = await _client.GetItemAsync(request);

        // Check if the item exists
        if (response.Item == null || !response.Item.Any())
        {
            return default; // Return default value for type T if item doesn't exist
        }

        // Deserialize the item to type T
        T? ret = JsonSerializer.Deserialize<T>(Document.FromAttributeMap(response.Item).ToJson());

        return ret;
    }
    public async Task Put(T item)
    {
        // Create a Document object representing the item to be put into DynamoDB
        var document = Document.FromJson(JsonSerializer.Serialize(item));

        // Create a Table object using the display type of the item
        var table = Table.LoadTable(_client, item.DisplayType);

        // Save the document to the table
        await table.PutItemAsync(document);

    }

    public async Task<T[]> GetRandomUnbound(int number = 5)
    {
        var unboundItems = new List<T>();
        var rnd = new Random();

        // Initialize the exclusive start key
        Dictionary<string, AttributeValue> exclusiveStartKey = null;

        // Loop until we have enough unbound items or we've scanned the entire table
        while (unboundItems.Count < number)
        {
            var request = new ScanRequest
            {
                TableName = typeof(T).Name,
                FilterExpression = "attribute_not_exists(Bound) OR Bound = :bound",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":bound", new AttributeValue { BOOL = false } }
            },
                Limit = number - unboundItems.Count, // Limit the number of items returned per page
                ExclusiveStartKey = exclusiveStartKey // Set the exclusive start key for pagination
            };

            var response = await _client.ScanAsync(request);

            // Convert scanned items to type T
            var scannedItems = response.Items.Select(item =>
                JsonSerializer.Deserialize<T>(Document.FromAttributeMap(item).ToJson())
            );

            // Add scanned items to the list
            unboundItems.AddRange(scannedItems);

            // Update the exclusive start key for pagination
            exclusiveStartKey = response.LastEvaluatedKey;

            // If there are no more items or we've reached the limit, break the loop
            if (exclusiveStartKey == null || unboundItems.Count >= number)
                break;
        }

        // Shuffle the list randomly
        unboundItems = unboundItems.OrderBy(item => rnd.Next()).ToList();

        // Take the top 'number' items
        return unboundItems.Take(number).ToArray();

        
    }

}