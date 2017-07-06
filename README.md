	# Nexosis .NET API Client

[![Build status](https://ci.appveyor.com/api/projects/status/cl1fac1a0ylggn72?svg=true)](https://ci.appveyor.com/project/Nexosis/nexosisclient-net)

## Installation

You need to be using a project referencing .NET Standard (any version of .NET Core or .NET Framework 4.6.2 or above).

	PM> Install-Package Nexosis.Api.Client 

## Usage

The most basic thing you can do with the API is submit some data and ask for predictions all at once. This can be done if you have a CSV file with the following code:

 ```csharp
    var client = new NexosisClient("YOUR API KEY HERE");
    using (var file = File.OpenText("C:\\path\\to\\file.csv"))
    {
        var session = await client.Sessions.CreateForecast(file, "sales", DateTimeOffset.Parse("2017-03-25 -0:00"), DateTimeOffset.Parse("2017-04-25 -0:00"));
        Console.WriteLine($"{session.Id}");
    }
 ```
    
 For this to work, the CSV file must have a header with the names of the columns in the file. One of those must be named "timeStamp", and in this example, there is a second column named "sales".
 
 Once the forecasting is complete, you will receive an email notification. Using the `sessionId` from above, you will want to get results with the following call:

 ```csharp
     var results = await client.Sessions.GetResults(sessionId);
     // results has a .Data property with the forecast values
 ```
     
 ## Issues
 
 Create a [new issue](https://github.com/Nexosis/nexosisclient-net/issues/new). Please include code to reproduce the error if possible.

 Pull requests are welcome.
 
