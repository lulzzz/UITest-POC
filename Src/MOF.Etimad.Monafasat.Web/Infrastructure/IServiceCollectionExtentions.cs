using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel.User;
using MOF.Etimad.Monafasat.Web.Authorization;
using MOF.Etimad.Monafasat.Web.Filters;
using MOF.Etimad.Monafasat.Web.TenderValidator;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public static class IServiceCollectionExtentions
   {
      public static void SetAllConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
      {
         services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
         services.AddSignalR();
         services.AddScoped<IUrlHelper>(factory =>
         {
            var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
            return new UrlHelper(actionContext);
         });
         services.AddScoped<ControllerExceptionFilterAttribute>();
         services.AddSingleton(configuration);
         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
         services.AddScoped<IApiClient, ApiHttpClient>();
         services.AddHttpContextAccessor();
         services.Configure<RootConfiguration>(configuration.GetSection("RootConfiguration"));
         var baseUrl = configuration.GetSection("RootConfiguration:APIConfiguration:MonafasatAPI").Value;
         services.AddHttpClient("MonafasatApi", x => { x.BaseAddress = new Uri(baseUrl); }).ConfigurePrimaryHttpMessageHandler(() =>
         {
            return new HttpClientHandler
            {
               ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
         });

         services.Configure<RequestLocalizationOptions>(options =>
         {
            options.DefaultRequestCulture = new RequestCulture("ar-SA");
            options.SupportedCultures = new List<CultureInfo> { new CultureInfo("ar-SA"), new CultureInfo("ar-SA") };
            options.RequestCultureProviders.Clear();
         });
         services.AddMemoryCache();
         services.AddMvc(options =>
         {
            options.SuppressAsyncSuffixInActionNames = false;
         });
         services.AddControllersWithViews(o =>
         {
            o.Conventions.Add(new FeatureConvention());
            o.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            o.MaxModelValidationErrors = 50;
            o.Filters.Add(new CultureSettingResourceFilter());
         }).AddRazorRuntimeCompilation()
         .AddRazorPagesOptions(option =>
         {
            option.Conventions.AllowAnonymousToPage("/Tender/AllTendersForVisitor");
         })
         .AddFluentValidation(fvc =>
         {
            fvc.RegisterValidatorsFromAssemblyContaining<CreateAnnouncementModelValidator>();
            fvc.RegisterValidatorsFromAssemblyContaining<EditTenderCommitteesDataModelValidator>();
            fvc.RegisterValidatorsFromAssemblyContaining<TenderDatesModelValidator>();
         });
         services.Configure<FormOptions>(x =>
         {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
         });
         services.AddPaging(options =>
         {
            options.ViewName = "Paging";
         });
         services.AddAuthorization();
         services.AddAutoMapper(typeof(Startup));
         services.AddSession();
         services.AddSession(option =>
         {
            option.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            option.Cookie.HttpOnly = true;
         });
         services.AddCors();
         // If using Kestrel:
         services.Configure<KestrelServerOptions>(options =>
         {
            options.AllowSynchronousIO = true;
         });

         // If using IIS:
         services.Configure<IISServerOptions>(options =>
         {
            options.AllowSynchronousIO = true;
         });
         if (env.EnvironmentName != "Development")
         {
            services.AddHsts(options =>
            {
               options.Preload = true;
               options.IncludeSubDomains = true;
               options.MaxAge = TimeSpan.FromDays(60);
               options.ExcludedHosts.Add(configuration.GetSection("RootConfiguration:DomainConfiguration:HostUrl").Value);
            });
         }
      }

      public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
      {

         AuthorizationPolicies.LoadAuthorizationInjection(services);
         services.AddAuthorization(authorizationOption => AuthorizationPolicies.LoadPolicies(authorizationOption));
         //services.AddDistributedMemoryCache();
         services.AddAuthentication(option =>
         {
            option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = "oidc";
         })
         .AddCookie((options) =>
         {
            //options.SlidingExpiration = true;
            //// Expire the session of 15 minutes of inactivity
            //options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
            options.AccessDeniedPath = configuration.GetSection("RootConfiguration:AuthorityConfiguration:AccessDeniedPath").Value;
         })
         .AddOpenIdConnect("oidc", option =>
         {
            option.Authority = configuration.GetSection("RootConfiguration:AuthorityConfiguration:AuthorityURL").Value;
            option.ClientSecret = configuration.GetSection("RootConfiguration:AuthorityConfiguration:ClientSecret").Value;
            option.ClientId = configuration.GetSection("RootConfiguration:AuthorityConfiguration:ClientId").Value;
            var changeValidateIssuer = configuration.GetSection("RootConfiguration:AuthorityConfiguration:ChangeValidateIssuer").Value.ToLower() == "true";
            option.ResponseType = "code id_token token";
            option.Scope.Add("openid");
            option.Scope.Add("offline_access");
            option.Scope.Add("profile");
            option.Scope.Add("roles");
            option.Scope.Add("MonafasatApi");
            option.SaveTokens = true;
            option.RequireHttpsMetadata = false;
            option.GetClaimsFromUserInfoEndpoint = true;
            option.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
            //option.SignedOutRedirectUri = new PathString("/signin-oidc");
            option.SignedOutRedirectUri = new PathString("/Account/Logout");

            if (changeValidateIssuer)
               option.TokenValidationParameters.ValidateIssuer = false;
            option.Events.OnAuthorizationCodeReceived = async (opt) =>
            {
               if (opt.Principal.UserRoles().Count == 0)
               {
                  return;
               }
               await SyncUserWithRolesAsync(opt);
            };
            option.Events.OnAuthenticationFailed = async (context) =>
            {
               context.HandleResponse();
               await context.Response.WriteAsync("option.Events.OnAuthenticationFailed: " + context.Exception.Message + "\n" + context.Exception.StackTrace);
            };
            option.Events.OnRemoteFailure = (context) =>
            {
               return Task.CompletedTask;
            };
            //option.Events.OnTicketReceived = async (context) =>
            //{
            //   context.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(3);
            //};
         });

      }

      private static async Task SyncUserWithRolesAsync(AuthorizationCodeReceivedContext opt)
      {
         string accessToken = opt.ProtocolMessage.AccessToken;
         IApiClient repository = opt.HttpContext.RequestServices.GetService<IApiClient>();
         LoggedUserModel loggedUserModel = await repository.PostAsync<LoggedUserModel>("Account/SyncUserProfile", null, null, accessToken);
         if (!string.IsNullOrEmpty(opt.Principal.SupplierNumber()))
         {
            IdentityConfigs.SetNotSubscription(opt.Principal, loggedUserModel.SubscirpeUrl);
            IdentityConfigs.SetIsSubscription(opt.Principal, loggedUserModel.IsSubscripe);
         }
         if (loggedUserModel.AssignedRoleLevelTypeModels.Count() > 0)
         {
            string firstRole = "";
            var nonRegisteredGovService = !loggedUserModel.IsSubscripe && opt.Principal.IsInRole(RoleNames.supplier) && loggedUserModel.AssignedRoleLevelTypeModels.Count > 1 && ((loggedUserModel.DefaultRoleDetails != null && loggedUserModel.DefaultRoleDetails.Contains(RoleNames.supplier)) || loggedUserModel.AssignedRoleLevelTypeModels.Where(x => x.AssignedRoleLevel == (int)Enums.AssignedRoleLevelType.NotAssigned).Select(s => s.GetDefaultRole).FirstOrDefault().Contains(RoleNames.supplier));
            if (!string.IsNullOrEmpty(loggedUserModel.DefaultRoleDetails) && loggedUserModel.AssignedRoleLevelTypeModels.Select(s => s.GetDefaultRole).Contains(loggedUserModel.DefaultRoleDetails) && !nonRegisteredGovService)
            {
               firstRole = loggedUserModel.DefaultRoleDetails;
            }
            else if (loggedUserModel.AssignedRoleLevelTypeModels.Where(x => x.AssignedRoleLevel == (int)Enums.AssignedRoleLevelType.NotAssigned).Count() > 0)
            {
               if (nonRegisteredGovService && loggedUserModel.AssignedRoleLevelTypeModels.Where(x => x.AssignedRoleLevel == (int)Enums.AssignedRoleLevelType.NotAssigned).Select(s => s.GetDefaultRole).FirstOrDefault().Contains(RoleNames.supplier))
                  firstRole = loggedUserModel.AssignedRoleLevelTypeModels.FirstOrDefault(a => !a.GetDefaultRole.Contains(RoleNames.supplier)).GetDefaultRole;
               else
                  firstRole = loggedUserModel.AssignedRoleLevelTypeModels.Where(x => x.AssignedRoleLevel == (int)Enums.AssignedRoleLevelType.NotAssigned).Select(s => s.GetDefaultRole).FirstOrDefault();
            }
            else
            {
               if (nonRegisteredGovService && loggedUserModel.AssignedRoleLevelTypeModels.FirstOrDefault().GetDefaultRole.Contains(RoleNames.supplier))
                  firstRole = loggedUserModel.AssignedRoleLevelTypeModels.FirstOrDefault(a => !a.GetDefaultRole.Contains(RoleNames.supplier)).GetDefaultRole;
               else
                  firstRole = loggedUserModel.AssignedRoleLevelTypeModels.FirstOrDefault().GetDefaultRole;
            }
            string isBranchOrCommittee = firstRole.Split(',')[0]; // 1 =branch , 2 = Committee
            string branchOrCommitteeId = firstRole.Split(',')[1];
            string roleName = firstRole.Split(',')[2];
            if (isBranchOrCommittee == ((int)Enums.AssignedRoleLevelType.Branch).ToString())
               IdentityConfigs.AddUserBranchId(opt.Principal, branchOrCommitteeId);
            else if (isBranchOrCommittee == ((int)Enums.AssignedRoleLevelType.Committee).ToString())
               IdentityConfigs.AddUserCommittee(opt.Principal, branchOrCommitteeId);
            IdentityConfigs.UpdateUserRoles(opt.Principal, new List<string> { roleName });
         }
         else
         {
            IdentityConfigs.ReomveAllUserRoles(opt.Principal);
         }
         // Remove UnUsed Calims
         // 1
         IdentityConfigs.RemoveClaimByName(opt.Principal, "nationalityCode");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "englishFullname");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "englishFirstName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "englishSecondName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "englishThirdName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "englishLastName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "dateOfBirth");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "dateOfBirthHijri");
         // 2
         IdentityConfigs.RemoveClaimByName(opt.Principal, "permission");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "lastLoginGate");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "idExpiryDateStringHijri");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "idExpiryDateString");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "firstName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "secondName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "thirdName");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "lastName");

         IdentityConfigs.RemoveClaimByName(opt.Principal, "genderString");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "at_hash");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "c_hash");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "s_hash");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "nbf");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "aud");
         IdentityConfigs.RemoveClaimByName(opt.Principal, "name");
         IdentityConfigs.UpdateUserRolesList(opt.Principal, loggedUserModel.AssignedRoleLevelTypeModels);
      }
   }
}
