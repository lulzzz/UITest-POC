using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.JobServices.Qualification;
using MOF.Etimad.Monafasat.Services.MainServices.SupplierQualificationDocument.Abstract;
using MOF.Etimad.Monafasat.SharedKernal;


namespace MOF.Etimad.Monafasat.Jobs.Helper
{
    public class QualificatonPrepareDependancies
    {

        public ServiceCollection SetupDependancies()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var services = new ServiceCollection();
            services.Configure<RootConfigurations>(config.GetSection("RootConfigurations"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(config);
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<IQulaificationServiceJob, QualificationServiceJob>();
            services.AddScoped<IQualificationServiceQueryJob, QualificationServiceQueryJob>();
            services.AddScoped<ISupplierQualificationDocumentAppService, SupplierQualificationDocumentAppService>();
            services.AddScoped<ISupplierQualificationDocumentDomainService, SupplierQualificationDocumentDomainService>();
            services.AddScoped<ISupplierQualificationDocumentQueries, SupplierQualificationDocumentQueries>();
            services.AddScoped<ISupplierQualificationDocumentCommands, SupplierQualificationDocumentCommands>();
            services.AddScoped<ISupplierQualificationDomainService, SupplierQualificationDomainService>();
            services.AddScoped<IQualificationQueries, QualificationQueries>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<IQualificationCommands, QualificationCommands>();
            services.AddScoped<INotificationProxy, NotificationProxy>();
            services.AddScoped<IIDMProxy, IDMProxy>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IBranchServiceQueries, BranchServiceQueries>();
            services.AddScoped<ICommitteeQueries, CommitteeQueries>();
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddScoped<IIDMQueries, IDMQueries>();
            services.AddScoped<IBlockAppService, BlockAppService>();
            services.AddScoped<IIDMAppService, IDMAppService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IBlockQueries, BlockQueries>();
            services.AddScoped<IBlockCommands, BlockCommands>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<ISupplierQueries, SupplierQueries>();
            services.AddScoped<IYasserProxy, YasserProxy>();
            services.AddScoped<IVerificationQueries, VerificationQueries>();
            services.AddScoped<ITenderQueries, TenderQueries>();
            services.AddScoped<ITenderCommands, TenderCommands>();
            services.AddScoped<ITenderDomainService, TenderDomainService>();
            services.AddScoped<IOfferQueries, OfferQueries>();
            services.AddScoped<INegotiationQueries, NegotiationQueries>();
            services.AddScoped<ICommunicationQueries, CommunicationQueries>();

            services.AddLogging();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(TenderPrepareDependancies));
            return services;
        }
    }
}
