namespace MyApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine($"In ConfigureServices()");
        services.AddControllers();
        services.AddLogging();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    Console.WriteLine($"Before AllowAnyOrigin()");
                    //options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
                    policy.WithOrigins("https://localhost:5001").AllowAnyMethod();
                    policy.WithOrigins("https://main.d1xs1i0yeekm3q.amplifyapp.com").AllowAnyMethod();

                    policy.AllowAnyOrigin();  //set the allowed origin  
                });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}