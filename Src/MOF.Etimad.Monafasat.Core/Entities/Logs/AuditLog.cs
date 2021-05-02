using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("_AuditLog")]
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string TransactionId { get; set; }

        [MaxLength(1)]
        public string AuditType { get; set; }

        [MaxLength(200)]
        public string TableName { get; set; }

        [MaxLength(200)]
        public string TableSchema { get; set; }

        [MaxLength(250)]
        public string UserName { get; set; }

        public DateTime TimeStamp { get; set; }

        [MaxLength(50)]
        public string PrimaryKey { get; set; }

        public string OldData { get; set; }

        public string NewData { get; set; }
    }
}
