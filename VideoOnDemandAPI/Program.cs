﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VideoOnDemandAPI.Entities;

namespace VideoOnDemandAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //1. Get the IWebHost which will host this application.
      var host = CreateWebHostBuilder(args).Build();

      //2. Find the service layer within our scope.
      using (var scope = host.Services.CreateScope())
      {
        //3. Get the instance of BoardGamesDBContext in our services layer
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<VideoOnDemandDBContext>();

        //4. Call the DataGenerator to create sample data
        DataGenerator.Initialize(services);
      }

      //Continue to run the application
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
