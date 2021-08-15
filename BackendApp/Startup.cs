using Amazon.S3;
using Clothesy.Application.Persistence;
using Clothesy.Application.Services;
using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Clothesy.ApiApp
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = "Gakko",

                            ValidateAudience = true,
                            ValidAudience = "Students",

                            ValidateLifetime = true,

                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto"))
                        };
                    });

            services.AddScoped<IClothesyDb, ClothesyDbContext>();
            services.AddDbContext<ClothesyDbContext>(opt =>
            {
                opt.UseSqlServer("Data Source=db-mssql;Initial Catalog=s15264;Integrated Security=True");
            });

            //Configure Mediatr
            services.AddMediatR(typeof(IClothesyDb).Assembly);

            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("ServiceConfiguration");
            services.AddAWSService<IAmazonS3>();
            services.Configure<ServiceConfiguration>(appSettingsSection);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors(conf =>
            {
                //TODO should be changed
                conf.AllowAnyOrigin();
                conf.AllowAnyMethod();
                conf.AllowAnyHeader();
            });

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
