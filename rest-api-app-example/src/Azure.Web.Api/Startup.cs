using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Filters;
using Azure.Web.Api.Business.Managers;
using Azure.Web.Api.Business.Services;
using Azure.Web.Api.Commons;
using Azure.Web.Api.DataLayer.MockData;
using Azure.Web.Api.DataLayer.Repositories;

namespace Azure.Web.Api
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
            services.AddLogging();

            services.AddApplicationInsightsTelemetry();

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                x => x.UseNetTopologySuite())
            );

            services.AddControllers().AddNewtonsoftJson(options =>
                           {
                               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                               options.SerializerSettings.Converters.Add(new StringEnumConverter());
                           })
                            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            #region MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(MyExceptionFilter));
                options.ModelValidatorProviders.Clear();
            })
            .AddDataAnnotationsLocalization()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreNullValues = false;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new MyDateTimeConverter());
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            #endregion

            #region Application Dependency Injection

            services.AddScoped<ITerritoryManager, TerritoryManager>();
            services.AddScoped<ITemperatureManager, TemperatureManager>();

            services.AddScoped<CountryService, CountryService>();
            services.AddScoped<DistrictService, DistrictService>();
            services.AddScoped<DetectionPointService, DetectionPointService>();
            services.AddScoped<TemperatureService, TemperatureService>();

            services.AddScoped<CountryRepository, CountryRepository>();
            services.AddScoped<DistrictRepository, DistrictRepository>();
            services.AddScoped<DetectionPointRepository, DetectionPointRepository>();
            services.AddScoped<TemperatureRepository, TemperatureRepository>();

            #endregion

            // Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // initialize database
            MockDataIntializer.InitDatabase(dbContext).GetAwaiter().GetResult();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
