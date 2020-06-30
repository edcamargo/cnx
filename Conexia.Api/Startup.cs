using Conexia.InfraStructure.CrossCutting.Entities;
using Conexia.InfraStructure.CrossCutting.Security;
using Conexia.InfraStructure.Data.Context;
using Conexia.InfraStructure.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace Conexia.Api
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
            services.AddControllers();
            services.AddRouting();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContextPool<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, inseira o token JWT.",
                    Name = "Autorização",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Documentação de Apis",
                        Version = "v1.0",
                        Description = "# Introducão\n Teste para Desenvolvedor Backend Conexia, espero que gostem! \n\n\n"
                                        + "API criada utilizando o padrão REST e o Pattern CQRS. \n\n\n"
                                        + "# Como usar a API?\n Logo a seguir você encontrará todos os recursos e metódos suportados pela API, sendo que essa página possibilita que você teste os recursos "
                                        + "e métodos diretamente através dela.\n\n\n# Autenticação\nVocê precisará de um Usuário e Senha para identificar a conta que está realizando solicitações para a API. \n",
                        Contact = new OpenApiContact
                        {
                            Name = "Edwin Ramos Camargo"
                        }
                    });
            });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new ApiVersion(new DateTime(2020, 6, 27));
                    options.Conventions.Add(new VersionByNamespaceConvention());
                }
            );

            services.AddSingleton<IConfiguration>(Configuration);

            DependencyInjection.DependencyInjectionHandler(ref services);
            DependencyInjection.DependencyInjectionRepository(ref services);
            DependencyInjection.DependencyInjectionServices(ref services);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Ativação Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api de Integração - Version 1.0");

                c.RoutePrefix = string.Empty;
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
        }
    }
}
