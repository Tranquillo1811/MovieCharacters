using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieCharacters.DAL.Repositories;
using MovieCharacters.BLL.Models;
using System;
using System.Reflection;
using System.IO;

namespace MovieCharactersAPI
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
            services.AddAutoMapper(typeof(Startup));

            services.Add(new ServiceDescriptor(typeof(ICharacterRepository), new CharacterRepository()));
            services.Add(new ServiceDescriptor(typeof(IFranchiseRepository), new FranchisesRepository()));
            services.Add(new ServiceDescriptor(typeof(IMovieRepository), new MovieRepository()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "MovieCharactersAPI", 
                    Version = "v1",
                    Description = "very simple Movie Characters App solely built for educational purposes",
                    TermsOfService = new Uri("https://github.com/Tranquillo1811/MovieCharacters"),
                    Contact = new OpenApiContact
                    {
                        Name = "Oliver Hauck",
                        Url = new Uri("https://github.com/Tranquillo1811")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache License © 2022",
                        Url = new Uri("https://github.com/Tranquillo1811/MovieCharacters/blob/main/LICENSE")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieCharactersAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
