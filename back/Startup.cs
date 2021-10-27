using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Politics.Data;
using Politics.Mapping;
using Politics.Model;

namespace Politics
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
      // Other DI initializations

      services.AddDbContext<PoliticsDbContext>(options =>
              options.UseNpgsql(Configuration.GetConnectionString("PoliticsDatabase")));

      var mapperConfig = new MapperConfiguration(config =>
      {
        config.AddProfile(new MappingProfile());
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);
      services.AddScoped<IPoliticiansRepository, PoliticiansRepository>();
      services.AddScoped<IPartiesRepository, PartiesRepository>();
      services.AddScoped<IStatementsRepository, StatementsRepository>();
      services.AddScoped<ITagsRepository, TagsRepository>();

      services.AddCors(options => options.AddPolicy(
        "PoliticsCORSPolicy",
        builder =>
        {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
      ));

      services.AddControllers();
      services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors("PoliticsCORSPolicy");

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Politics API");
        c.RoutePrefix = string.Empty;
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
