using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.WebApi;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.IO;

namespace MOF.Etimad.Monafasat.FunctionalTests.Base
{
    public class BaseTestApiController
    {
        protected IAppDbContext _appDbContext;
        protected string _connection;
        protected IConfigurationRoot _configuration;
        protected IHttpContextAccessor _httpContextAccessor;
        protected ServiceCollection services;

        public BaseTestApiController()
        {
            this.SetUp();

        }


        public void SetUp()
        {
            services = new ServiceCollection();


            _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext = new DefaultHttpContext();

            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            services.Configure<RootConfigurations>(_configuration.GetSection("RootConfigurations"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure())
                                                                                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging());
            services.AddMemoryCache();
            services.AddLogging();
            services.AddAutoMapper(typeof(Startup));

            services.SetAllConfiguration(_configuration);
        }


    }
}