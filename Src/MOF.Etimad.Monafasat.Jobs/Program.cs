using FluentScheduler;
using System;
using System.Linq;
using System.Threading;

namespace MOF.Etimad.Monafasat.Jobs
{
    class Program
    {
        private static void Main(string[] args)
        {
            var registry = new Registry();
            //===============================================interval Key Config===================
            int ResendNotificationTimeInMinutes = Convert.ToInt16(KeyConfig.GetResendFailNotificationTimeInMinutes);
            int CheckFinishedBiddingsRoundsInMinutes = Convert.ToInt16(KeyConfig.CheckFinishedBiddingsRoundsInMinutes);
            int NegotiationMinutes = Convert.ToInt16(KeyConfig.NegotiationMinutes);
            int PlaintMinutes = Convert.ToInt16(KeyConfig.PlaintMinutes);
            int UpdateOfferForExtendOfferValidityMinutes = Convert.ToInt16(KeyConfig.UpdateOfferForExtendOfferValidityMinutes);
            int OpenOffersMinutes = Convert.ToInt16(KeyConfig.OpenOffersMinutes);
            int CheckingOffersMinutes = Convert.ToInt16(KeyConfig.CheckingOffersMinutes);
            int SadadUpdateAndCancelMinutes = Convert.ToInt16(KeyConfig.SadadUpdateAndCancelMinutes);
            int EMarketPlaceMinutes = Convert.ToInt16(KeyConfig.EMarketPlaceMinutes);
            int NoficationSentForStoppingPeriodTimeMinutes = Convert.ToInt16(KeyConfig.NoficationSentForStoppingPeriodTimeMinutes);
            int UpdateAnnouncementTemplateListPeriodTimeMinutes = Convert.ToInt16(KeyConfig.UpdateAnnouncementTemplateListPeriodTimeMinutes);
            int RecalculateSupplierQualificationPointsMinutes = Convert.ToInt16(KeyConfig.RecalculateSupplierQualificationPointsMinutes);

            bool ResendFaioedNotificationStatus = Convert.ToBoolean(KeyConfig.GetResendFailNotificationStatus);
            bool CheckFinishedBiddingsRoundsJobStatus = Convert.ToBoolean(KeyConfig.CheckFinishedBiddingsRoundsJobStatus);
            bool NegotiationStatus = Convert.ToBoolean(KeyConfig.NegotiationStatus);
            bool PlaintStatus = Convert.ToBoolean(KeyConfig.PlaintStatus);
            bool UpdateOfferForExtendOfferValidityStatus = Convert.ToBoolean(KeyConfig.UpdateOfferForExtendOfferValidityStatus);
            bool OpenOfferNotifications = Convert.ToBoolean(KeyConfig.OpenOfferNotifications);
            bool CheckingOfferNotifications = Convert.ToBoolean(KeyConfig.UpdateOfferCheckingValidityStatus);
            bool SadadUpdateAndCancel = Convert.ToBoolean(KeyConfig.SadadUpdateAndCancelStatus);
            bool EMarketPlace = Convert.ToBoolean(KeyConfig.EMarketPlaceStatus);
            bool NoficationSentForStoppingPeriod = Convert.ToBoolean(KeyConfig.NoficationSentForStoppingPeriodStatus);
            bool UpdateAnnouncementTemplateListStatus = Convert.ToBoolean(KeyConfig.UpdateAnnouncementTemplateListStatus);
            bool RecalculateSupplierQualificationPointsStatus = Convert.ToBoolean(KeyConfig.RecalculateSupplierQualificationPointsStatus);

            //============================================Scedule========================================
            if (ResendFaioedNotificationStatus)
            {
                registry.Schedule<ResendNotification>().ToRunNow().AndEvery(ResendNotificationTimeInMinutes).Minutes();
            }
            if (CheckFinishedBiddingsRoundsJobStatus)
            {
                registry.Schedule<CheckFinishedBiddingsRounds>().ToRunNow().AndEvery(CheckFinishedBiddingsRoundsInMinutes).Minutes();
            }
            if (NegotiationStatus)
            {
                registry.Schedule<Negotiation>().ToRunNow().AndEvery(NegotiationMinutes).Minutes();
            }
            if (PlaintStatus)
            {
                registry.Schedule<Plaint>().ToRunNow().AndEvery(PlaintMinutes).Minutes();
            }

            if (OpenOfferNotifications)
            {
                registry.Schedule<OpenOffers>().ToRunNow().AndEvery(OpenOffersMinutes).Minutes();
            }
            //for checking
            if (CheckingOfferNotifications)
            {
                registry.Schedule<CheckingOffers>().ToRunNow().AndEvery(CheckingOffersMinutes).Minutes();
            }

            if (NoficationSentForStoppingPeriod)
            {
                registry.Schedule<GetAllFinishedStoppingAwardingPeriodTenders>().ToRunNow().AndEvery(NoficationSentForStoppingPeriodTimeMinutes).Minutes();
            }

            if (UpdateOfferForExtendOfferValidityStatus)
            {
                registry.Schedule<UpdateOfferStatus>().ToRunNow().AndEvery(UpdateOfferForExtendOfferValidityMinutes).Minutes();
            }
            if (UpdateAnnouncementTemplateListStatus)
            {
                registry.Schedule<updateAnnouncementListStatus>().ToRunNow().AndEvery(UpdateAnnouncementTemplateListPeriodTimeMinutes).Minutes();
            }
            if (SadadUpdateAndCancel)
            {
                registry.Schedule<SadadUpdateAndCancel>().ToRunNow().AndEvery(SadadUpdateAndCancelMinutes).Minutes();
            }
            if (EMarketPlace)
            {
                registry.Schedule<EMarketPlace>().ToRunNow().AndEvery(EMarketPlaceMinutes).Minutes();
            }
            if (RecalculateSupplierQualificationPointsStatus)
            {
                registry.Schedule<QualificationReCalculateSupplierPoints>().ToRunNow().AndEvery(RecalculateSupplierQualificationPointsMinutes).Minutes();
            }
            //==========================================Set all jobs NonReentrantAsDefault=======================
            registry.NonReentrantAsDefault();
            //===========================================Event Raised when an exception occures=====================
            JobManager.JobException += (info) => Console.WriteLine(info.Exception);
            //============================================Register To Run============
            JobManager.Initialize(registry);
            //=============================================wait unless running scedules Ended=====
            while (JobManager.RunningSchedules.Any())
            {
                Thread.Sleep(/*Timeout.Infinite*/ 10000);
            }
        }
    }
}