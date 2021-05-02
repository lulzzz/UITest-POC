using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace MOF.Etimad.Monafasat.Integration.Infrastructure
{
    public static class Logger
    {

        public static void LogToFile(dynamic request, dynamic response, string fileName = null)
        {

            if (fileName == null)
            {
                fileName = Guid.NewGuid().ToString();
            }

            var logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\__SvcLog__\\" + (request is string == true ? "RESTServices" : request?.MsgRqHdr?.ServiceId) + "\\";

            #region Create the Log file directory if it does not exists

            var logFileInfo = new FileInfo(logFilePath);
            if (logFileInfo.DirectoryName != null)
            {
                var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists)
                {
                    logDirInfo.Create();
                }
            }
            #endregion

            var logData = new { Request = request, Response = response };
            string jsonResult = JsonConvert.SerializeObject(logData, Formatting.Indented);

            File.WriteAllText($@"{logFilePath}{fileName}.json", jsonResult, Encoding.UTF8);
        }

        public static string GetJsonString(dynamic request, dynamic response, string fileName = null)
        {
            var logData = new { Request = request, Response = response };
            return JsonConvert.SerializeObject(logData, Formatting.Indented);
        }
    }
}
