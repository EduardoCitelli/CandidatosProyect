namespace CandidadatosProyect.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using ServiceStack.Text;
    using System.IO;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerDocument();

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                        
            services.AddSingleton((ILogger)new LoggerConfiguration()
                                        .WriteTo.RollingFile(Path.Combine("C:\\Users\\Eduvino\\source\\repos\\CandidatosProyect\\CandidatosProyect.Client\\wwwroot\\Logs", "log-{Date}.txt"))
                                        .CreateLogger());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Log.Logger = new LoggerConfiguration()
                            .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"))
                            .CreateLogger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();

            app.UseSwaggerUi3();

            loggerFactory.AddSerilog();
        }
    }
}
