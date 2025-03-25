using Microsoft.OpenApi.Models;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Application.UseCases;
using System.Reflection;

namespace CleanArchitecture.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //✅ Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Clean Architecture API",
                        Version = "v1",
                        Description = "Clean Architecture API using ASP.NET Core",
                        Contact = new OpenApiContact
                        {
                            Name = "Ruben Lecona",
                            Email = "ruben.lecona@gmail.com",
                            Url = new Uri("https://github.com/jrlecona")
                        }
                    }
                );

                // 📝 Enable Comment XML in Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Inyección de dependencias
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<CreateUserUseCase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //📝 Enable XML comment in Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
