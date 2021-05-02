using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.TestsBuilders.InvitationDefaults
{
    public class InvitationDefaults
    {
        private readonly string _Cr = "1299659801";
        public Invitation GetInvitation()
        {
            return new Invitation(_Cr, Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Invitation, false);
        }

        public List<Invitation> GetInvitations()
        {
            return new List<Invitation>()
            {
                GetInvitation()
            };
        }

        public QueryResult<TenderInvitationDetailsModel> GetTenderInvitationDetailsModelQueryResult()
        {
            List<TenderInvitationDetailsModel> items =  new List<TenderInvitationDetailsModel>()
            {
                new TenderInvitationDetailsModel()
                {
                }
            };
            return new QueryResult<TenderInvitationDetailsModel>(items, items.Count, 1, 10);
        }
    }
}
