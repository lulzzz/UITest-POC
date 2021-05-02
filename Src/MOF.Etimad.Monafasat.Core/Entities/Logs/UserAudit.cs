using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("_UserAudit", Schema = "dbo")]

    public class UserAudit

    {
        [Key]
        public int Id { get; private set; }

        public string UserId { get; private set; }


        public DateTime Timestamp { get; private set; } = DateTime.Now;

        public string UserName { get; set; }

        public string FullName { get; set; }

        //Success or Fail
        public string Process { get; set; }

        public string ProcessId { get; set; }


        public String AuditEvent { get; set; }

        public string IpAddress { get; private set; }

        public static UserAudit CreateAuditEvent(string userName, string processId, string process, string auditEventType, string ipAddress)
        {
            return new UserAudit { ProcessId = processId, Process = process, AuditEvent = auditEventType, IpAddress = ipAddress, UserName = userName };
        }

        public static UserAudit CreateAuditEvent(string userId, string userName, string fullName, string process, string processId, string auditEventType, string ipAddress)
        {
            return new UserAudit
            {
                UserId = userId,
                AuditEvent = auditEventType,
                IpAddress = ipAddress,
                UserName = userName,
                FullName = fullName,
                Process = process,
                ProcessId = processId
            };
        }
    }
}
