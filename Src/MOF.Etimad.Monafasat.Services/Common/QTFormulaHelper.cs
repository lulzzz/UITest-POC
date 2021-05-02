using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace MOF.Etimad.Monafasat.Services
{
    public static class QTFormulaHelper
    {
        public static bool IsNumeric(string input, NumberStyles numberStyle)
        {
            bool result = double.TryParse(input, numberStyle, CultureInfo.CurrentCulture, out _);
            return result;
        }

        public static decimal Calculate(string formula, List<TenderQuantityItemDTO> quantityTableItems, long? itemNumber, long? HeaderId, bool IsDefault = true)
        {
            List<string> data = new List<string>();
            string[] Ids = formula.Split(new string[] { "##" }, StringSplitOptions.None);
            for (int id = 0; id < Ids.Count(); id++)
            {
                if (IsNumeric(Ids[id], NumberStyles.AllowDecimalPoint))
                {
                    var x = quantityTableItems.FirstOrDefault(d => d.ColumnId == int.Parse(Ids[id]) && (HeaderId.HasValue ? d.TenderFormHeaderId == HeaderId : true) && (itemNumber.HasValue ? d.ItemNumber == itemNumber : true));
                    var value = IsDefault ? x.Value : x.AlternativeValue;
                    var Value = Convert.ToDecimal(string.IsNullOrEmpty(value) ? "0" : value);
                    data.Add(string.Format("{0:0.0000}", Value));
                }
                else
                    data.Add(Ids[id]);
            }
            string finalEquation = "";
            foreach (var item in data)
            {
                finalEquation += item;
            }
            decimal Result;
            finalEquation = finalEquation.Replace("%", "/100");
            DataTable dt2 = new DataTable();
            var v = dt2.Compute(finalEquation, "");
            Result = Convert.ToDecimal(v);
            Result = Convert.ToDecimal(string.Format("{0:0.00}", Result));
            return Result;
        }
    }
}
