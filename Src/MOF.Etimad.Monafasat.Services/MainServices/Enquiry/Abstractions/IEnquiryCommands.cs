using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IEnquiryCommands
    {
        Task<Enquiry> CreateAsync(Enquiry enquiry);
        Task<Enquiry> UpdateAsync(Enquiry enquiry);

        Task<EnquiryReply> CreateReplyAsync(EnquiryReply enquiryReply);
        Task<EnquiryReply> UpdateReplyAsync(EnquiryReply enquiryReply);

    }
}
