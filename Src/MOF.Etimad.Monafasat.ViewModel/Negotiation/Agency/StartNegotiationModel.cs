using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class StartNegotiationModel
    {

        public string TenderIdString { get; set; }

        [Required(ErrorMessageResourceName = "CommunicationRequestRequired", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public int enNegotiationTypeId { get; set; }

        public List<SelectListItem> NegotaitionTypes = new List<SelectListItem>();
    }
}
