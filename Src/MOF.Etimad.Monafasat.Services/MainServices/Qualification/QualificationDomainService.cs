using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class QualificationDomainService : IQualificationDomainService
    {
        private readonly IAppDbContext context;
        private IQualificationQueries _tenderQueries;
        private ICommunicationQueries _communicationQueries;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RootConfigurations _rootConfiguration;
        private readonly IIDMQueries _iDMQueries;

        public QualificationDomainService(IAppDbContext context, IQualificationQueries tenderQueries, IHttpContextAccessor httpContextAccessor, ICommunicationQueries communicationQueries, IOptionsSnapshot<RootConfigurations> rootConfiguration, IIDMQueries iDMQueries)
        {
            this.context = context;
            _tenderQueries = tenderQueries;
            _httpContextAccessor = httpContextAccessor;
            _communicationQueries = communicationQueries;
            _rootConfiguration = rootConfiguration.Value;
            _iDMQueries = iDMQueries;
        }

        public async Task IsValidToCreate(PreQualificationSavingModel tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId == 0)
            {
                tender.TenderStatusId = (int)Enums.TenderStatus.Established;
            }

            if (tender.InsideKSA)
            {
                if (tender.TenderAreaIDs == null || tender.TenderAreaIDs.Count() == 0)
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
                }
            }
            else
            {
                if (tender.TenderCountriesIDs == null || tender.TenderCountriesIDs.Count() == 0)
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
                }
            }

            if (tender.TechnicalOrganizationId == null && tender.IsAgancyHasTechnicalCommittee && _httpContextAccessor.HttpContext.User.UserRole() != RoleNames.PreQualificationCommitteeSecretary)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTechnicalCommittee);
            }
            if (tender.PreQualificationCommitteeId == 0)
            {
                tender.PreQualificationCommitteeId = null;
            }
            if (tender.LastOfferPresentationDate == null || tender.LastEnqueriesDate == null || string.IsNullOrEmpty(tender.LastOfferPresentationTime))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterAllQualificationDates);
            }

            if (tender.LastOfferPresentationDate <= tender.LastEnqueriesDate)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.InvalidLastEnqueriesDate);
            }

            string message = "";
            int flag = 1;

            if (tender.LastEnqueriesDate < DateTime.Now)
            {
                flag *= 0;
                message = Resources.SharedResources.ErrorMessages.DateLessThanToday + "\n";
                throw new BusinessRuleException(message);
            }
            if (tender.OffersCheckingDate.HasValue && tender.OffersCheckingDate <= tender.LastEnqueriesDate)
            {
                flag *= 0;
                message = Resources.TenderResources.ErrorMessages.CheckingDateLessThanEnquiriesDate + "\n";
                throw new BusinessRuleException(message);
            }

            Util.DetermineTimesForDates(tender.LastOfferPresentationDate.Value, (int)Enums.TimeMessagesType.Qualification, _rootConfiguration);
        }

        public async Task IsValidToCreatePostQualification(PostQualificationApplyDocumentsModel tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExit);
            if (tender.TenderStatusId == 0)
            {
                tender.TenderStatusId = (int)Enums.TenderStatus.Established;
            }

            if (tender.InsideKSA)
            {
                if (tender.TenderAreaIDs == null || tender.TenderAreaIDs.Count() == 0)
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
                }
            }
            else
            {
                if (tender.TenderCountriesIDs == null || tender.TenderCountriesIDs.Count() == 0)
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
                }
            }

            if (tender.TechnicalOrganizationId == null && tender.IsAgancyHasTechnicalCommittee && _httpContextAccessor.HttpContext.User.UserRole() != RoleNames.PreQualificationCommitteeSecretary)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTechnicalCommittee);
            }
            if (tender.PreQualificationCommitteeId == 0)
            {
                tender.PreQualificationCommitteeId = null;
            }
            if (tender.LastOfferPresentationDate == null || tender.LastEnqueriesDate == null || string.IsNullOrEmpty(tender.LastOfferPresentationTime))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterAllQualificationDates);
            }
            string message = "";
            int flag = 1;

            if (tender.LastEnqueriesDate < DateTime.Now)
            {
                flag *= 0;
                message = Resources.SharedResources.ErrorMessages.DateLessThanToday + "\n";
                throw new BusinessRuleException(message);
            }
            if (tender.OffersCheckingDate.HasValue && tender.OffersCheckingDate <= tender.LastEnqueriesDate)
            {
                flag *= 0;
                message = Resources.TenderResources.ErrorMessages.CheckingDateLessThanEnquiriesDate + "\n";
                throw new BusinessRuleException(message);
            }

            if (tender.OffersCheckingDate.HasValue && tender.LastOfferPresentationDate >= tender.OffersCheckingDate)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.InvalidOffersCheckingDate);
            }

            Util.DetermineTimesForDates(tender.LastOfferPresentationDate.Value, (int)Enums.TimeMessagesType.Qualification, _rootConfiguration);
        }

        public async Task CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId)
        {
            var result = await _communicationQueries.CanAddPostqualificationIfTenderHasExtendOfferValidity(tenderId);
            if (result)
            {
                throw new BusinessRuleException("لايمكن اضافة تاهيل لاحق لأن هذه المنافسة عليها طلب تمديد سريان عروض سارى");
            }
        }

        public async Task CheckIfSupplierHavePostQualificationBefore(List<string> crs, int tenderId)
        {
            List<string> invitedSuppliers = new List<string>();
            if (crs != null && crs.Any())
            {
                foreach (var item in crs)
                {
                    var activeSupplierinvitation = context.PostQualificationSuppliersInvitations.
                        Where(a => a.CommercialNumber == item && a.PostQualification.IsActive == true && a.PostQualification.PostQualificationTenderId == tenderId && a.PostQualification.TenderStatusId != (int)Enums.TenderStatus.Canceled)
                        .FirstOrDefault();
                    if (activeSupplierinvitation != null)
                    {
                        invitedSuppliers.Add(item);
                    }
                }
                if (invitedSuppliers.Count() == 1)
                {
                    throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.CanNotCreatePostQualificationAsExitActiveOne);
                }
                else if (invitedSuppliers.Count() > 1)
                {
                    throw new BusinessRuleException("لا يمكن إضافة التأهيل للسجلات التجارية  " + " " + string.Join(" , ", invitedSuppliers));
                }

                var isTenderHasPostQualification = context.PostQualificationSuppliersInvitations.
                        Any(a => a.PostQualification.IsActive == true && a.PostQualification.PostQualificationTenderId == tenderId && a.PostQualification.TenderStatusId != (int)Enums.TenderStatus.Canceled && a.PostQualification.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckConfirmed);
                if (isTenderHasPostQualification)
                {
                    throw new BusinessRuleException("لا يمكن إضافة تأهيل لاحق للمورد حيث أن هناك تأهيل لاحق أخر غير منتهى");
                }
            }
            else
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.UnexpectedError);
            }
        }

        public async Task CheckMasterWeightForQualification(PreQualificationSavingModel model)
        {
            if (model.TechnicalAdministrativeCapacity == 0 || model.FinancialCapacity == 0)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight);
            }
        }

        public async Task IsValidToSendQualificationForApprovement(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Established)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToSendTenderForApprovement);
            }

            if (tender.LastOfferPresentationDate < DateTime.Now || tender.LastEnqueriesDate < DateTime.Now || tender.OffersCheckingDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationDateMustBelargeThanToday);
            }

            if (tender.LastOfferPresentationDate <= tender.LastEnqueriesDate)
            {
                throw new BusinessRuleException("اخر موعد تقديم فحص التاهيل يجب أن يكون أكبر من  اخر موعد لاستلام استفسارات");
            }

            if (tender.OffersCheckingDate <= tender.LastOfferPresentationDate)
            {
                throw new BusinessRuleException("تاريخ فحس التاهيل يجب أن يكون أكبر من اخر موعد لاستلام العروض");
            }


        }
        public async Task IsValidToSendQualificationForApprovementFromCommitteeManager(Tender tender)
        {
            if (tender.TechnicalAdministrativeCapacity == 0 || tender.FinancialCapacity == 0)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight);
            }
            if (tender.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToSendTenderForApprovement);
            }

            if (tender.LastOfferPresentationDate < DateTime.Now || tender.LastEnqueriesDate < DateTime.Now || tender.OffersCheckingDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationDateMustBelargeThanToday);
            }

            if (tender.LastOfferPresentationDate <= tender.LastEnqueriesDate)
            {
                throw new BusinessRuleException("اخر موعد تقديم فحص التاهيل يجب أن يكون أكبر من  اخر موعد لاستلام استفسارات");
            }

            if (tender.OffersCheckingDate <= tender.LastOfferPresentationDate)
            {
                throw new BusinessRuleException("تاريخ فحس التاهيل يجب أن يكون أكبر من اخر موعد لاستلام العروض");
            }


        }
        public async Task IsValidToAcceptQualificationFromCommitteSecrtary(Tender qulaification)
        {
            if (qulaification.PreQualificationCommitteeId == null)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification);
            }
            else if (qulaification.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee() || qulaification.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification);

            }
            if (qulaification.TenderStatusId != (int)Enums.TenderStatus.QualificationCommitteeApproval)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification);
            }

        }
        public async Task IsValidToApproveQualification(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Pending && !(tender.IsLowBudgetDirectPurchase != null && tender.IsLowBudgetDirectPurchase == true && tender.TenderStatusId == (int)Enums.TenderStatus.Established))
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification);
            }
            if (tender.LastOfferPresentationDate <= DateTime.Now)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.IsValidQualificationDateForApprovement);
            }
        }
        public async Task IsValidToApprovePreQualificationFromCommitteeManager(Tender tender, string agencyCode)
        {
            if (tender.AgencyCode != agencyCode)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification);
            if (!tender.isValidToApproveByLastOfferPresentationDate())
            {
                throw new BusinessRuleException(Resources.QualificationResources.Messages.CantApprovePresentaionDatePassed);
            }
        }

        public async Task IsValidToRejectQualification(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Pending && tender.TenderStatusId != (int)Enums.TenderStatus.QualificationCommitteeApproval)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToRejectApproveQualification);
            }
        }
        public async Task IsValidToReopenQualification(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Rejected)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToReopenQualification);
            }
        }

        public void IsValidToStartCheckingTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);
            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                MemberInfo property = typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.Approved));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Approved));
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationCheckingStatus + tenderStatus);
            }
        }

        public void IsValidToAccecssCheckingTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);
            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();
        }

        public void IsValidToAccecssPostQualificationCheckingTender(Tender tender, List<string> roles)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);
            if (tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                int? CommitteeId = null;
                if (tender.OffersCheckingCommitteeId != null)
                {
                    CommitteeId = (roles.Any(s => RoleNames.OffersCheckManager.ToString().Contains(s) || RoleNames.OffersCheckSecretary.ToString().Contains(s)) ? tender.OffersCheckingCommitteeId : tender.DirectPurchaseCommitteeId);
                }

                if (tender.DirectPurchaseCommitteeId != null)
                {
                    CommitteeId = (roles.Any(s => RoleNames.OffersPurchaseManager.ToString().Contains(s) || RoleNames.OffersPurchaseSecretary.ToString().Contains(s)) ? tender.DirectPurchaseCommitteeId : tender.OffersCheckingCommitteeId);
                }

                if (CommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                    throw new AuthorizationException();
            }
        }

        public void IsValidToReopenCheckTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);
            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckRejected)
            {
                MemberInfo property = typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.DocumentCheckRejected));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.DocumentCheckRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToSendTenderToApproveChecking(Tender tender, List<string> roles)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);

            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();




            foreach (SupplierPreQualificationDocument offer in tender.PreQualificationApplyDocuments)
                if (offer.PreQualificationResult == null)
                    throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferStatusOrTechnicalEvaluationNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.DocumentChecking)
            {
                MemberInfo property = typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.DocumentChecking));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.DocumentChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.OffersCheckingDate.HasValue && tender.OffersCheckingDate.Value.Date > DateTime.Today.Date)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.OffersCheckingDateMoreThanToday);
        }

        public void IsValidToApproveTenderChecking(Tender tender, List<string> roles)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);

            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();






            if (tender.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.DocumentCheckPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToRejectCheckTender(Tender tender, List<string> roles)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotExist);

            if (tender.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();





            if (tender.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.DocumentCheckPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + " " + tenderStatus);
            }
        }


        public async Task CheckQualificationDateValidation(Tender qualification)
        {
            if (qualification.OffersCheckingDate.Value > DateTime.Now)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.AfterDate);
            }
        }

        public async Task<UserProfile> GetDirectPurchaseMemberProfile(int userId)
        {
            var user = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(userId);
            return user;
        }
    }
}
