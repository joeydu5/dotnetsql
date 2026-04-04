using System.Data;
using System.Text.Json;
using CrashCourse.Data;
using CrashCourse.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var basePath = Directory.GetCurrentDirectory();
Console.WriteLine("Base Path: " + basePath);
IConfiguration config = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json").Build();

var dapper = new DataContextDapper(config);
var entityFramework = new DataContextEF(config);

var rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");


Console.WriteLine(rightNow);

// var myComputer = new Computer
// {
//     Motherboard = "ASUS ROG STRIX Z490-E",
//     HasWifi = true,
//     HasLTE = false,
//     CPUCores = 12,
//     ReleaseDate = DateTime.Now,
//     Price = 1500.00m,
//     VideoCard = "RTX5070"
// };


// var sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
//     Motherboard,
//     HasWifi,
//     HasLTE,
//     CPUCores,
//     ReleaseDate,
//     Price,
//     VideoCard)
// VALUES('" + myComputer.Motherboard
//           + "','" + myComputer.HasWifi
//           + "','" + myComputer.HasLTE
//           + "','" + myComputer.CPUCores
//           + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
//           + "','" + myComputer.Price
//           + "','" + myComputer.VideoCard
//           + "')\n\n";

// File.WriteAllText("log.txt", sql);
//
// using StreamWriter openFile = new("log.txt", true);
// openFile.WriteLine(sql);
//
// openFile.Close();
//
// var readFile = File.ReadAllText("log.txt");
//
// Console.WriteLine(readFile);


// Read JSON data
var computersJson = File.ReadAllText("Computers.json");
// Console.WriteLine(fileText);

// camel case settings for System Text Json for both serialize and deserialize
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

// camel case settings for Newtonsoft to serialize (deserialize doesn't need settings)
// 'camelCasePropertyNames' is the default setting for Newtonsoft, but we can set it explicitly to 'camelCasePropertyNames
var settings = new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

// user System Text Json to deserialize
var computersSystemText = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
// use Newtonsoft to deserialize
var computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

// Print out on the screen
if (computersNewtonsoft != null)
    foreach (var computer in computersNewtonsoft)
    {
        // Console.WriteLine(computer.Motherboard);
    }

// use System Text Json to serialize
var computerCopySystemText = System.Text.Json.JsonSerializer.Serialize(computersSystemText, options);
File.WriteAllText("ComputersSystemText.json", computerCopySystemText);

// use Newtonsoft to serialize
var computerCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);
File.WriteAllText("ComputersNewtonsoft.json", computerCopyNewtonsoft);