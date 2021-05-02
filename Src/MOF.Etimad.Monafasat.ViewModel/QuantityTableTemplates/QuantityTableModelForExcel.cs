using System.Collections.Generic;
using System.Linq;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantityTableModelForExcel
    {

        #region FormhidenFileds
        public string EncryptedNegotiationId { get; set; }

        public int TenderId { get; set; }

        public string EncryptedOfferId { get; set; }

        public string EncryptedTenderId { get; set; }

        public long FormId { get; set; }
        public int TableId { get; set; }

        #endregion


        #region Display
        public string FormName { get; set; }
        public string TableName { get; set; }
        public string TemplateName { get; set; }
        #endregion

        #region FormCOnfig
        public string SubmitAction { get; set; }

        public bool AddAlternative { get; set; }

        public bool EditAlternative { get; set; }

        public bool DeleteAlternative { get; set; }

        public bool CanEditAlternative { get; set; }

        public bool AllowEdit { get; set; }

        public bool HasAlternative { get; set; }

        public bool IsReadOnly { get; set; }

        public bool ApplySelected { get; set; }

        public bool EnablePageing { get; set; }

        #endregion

        public int TemplateId { get; set; }

        #region Data
        public List<FormHeaderModel> TenderFormHeaders { get; set; }

        public List<TenderQuantityTableItemModelforExcel> Columns { get; set; }
        public int ColmnsRepeatedWithHeader => Columns.Where(c => c.IsHeaderRepeated).Count();
        public List<QTableItemDataModel> Body { get; set; } = new List<QTableItemDataModel>();
        public List<FooterModel> Footers { get; set; } = new List<FooterModel>();

        #endregion

        public int HeadrsCount => (TenderFormHeaders.Count() > 1 ? TenderFormHeaders.Count() : 1);
        public List<TenderQuantityTableItemModelforExcel> NotrepeatedCols => HeadrsCount > 1 ? Columns.Where(c => !c.IsHeaderRepeated && c.ColumnTypeId != (int)ColumnTypeEnum.Description).OrderBy(x => x.DisplayOrder).ToList() : Columns.Where(c => c.ColumnTypeId != (int)ColumnTypeEnum.Description).OrderBy(x => x.DisplayOrder).ToList();


        public List<TenderQuantityTableItemModelforExcel> TitleCols => HeadrsCount > 1 ? Columns.Where(c => !c.IsHeaderRepeated && c.ColumnTypeId == (int)ColumnTypeEnum.Title).OrderBy(x => x.DisplayOrder).ToList() : Columns.Where(c => c.ColumnTypeId == (int)ColumnTypeEnum.Title).OrderBy(x => x.DisplayOrder).ToList();

        public List<TenderQuantityTableItemModelforExcel> DescriptionCols => HeadrsCount > 1 ? Columns.Where(c => !c.IsHeaderRepeated && c.ColumnTypeId == (int)ColumnTypeEnum.Description && c.CanEdit).OrderBy(x => x.DisplayOrder).ToList() : Columns.Where(c => c.ColumnTypeId == (int)ColumnTypeEnum.Description && c.CanEdit).OrderBy(x => x.DisplayOrder).ToList();

        public List<TenderQuantityTableItemModelforExcel> RepeatedCols => HeadrsCount > 1 ? Columns.Where(c => c.IsHeaderRepeated).OrderBy(x => x.DisplayOrder).ToList() : new List<TenderQuantityTableItemModelforExcel>();
        public int ColsCount => NotrepeatedCols.Count() + (RepeatedCols.Count() * HeadrsCount);
        public List<long> ItemsNumbers => Body.Select(i => i.ItemNumber).Distinct().ToList();
        public string TableIdentifire => TableId + "_" + FormId;

        public string Countries { get; set; }
        public string RowTypes { get; set; }
        public int FormCategoryId { get; set; }
    }

    public class FormHeaderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class QTableItemDataModel
    {
        public long Id { get; set; }
        public long TenderId { get; set; }

        public long ColumnId { get; set; }

        public long? TenderFormHeaderId { get; set; }

        public long OfferId { get; set; }

        public long ItemNumber { get; set; }
        public string Value { get; set; }

        public string Value_Text { get; set; }
        public string AlternativeValue { get; set; }
        public string AlternativeValue_Text { get; set; }
        public bool IsDefault { get; set; }

        public string EncryptedOfferId { get; set; }
    }

    public class FooterModel
    {
        public long FooterId { get; set; }
        public string FooterName { get; set; }
        public IEnumerable<ColumnFooterModel> ColumnFooters { get; set; } = new List<ColumnFooterModel>();
    }

    public class ColumnFooterModel
    {
        public decimal Value { get; set; }
        public decimal AlternativeValue { get; set; }
        public string Value_Text { get; set; }
        public string AlternativeValue_Text { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int ColumnTypeId { get; set; }
    }

    public class TenderQuantityTableItemModelforExcel
    {
        public long ColumnId { get; set; }
        public string ColumnName { get; set; }
        public bool IsHeaderRepeated { get; set; }
        public int DisplayOrder { get; set; }
        public int FieldTypeId { get; set; }
        public bool CanRead { get; set; }
        public bool CanEdit { get; set; }


        public int? ColumnTypeId { get; set; }

        public string DataSource { get; set; }
        public ValidationTypeModel ValidationType { get; set; }

    }

    public class ValidationTypeModel
    {
        public int validationTypeId { get; set; }
        public string Regx { get; set; }
        public bool IsMandatory { get; set; }
        public string ValidationMessage { get; set; }

        public string DefaultValue { get; set; }
    }
}
