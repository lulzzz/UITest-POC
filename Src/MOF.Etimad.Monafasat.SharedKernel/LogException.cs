using System;
using System.Text;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public static class LogException
    {
        public static void Log(Exception ex, string refNo = null, string serviceName = "")
        {
            if (ex == null) return;

            var sb = new StringBuilder();

            sb.AppendFormat("DateTime: {0}", DateTime.Now.ToString())
                .AppendLine();
            if (!string.IsNullOrEmpty(serviceName))
            {
                sb.AppendFormat("Service: {0}", serviceName).AppendLine();
            }

            sb.AppendFormat("RefNo: {0}", refNo)
            .AppendLine()
            .AppendFormat("Ex: {0}", ex.ToString())
            .AppendLine()
            .AppendFormat("Trace: {0}", ex.StackTrace?.ToString())
            .AppendLine()
            .AppendLine("=============================================================")
            .AppendLine();

            System.IO.File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "/_log.txt", new string[] { sb.ToString() });

        }
    }
}
