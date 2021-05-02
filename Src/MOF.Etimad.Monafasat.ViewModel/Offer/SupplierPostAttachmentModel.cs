using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierPostAttachmentModel
    {

        public string files { get; set; }
        public string tenderId { get; set; }

        public Enums.AttachmentType type { get; set; }

    }
}