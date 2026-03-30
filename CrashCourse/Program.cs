using System.Data;
using CrashCourse.Data;
using CrashCourse.Models;


var dapper = new DataContextDapper();

var rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

Console.WriteLine(rightNow);

var myComputer = new Computer
{
    Motherboard = "Z690",
    HasWifi = true,
    HasLTE = true,
    CPUCores = 4,
    ReleaseDate = DateTime.Now,
    Price = 943.87m,
    VideoCard = "RTX2060"
};

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

var result = dapper.ExecuteSql(sql);
Console.WriteLine(result);

var sqlSELECT = @"SELECT
    Computer.Motherboard,
    Computer.HasWifi,
    Computer.HasLTE,
    Computer.CPUCores,
    Computer.ReleaseDate,
    Computer.Price,
    Computer.VideoCard
    FROM TutorialAppSchema.Computer";

var computers = dapper.LoadData<Computer>(sqlSELECT);

Console.WriteLine(sqlSELECT);

foreach (var computer in computers)
    Console.WriteLine("Motherboard: " + computer.Motherboard + " "
                      + "HasWifi: " + computer.HasWifi + " "
                      + "HasLTE: " + computer.HasLTE + " "
                      + "CPUCores: " + computer.CPUCores + " "
                      + "ReleaseDate: " + computer.ReleaseDate + " "
                      + "Price: " + computer.Price + " "
                      + "VideoCard: " + computer.VideoCard);