using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities.Settings
{
    [Table("ConfigurationSetting", Schema = "Settings")]

    public class ConfigurationSetting
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        
        [StringLength(300)]
        public string Description { get; set; }

    }
}
