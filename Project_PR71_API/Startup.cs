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

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ISavePostService, SavePostService>();
            services.AddScoped<IChatService, ChatService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowUTgramFront", builder => builder
                    //.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build()
                );
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseCors("AllowUTgramFront");

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
