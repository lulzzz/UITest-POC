using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{

   public static class WebUtil
   {

      #region Serialize/Deserialize/Clone Utilities

      private static List<string> preTypes = new List<string>();

      /// <summary>
      /// Creates a clone of complex type object with all complex sub types avoiding the serialization infinit loop.
      /// </summary>
      /// <param name="objSource">Object instance to clone</param>
      /// <returns>the clone</returns>
      public static object CloneObject(object objSource)
      {
         //step : 1 Get the type of source object and create a new instance of that type
         Type typeSource = objSource.GetType();
         object objTarget = Activator.CreateInstance(typeSource);

         if (typeSource.IsGenericType)
         {
            int index = typeSource.GenericTypeArguments[0].Name.IndexOf('_');
            string typeName = index == -1 ? typeSource.GenericTypeArguments[0].Name : typeSource.GenericTypeArguments[0].Name.Substring(0, index);
            if (preTypes.Contains(typeName))
               return objTarget;
            Convert.ChangeType(objTarget, typeSource);
            foreach (var item in (dynamic)Convert.ChangeType(objSource, typeSource))
            {
               var value = CloneObject(item);
               ((dynamic)objTarget).Add(value);
            }
         }

         //Step2 : Get all the properties of source object type
         PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

         //Step : 3 Assign all source property to taget object 's properties
         foreach (PropertyInfo property in propertyInfo)
         {
            //Check whether property can be written to
            if (property.CanWrite)
            {
               //Step : 4 check whether property type is value type, array, enum or string type
               if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)) || property.PropertyType.IsArray)
               {
                  property.SetValue(objTarget, property.GetValue(objSource, null), null);
               }
               //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
               else
               {
                  object objPropertyValue = property.GetValue(objSource, null);
                  if (objPropertyValue == null)
                  {
                     property.SetValue(objTarget, null, null);
                  }
                  else
                  {
                     int index = typeSource.Name.IndexOf('_');
                     string typeName = index == -1 ? typeSource.Name : typeSource.Name.Substring(0, index);
                     if (!preTypes.Contains(typeName))
                        preTypes.Add(typeName);
                     property.SetValue(objTarget, CloneObject(objPropertyValue), null);
                  }
               }
            }
         }
         return objTarget;
      }

      /// <summary>
      /// Serialize one flat object of T type to xml string
      /// </summary>
      /// <typeparam name="T">Type of the object</typeparam>
      /// <param name="SerializedObject">Object to serialize</param>
      /// <returns>Empty string if any exception</returns>
      public static string SerializeXML<T>(T SerializedObject)
      {
         try
         {
            StringBuilder xml = new StringBuilder();
            Type objTyp = typeof(T);
            foreach (var prop in typeof(T).GetProperties().ToList())
            {
               if (prop.PropertyType.Namespace != objTyp.Namespace &&
                   prop.PropertyType.Namespace != "System.Collections.Generic" &&
                   !prop.IsDefined(typeof(XmlIgnoreAttribute)))
                  if (prop.GetValue(SerializedObject, null) != null)
                  {
                     xml.AppendFormat("\n<{0}>{1}</{0}>", prop.Name, prop.GetValue(SerializedObject, null).ToString());
                  }
            }
            return string.Format(@"<?xml version='1.0' encoding='utf-16'?>
                           <{0} xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>{1}</{0}>", objTyp.Name, xml.ToString());
         }
         catch (Exception)
         {
            return "";
         }
      }

      /// <summary>
      /// Serialize list object of T type to xml string
      /// </summary>
      /// <typeparam name="T">List Type</typeparam>
      /// <param name="serializedObjectLst">The list object</param>
      /// <returns>Empty string if any exception</returns>
      public static string SerializeListXML<T>(List<T> serializedObjectLst)
      {
         try
         {
            StringBuilder xml = new StringBuilder();
            Type objTyp = typeof(T);
            foreach (T obj in serializedObjectLst)
            {
               xml.AppendFormat("<{0}>", objTyp.Name);
               foreach (var prop in typeof(T).GetProperties().ToList())
               {
                  if (prop.PropertyType.Namespace != objTyp.Namespace &&
                      prop.PropertyType.Namespace != "System.Collections.Generic" &&
                      !prop.IsDefined(typeof(XmlIgnoreAttribute)))
                     if (prop.GetValue(obj, null) != null)
                        xml.AppendFormat("\n<{0}>{1}</{0}>", prop.Name, prop.GetValue(obj, null).ToString());
               }
               xml.AppendFormat("</{0}>", objTyp.Name);
            }

            return string.Format(@"<?xml version='1.0' encoding='utf-16'?>
                           <{0}s xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>{1}</{0}s>", objTyp.Name, xml.ToString());
         }
         catch
         {
            return "";
         }
      }

      /// <summary>
      /// Return object instance of type T deserialized from string
      /// </summary>
      /// <typeparam name="T">Object type</typeparam>
      /// <param name="SerializedObj">The strialization string</param>
      /// <returns>T object</returns>
      public static T DeserializeItstring<T>(string SerializedObj)
      {
         T instance = (T)Activator.CreateInstance(typeof(T));
         byte[] byteArray = Encoding.Unicode.GetBytes(SerializedObj);
         MemoryStream stream = new MemoryStream(byteArray);
         XDocument doc = XDocument.Load(stream);
         Type objType = typeof(T);
         foreach (var prop in objType.GetProperties().ToList())
         {
            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (doc.Element(objType.Name).Element(prop.Name) != null)
            {
               prop.SetValue(instance, Convert.ChangeType(doc.Element(objType.Name).Element(prop.Name).Value, t));
            }
         }
         return instance;
      }

      /// <summary>
      /// Return list object of type T deserialized from string
      /// </summary>
      /// <typeparam name="T">object type</typeparam>
      /// <param name="serializedLstObj">the serialization string of the list</param>
      /// <returns></returns>
      public static List<T> DeserializeLst<T>(string serializedLstObj)
      {
         List<T> collectionList = new List<T>();
         Type objType = typeof(T);
         T instance = default(T);
         byte[] byteArray = Encoding.Unicode.GetBytes(serializedLstObj);
         MemoryStream stream = new MemoryStream(byteArray);
         XDocument doc = XDocument.Load(stream);
         var list = doc.Root.Elements(objType.Name).ToList();
         foreach (XElement innerXMLObj in list)
         {
            instance = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in objType.GetProperties().ToList())
            {
               Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
               if (innerXMLObj.Element(prop.Name) != null)
               {
                  prop.SetValue(instance, Convert.ChangeType(innerXMLObj.Element(prop.Name).Value, t));
               }
            }
            collectionList.Add(instance);
         }

         return collectionList;
      }

      #endregion Serialize/Deserialize/Clone Utilities

      #region IO/ Temp folder Utilities

      /// <summary>
      /// Clean Temp folder
      /// </summary>
      public static void CleanTempFolder(string tempFolder, string webRoot)
      {
         string tempPath = Path.Combine(webRoot, tempFolder);
         if (System.IO.Directory.Exists(tempPath))
         {
            string[] tempFiles = System.IO.Directory.GetFiles(tempPath);
            foreach (string tempFile in tempFiles)
            {
               if (tempFile.EndsWith(".config")) continue;
               try
               {
                  System.IO.File.Delete(tempFile);
               }
               catch { }
            }
         }
      }

      /// <summary>
      /// Returns byte array of the file and optionally delete it
      /// </summary>
      /// <param name="PathToFile">phizical path of the file</param>
      /// <param name="deleteTheFile">If true delete the file before return</param>
      /// <returns></returns>
      public static byte[] GetArrayFromFile(string PathToFile, bool deleteTheFile = true)
      {
         using (FileStream fs = new FileStream(PathToFile, FileMode.OpenOrCreate, FileAccess.Read))
         {
            byte[] matriz = new byte[fs.Length];
            fs.Read(matriz, 0, System.Convert.ToInt32(fs.Length));
            if (deleteTheFile)
            {
               try { File.Delete(PathToFile); }
               catch { }
            }
            return matriz;
         }
      }

      /// <summary>
      /// Returns byte array of a file in temp folder and optionally delete it
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="deleteTheFile"></param>
      /// <returns></returns>
      public static byte[] GetArrayFromFileInTempFolder(string tempFolder, string webRoot, string fileName, bool deleteTheFile = true)
      {
         var fileFullPath = Path.Combine(webRoot, tempFolder, fileName);
         return GetArrayFromFile(fileFullPath, deleteTheFile);
      }
      public static byte[] GetArrayFromFileInTempFolder(string fileName, string root, bool deleteTheFile = true)
      {
         fileName = fileName.Replace("/Upload/GetFile?id=", "");
         //var fileFullPath = Path.Combine(GetTempFolderServer(), fileName);
         return GetArrayFromFileInTempFolder("Temp", root, fileName);
      }
      public static string GetTempFolder()
      {
         return "Temp";
         //return ConfigurationManager.AppSettings["TempDir"];
      }

      public static string GetTempFolderServer(string subdir = "")
      {

         return "/" + GetTempFolder() + "/" + subdir;
      }
      /// <summary>
      /// Create file with specific byteArray content
      /// </summary>
      /// <param name="fullFileName">file name to create</param>
      /// <param name="byteArray">file content</param>
      public static void ByteArrayToFile(string fullFileName, byte[] byteArray)
      {
         // Open file for reading
         using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create, FileAccess.Write))
         {
            // Writes a block of bytes to this stream using data from
            // a byte array.
            fileStream.Write(byteArray, 0, byteArray.Length);
         }
      }

      /// <summary>
      /// Create file in temp folder with specific content and Extension. file name will be of HashedSessionID__Guid[Ext]
      /// </summary>
      /// <param name="byteArray">File content</param>
      /// <param name="Ext">file extension</param>
      /// <returns>relative path of the saved file</returns>
      //public static string ByteArrayToFileInTempFolder(byte[] byteArray, string Ext = ".pdf")
      //{
      //    string fileName = GenerateNewFileName(Ext);
      //    string filePath = Path.Combine(GetTempFolderServer(), fileName);
      //    try
      //    {
      //        ByteArrayToFile(filePath, byteArray);
      //        return fileName;
      //    }
      //    catch
      //    {
      //        return "";
      //    }
      //}

      ///// <summary>
      ///// Method to resize photo to fit user profile info display.save the new image to the Temp folder
      ///// </summary>
      ///// <param name = "originalImage" > Image to resize</param>
      ///// <returns>Temp file relative path</returns>
      //public static string getProfilePhoto(byte[] originalImage)
      //{
      //    string fileName = GenerateNewFileName(".Jpeg");
      //    string filePath = Path.Combine(GetTempFolderServer(), fileName);
      //    try
      //    {
      //        //get bitmap from byte array
      //        Bitmap bmp;
      //        using (var ms = new MemoryStream(originalImage))
      //        {
      //            bmp = new Bitmap(ms);
      //            ResizeImage(bmp, 60, 60, 500, filePath);
      //        }

      //        return string.Format("{0}/{1}", GetTempFolder(), fileName);
      //    }
      //    catch
      //    {
      //        return "";
      //    }
      //}

      //public static string ImageByteArrayToFileInThumbsFolder(string tempFolder, string userName, string webRoot, byte[] byteArray)
      //{
      //    if (byteArray == null) return "";
      //    string fileName = string.Format("{0}__thumb{1}", userName, ".jpg");
      //    string filePath = Path.Combine(webRoot, tempFolder, fileName);
      //    try
      //    {
      //        ByteArrayToFile(filePath, byteArray);
      //        return string.Format("{0}/{1}", tempFolder, fileName);
      //    }
      //    catch
      //    {
      //        return "";
      //    }
      //}

      public static string GetUserThumbPhotoUrl(string tempFolder, string webRoot, string userName)
      {
         string fileName = string.Format("{0}__thumb{1}", userName, ".jpg");
         string filePath = Path.Combine(webRoot, tempFolder, fileName);
         try
         {
            if (File.Exists(filePath))
               return string.Format("{0}/{1}", tempFolder, fileName);
            return "";
         }
         catch
         {
            return "";
         }
      }

      public static string GetSessionIdentifier(HttpContext _httpContext)
      {
         if (_httpContext.Session.GetString("sessionIdentifier") == null) _httpContext.Session.SetString("sessionIdentifier", Guid.NewGuid().ToString());
         return _httpContext.Session.GetString("sessionIdentifier");
      }

      public static string GenerateNewFileName(string extension, HttpContext _httpContext)
      {
         if (string.IsNullOrWhiteSpace(extension))
            throw new ArgumentNullException("File extension should be provided!");

         if (!extension.StartsWith("."))
            extension = string.Concat(".", extension);
         extension = extension.ToLower();

         return string.Format("{0}{1}", GenerateNewFileName(_httpContext), extension);
      }

      public static string GenerateNewFileName(HttpContext _httpContext)
      {
         return string.Format("{0}__{1}", GetSessionIdentifier(_httpContext), Path.GetRandomFileName());
      }

      //public static string GetTempFolder(string configuration)
      //{
      //    return configuration.GetSection("Configuration:TempDir").Value;
      //}

      //public static string GetTempFolderServer(string tempDir,string webRoot)
      //{
      //    return Path.Combine(new HostingEnvironment().WebRootPath, GetTempFolder(configuration));
      //}
      /// <summary>
      /// Returns byte array of a file in temp folder and optionally delete it
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="deleteTheFile"></param>
      /// <returns></returns>

      #endregion IO/ Temp folder Utilities

      #region Razor

      ///// <summary>
      ///// Returns the string represents rendered view/partial view using provided model
      ///// </summary>
      ///// <param name="thisController">Caller controller</param>
      ///// <param name="viewName">View/partial view name</param>
      ///// <param name="model">View model</param>
      ///// <returns>String</returns>
      //public static string RenderPartialViewToString(ControllerBase thisController, string viewName, object model)
      //{
      //    // assign the model of the controller from which this method was called to the instance of the passed controller (a new instance, by the way)
      //    thisController.ViewData.Model = model;

      //    // initialize a string builder
      //    using (StringWriter sw = new StringWriter())
      //    {
      //        ViewEngineResult viewResult;
      //        //= ViewEngines.Engines.FindView(thisController.ControllerContext, viewName, null);
      //        // find and load the view or partial view, pass it through the controller factory

      //        //if (viewResult.View == null)
      //        viewResult = ViewEngines.Engines.FindPartialView(thisController.ControllerContext, viewName);
      //        ViewContext viewContext = new ViewContext(thisController.ControllerContext, viewResult.View, thisController.ViewData, sw);

      //        // render it
      //        viewResult.View.RenderAsync(viewContext);//, sw

      //        //return the razorized view/partial-view as a string
      //        return sw.ToString();
      //    }
      //}


      #endregion Razor

      #region Others

      public static string IsActiveMenuItem(IHttpContextAccessor httpContextAccessor, string controllerName)
      {
         return httpContextAccessor.HttpContext.GetRouteData().Values["controller"].ToString().ToLower() == controllerName.ToLower()
             ? "active"
             : "";
      }

      /// <summary>
      /// Generates simple random code of 5 degits
      /// </summary>
      /// <returns></returns>
      public static string GenerateRandomCode()
      {
         return (new System.Random().Next(10000, 99999).ToString());
      }

      /// <summary>
      /// Generates alphanumeric random code of specific length
      /// </summary>
      /// <param name="length"></param>
      /// <returns></returns>
      public static string GenerateComplexRandomCode(int length)
      {
         string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
         string res = "";
         System.Random rnd = new System.Random();
         while (0 < length--)
            res += valid[rnd.Next(valid.Length)];
         return res;
      }

      /// <summary>
      /// Returns DateTime string with specific format using UmAlQuraCalendar
      /// </summary>
      /// <param name="dateTime">DateTime object</param>
      /// <param name="format">date time format to use</param>
      /// <returns></returns>
      public static string GetHigriDateWithFormat(DateTime? dateTime, string format)
      {
         string result = String.Empty;

         if (dateTime == null)
            return result;

         try
         {
            CultureInfo arCul = new CultureInfo("ar-SA");
            arCul.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
            result = dateTime.Value.ToString(format, arCul);
         }
         catch { result = String.Empty; }
         return result;
      }

      /// <summary>
      /// Check if provided string is saudi number (started with 05, 009665 or +9665)
      /// </summary>
      /// <param name="mobileNo">string to check</param>
      /// <returns></returns>
      public static bool IsSAMobileNo(string mobileNo)
      {
         double res;
         if (double.TryParse(mobileNo, out res) &&
                 ((mobileNo.Length == 10 && mobileNo.StartsWith("05"))
                 || (mobileNo.Length == 14 && mobileNo.StartsWith("009665"))
                 || (mobileNo.Length == 13 && mobileNo.StartsWith("+9665"))
                 ))
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public static void StoreSecureCookie(HttpContext httpContext, string sToken)
      {
         bool isSecure = false;
         if (httpContext.Request.IsHttps)
            isSecure = true;

         httpContext.Response.Cookies.Append("SecureCookie", Helpers.EncryptionHelper.Encrypt(sToken), new CookieOptions { Secure = isSecure, Expires = DateTime.UtcNow.AddDays(1) });
      }

      public static string FetchTokenBySecureCookie(HttpContext httpContext)
      {
         string sToken = "";
         var cookie = httpContext.Request.Cookies["SecureCookie"];

         if (cookie != null)
         {
            sToken = cookie;
         }

         return sToken;
      }

      public static string FetchTokenByUserCookie(HttpContext httpContext)
      {
         string sToken = "";
         var cookie = httpContext.Request.Cookies["UserCookie"];

         if (cookie != null)
         {
            sToken = cookie;
         }

         return sToken;
      }

      //public static void StoreSecureCookie(string name)
      //{
      //    var cookie = new HttpCookie(name);
      //    if (HttpContext.Current.Request.IsSecureConnection)
      //        cookie.Secure = true;
      //    cookie.Value = GetHashKey();
      //    cookie.Expires = DateTime.UtcNow.AddDays(1);
      //    HttpContext.Current.Response.Cookies.Add(cookie);
      //}

      //public static string GetHashKey(HttpContext httpContext)
      //{
      //    var requestUserAgent = httpContext.Request.Headers["User-Agent"];

      //    //myStr.Append(Request.LogonUserIdentity.User.Value);
      //    return Helpers.EncryptionHelper.Encrypt(requestUserAgent.ToString());
      //}

      public static void StoreUserCookie(HttpContext httpContext, string sToken)
      {
         bool isSecure = false;
         if (httpContext.Request.IsHttps)
            isSecure = true;

         var requestUserAgent = httpContext.Request.Headers["User-Agent"];

         httpContext.Response.Cookies.Append("UserCookie", Helpers.EncryptionHelper.Encrypt(requestUserAgent), new CookieOptions { Secure = isSecure, Expires = DateTime.UtcNow.AddDays(1) });
      }

      public static void ClearCookiesAndSessions(IHttpContextAccessor httpContextAccessor)
      {
         //HttpCookie aCookie;
         var applicationCookieName = ".AspNetCore.Session";
         //var aspNetCookieName = ".AspNetCore.Identity.Application";
         httpContextAccessor.HttpContext.Session.Clear();
         httpContextAccessor.HttpContext.Response.Cookies.Append(applicationCookieName, "");

         //SessionIDManager sm = new SessionIDManager();
         //string newId = sm.CreateSessionID(System.Web.HttpContext.Current);
         //bool isRedirect = false;
         //bool isAdded = false;
         //sm.SaveSessionID(System.Web.HttpContext.Current, newId, out isRedirect, out isAdded);
         //SessionIDManager manager = new SessionIDManager();
         //string newSessionId = manager.CreateSessionID(HttpContext.Current);
         //HttpContext.Current.Response.Cookies.Add(new HttpCookie(aspNetCookieName, newSessionId));

         //if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
         //{
         //    HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
         //    HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
         //}

         //if (HttpContext.Current.Request.Cookies["AuthToken"] != null)
         //{
         //    HttpContext.Current.Response.Cookies["AuthToken"].Value = string.Empty;
         //    HttpContext.Current.Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
         //}

         //var myCookies = HttpContext.Current.Request.Cookies.AllKeys;
         //foreach (var cookie in myCookies)
         //{
         //    HttpContext.Current.Request.Cookies[cookie].Expires = DateTime.Now.AddYears(-1);
         //    HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddYears(-1);
         //    HttpContext.Current.Response.Cookies.Add(HttpContext.Current.Response.Cookies[cookie]);
         //    HttpContext.Current.Request.Cookies.Add(HttpContext.Current.Request.Cookies[cookie]);
         //}
         //int limit = HttpContext.Current.Request.Cookies.Count;
         //for (int i = 0; i < limit; i++)
         //{
         //    //foreach (var cookie in myCookies)
         //    //{
         //    //    if (HttpContext.Current.Request.Cookies[cookie] != null)
         //    //    {
         //    //        cookieName = HttpContext.Current.Request.Cookies[cookie].Name;
         //    //        aCookie = new HttpCookie(cookieName);
         //    //        HttpContext.Current.Request.Cookies.Remove(cookieName);
         //    //        aCookie.Expires = DateTime.Now.AddYears(-1);
         //    //        HttpContext.Current.Response.Cookies.Add(aCookie);
         //    //    }
         //    if (HttpContext.Current.Request.Cookies.Count > i)
         //    {
         //        cookieName = HttpContext.Current.Request.Cookies[i].Name;
         //        aCookie = new HttpCookie(cookieName);
         //        HttpContext.Current.Request.Cookies.Remove(cookieName);
         //        aCookie.Expires = DateTime.Now.AddYears(-1);
         //        HttpContext.Current.Response.Cookies.Add(aCookie);
         //    }
         //}
         ////}


         //// Clear .AspNet.ApplicationCookie from Response
         //if (HttpContext.Current.Response.Cookies[applicationCookieName] != null)
         //    HttpContext.Current.Response.Cookies.Remove(applicationCookieName);

         //if (HttpContext.Current.Response.Cookies[aspNetCookieName] != null)
         //    HttpContext.Current.Response.Cookies[aspNetCookieName].Expires = DateTime.Now.AddDays(-30); //Delete the cookie

         //// Clear .AspNet.ApplicationCookie from Request
         //if (HttpContext.Current.Request.Cookies[applicationCookieName] != null)
         //    HttpContext.Current.Request.Cookies.Remove(applicationCookieName);

         //// Add new cookie and name it .AspNet.ApplicationCookie for Response
         //HttpCookie currentUserCookie = new HttpCookie(applicationCookieName);
         //currentUserCookie.Expires = DateTime.Now.AddYears(-1);
         //currentUserCookie.Value = null;
         //HttpContext.Current.Response.SetCookie(currentUserCookie);

         //// Logout from the system                      
         //HttpContext.Current.Request.GetOwinContext().Authentication.SignOut();
         //HttpContext.Current.Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
         //HttpContext.Current.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);

         //// Clear all cookies and Sessions
         //HttpContext.Current.Response.Cookies.Clear();
         //HttpContext.Current.Request.Cookies.Clear();
         //HttpContext.Current.Session.Clear();
         //HttpContext.Current.Session.Abandon();
         //HttpContext.Current.Session.RemoveAll();
      }

      #endregion Others
   }
}


