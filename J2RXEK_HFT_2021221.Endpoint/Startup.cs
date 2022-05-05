using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Endpoint.Services;
using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace J2RXEK_HFT_2021221.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IDriverLogic, DriverLogic>();
            services.AddTransient<ITeamLogic, TeamLogic>();
            services.AddTransient<IChampionshipLogic, ChampionshipLogic>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IChampionshipRepository, ChampionshipRepository>();
            services.AddTransient<ChampionshipDBContext, ChampionshipDBContext>();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x=>x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:62424"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
