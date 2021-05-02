using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults
{
    public class TenderDatesDefaults
    {
        public TenderDatesModel GetTenderDatesWithOpenDate()
        {
            int tenderId = 1;
            return new TenderDatesModel
            {
                TenderId = tenderId,
                TenderIdString = Util.Encrypt(tenderId),
                LastEnqueriesDate = DateTime.Now.Date.AddDays(1),
                LastOfferPresentationDate = DateTime.Now.Date.AddDays(2),
                OffersOpeningDate = DateTime.Now.Date.AddDays(3),
                TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing
            };
        }
        public TenderDatesModel GetTenderDatesWithCheckDate()
        {
            int tenderId = 1;
            return new TenderDatesModel
            {
                TenderId = tenderId,
                TenderIdString = Util.Encrypt(tenderId),
                LastEnqueriesDate = DateTime.Now.Date.AddDays(1),
                LastOfferPresentationDate = DateTime.Now.Date.AddDays(2),
                OffersCheckingDate = DateTime.Now.Date.AddDays(3),
                TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing
            };
        }
    }
}
