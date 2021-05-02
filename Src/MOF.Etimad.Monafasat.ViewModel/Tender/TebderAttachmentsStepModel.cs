using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TebderAttachmentsStepModel
    {
        //[Display(Name = "المرفقات و كراسات الشروط")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AttachmentsMainConditions")]
        public List<int> AttachmentIDs { set; get; }
    }
}
