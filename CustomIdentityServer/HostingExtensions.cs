using CustomIdentityServer.Data;
using CustomIdentityServer.Models;
using Duende.IdentityServer;
using Identity_Server.CustomProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomIdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            builder.Services.AddIdentity<User,Role>()
                .AddDefaultTokenProviders();
            builder.Services.AddTransient<IUserStore<User>, CustomUserStore>();
            builder.Services.AddTransient<IRoleStore<Role>, CustomRoleStore>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<User>();

            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //    builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //               .AllowAnyHeader()
            //               .AllowAnyMethod();
            //    });
            //});
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            //app.UseCors();
            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}