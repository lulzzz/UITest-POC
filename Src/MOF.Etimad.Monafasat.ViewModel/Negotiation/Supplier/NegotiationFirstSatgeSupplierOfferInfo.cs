using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationFirstSatgeSupplierOfferInfo
    {
        public decimal OfferMainAmount { get; set; }
        public decimal OfferMainAmountAfterDiscount { get; set; }
        public string TenderIdString { get; set; }
        public string OfferMainAmountAfterDiscountText { get; set; }
        public string NegotiationIdString { get; set; }
        public decimal ReductionAmount
        {
            get
            {
                return OfferMainAmount - OfferMainAmountAfterDiscount;

            }
        }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int RemainingDays { get; set; }
        public int RemaininMinutes { get; set; }
        public int RemainingHours { get; set; }
        public Enums.enNegotiationSupplierStatus StatusId { get; set; }
        public string StatusName { get; set; }
        public int ReductionLetterFileName { get; set; }
        public string RemainingPeroidText
        {
            get
            {
                string remainingTime = "";
                if (RemainingDays > 0)
                {
                    if (RemainingDays == 1)
                    {
                        remainingTime = RemainingDays + " " + Resources.CommunicationRequest.DisplayInputs.Day;
                    }
                    else
                    {
                        remainingTime = RemainingDays + " " + Resources.CommunicationRequest.DisplayInputs.Days;
                    }
                    if (RemainingHours != 0)
                    {
                        if (RemainingHours > 1)
                        {
                            remainingTime = remainingTime + " " + Resources.SharedResources.DisplayInputs.And + " " + RemainingHours + " " + Resources.CommunicationRequest.DisplayInputs.Hours;
                        }
                        else
                        {
                            remainingTime = remainingTime + " " + Resources.SharedResources.DisplayInputs.And + " " + RemainingHours + " " + Resources.CommunicationRequest.DisplayInputs.Hour;
                        }

                    }

                }
                else
                {

                    if (RemainingHours != 0)
                    {
                        if (RemainingHours > 1)
                        {
                            remainingTime = RemainingHours + " " + Resources.CommunicationRequest.DisplayInputs.Hours;
                        }
                        else if (RemainingHours == 1)
                        {
                            remainingTime = RemainingHours + " " + Resources.CommunicationRequest.DisplayInputs.Hour;
                        }


                    }
                    else
                    {


                        if (RemaininMinutes != 0)
                        {
                            if (RemaininMinutes > 2)
                            {
                                remainingTime = RemaininMinutes + " " + Resources.CommunicationRequest.DisplayInputs.Minute;
                            }
                            else if (RemaininMinutes == 1 || RemaininMinutes == 2)
                            {
                                remainingTime = RemaininMinutes + " " + Resources.CommunicationRequest.DisplayInputs.Minute;
                            }


                        }
                        else
                        {

                            remainingTime = Resources.CommunicationRequest.DisplayInputs.TimeEnded;
                        }
                    }

                }

                return remainingTime;

            }
        }
        public string SupplierReplayPeroidText
        {
            get
            {
                string remainingTime = "";
                if (Days > 0)
                {
                    if (Days == 1)
                    {
                        remainingTime = Days + " " + Resources.CommunicationRequest.DisplayInputs.Day;
                    }
                    else
                    {
                        remainingTime = Days + " " + Resources.CommunicationRequest.DisplayInputs.Days;
                    }
                    if (Hours != 0)
                    {
                        if (Hours > 1)
                        {
                            remainingTime = remainingTime + " " + Resources.SharedResources.DisplayInputs.And + " " + Hours + " " + Resources.CommunicationRequest.DisplayInputs.Hours;
                        }
                        else
                        {
                            remainingTime = remainingTime + " " + Resources.SharedResources.DisplayInputs.And + " " + Hours + " " + Resources.CommunicationRequest.DisplayInputs.Hour;
                        }

                    }

                }
                else
                {
                    if (Hours != 0)
                    {
                        if (Days == 1)
                        {
                            remainingTime = remainingTime + " " + Resources.SharedResources.DisplayInputs.And;
                        }
                        if (Hours > 0)
                        {
                            remainingTime = Hours + " " + Resources.CommunicationRequest.DisplayInputs.Hours;
                        }
                        else if (Hours == 1)
                        {
                            remainingTime = Hours + " " + Resources.CommunicationRequest.DisplayInputs.Hour;
                        }
                        else
                        {
                            remainingTime = Resources.CommunicationRequest.DisplayInputs.TimeEnded;
                        }

                    }

                }

                return remainingTime;

            }
        }

        public NegotiationAttachmentViewModel ReductionLetter { get; set; }
        public decimal? NegotiationPercent { get; set; }
    }
}
