using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Projeto.CrossCutting.Criptography;
using Projeto.CrossCutting.Mail;
using Projeto.Data.Contracts;
using Projeto.Data.Repositories;

namespace Projeto.Presentation
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("Carnaval");

            services.AddTransient<IPerfilRepository, PerfilRepository>
                (map => new PerfilRepository(connectionString));

            services.AddTransient<IUsuarioRepository, UsuarioRepository>
                (map => new UsuarioRepository(connectionString));

            services.AddTransient<MD5Encrypt>();

            services.AddTransient<SHA1Encrypt>();

            var mailSettings = new MailSettings();

            new ConfigureFromConfigurationOptions<MailSettings>
                        (configuration.GetSection("MailSettings"))
                        .Configure(mailSettings);

            services.AddTransient<MailService>
                (map => new MailService(mailSettings));

            services.Configure<CookiePolicyOptions>(options => 
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication
                (CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSession();

            services.AddMvc().SetCompatibilityVersion
                (CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes => 
            {
                routes.MapRoute(name: "default", 
                template: "{controller=Home}/{action=Index}/{id?}");
            });           
        }
    }
}
