using helpmeinvest.Models;
using helpmeinvest.Repositories;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
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

            services.Configure<IEnumerable<NewAccountType>>(
                Configuration.GetSection("NewAccountTypes"));

            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSingleton<IHelpMeInvestDbSettings>(sp =>
                sp.GetRequiredService<IOptions<HelpMeInvestDbSettings>>().Value);

            services.AddSingleton<AccountRepo>();
            services.AddSingleton<CustomerRepo>();
            services.AddSingleton<ChannelIntegrationRepo>();

            services.AddTransient<AccountService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<ChannelIntegrationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "helpmeinvest", Version = "v1" });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
