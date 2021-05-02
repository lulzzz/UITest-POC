using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{

    public class ExcelHeader
    {
        public long activityId { get; set; }
        public string activityName { get; set; }
        public string formName { get; set; }
        public long formId { get; set; }
        public List<ExcelHeaderCell> excelHeaderCells { get; set; }


    }
    public class ExcelHeaderCell
    {

        public long columnId { get; set; }
        public string ColumnName { get; set; }
        public bool isHeaderRepeated { get; set; }
        public bool isDescription { get; set; }
        public int columnOrder { get; set; }
        public int? columnTypeId { get; set; }
    }
}
