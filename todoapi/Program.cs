using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace todos
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args)
        .UseUrls("http://0.0.0.0:3000/")
        .Build()
        .Run();
    }

    static void LogGrade(object sender, GradeAddedEventArgs e)
    {
      Console.WriteLine($"Grade added: {e.Grade}");
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
  }
}
