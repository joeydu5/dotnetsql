using System.Data;
using System.Text.Json;
using CrashCourse.Data;
using CrashCourse.Models;
using Microsoft.Extensions.Configuration;

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

var computersJson = File.ReadAllText("Computers.json");
// Console.WriteLine(fileText);

var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

var computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

if (computers != null)
    foreach (var computer in computers)
        Console.WriteLine(computer.Motherboard);