using CrashCourse.Models;

Computer computer = new Computer()
{
    Motherboard = "Z690",
    HasWifi = true,
    ReleaseDate = DateTime.Now,
    Price = 943.87m,
    VideoCard = "RTX2060"
};

Console.WriteLine(computer.Motherboard);
Console.WriteLine(computer.HasWifi);
Console.WriteLine(computer.ReleaseDate);



