namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class EncryptionConfiguration
   {
      public string EncryptionKey { get; set; }
      public string EncryptionIV { get; set; }
      public string EncryptionPrefix { get; set; }
      public string HashIterationCounts { get; set; }
   }

   public class MainConfiguration
   {
      public string ExceptionDevelopmentMode { get; set; }
   }

   public class APIConfiguration
   {
      public string MonafasatAPI { get; set; }
   }

   public class ReportingConfiguration
   {
      public string Web { get; set; }
   }

   public class UniqueSectionConfiguration
   {
      public bool IsPhoneNumberlUniqe { get; set; }
      public bool IsEmailUniq { get; set; }
   }

   public class WorkingTimeConfiguration
   {
      public string StatringTime { get; set; }
      public string EndTime { get; set; }
   }



   public class WorkingEndTimeConfiguration
   {
      public string AllDaysTime { get; set; }
      public string VacationDaysTime { get; set; }
   }

   public class AuthorityConfiguration
   {
      public string AuthorityURL { get; set; }
      public string ClientSecret { get; set; }
      public string ClientId { get; set; }
      public string AccessDeniedPath { get; set; }
      public bool ChangeValidateIssuer { get; set; }
   }

   public class SubScriptionConfiguration
   {
      public string IsSubscribe { get; set; }
   }

   public class CachingConfiguration
   {
      public string CachingDays { get; set; }
      public string CachingSeconds { get; set; }
      public string CachingHours { get; set; }
      public string CachingMinutes { get; set; }
      public string TenderForVisitorInMinutes { get; set; }
   }

   public class DomainConfiguration
   {
      public string HostUrl { get; set; }
   }

   public class IsOldSystemConfiguration
   {
      public bool Value { get; set; }
   }

   public class MonafasatURLConfiguration
   {
      public string MonafasatURLValue { get; set; }
   }

   public class ConditionsTemplateConfiguration
   {
      public string RegulationsDate { get; set; }
   }

   public class OldMonafasatConfiguration
   {
      public bool ShowLink { get; set; }
      public string VisitorLinkUrl { get; set; }
      public string SupplierLinkUrl { get; set; }
      public string GovLinkUrl { get; set; }
   }

   public class LocalContentFilesConfiguration
   {
      public string LocalContentManualForGov { get; set; }
      public string LocalContentManualForCommittee { get; set; }
   }

   public class MemoryCacheTimeOutMinutesConfiguration
   {
      public int MemoryCacheTimeOutMinutesValue { get; set; }
   }

   public class HttpClientSettingConfiguration
   {
      public int Timeout { get; set; }
      public int UploadTimeout { get; set; }
   }

   public class Testing
   {
      public bool IsTesting { get; set; }
      public string AccessToken { get; set; }
   }

   public class OfferTimesConfiguration
   {
      public int StartOfferTime { get; set; }
      public int EndOfferTime { get; set; }
      public int StartOfferVacationTime { get; set; }
      public int EndOfferVacationTime { get; set; }
   }
   public class AdrumConfiguration
   {
      public string beaconUrlHttps { get; set; }
      public string appKey { get; set; }
   }

   public class RootConfiguration
   {
      public EncryptionConfiguration EncryptionConfiguration { get; set; } = new EncryptionConfiguration();
      public MainConfiguration MainConfiguration { get; set; } = new MainConfiguration();
      public APIConfiguration APIConfiguration { get; set; } = new APIConfiguration();
      public ReportingConfiguration ReportingConfiguration { get; set; } = new ReportingConfiguration();
      public UniqueSectionConfiguration UniqueSectionConfiguration { get; set; } = new UniqueSectionConfiguration();
      public WorkingTimeConfiguration WorkingTimeConfiguration { get; set; } = new WorkingTimeConfiguration();
      public AuthorityConfiguration AuthorityConfiguration { get; set; } = new AuthorityConfiguration();
      public SubScriptionConfiguration SubScriptionConfiguration { get; set; } = new SubScriptionConfiguration();
      public CachingConfiguration CachingConfiguration { get; set; } = new CachingConfiguration();
      public DomainConfiguration DomainConfiguration { get; set; } = new DomainConfiguration();
      public IsOldSystemConfiguration IsOldSystemConfiguration { get; set; } = new IsOldSystemConfiguration();
      public MonafasatURLConfiguration MonafasatURLConfiguration { get; set; } = new MonafasatURLConfiguration();
      public ConditionsTemplateConfiguration ConditionsTemplateConfiguration { get; set; } = new ConditionsTemplateConfiguration();
      public OldMonafasatConfiguration OldMonafasatConfiguration { get; set; } = new OldMonafasatConfiguration();
      public LocalContentFilesConfiguration LocalContentFilesConfiguration { get; set; } = new LocalContentFilesConfiguration();
      public MemoryCacheTimeOutMinutesConfiguration MemoryCacheTimeOutMinutesConfiguration { get; set; } = new MemoryCacheTimeOutMinutesConfiguration();
      public HttpClientSettingConfiguration HttpClientSettingConfiguration { get; set; } = new HttpClientSettingConfiguration();
      public Testing Testing { get; set; } = new Testing();
      public OfferTimesConfiguration OfferTimesConfiguration { get; set; }
      public AdrumConfiguration AdrumConfiguration { get; set; }

      public WorkingEndTimeConfiguration WorkingEndTimeConfiguration { get; set; }


   }
}
