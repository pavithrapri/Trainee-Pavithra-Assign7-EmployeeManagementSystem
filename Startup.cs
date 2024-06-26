using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystems_A6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register ICosmosDbService
            services.AddSingleton<ICosmosDbService>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var cosmosClient = new CosmosClient(configuration["CosmosDb:Account"], configuration["CosmosDb:Key"]);
                var databaseName = configuration["CosmosDb:DatabaseName"];
                var containerName = "EmployeeContainer";
                var logger = serviceProvider.GetRequiredService<ILogger<CosmosDbService>>();
                return new CosmosDbService(cosmosClient, databaseName, containerName, logger);
            });

            // Register IEmployeeService
            services.AddSingleton<IEmployeeService, EmployeeService>();

            // Register IExcelService
            services.AddSingleton<IExcelService, ExcelService>();

            // Register Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagementSystems_A6", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagementSystems_A6 v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
