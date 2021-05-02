using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.TestsBuilders.Enquiry
{
    public class EnquiryDefaults
    {
        public Core.Entities.Enquiry GetEnquiryData()
        {
            var enquiry = new Core.Entities.Enquiry("name", 1, "1234");
            return enquiry;
        }
        public Core.Entities.EnquiryReply GetEnquiryReplyData()
        {
            var enquiryReply = new Core.Entities.EnquiryReply("Message", 1, 1, 1, true);

            return enquiryReply;
        }
        public Core.Entities.EnquiryReply GetEnquiryReplyDataWithTender()
        {
            var enquiry = new Core.Entities.Enquiry("name", 1, "1234");
            var tender = new TenderDefault().GetGeneralTender();
            
            enquiry.SetTender(tender);
            var enquiryReply = new Core.Entities.EnquiryReply("Message", 1, 1, 1, true);

            enquiryReply.SetEnquiry(enquiry);
            return enquiryReply;
        }
        public Core.Entities.Enquiry GetEnquiryDataWithTender()
        {
            var enquiry = new Core.Entities.Enquiry("name", 1, "1234");
            var tender = new TenderDefault().GetGeneralTender();
            
            enquiry.SetTender(tender);
 
             return enquiry;
        }

        
        public Core.Entities.EnquiryReply GetEnquiryReplyDataWithCommunicationRequest()
        {
            var enquiry = new Core.Entities.Enquiry("name", 1, "1234");
            var tender = new CommunicationRequestDefault().GetGeneralAgencyCommunicationRequest();
            enquiry.SetCommunicationRequest(tender);
            var enquiryReply = new Core.Entities.EnquiryReply("Message", 1, 1, 1, true);
            enquiryReply.SetEnquiry(enquiry);
            enquiryReply.ApproveEnquiryReply();
            return enquiryReply;
        }
        public EnquiryModel GetEnquiryModel()
        {
            EnquiryModel model = new EnquiryModel
            {
                TenderId = 1,
                CommericalRegisterNo = "123",
                EnquiryName = "Enquiry Name"
            };

            return model;
        }
        public EnquiryReplyModel GetEnquiryReplyModel()
        {
            EnquiryReplyModel model = new EnquiryReplyModel
            {
                EnquiryReplyId = 1,
                EnquiryId = 1,
                EnquiryReplyMessage = "message",
                CommitteeId = 1,
                EnquiryReplyStatusId = (int)Enums.EnquiryReplyStatus.Pending,
                IsComment = false
            };

            return model;
        }
        
        public JoinTechnicalCommitteeModel GetJoinTechnicalCommitteeModel()
        {
            JoinTechnicalCommitteeModel model = new JoinTechnicalCommitteeModel
            {
                 
                EnquiryId = 1,
                JoinedCommitteeId= 1,
                SelectedUserCommitteeId = 2,
                EnquiryComment = "Comment"

            };

            return model;
        }
    }
}
