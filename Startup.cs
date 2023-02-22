using IssueWebApp.Data;
using IssueWebApp.Repositories;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IssueWebApp
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {


         //JWT
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new()
            {
               //ValidateLifetime = true,
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value))
            });

         //Custom Services
         services.AddCors(c =>
         c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
         services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(Configuration.GetConnectionString("DemoAppConn")));

         //Dependency
         services.AddScoped<IDivisionRepository, DivisionRepository>();
         services.AddScoped<IIssueRepository, IssueRepository>();
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IAnswerRepository, AnswerRepository>();
         services.AddScoped<ICommentRepository, CommentRepository>();

         services.AddMvc();
         services.AddControllers().AddJsonOptions(options =>
            {
               options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "IssueWebApp", Version = "v1" });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         //Customer helper
         using (var scope = app.ApplicationServices.CreateScope())
         {
            var serviceProvider = scope.ServiceProvider;
            DataHelper.ManageDataAsync(serviceProvider).Wait();
         }


         app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         app.UseStaticFiles();

         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IssueWebApp v1"));
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         //Always above authorization
         app.UseAuthentication();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
              name: "StatusIssue",
              pattern: "api/{status:ind}/issues",
              defaults: new { controller = "Issue", action = "GetIssueByStatus" }
              );
         });
      }
   }
}