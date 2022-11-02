using API.Extensions;
using API.Middleware;
using API.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddCors();
            services.AddIdentityServices(_config);
            services.AddSignalR();

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors((pb) =>
            {
                pb.AllowAnyHeader();
                pb.AllowAnyMethod();
                pb.AllowCredentials();
                pb.WithOrigins("https://localhost:4200");
            });

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PresenceHub>("hubs/presence");
                endpoints.MapHub<MessageHub>("hubs/message");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
