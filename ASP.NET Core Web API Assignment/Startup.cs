using Microsoft.EntityFrameworkCore;
using Common.DataContexts;
using Core.Interfaces;
using Sigma_Software_Task.Repositories;
using Core.Entities;
using Sigma_Software_Task.StartupServices;

namespace Sigma_Software_Task
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddResponseCaching();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<DBContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString"), b => b.MigrationsAssembly("Common")); });
            services.AddCors();
            #region DI

            services.AddScoped<DataContextsHub>();
            services.AddSingleton<CSVContext>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IGetRepository<Candidate>, DBConfigGetRepository<Candidate>>();
            services.AddScoped<IGetService<Candidate>, DBConfigGetService<Candidate>>();


            bool isCSV = Configuration.GetValue<bool>("isCSV");
            if (isCSV)
                services.AddScoped<ICandidateRepository, CSVCandidatesRepository>();
                services.AddHostedService<CsvDataloadService>();

            #endregion

        }

    }
}
