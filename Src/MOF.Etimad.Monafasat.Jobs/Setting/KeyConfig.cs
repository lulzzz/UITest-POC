using System.Configuration;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class KeyConfig
    {

        public static string GetPushNotificationStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["PushNotificationStatus"].ToString();
            }
        }
        public static string GetPushNotificationTime
        {
            get
            {
                return ConfigurationManager.AppSettings["PushNotificationTimeInMinutes"].ToString();
            }
        }
        public static string GetResendFailNotificationTimeInMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["ResendFailNotificationTimeInMinutes"].ToString();
            }
        }
        public static string GetResendFailNotificationStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["ResendFailNotificationStatus"].ToString();
            }
        }
        public static string CheckFinishedBiddingsRoundsInMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["CheckFinishedBiddingsRoundsInMinutes"].ToString();
            }
        }
        public static string NegotiationMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["NegotiationMinutes"].ToString();
            }
        }
        public static string PlaintMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["PlaintMinutes"].ToString();
            }
        }
        public static string CheckFinishedBiddingsRoundsJobStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["CheckFinishedBiddingsRoundsJobStatus"].ToString();

            }
        }
        public static string NegotiationStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["NegotiationStatus"].ToString();

            }
        }
        public static string PlaintStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["PlaintStatus"].ToString();

            }
        }

        public static string UpdateOfferForExtendOfferValidityMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateOfferForExtendOfferValidityMinutes"].ToString();
            }
        }
        public static string OpenOffersMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["OpenOffersMinutes"].ToString();
            }
        }

        public static string CheckingOffersMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["CheckingOffersMinutes"].ToString();
            }
        }

        public static string NoficationSentForStoppingPeriodTimeMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["NoficationSentForStoppingPeriodTimeMinutes"].ToString();
            }
        }

        public static string UpdateAnnouncementTemplateListPeriodTimeMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateAnnouncementTemplateListPeriodTimeMinutes"].ToString();
            }
        }
        public static string SadadUpdateAndCancelMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["SadadUpdateAndCancelMinutes"].ToString();
            }
        }

        public static string EMarketPlaceMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["EMarketPlaceMinutes"].ToString();
            }
        }

        public static string RecalculateSupplierQualificationPointsMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["RecalculateSupplierQualificationPointsMinutes"].ToString();
            }
        }

        public static string UpdateOfferForExtendOfferValidityStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateOfferForExtendOfferValidityStatus"].ToString();
            }
        }

        public static string OpenOfferNotifications
        {
            get
            {
                return ConfigurationManager.AppSettings["OpenOfferNotifications"].ToString();
            }
        }
        public static string OffersOpeningStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["OffersOpeningStatus"].ToString();

            }
        }
        public static string SadadUpdateAndCancelStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["SadadUpdateAndCancelStatus"].ToString();
            }
        }

        public static string EMarketPlaceStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["EMarketPlaceStatus"].ToString();
            }
        }


        public static string UpdateOfferCheckingValidityStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateOfferCheckingValidityStatus"].ToString();
            }
        }

        public static string NoficationSentForStoppingPeriodStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["NoficationSentForStoppingPeriodStatus"].ToString();
            }
        }
        public static string UpdateAnnouncementTemplateListStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateAnnouncementTemplateListStatus"].ToString();
            }
        }
        public static string RecalculateSupplierQualificationPointsStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["RecalculateSupplierQualificationPointsStatus"].ToString();
            }
        }
    }
}
