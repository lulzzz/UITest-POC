using System.Collections.Generic;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AjaxResponse<T>
    {
        public enAjaxResponseMessageType enMessageType { get; set; }
        public string enMessageTypeString { get { return enMessageType.ToString(); } }
        public string Message { get; set; }
        public string htmlData { get; set; }
        public int tableId { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
        public T Data { get; set; }


    }
}
