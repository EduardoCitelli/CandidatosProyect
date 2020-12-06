namespace CandidadatosProyect.Api
{
    using CandidatosProyect.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Sinks.MSSqlServer;
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

            services.AddSingleton<ICandidatosService>(new CandidatosService());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var conectionString = @"Server=EDU-PC\EDU;Database=CandidatosProyect;Trusted_Connection=True;";

            var sqlLoggerOptions = new MSSqlServerSinkOptions()
            {
                AutoCreateSqlTable = true,
                SchemaName = "Logger",
                TableName = "Logs",
                BatchPostingLimit = 1
            };

            Log.Logger = new LoggerConfiguration()
                            .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"))
                            .WriteTo.MSSqlServer(conectionString, sqlLoggerOptions)
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
