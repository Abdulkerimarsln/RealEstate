using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstate_Dapper_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = "/Login/index/";
                opt.LogoutPath= "/Login/Logout";
                opt.AccessDeniedPath= "/Pages/AccessDenied/";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "RealEstateJwt";
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ILoginService, LoginService>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                // Property rota tanýmý
                endpoints.MapControllerRoute(
                    name: "property",
                    pattern: "property/{slug}/{id?}",
                    defaults: new { controller = "Property", action = "PropertySingle" }
                );

                // Areas rota tanýmý
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                // Default rota tanýmý
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.Run();
        }
    }
}
