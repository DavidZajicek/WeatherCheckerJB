using WeatherCheckerApi.Services;

namespace WeatherCheckerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var apiKeysSection = Configuration.GetSection("ApiKeys");

            var apiKeys = apiKeysSection.Get<List<string>>();

            if (apiKeys == null)
            {
                throw new ApplicationException("ApiKeys configuration is missing or invalid.");
            }

            services.AddSingleton<ApiKeyTracker>(provider => new ApiKeyTracker(apiKeys));

            services.AddControllers();
            services.AddScoped<WeatherCheckerService>(_ => new WeatherCheckerService("8b7535b42fe1c551f18028f64e8688f7"));
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




    }
}