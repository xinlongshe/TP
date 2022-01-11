using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Test_4._0.Data;
using Test_4._0.Data.Model;
using Test_4._0.Common;
using Microsoft.AspNetCore.Mvc;

namespace Test_4._0
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
            services.AddControllers();


            //·¢ÓÊ¼þ ×¢Èë
            var config = new SendMailConfig();
            Configuration.GetSection("CommonConfig:sendMessage:SendMailConfig").Bind(config);
            services.AddSingleton(config);
            services.AddScoped<ISendMail, SendMail>();

            services.AddTransient<IDapperRepository<PrivacyUser>, DapperRepository<PrivacyUser>>();
            services.AddTransient<IDapperRepository<Trainer>, DapperRepository<Trainer>>();
            services.AddTransient<IDapperRepository<Trainee>, DapperRepository<Trainee>>();

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "test", Version = "v1" });
                var documentFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
                    Assembly.GetEntryAssembly()?.GetName().Name + ".xml");

                if (File.Exists(documentFile))
                {
                    options.IncludeXmlComments(documentFile, true);
                }
                var coreXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
                    "test.Swagger.xml");
                if (File.Exists(coreXml))
                {
                    options.IncludeXmlComments(coreXml, true);
                }

            });

            #endregion
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //JSON serializer
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddRazorPages();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                   .AddRazorPagesOptions(o =>
                   {
                       o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
                   });
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

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            //Enbale CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "test API V1"); });

        }
    }
}