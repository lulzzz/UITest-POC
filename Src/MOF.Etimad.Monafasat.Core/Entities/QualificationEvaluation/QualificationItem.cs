using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationItem", Schema = "Qualification")]
    public partial class QualificationItem
    {

        public QualificationItem()
        {
            QualificationConfigurations = new HashSet<QualificationConfiguration>();
            QualificationSupplierDatas = new HashSet<QualificationSupplierData>();
        }

        public int ID { get; private set; }

        public int SubCategoryId { get; private set; }

        public int QualificationItemTypeId { get; private set; }


        public string Name { get; private set; }

        public string NameEn { get; private set; }

        public bool IsDeleted { get; private set; }
        public int Code { get; set; }

        public bool IsConfigure { get; private set; }

        public virtual ICollection<QualificationConfiguration> QualificationConfigurations { get; private set; }

        [ForeignKey(nameof(QualificationItemTypeId))]
        public virtual QualificationItemType QualificationItemType { get; private set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual QualificationSubCategory QualificationSubCategory { get; private set; }


        public virtual ICollection<QualificationSupplierData> QualificationSupplierDatas { get; private set; }
    }
}
