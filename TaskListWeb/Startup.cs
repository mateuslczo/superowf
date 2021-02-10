using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using TaskList._01___Application.TokenSecurity;
using TaskList._01___Application.TokenSecurity.TokenSecurityModels;
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

            /*swagger*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Api - Desafio SuperoWF",
                            Version = "v1",
                            Description = "Viabilizar requisições e persistencia de dados",
                            Contact = new OpenApiContact
                            {
                                Name = "Marcos Mateus",
                                Email = "mateuslczo@gmail.com",
                            }
                        });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Copie 'Bearer ' + token'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
              });

                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            /* WebToken*/
            // A titulo de simulação os dados de usuário para authenticação estão fixos na classe
            services.AddTransient<UserLogin>();

            var configToken = new TokenSecurityConfig();

            services.AddSingleton(configToken);

            var tokenConfig = new TokenConfig();

            new ConfigureFromConfigurationOptions<TokenConfig>(Configuration.GetSection("DataTokenConfiguration"))
                                                               .Configure(tokenConfig);

            services.AddSingleton(tokenConfig);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = configToken.Key;
                paramsValidation.ValidAudience = tokenConfig.Audience;
                paramsValidation.ValidIssuer = tokenConfig.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                paramsValidation.ClockSkew = TimeSpan.Zero;

            });

            // Ativa o uso do token como forma de autorizar o acesso a recursos do projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                          .RequireAuthenticatedUser()
                                          .Build());
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Desafio SuperoWf"); });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
