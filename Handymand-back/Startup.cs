using Handymand.Controllers.Chat;
using Handymand.Data;
using Handymand.Repository.DatabaseRepositories;
using Handymand.Services;
using Handymand.Utilities;
using Handymand.Utilities.JWTUtils;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand
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
            //services.AddCors();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins(
                        "http://localhost")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod();
            }));


            services.AddControllers();

            services.AddSignalR().AddMessagePackProtocol();


            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Handymand", Version = "v1" });
            });
            services.AddDbContext<HandymandContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<IContractsSkillsRepository, ContractsSkillsRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IFreelancerRepository, FreelancerRepository>();
            services.AddTransient<IJobOfferRepository, JobOfferRepository>();
            services.AddTransient<IOffersRepository, OffersRepository>();

            services.AddTransient<IJobOfferService, JobOfferService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IOffersService, OffersService>();


            services.AddScoped<IJWTUtils, JWTUtils>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Handymand v1"));
            }


            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JWTMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/signalr");
            });

            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }
        }
    }
}
