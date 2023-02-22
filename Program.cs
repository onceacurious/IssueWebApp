using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IssueWebApp
{
   public class Program
   {
      public static async void Main(string[] args)
      {
         var host = CreateHostBuilder(args).Build();

         // Create a scope for dependency injection
         using (var scope = host.Services.CreateScope())
         {
            // Call the method to manage data asynchronously
            await DataHelper.ManageDataAsync(scope.ServiceProvider);
         }

         // Run the application
         await host.RunAsync();

         //CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}