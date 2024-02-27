using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;
using Project_PR71_API.Services;

namespace Project_PR71_API
{
    public class Startup
    {
        //var builder = WebApplication.CreateBuilder(args);

        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc();
            services.AddHttpClient();
            services.AddEntityFrameworkNpgsql().AddDbContext<Models.DataContext>( opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging(true);
            });

            services.AddSwaggerGen();

            services.AddScoped<IPostService, PostService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var datacontext = serviceScope.ServiceProvider.GetService<DataContext>())
                {
                    datacontext.Database.Migrate();
                }
            }
        }
           
    }
}
