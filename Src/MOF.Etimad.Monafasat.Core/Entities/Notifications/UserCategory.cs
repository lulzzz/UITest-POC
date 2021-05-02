using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("UserProfile", Schema = "Notification")]

    public class UserCategory : AuditableEntity
    {

        public UserCategory()
        {
        }
        public UserCategory(string categoryName, string summary)
        {
            CategoryName = categoryName;
            Summary = summary;
        }
        public UserCategory(string categoryName, string summary, List<UserProfile> users)
        {
            CategoryName = categoryName;
            Summary = summary;

        }
        [Key]
        public int Id { get; private set; }
        public string CategoryName { get; private set; }
        public string Summary { get; private set; }
    }
}
