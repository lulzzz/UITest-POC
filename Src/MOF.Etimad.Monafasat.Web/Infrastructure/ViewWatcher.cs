using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class ViewWatcher
   {
      private readonly IFileProvider _fileProvider;

      public ViewWatcher(IWebHostEnvironment hostingEnv)
      {
         _fileProvider = hostingEnv.ContentRootFileProvider;
      }

      public void WatchThis(string path)
      {
         var token = _fileProvider.Watch(path);
         token.RegisterChangeCallback((_) =>
         {
            System.Diagnostics.Debugger.Launch();
            System.Diagnostics.Debugger.Break();
         }, null);
      }
   }
}
