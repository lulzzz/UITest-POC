using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Country", Schema = "LookUps")]
    public class Country
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryId { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }
        public bool? IsGolf { get; private set; } = false;

        public List<TenderCountry> TenderCountries { private set; get; }
        #endregion


    }
}
