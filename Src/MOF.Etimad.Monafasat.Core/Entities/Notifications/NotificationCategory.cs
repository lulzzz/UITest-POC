using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("NotificationCategory", Schema = "LookUps")]
    public class NotificationCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(1024)]
        public string ArabicName { get; set; }

        [StringLength(1024)]
        public string EnglishName { get; set; }



    }

}