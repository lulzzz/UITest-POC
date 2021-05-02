using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MOF.Etimad.Monafasat.Web
{
   public static class SessionExtensions
   {
      public static byte[] SetObject<T>(this ISession session, string key, T obj)
      {
         if (obj == null)
            return null;
         BinaryFormatter bf = new BinaryFormatter();
         using (MemoryStream ms = new MemoryStream())
         {
            bf.Serialize(ms, obj);
            var byteArray = ms.ToArray();
            session.Set(key, byteArray);

            return byteArray;
         }
      }

      public static T GetObject<T>(this ISession session, string key)
      {
         var arrBytes = session.Get(key);
         if (arrBytes == null || arrBytes.Length == 0)
            return default(T);

         MemoryStream memStream = new MemoryStream();
         BinaryFormatter binForm = new BinaryFormatter();

         memStream.Write(arrBytes, 0, arrBytes.Length);
         memStream.Seek(0, SeekOrigin.Begin);

         return (T)binForm.Deserialize(memStream);
      }
   }
}
