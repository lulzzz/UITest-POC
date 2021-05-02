using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MOF.Etimad.Monafasat.WebApi.Infrastructure
{
   public static class ConfigurationHelper
   {
      #region GetConfiguration()
      public static IConfigurationRoot GetConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
      {
         var builder = new ConfigurationBuilder()
             .SetBasePath(path)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

         if (!String.IsNullOrWhiteSpace(environmentName))
         {
            builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
         }

         builder = builder.AddEnvironmentVariables();

         //if (addUserSecrets)
         //{
         //   builder.AddUserSecrets(); // requires adding Microsoft.Extensions.Configuration.UserSecrets from NuGet.
         //}
         return builder.Build();
      }
      #endregion
   }
}
