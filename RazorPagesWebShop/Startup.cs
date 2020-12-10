using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Data;
using Microsoft.AspNetCore.Identity;
using RazorPagesWebShop.Models;
using Microsoft.AspNetCore.Http;

namespace RazorPagesWebShop
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
            services.AddRazorPages();

            services.AddDbContext<RazorPagesWebShopContext>(options =>
                //options.UseSqlite(Configuration.GetConnectionString("RazorPagesWebShopContext")));
                options.UseSqlServer(Configuration.GetConnectionString("RazorPagesWebShopContext")));
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<RazorPagesWebShopContext>().AddDefaultTokenProviders().AddDefaultUI();
            //services.AddDbContext<RazorPagesWebShopContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("WebshopContext"))); //for getting the logged user
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //TODO
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
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
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();
            });
        }
    }
}
