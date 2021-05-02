using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ColumnDto
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "إسم العمود")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "النوع")]
        public int FieldTypeId { get; set; }

        [Display(Name = " مكرر تحت العنوان الرئيسى")]
        public bool IsHeaderRepeated { get; set; }

        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "ترتيب العرض")]
        public int DisplayOrder { get; set; }

        public int ValidationTypeId { get; set; }

        public string DataSource { get; set; }

        public int? ColumnTypeId { get; set; }

        public string DefaultValue { get; set; }
    }
}
