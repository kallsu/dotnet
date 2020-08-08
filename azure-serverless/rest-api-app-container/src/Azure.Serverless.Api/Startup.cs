using System;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;
using Azure.Serverless.Api.Datalayer.Context;
using Microsoft.IdentityModel.Tokens;
using src.Azure.Serverless.Api.Filters;
using src.Azure.Serverless.Api.Commons;

namespace Azure.Serverless.Api
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

            services.AddDbContext<MyDbContext>(options => options.UseNpgsql(
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
                options.Filters.Add(typeof(TokenFilter));
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

            #region JWT
            var secret = Encoding.ASCII.GetBytes(Configuration.GetSection("TokenManagementOptions:Secret").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();
            #endregion


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
