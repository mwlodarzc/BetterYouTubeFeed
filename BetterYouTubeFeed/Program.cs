using BYTF.Data;
using BYTF.Models;
using System;
using System.Linq;
using System.Net.Http.Headers;

using BYTFContext context = new();

var videos = context.Videos
    .OrderBy(v => v.VideoId);


foreach (Video v in videos)
{
    Console.WriteLine($"Id:      {v.VideoId}");
    Console.WriteLine($"Name:    {v.Title}");
    Console.WriteLine($"Date:    {v.UploadDate}");
    Console.WriteLine(new string('-',20));



}


/*


Video Concordia = new()
{
    Title = "The Cost of Concordia",
    Link = "Qh9KBwqGxTI",
    UploadDate = "17-02-2021",
    DownloadDate = "28-3-2023"
};
context.Videos.Add(Concordia);

Video Space = new()
{
    Title = "space.",
    Link = "MST-0jsOTN0",
    UploadDate = "30-11-2022",
    DownloadDate = "28-3-2023"
};
context.Videos.Add(Space);


context.SaveChanges();
*/