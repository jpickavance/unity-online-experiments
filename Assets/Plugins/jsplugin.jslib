mergeInto(LibraryManager.library,
{
    fullscreenListener: function() //could collapse this into function above if I can get the unity scene from js
    {
        if(document.addEventListener)
        {
            document.addEventListener('fullscreenchange', exitHandler, false);
            document.addEventListener('mozfullscreenchange', exitHandler, false);
            document.addEventListener('MSFullscreenChange', exitHandler, false);
            document.addEventListener('webkitfullscreenchange', exitHandler, false);
            
        }
        function exitHandler()
        {
            if (document.webkitIsFullScreen || document.mozFullScreen || (document.msFullscreenElement !== null && document.msFullscreenElement !== undefined))
            {
                SendMessage('Pause', 'ToggleFullscreen', 'fullscreen');
            }
            else if (!document.webkitIsFullScreen || !document.mozFullScreen || (document.msFullscreenElement == null && document.msFullscreenElement !== undefined))
            {
                SendMessage('Pause', 'ToggleFullscreen', 'exitFullscreen');
            }
        }
    },
    lockListener: function()
    {
        if(document.addEventListener)
        {
            document.addEventListener('pointerlockchange', exitHandler2, false);
            document.addEventListener('mozpointerlockchange', exitHandler2, false);
            document.addEventListener('webkitpointerlockchange', exitHandler2, false);
            document.addEventListener('mspointerlockchange', exitHandler2, false);
        }

      function exitHandler2()
      {
        var canvas = document.getElementById('#canvas');
        if (document.pointerLockElement === canvas || document.mozPointerLockElement === canvas || document.webkitPointerLockElement === canvas || document.msPointerLockElement === canvas)
        {
          SendMessage('Pause', 'ToggleLock', 'lock');
        }
        else
        {
          SendMessage('Pause', 'ToggleLock', 'unlock');
        }
      }
    },
    UnloadListener: function()
    {
        window.addEventListener('beforeunload', function(e){
            e.preventDefault(); // If you prevent default behavior in Mozilla Firefox prompt will always be shown
            // Chrome requires returnValue to be set
            var dialogText = "Your progress has been saved! Would you like to exit the page?";
            e.returnValue = dialogText;
            SendMessage('OnCloseListener', 'OnClose');
        });
    },
    getScreenWidth: function()
    {
        var widthPx = String(screen.width);
        var bufferSize = lengthBytesUTF8(widthPx) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(widthPx, buffer, bufferSize);
        return buffer;
    },
    getScreenHeight: function()
    {
        var heightPx = String(screen.height);
        var bufferSize = lengthBytesUTF8(heightPx) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(heightPx, buffer, bufferSize);
        return buffer;
    },
    getPixelRatio: function()
    {
        var pxRatio = String(window.devicePixelRatio);
        var bufferSize = lengthBytesUTF8(pxRatio) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(pxRatio, buffer, bufferSize);
        return buffer;
    },
    getBrowserVersion: function()
    {
        function get_browser() 
        {
            var ua=navigator.userAgent,tem,M=ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || []; 
            if(/trident/i.test(M[1])){
                tem=/\brv[ :]+(\d+)/g.exec(ua) || []; 
                return {name:'IE',version:(tem[1]||'')};
                }   
            if(M[1]==='Chrome'){
                tem=ua.match(/\bOPR|Edge\/(\d+)/)
                if(tem!=null)   {return {name:'Opera', version:tem[1]};}
                }   
            M=M[2]? [M[1], M[2]]: [navigator.appName, navigator.appVersion, '-?'];
            if((tem=ua.match(/version\/(\d+)/i))!=null) {M.splice(1,1,tem[1]);}
            return {
            name: M[0],
            version: M[1]}
        };
        var browser = get_browser();
        var browserName = String(browser.name);
        var version = String(browser.version);
        var browserVersion = browserName + ", " + version;
        var bufferSize = lengthBytesUTF8(browserVersion) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(browserVersion, buffer, bufferSize);
        return buffer;
    },
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
            accessKeyId: "YOUR_ACCESS_ID",
            secretAccessKey: "YOUR_SECRET_KEY"
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
            accessKeyId: "YOUR_ACCESS_ID",
            secretAccessKey: "YOUR_SECRET_KEY"
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
            accessKeyId: "YOUR_ACCESS_ID",
            secretAccessKey: "YOUR_SECRET_KEY"
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
            accessKeyId: "YOUR_ACCESS_ID",
            secretAccessKey: "YOUR_SECRET_KEY"
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