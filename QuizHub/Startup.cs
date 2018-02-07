using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizHub.Auth;
using QuizHub.Database;
using QuizHub.Models.ConfigurationModels;
using QuizHub.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MyCodeCamp
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IGenerateJwts, QuizJwt>();
            services.AddSingleton<IGenerateJwts, QuizJwt>();
            services.AddSingleton<IGetMongoCollections, QuizHubRepository>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IRegisterUserService, RegisterUserService>();
            services.AddSingleton<IHashPasswords, QuizPasswordHash>();
            services.AddSingleton<IVerifyPasswords, QuizPasswordHash>();
            services.AddSingleton<ILoginService, LoginService>();
            
            services.AddCors(config =>
            {
                config.AddPolicy("TestPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .WithOrigins("https://www.reddit.com");
                });
            });
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
            });

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);
        }
    }
}
