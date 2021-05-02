using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public class ContractAppService :IContractAppService
    {
        private readonly IContractQueries _contractQueries;
        private readonly RootConfigurations _rootConfiguration;

        public ContractAppService(IContractQueries contractQueries, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _contractQueries = contractQueries;
            _rootConfiguration = rootConfiguration.Value; 
        }

        public async Task<ContractResponseViewModel> CheckTenderCanBeLinkedToContract(string referenceNumber, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(referenceNumber), referenceNumber);
            Check.ArgumentNotNullOrEmpty(nameof(agencyCode), agencyCode);
            var result = await _contractQueries.FindTenderByAgencyAndReferenceNumberForContractLinking(referenceNumber);
            ContractResponseViewModel contractResponseViewModel = BuildContractResponseViewModel(result, agencyCode);
            return contractResponseViewModel;
        }


        private ContractResponseViewModel BuildContractResponseViewModel(Tender tender, string agencyCode)
        {


            if (tender == null)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.InvalidTenderReference,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.ReferenceNumberNotFound,
                    MessageCode = "invalid_tender_reference",
                    canRegisterContract = false
                };
            }


            if (tender.AgencyCode != agencyCode)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.InvalidTenderReference,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.AgencyCodeNotSameAsTender,
                    MessageCode = "invalid_tender_reference",
                    canRegisterContract = false,
                    TenderReferenceNumber = tender.ReferenceNumber
                };
            }

            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedConfirmed)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.StillInAwardingProcess,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.NotAwardedStatusYet,
                    MessageCode = "tender_still_in_awarding_process",
                    canRegisterContract = false,
                    TenderReferenceNumber = tender.ReferenceNumber,
                    TenderName = tender.TenderName,
                    AgencyCode = tender.AgencyCode
                };
            }
            if (tender.AwardingStoppingPeriod.HasValue)
            {
                int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
                int esclationPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.EscalationPeriod);
                if (!tender.isTenderFinalAwarded(esclationPeriod, plaintReviewingPeriod))
                {
                    return new ContractResponseViewModel
                    {
                        StatusCode = ServiceStatusCodes.StillInStoppingPeriod,
                        StatusDesc = ServiceStatusCodes.FailedString,
                        Message = Resources.MonafasatContract.Messages.PlaintStopPeriodNotEndedYet,
                        MessageCode = "tender_still_in_complain_process",
                        canRegisterContract = false,
                        TenderReferenceNumber = tender.ReferenceNumber,
                        TenderName = tender.TenderName,
                        AgencyCode = tender.AgencyCode
                    };
                }
            }


            return new ContractResponseViewModel
            {
                StatusCode = ServiceStatusCodes.TenderCanBeLinked,
                StatusDesc = ServiceStatusCodes.ContractSuccessString,
                Message = Resources.MonafasatContract.Messages.SuccessMessage,
                MessageCode = "tender_valid_can_link",
                canRegisterContract = true,
                TenderReferenceNumber = tender.ReferenceNumber,
                TenderName = tender.TenderName,
                AgencyCode = tender.AgencyCode
            };
        }

    }


}
