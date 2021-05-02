using System;
using System.Globalization;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class VacationsDateModel
    {
        public int VacationId { get; set; }
        public string VacationName { get; set; }
        public DateTime? VacationDate { get; set; }
        public string VacationDateString { get { return VacationDate.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture); } set { } }
    }
}
