using System.Data;
using CrashCourse.Data;
using CrashCourse.Models;


var dapper = new DataContextDapper();

var rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

var entityFramework = new DataContextEF();

Console.WriteLine(rightNow);

var myComputer = new Computer
{
    Motherboard = "ASUS ROG STRIX Z490-E",
    HasWifi = true,
    HasLTE = false,
    CPUCores = 12,
    ReleaseDate = DateTime.Now,
    Price = 1500.00m,
    VideoCard = "RTX5070"
};

// using EF to add a new row in the DB table.
entityFramework.Add(myComputer);
entityFramework.SaveChanges();

var sql = @"INSERT INTO TutorialAppSchema.Computer(
Motherboard,
HasWifi,
HasLTE,
CPUCores,
ReleaseDate,
Price,
VideoCard)
VALUES('" + myComputer.Motherboard
          + "','" + myComputer.HasWifi
          + "','" + myComputer.HasLTE
          + "','" + myComputer.CPUCores
          + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
          + "','" + myComputer.Price
          + "','" + myComputer.VideoCard
          + "')";
// Console.WriteLine(sql);
// int res = dbConnection.Execute(sql);
// Console.WriteLine(res);

// var result = dapper.ExecuteSql(sql);
// Console.WriteLine(result);

var sqlSELECT = @"SELECT
    Computer.Motherboard,
    Computer.HasWifi,
    Computer.HasLTE,
    Computer.CPUCores,
    Computer.ReleaseDate,
    Computer.Price,
    Computer.VideoCard
    FROM TutorialAppSchema.Computer";

// var computers = dapper.LoadData<Computer>(sqlSELECT);

var computersEf = entityFramework.Computer?.ToList<Computer>();


Console.WriteLine(sqlSELECT);

if (computersEf != null)
    foreach (var computer in computersEf)
        Console.WriteLine("Motherboard: " + computer.Motherboard + " "
                          + "ComputerId: " + computer.ComputerId + " "
                          + "HasWifi: " + computer.HasWifi + " "
                          + "HasLTE: " + computer.HasLTE + " "
                          + "CPUCores: " + computer.CPUCores + " "
                          + "ReleaseDate: " + computer.ReleaseDate + " "
                          + "Price: " + computer.Price + " "
                          + "VideoCard: " + computer.VideoCard);