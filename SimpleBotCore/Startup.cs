using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBotCore.Bot;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using MongoDB.Driver;

namespace SimpleBotCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var cliente = new MongoClient();
            var perguntasRepositorio = new PerguntasMongoRepositorio(cliente);
            services.AddSingleton<IPerguntasRepositorio>(perguntasRepositorio);

            services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
            services.AddSingleton<IBotDialogHub, BotDialogHub>();
            services.AddSingleton<BotDialog, SimpleBot>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddSingleton<IUserProfileRepository, UserProfileMockRepository>();

            services.AddControllers();
        }

        public void ConfigureDataAcess(IServiceCollection services)
        {

            services.AddSingleton(Configuration.GetConnectionString("MongoDb"));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
