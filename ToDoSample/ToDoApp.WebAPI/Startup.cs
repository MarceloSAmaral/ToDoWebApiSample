using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using ToDoApp.AppServices;
using ToDoApp.CoreObjects.AppInterfaces;
using ToDoApp.CoreObjects.RepoInterfaces;
using ToDoApp.Data;
using AutoMapper;

namespace ToDoApp.WebAPI
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
            services.AddDbContext<ToDoAppContext>(opt => opt.UseInMemoryDatabase("ToDoAppDB"), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUsersApplication, UserAppService>();
            services.AddScoped<IToDoItemsApplication, ToDoItemsApplication>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoApp.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoApp.WebAPI v1"));

                CreateInitialData(app.ApplicationServices);
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CreateInitialData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ToDoAppContext>();

            var defaultUser = new CoreObjects.Entities.User()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000001"), /*Easy to remember. This value should be a constant, for sure.*/
            };
            context.Users.Add(defaultUser);

            var someoneelseUser = new CoreObjects.Entities.User()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000002"),
            };
            context.Users.Add(someoneelseUser);
            context.SaveChanges();
        }
    }
}
