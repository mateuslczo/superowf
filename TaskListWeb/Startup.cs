using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskList._01___Application.ViewModels.MapperConfig;
using TaskList._01___Domain.Interfaces;
using TaskList._03___Infra.Repositories;
using TaskList._03___Infra.Repositories.DatabaseContext;

namespace TaskListWeb
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
            services.AddControllers();

         services.AddDbContext<DataContext>(options => options
                                               .UseInMemoryDatabase("DBTASKLIST")
                                               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                                   );

            services.AddScoped<DataContext, DataContext>();
            services.AddScoped<IDataTransaction, DataTransaction>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddCors();

            /*Automapper*/
            var config = new AutoMapper.MapperConfiguration(c =>
            {

                c.AddProfile(new MapperConfig());

            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //} else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
