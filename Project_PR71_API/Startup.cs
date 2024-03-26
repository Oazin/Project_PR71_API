using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project_PR71_API.Configuration;
using Project_PR71_API.Services;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API
{
    public class Startup
    {

        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc();
            services.AddHttpClient();
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>( opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging(true);
            });

            services.AddSwaggerGen();

            // Add services
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ISavePostService, SavePostService>();
            services.AddScoped<IChatService, ChatService>();

            // Add authentication
            services.AddCors(options =>
            {
                options.AddPolicy("AllowUTgramFront", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build()
                );
            });

        }

        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Update database
            UpdateDatabase(app);

            app.UseHttpsRedirection();
            
            // Enable CORS
            app.UseCors("AllowUTgramFront");

            // Enable routing
            app.UseRouting();

            // Enable Swagger
            app.UseSwagger();

            // Enable Swagger UI
            app.UseSwaggerUI();

            // Enable authentication
            app.UseAuthentication();

            // Enable authorization
            app.UseAuthorization();

            // Enable endpoints
            app.UseEndpoints(endpoints =>
            {
                // Map controllers
                endpoints.MapControllers();
            });
        }

        // Update database
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            // Create database if not exists
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Get the database context
                using (var datacontext = serviceScope.ServiceProvider.GetService<DataContext>())
                {
                    // Apply any pending migrations
                    datacontext.Database.Migrate();
                }
            }
        }
           
    }
}
