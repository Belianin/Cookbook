using System;
using Cookbook.Api.Auth;
using Cookbook.Common;
using Cookbook.Recipes.Repositories;
using Cookbook.Tags.Repositories;
using Cookbook.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cookbook.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultScheme = AuthConsts.Scheme;
        })
            .AddScheme<AuthenticationSchemeOptions, AuthHandler>(AuthConsts.Scheme, null);
        services.AddSession(x =>
        {
            x.Cookie.Name = "cookbook.sid";
            x.Cookie.IsEssential = true;
            x.Cookie.HttpOnly = true;
            x.IdleTimeout = TimeSpan.FromHours(12);
        });

        services.AddIdentityCore<User>(x =>
        {

        });

        services.AddSingleton<SessionStore>();
        services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
        services.AddSingleton<ITicketStore, InMemoryTicketStore>();
        services.AddSingleton(new SharedDirectoryWatcher(Configuration["LocalFilesData:Directory"]));
        services.AddSingleton<ITagsRepository>(x => new JsonFileTagsRepository(
            x.GetRequiredService<SharedDirectoryWatcher>(),
            Configuration["LocalFilesData:FileNames:Tags"]));
        services.AddSingleton<IRecipesRepository>(x => new JsonFileRecipesRepository(
            x.GetRequiredService<SharedDirectoryWatcher>(),
            Configuration["LocalFilesData:FileNames:Recipes"]));
        
        services.AddMvc(x =>
        {
            x.EnableEndpointRouting = false;
            x.RespectBrowserAcceptHeader = true;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            //app.UseHsts();
        }

        app.UseSession();
        app.UseAuthentication();    // аутентификация
        app.UseAuthorization();     // авторизация
        
        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseMvc();
    }
}