using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using ReadingIsGood.Domain.IMongoDb;
using ReadingIsGood.Domain.MongoDb;
using ReadingIsGood.Infrastructure.MongoDbContext;
using ReadingIsGood.Domain.Services;
using ReadingIsGood.Infrastructure.Services;
using ReadingIsGood.Infrastructure.Security;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReadingIsGood.API.Mapping;
using TokenHandler = ReadingIsGood.Infrastructure.Security.TokenHandler;
using System.Reflection;
using System.IO;

namespace ReadingIsGood.API
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
            #region Configure Mongo
            services.Configure<MongoDbConfig>(Configuration.GetSection("MongoDb"));
            services.AddSingleton<IMongoDbConfig>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbConfig>>().Value);
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            #endregion

            #region Configure Services 

            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRespository>();
            services.AddScoped<ICustomerRepository, CustomerRespository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion

            #region Jwt Token

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));

            services.AddAuthentication(jwtbeareroptions =>
            {
                jwtbeareroptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwtbeareroptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtbeareroptions =>
                {
                    jwtbeareroptions.SaveToken = true;
                jwtbeareroptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.FromMinutes(10)
                };
            });

            #endregion

            #region  Auto Mapper Configurations

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapping());
                mc.AddProfile(new OrderMapping());
                mc.AddProfile(new CustomerMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Reading Is Good API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Okan Özmen",
                        Email = "okanozmen88@gmail.com"
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Access Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReadingIsGood.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
