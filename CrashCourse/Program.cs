using System.Data;
using System.Text.Json;
using AutoMapper;
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

// var rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");


// Console.WriteLine(rightNow);

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

var computerJsonSnake = File.ReadAllText("ComputersSnake.json");
Console.WriteLine(computerJsonSnake);

var mapperConfig = new MapperConfiguration((cfg) =>
{
    cfg.CreateMap<ComputerSnake, Computer>()
        .ForMember(destination => destination.ComputerId,
            options => options.MapFrom(source => source.computer_id))
        .ForMember(destination => destination.Motherboard,
            options => options.MapFrom(source => source.motherboard))
        .ForMember(destination => destination.CPUCores,
            options => options.MapFrom(source => source.cpu_cores))
        .ForMember(destination => destination.HasWifi,
            options => options.MapFrom(source => source.has_wifi))
        .ForMember(destination => destination.HasLTE,
            options => options.MapFrom(source => source.has_lte))
        .ForMember(destination => destination.ReleaseDate,
            options => options.MapFrom(source => source.release_date))
        .ForMember(destination => destination.Price,
            options => options.MapFrom(source => source.price))
        .ForMember(destination => destination.VideoCard,
            options => options.MapFrom(source => source.video_card));
}); // Source Model and Destination Model map ComputerSnake ---> Computer

var mapper = new Mapper(mapperConfig);

var computersSnakeJson = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computerJsonSnake);
// if (computerJsonSnake != null)
// {
//     var computersMapped = mapper.Map<IEnumerable<Computer>>(computersSnakeJson);
//     foreach (var computer in computersMapped)
//     {
//         Console.WriteLine(computer.Motherboard);
//     }
// }

var computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJsonSnake);
if (computersSystem != null)
{
    foreach (var computer in computersSystem)
    {
        Console.WriteLine(computer.Motherboard);
    }
}

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
{
    foreach (var computer in computersNewtonsoft)
    {
        var sql = @"INSERT INTO TutorialAppSchema.Computer(
     Motherboard,
     HasWifi,
     HasLTE,
     CPUCores,
     ReleaseDate,
     Price,
     VideoCard)
 VALUES('" + EscapeSingleQuotes(computer.Motherboard)
           + "','" + computer.HasWifi
           + "','" + computer.HasLTE
           + "','" + computer.CPUCores
           + "','" + (computer.ReleaseDate?.ToString("yyyy-MM-dd") ?? "NULL")
           + "','" + computer.Price
           + "','" + EscapeSingleQuotes(computer.VideoCard)
           + "')";

        // dapper.ExecuteSql(sql);
    }
}

// use System Text Json to serialize
var computerCopySystemText = System.Text.Json.JsonSerializer.Serialize(computersSystemText, options);
// File.WriteAllText("ComputersSystemText.json", computerCopySystemText);

// use Newtonsoft to serialize
var computerCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);
// File.WriteAllText("ComputersNewtonsoft.json", computerCopyNewtonsoft);

static string EscapeSingleQuotes(string input)
{
    return input.Replace("'", "''");
}