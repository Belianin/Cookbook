using Cookbook.Common;
using Cookbook.Recipes.Repositories;
using Cookbook.Tags.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseMvc();
    }
}