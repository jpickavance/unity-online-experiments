mergeInto(LibraryManager.library,
{
    ReadData: function (tableName, token)
    {
        var params =
        {
            TableName: Pointer_stringify(tableName),
            Key:
            {
                "tokenId": Pointer_stringify(token)
            },
            AttributesToGet: [
                "tokenId", "available"
            ]
        };
        var awsConfig =
        {
            region: "us-east-2",
            endpoint: "https://dynamodb.us-east-2.amazonaws.com",
            accessKeyId: "YOUR ACCESS ID",
            secretAccessKey: "YOUR SECRET KEY"
        };
        AWS.config.update(awsConfig);
        var docClient = new AWS.DynamoDB.DocumentClient();
        docClient.get(params, function(err, data)
        {
            if (err)
            {
                var returnStr = "Please enter a valid secret key";
                SendMessage('MainMenu', 'ErrorCallback', returnStr);
            }
            else if (data.Item == undefined)
            {
                var returnStr = "The token you have entered isn't recognised";
                SendMessage('MainMenu', 'ErrorCallback', returnStr);
            }
            else
            {
                console.log(data);
                var returnStr = JSON.stringify(data.Item);
                SendMessage('MainMenu', 'StringCallback', returnStr);
            }
        });
    },
    UpdateToken: function (tableName, token)
    {
        var params =
        {
            TableName: Pointer_stringify(tableName),
            Key:
            {
                "tokenId": Pointer_stringify(token)
            },
            UpdateExpression: "set available = :a",
            ExpressionAttributeValues:
            {
                ":a": false
            },
            ReturnValues:"UPDATED_NEW"
        };
        var awsConfig =
        {
            region: "us-east-2",
            endpoint: "https://dynamodb.us-east-2.amazonaws.com",
            accessKeyId: "YOUR ACCESS ID",
            secretAccessKey: "YOUR SECRET KEY"
        };
        AWS.config.update(awsConfig);
        var docClient = new AWS.DynamoDB.DocumentClient();
        docClient.update(params, function(err, data)
        {
            if (err)
            {
                console.log(err);
            }
            else if (data )
            {
                console.log("tokenId in tokenTable updated");
            }
        });
    },
    InsertData: function (tableName, token, trialNum, movementTime, timeData, posxData, poszData)
    {
        var params =
        {
            TableName: Pointer_stringify(tableName),
            Item:
            {
                "tokenId": Pointer_stringify(token),
                "trial": Pointer_stringify(trialNum),
                "mt": Pointer_stringify(movementTime),
                "time": Pointer_stringify(timeData),
                "pos_x": Pointer_stringify(posxData),
                "pos_y": Pointer_stringify(poszData)
            }
        };
        var awsConfig =
        {
            region: "us-east-2",
            endpoint: "https://dynamodb.us-east-2.amazonaws.com",
            accessKeyId: "YOUR ACCESS ID",
            secretAccessKey: "YOUR SECRET KEY"
        };
        AWS.config.update(awsConfig);
        var docClient = new AWS.DynamoDB.DocumentClient();
        var returnStr = "Error";
        docClient.put(params, function(err, data)
        {
            if (err)
            {
                returnStr = "Error:" + JSON.stringify(err, undefined, 2);
                SendMessage('ExperimentController', 'StringCallback', returnStr);
            }
            else
            {
                returnStr = "Data Inserted:" + JSON.stringify(data, undefined, 2);
                SendMessage('ExperimentController', 'StringCallback', returnStr);
            }
        });
    },
    InsertUser: function (tableName, token, handedness, consentTime, startTime)
    {
        var params =
        {
            TableName: Pointer_stringify(tableName),
            Item:
            {
                "tokenId": Pointer_stringify(token),
                "handedness": Pointer_stringify(handedness),
                "consentTime": Pointer_stringify(consentTime),
                "startTime": Pointer_stringify(startTime)
            }
        };
        var awsConfig =
        {
            region: "us-east-2",
            endpoint: "https://dynamodb.us-east-2.amazonaws.com",
            accessKeyId: "YOUR ACCESS ID",
            secretAccessKey: "YOUR SECRET KEY"
        };
        AWS.config.update(awsConfig);
        var docClient = new AWS.DynamoDB.DocumentClient();
        var returnStr = "Error";
        docClient.put(params, function(err, data)
        {
            if (err)
            {
                returnStr = "Error:" + JSON.stringify(err, undefined, 2);
                SendMessage('ExperimentController', 'StringCallback', returnStr);
            }
            else
            {
                returnStr = "Data Inserted:" + JSON.stringify(data, undefined, 2);
                SendMessage('ExperimentController', 'StringCallback', returnStr);
            }
        });
    },
});