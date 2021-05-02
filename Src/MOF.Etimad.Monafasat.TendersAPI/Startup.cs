using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TendersAPI.Services;

namespace MOF.Etimad.Monafasat.TendersAPI
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
            services.Configure<RootConfigurations>(Configuration.GetSection("RootConfigurations"));

            services.AddControllers();
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBankGuaranteeAppService, BankGuaranteeAppService>();
            services.AddScoped<IBankGuaranteeQueries, BankGuaranteeQueries>();
            services.AddScoped<IOfferCommands, OfferCommands>();
            services.AddScoped<IContractAppService, ContractAppService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IContractQueries, ContractQueries>();
            #region
            services.AddScoped<ITenderQueries, TenderQueries>();
            services.AddScoped<ITenderAppService, TenderAppService>();
            services.AddScoped<ITenderAppService, TenderAppService>();
            services.AddScoped<IQuantityTemplatesProxy, QuantityTemplatesProxy>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<ITenderCommands, TenderCommands>();
            services.AddScoped<ITenderDomainService, TenderDomainService>();
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IBillAppService, BillAppService>();
            //services.AddScoped<AppDbContext, AppDbContext>();
            services.AddScoped<IIDMQueries, IDMQueries>();
            services.AddScoped<ICommitteeQueries, CommitteeQueries>();
            services.AddScoped<ISupplierQueries, SupplierQueries>();
            services.AddScoped<IBlockAppService, BlockAppService>();
            services.AddScoped<IIDMAppService, IDMAppService>();
            services.AddScoped<IBranchAppService, BranchAppService>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IBillCommand, BillCommand>();
            services.AddScoped<INationalProductSettingsAppService, NationalProductSettingsAppService>();
            services.AddScoped<ISRMFrameworkAgreementManageProxy, SRMFrameworkAgreementManageProxy>();
            services.AddScoped<IOfferQueries, OfferQueries>();
            services.AddScoped<ICommunicationQueries, CommunicationQueries>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IIDMProxy, IDMProxy>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<INotificationProxy, NotificationProxy>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddScoped<IBranchServiceQueries, BranchServiceQueries>();
            services.AddScoped<INegotiationQueries, NegotiationQueries>();
            services.AddScoped<IBillingProxy, BillingProxy>();
            services.AddScoped<IBillQueries, BillQueries>();
            services.AddScoped<IBillArchiveCommand, BillArchiveCommand>();
            services.AddScoped<IBlockQueries, BlockQueries>();
            services.AddScoped<IBlockCommands, BlockCommands>();
            services.AddScoped<IYasserProxy, YasserProxy>();
            services.AddScoped<IVerificationQueries, VerificationQueries>();
            services.AddScoped<ISupplierCommands, SupplierCommands>();
            services.AddScoped<IBranchServiceCommand, BranchServiceCommand>();
            services.AddScoped<IBranchServiceDomain, BranchServiceDomain>();
            services.AddScoped<INationalProductSettingsCommandService, NationalProductSettingsCommandService>();
            services.AddScoped<INationalProductSettingsQueryService, NationalProductSettingsQueryService>();
            #endregion
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure())
                                                                                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging());
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Monafasat API", Description = "Monafasat API" }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Monafasata Api");
            });
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
