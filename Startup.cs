using helpmeinvest.Models;
using helpmeinvest.Repositories;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace helpmeinvest
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
            services.Configure<HelpMeInvestDbSettings>(
                Configuration.GetSection(nameof(HelpMeInvestDbSettings)));

            services.Configure<NewAccountTypes>(
                Configuration.GetSection("NewAccountTypes"));

            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            //services.AddMvc();

            services.AddSingleton<IHelpMeInvestDbSettings>(sp =>
                sp.GetRequiredService<IOptions<HelpMeInvestDbSettings>>().Value);

            services.AddSingleton<INewAccountTypes>(sp => 
                sp.GetRequiredService<IOptions<NewAccountTypes>>().Value);

            services.AddSingleton<AccountRepo>();
            services.AddSingleton<CustomerRepo>();
            services.AddSingleton<ChannelIntegrationRepo>();
            services.AddSingleton<CredentialRepo>();

            services.AddTransient<AccountService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<ChannelIntegrationService>();
            services.AddTransient<AccountEligibilityService>();
            services.AddTransient<LoginService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "helpmeinvest", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] { }}
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:44307")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "helpmeinvest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
