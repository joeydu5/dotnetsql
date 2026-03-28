using System.Data;
using CrashCourse.Models;
using Dapper;
using Microsoft.Data.SqlClient;


string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User ID=sa;Password=Password123;";

IDbConnection dbConnection = new SqlConnection(connectionString);

string sqlCommand = "SELECT GETDATE()";

DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);
Console.WriteLine(rightNow);

Computer myComputer = new Computer()
{
    Motherboard = "Z690",
    HasWifi = true,
    HasLTE = true,
    CPUCores = 4,
    ReleaseDate = DateTime.Now,
    Price = 943.87m,
    VideoCard = "RTX2060"
};

string sql = @"INSERT INTO TutorialAppSchema.Computer(
Motherboard,
HasWifi,
HasLTE,
CPUCores,
ReleaseDate,
Price,
VideoCard)
VALUES('"+ myComputer.Motherboard
         + "','"+ myComputer.HasWifi
         + "','"+ myComputer.HasLTE
         + "','"+ myComputer.CPUCores
         + "','"+ myComputer.ReleaseDate.ToString("yyyy-MM-dd")
         + "','"+ myComputer.Price
         + "','"+ myComputer.VideoCard
+ "')";
// Console.WriteLine(sql);
// int res = dbConnection.Execute(sql);
// Console.WriteLine(res);

string sqlSELECT = @"SELECT
    Computer.Motherboard,
    Computer.HasWifi,
    Computer.HasLTE,
    Computer.CPUCores,
    Computer.ReleaseDate,
    Computer.Price,
    Computer.VideoCard
    FROM TutorialAppSchema.Computer";

IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSELECT);

Console.WriteLine(sqlSELECT);

foreach (Computer computer in computers)
{
    Console.WriteLine("Motherboard: " + computer.Motherboard + " "
                                               + "HasWifi: " + computer.HasWifi + " "
                                               + "HasLTE: " + computer.HasLTE + " "
                                               + "CPUCores: " + computer.CPUCores + " "
                                               + "ReleaseDate: "+ computer.ReleaseDate + " "
                                               + "Price: " + computer.Price + " "
                                               + "VideoCard: " + computer.VideoCard );
}