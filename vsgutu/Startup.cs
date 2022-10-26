using BusinessLayer;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace vsgutu
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDBContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("DataLayer")));

            //DP    
            services.AddTransient<IUsersRepository, EFDBUsersRepository>();
            services.AddTransient<IScoreRepository, EFDBScoreRepository>();
            services.AddTransient<IRolesOfUsersRepository, EFDBRolesOfUsersRepository>();
            services.AddTransient<ILessonRepository, EFDBLessonRepository>();
            services.AddTransient<IGroupsRepository, EFDBGroupsRepository>();
            services.AddTransient<IDisciplineRepository, EFDBDisciplineRepository>();
            services.AddTransient<ITypeLessonRepository, EFDBTypeLessonRepository>();
            services.AddTransient<IEKRepository, EFDBEKRepository>();
            services.AddTransient<ISessionScoreRepository, EFDBSessionScoreRepository>();

            services.AddScoped<DataManager>();


            services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config=>
                {
                    config.LoginPath = "/Login/Index";
                    config.AccessDeniedPath = "/Login/AccessDenied";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });
                options.AddPolicy("Student", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Student");
                });
                options.AddPolicy("Teacher", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Teacher");
                });
            });




            services.AddControllersWithViews();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
