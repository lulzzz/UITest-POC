using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ISMEASizeInquiryProxy
    {
        Task<LocalContentViewModel> SMEASizeInquiry(string crNumber);
    }
}
