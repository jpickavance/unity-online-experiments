function generateTokens(length)
{
    //configure dynamo db
    var AWS = require("aws-sdk");

    AWS.config.update({
        region: "us-east-2",
        endpoint: "https://dynamodb.us-east-2.amazonaws.com",
        accessKeyId: "YOUR_ACCESS_ID",
        secretAccessKey: "YOUR_SECRET_KEY"
    });

    var docClient = new AWS.DynamoDB.DocumentClient();

    //generate and post n = length tokens
    for (i = 0; i < length; i++)
    {
        var token = [...Array(7)].map(i=>(~~(Math.random()*36)).toString(36)).join('');
        var params =
        {
            TableName: "tokenTable",
            Item:
            {
                "tokenId": token,
                "available": true,
            }
        }
        docClient.put(params, function(err, data)
        {
            if (err)
            {
                console.log(err)
            }
            else
            {
                console.log("token table successfully updated")
            }
        });
    }
}

generateTokens(100);