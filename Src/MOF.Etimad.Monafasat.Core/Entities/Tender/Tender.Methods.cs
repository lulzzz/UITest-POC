using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class Tender
    {

        #region Methods====================================================
        // To Add Or Edit Pre Qualification
        public void CreatePreQualification(int tenderID, int tenderType, string tenderName, int? technicalOrganizationId, int? preQualificationCommitteeId, DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, DateTime? offersCheckingDate, int tenderStatusId, decimal tenderPointsToPass, decimal technicalAdministrativeCapacity, decimal financialCapacity, int? qualificationTypeId, string agencyCode, int branchId)
        {
            TenderId = tenderID;
            TenderTypeId = tenderType;
            TenderName = tenderName;
            TechnicalOrganizationId = technicalOrganizationId;
            PreQualificationCommitteeId = preQualificationCommitteeId;
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            OffersCheckingDate = offersCheckingDate;
            TenderStatusId = tenderStatusId;
            TenderPointsToPass = tenderPointsToPass;
            TechnicalAdministrativeCapacity = technicalAdministrativeCapacity;
            FinancialCapacity = financialCapacity;
            QualificationTypeId = qualificationTypeId;
            AgencyCode = agencyCode;
            BranchId = branchId;
            if (tenderID == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public void UpdateQualificationMasterData(int tenderID, decimal tenderPointsToPass, decimal technicalAdministrativeCapacity, decimal financialCapacity, int? qualificationTypeId)
        {
            TenderPointsToPass = tenderPointsToPass;
            TechnicalAdministrativeCapacity = technicalAdministrativeCapacity;
            FinancialCapacity = financialCapacity;
            QualificationTypeId = qualificationTypeId;
            if (tenderID == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public bool IsOldFlow(DateTime startdate)
        {
            return SubmitionDate < startdate;
        }

        public bool IsValidToApplyOfferLocalContentChanges(DateTime localContentSettingDate)
        {
            return this.CreatedAt >= localContentSettingDate
                && this.TenderTypeId != (int)Enums.TenderType.CurrentTender
                && this.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase
                && this.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects;
        }
                
        public bool IsNewAwarding(DateTime releaseDate)
        {
            var validTenderType = TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && TenderTypeId != (int)Enums.TenderType.CurrentTender && TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && TenderTypeId != (int)Enums.TenderType.Competition && TenderTypeId != (int)Enums.TenderType.ReverseBidding;
            var awardingHistory = TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwarding);
            if (!validTenderType)
                return false;
            else if (awardingHistory != null)
                return TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwarding)?.CreatedAt.Date >= releaseDate;
            else
                return true;
        }

        public bool isValidToApproveByLastOfferPresentationDate()
        {
            return LastOfferPresentationDate > DateTime.Now;
        }

        public void UpdateConditionsTemplateSeventhStep(List<TenderConditionsTemplateTechnicalOutput> tenderConditionsTemplateTechnicalOutput, List<TenderConditionsTemplateTechnicalDeclration> tenderConditionsTemplateTechnicalDelrations
            , string projectsScope, string worksProgram, string workLocationDetails, string servicesAndWorkImplementationsMethod)
        {
            TenderConditionsTemplate.UpdateConditionsTemplateSeventhStep(
                    tenderConditionsTemplateTechnicalOutput: tenderConditionsTemplateTechnicalOutput != null ? tenderConditionsTemplateTechnicalOutput.Select(a => new TenderConditionsTemplateTechnicalOutput
                      (
                          tenderConditionsTemplateTechnicalOutputId: a.TenderConditionsTemplateTechnicalOutputId,
                          outState: a.OutputStage,
                          outputName: a.OutputName,
                          outDescriptions: a.OutputDescriptions,
                          outDeliveryTime: a.OutputDeliveryTime
                      )).ToList() : new List<TenderConditionsTemplateTechnicalOutput>(),
                    tenderConditionsTemplateTechnicalDelrations: tenderConditionsTemplateTechnicalDelrations != null ? tenderConditionsTemplateTechnicalDelrations.Select(a => new TenderConditionsTemplateTechnicalDeclration(
                        tenderConditionsTemplateTechnicalDeclrationId: a.TenderConditionsTemplateTechnicalDeclrationId,
                        decleration: a.Decleration,
                        term: a.Term
                        )).ToList() : new List<TenderConditionsTemplateTechnicalDeclration>(),
                   projectsScope: projectsScope,
                   worksProgram: worksProgram,
                   workLocationDetails: workLocationDetails,
                   servicesAndWorkImplementationsMethod: servicesAndWorkImplementationsMethod);
            EntityUpdated();
        }
        public Tender UpdateBasicData(int tenderType, int? invitationType, string tenderName, string tenderNumber, string purpose, int? announcementTemplateId, int? technicalOrganizationId, int? offersOpeningCommitteeId, int? offersCheckingCommitteeId,
                        int? directPurchaseCommitteeId, int? vROCommitteeId, int? reasonForPurchaseTenderTypeId, int? reasonForLimitedTenderTypeId, string tenderTypeOtherReason, int? preQualificationId, int offerPresentationWayId,
                        decimal? estimatedValue, decimal? conditionsBookletPrice, int userId, int? agreementMonthes, int? agreementYears, int? agreementDays, int? agreementTypeId, IList<string> agenciesIds,
                        int? numberOfWinnder, decimal? bonusValue, int? createdByTypeId, bool isRelatedToVro,
                        bool? supplierNeedSample, string samplesDeliveryAddress, int? quantityTableVersionId, int? preAnnouncementId, List<AgencyBudgetNumber> agencyBudgetNumbers = null, bool? isLowBudgetDirectPurchase = null, int? directPurchaseCommitteeMemberId = null)
        {
            TenderTypeId = tenderType;
            InvitationTypeId = invitationType;
            TenderName = tenderName;
            TenderNumber = tenderNumber;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            Purpose = purpose;
            AnnouncementTemplateId = announcementTemplateId;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            DirectPurchaseCommitteeId = directPurchaseCommitteeId;
            VROCommitteeId = vROCommitteeId;
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, TenderActions.UpdatePreQualification, "", userId);
            ConditionsBookletPrice = tenderType == (int)Enums.TenderType.CurrentDirectPurchase || tenderType == (int)Enums.TenderType.NewDirectPurchase || tenderType == (int)Enums.TenderType.Competition || tenderType == (int)Enums.TenderType.NationalTransformationProjects ? 0 : conditionsBookletPrice;
            ReasonForPurchaseTenderTypeId = reasonForPurchaseTenderTypeId;
            ReasonForLimitedTenderTypeId = reasonForLimitedTenderTypeId;
            TenderTypeOtherSelectionReason = tenderTypeOtherReason;
            PreQualificationId = preQualificationId;
            OfferPresentationWayId = isLowBudgetDirectPurchase == true ? (int)Enums.OfferPresentationWayId.OneFile : offerPresentationWayId;
            EstimatedValue = estimatedValue;
            AgreementMonthes = agreementMonthes;
            AgreementYears = agreementYears;
            AgreementDays = agreementDays;
            AgreementTypeId = agreementTypeId;
            UpdateBeneficiaryAgencies(agenciesIds);
            NumberOfWinners = numberOfWinnder;
            BonusValue = bonusValue;
            CreatedByTypeId = createdByTypeId;
            AgencyBudgetNumbers = agencyBudgetNumbers;
            if (isRelatedToVro || tenderType == (int)Enums.TenderType.NationalTransformationProjects)
            {
                SupplierNeedSample = supplierNeedSample;
                SamplesDeliveryAddress = samplesDeliveryAddress;
            }
            QuantityTableVersionId = quantityTableVersionId;
            PreAnnouncementId = preAnnouncementId;
            IsLowBudgetDirectPurchase = isLowBudgetDirectPurchase;
            DirectPurchaseMemberId = directPurchaseCommitteeMemberId;
            EntityUpdated();
            return this;
        }

        public void SetUnitStatus(Enums.TenderUnitStatus tenderUnitStatus)
        {
            TenderUnitStatusId = (int)tenderUnitStatus;
            EntityUpdated();
        }
        public void SetUnitSpacialistWouldLikeToAttendTheCommitte(bool? unitSpacialistWouldLikeToAttendTheCommitte)
        {
            this.UnitSpacialistWouldLikeToAttendTheCommitte = unitSpacialistWouldLikeToAttendTheCommitte;
            EntityUpdated();
        }
        public void UpdateOfferOpeningNotificationStatus()
        {
            OpeningNotificationSent = true;
            EntityUpdated();
        }
        public void UpdateOfferCheckingNotificationStatus()
        {
            CheckingNotificationSent = true;
            EntityUpdated();
        }
        public AgencyCommunicationRequest GetCommunicationRequests(Enums.AgencyCommunicationRequestType agencyCommunicationRequestType)
        {
            return this.AgencyCommunicationRequests.FirstOrDefault(w => w.AgencyRequestTypeId == (int)agencyCommunicationRequestType);
        }
        public void AddInvitationForQualifiedSuppliers(List<string> commericalRegesterNos)
        {
            foreach (var item in Invitations)
            {
                item.Delete();
            }

            foreach (var item in commericalRegesterNos)
            {
                Invitations.Add(new Invitation(item, InvitationStatus.ToBeSent, InvitationRequestType.Invitation, true, (int)Enums.InvititedSupplierTypes.HasCR));
            }
            EntityUpdated();
        }
        public void DeactiveAllAssingments()
        {
            TenderUnitAssigns.ForEach(a => a.DeactiveAssignments());
        }
        public void DeleteUnitModificationAttachments()
        {
            Attachments.ForEach(a => a.DeActive());
        }

        public Tender UpdatePreQualificationData(PreQualificationSavingModel model, List<TenderAttachment> attachments, int userId)
        {
            TenderTypeId = (int)Enums.TenderType.PreQualification;
            TenderName = model.TenderName;
            TenderStatusId = model.TenderStatusId;
            TechnicalOrganizationId = model.TechnicalOrganizationId;
            PreQualificationCommitteeId = model.PreQualificationCommitteeId;
            LastEnqueriesDate = model.LastEnqueriesDate;
            LastOfferPresentationDate = model.LastOfferPresentationDate;
            OffersCheckingDate = model.OffersCheckingDate;
            UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription,/* userId,*/ model.TenderCountriesIDs, false);
            UpdateAttachments(attachments, userId, false);
            EntityUpdated();
            return this;
        }
        public Tender UpdateQualificationDataFromSecrary(PreQualificationSavingModel model, List<TenderAttachment> attachments, int userId)
        {
            TenderStatusId = model.TenderStatusId;
            LastEnqueriesDate = model.LastEnqueriesDate;
            LastOfferPresentationDate = model.LastOfferPresentationDate;
            OffersCheckingDate = model.OffersCheckingDate;
            UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription,/* userId,*/ model.TenderCountriesIDs, false);
            UpdateAttachments(attachments, userId, false);
            EntityUpdated();
            return this;
        }

        public Tender UpdateTenderDates(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, DateTime? offersOpeningDate, DateTime? offersCheckingDate,
                                        string intialGurantteeAddress, decimal? initialGuaranteePercentage, bool? supplierNeedSample, string samplesDeliveryAddress, int? offersOpeningAddressId, string buildingName, string floarNumber, string departmentName, DateTime? deliveryDate)
        {
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            OffersOpeningDate = offersOpeningDate;
            OffersCheckingDate = offersCheckingDate;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            SupplierNeedSample = supplierNeedSample;
            SamplesDeliveryAddress = samplesDeliveryAddress;
            OffersOpeningAddress = null;
            OffersOpeningAddressId = offersOpeningAddressId;
            InitialGuaranteeAddress = intialGurantteeAddress;
            InitialGuaranteePercentage = initialGuaranteePercentage;
            BuildingName = buildingName;
            FloarNumber = floarNumber;
            DepartmentName = departmentName;
            DeliveryDate = deliveryDate;
            EntityUpdated();
            return this;
        }
        public Tender AddEditTenderDates(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, DateTime? offersOpeningDate, DateTime? offersCheckingDate,
                                         bool? supplierNeedSample, string samplesDeliveryAddress, int? offersOpeningAddressId, string buildingName, string floarNumber, string departmentName, DateTime? deliveryDate, int? awardingStoppingeriod)
        {
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            OffersOpeningDate = offersOpeningDate;
            OffersCheckingDate = offersCheckingDate;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            SupplierNeedSample = supplierNeedSample;
            SamplesDeliveryAddress = samplesDeliveryAddress;
            OffersOpeningAddress = null;
            OffersOpeningAddressId = offersOpeningAddressId;
            BuildingName = buildingName;
            FloarNumber = floarNumber;
            DepartmentName = departmentName;
            DeliveryDate = deliveryDate;
            AwardingStoppingPeriod = awardingStoppingeriod;

            EntityUpdated();
            return this;
        }

        public Tender UpdateFinalAndInitialGuarantee(decimal? finalGuaranteePercentage, decimal? initialGuaranteePercentage, string intialGurantteeAddress)
        {
            AwardingDiscountPercentage = finalGuaranteePercentage;
            InitialGuaranteePercentage = initialGuaranteePercentage;
            InitialGuaranteeAddress = intialGurantteeAddress;
            EntityUpdated();
            return this;
        }


        public Tender UpdateTenderInApproveVRO(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, DateTime? offersOpeningDate, int vroCommitteeId, int? technicalOrganizationId, int offersOpeningAddressId/*, bool? supplierNeedSample, string samplesDeliveryAddress*/)
        {
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            OffersOpeningDate = offersOpeningDate;
            VROCommitteeId = vroCommitteeId;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersOpeningAddressId = offersOpeningAddressId;
            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderBranchAndAgency(int branchId, string agencyCode)
        {
            VRORelatedBranchId = BranchId;
            BranchId = branchId;
            Branch = null;
            AgencyCode = agencyCode;
            Agency = null;
            EntityUpdated();
            return this;
        }
        // called after accept date revision to update all dates
        public Tender UpdateDates(TenderDatesChange tenderRevisionDate, int userId)
        {
            LastEnqueriesDate = tenderRevisionDate.LastEnqueriesDate;
            LastOfferPresentationDate = tenderRevisionDate.LastOfferPresentationDate;
            OffersOpeningDate = tenderRevisionDate.OffersOpeningDate;
            OffersCheckingDate = tenderRevisionDate.OffersCheckingDate;
            TenderStatusId = (int)Enums.TenderStatus.Approved;
            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderRelations(IList<int> AreaIds, IList<int> ActivityIds, IList<int> ConstructionWorksIds, IList<int> MentainanceRunnigWorksIds, bool? insideKSA, string details, string activityDescription, /*int userId,*/ IList<int> CountriesIds, bool addHistory = true, int? templateYears = 0, int statusId = 0)
        {
            InsideKSA = insideKSA;
            Details = details;
            TenderStatusId = statusId != 0 ? statusId : TenderStatusId;
            UpdateAreas(AreaIds, insideKSA);
            UpdateCountries(CountriesIds, insideKSA);
            UpdateActivities(ActivityIds);
            UpdateConstructionWorks(ConstructionWorksIds);
            UpdateRunningMentainanceWorks(MentainanceRunnigWorksIds);
            ActivityDescription = activityDescription;
            if (templateYears != TemplateYears)
                foreach (var table in TenderQuantityTables)
                    table.DeleteTenderQuantityTableWithItems();
            TemplateYears = templateYears;
            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderQuantityTablesItems(int? templateYears)
        {
            TemplateYears = templateYears;
            foreach (var table in TenderQuantityTables)
                table.DeleteTenderQuantityTablesItems();
            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderTemplateYears(int? templateYears)
        {
            TemplateYears = templateYears;
            EntityUpdated();
            return this;
        }
        public void DeleteTenderQuantityTables()
        {
            foreach (var table in TenderQuantityTables)
                table.Delete();
        }
        public void DeleteTenderQuantityTablesItems()
        {
            foreach (var table in TenderQuantityTables)
                table.DeleteTenderQuantityTablesItems();
        }
        public Tender UpdateTenderRelationsWithoutQuantityTables(IList<int> AreaIds, IList<int> ActivityIds, IList<int> ConstructionWorksIds, IList<int> MentainanceRunnigWorksIds, bool? insideKSA, string details, string activityDescription, IList<int> CountriesIds, bool? isTawreed, bool addHistory = true, int statusId = 0)
        {
            InsideKSA = insideKSA;
            Details = details;
            ActivityDescription = activityDescription;
            TenderStatusId = statusId != 0 ? statusId : TenderStatusId;
            UpdateAreas(AreaIds, insideKSA);
            UpdateCountries(CountriesIds, insideKSA);
            UpdateActivities(ActivityIds);
            UpdateConstructionWorks(ConstructionWorksIds);
            UpdateRunningMentainanceWorks(MentainanceRunnigWorksIds);
            IsTenderContainsTawreedTables = isTawreed;
            EntityUpdated();
            return this;
        }
        public void SetIsUnitSecreteryAccepted(bool? isUnitSecreteryAccepted)
        {
            this.IsUnitSecreteryAccepted = isUnitSecreteryAccepted;
            EntityUpdated();
        }
        public Tender UpdateQuantityTableName(long tableId, string name)
        {
            TenderQuantityTable quantityTable = TenderQuantityTables.FirstOrDefault(q => q.Id == tableId);
            quantityTable.UpdateName(name);
            EntityUpdated();
            return this;
        }
        public Tender UpdateQuantityTableChangesName(int changeRequestId, long tableId, string name)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(x => x.TenderChangeRequestId == changeRequestId);
            changeRequest.UpdateQuantityTableName(tableId, name);
            EntityUpdated();
            return this;
        }
        public Tender UpdateHasAlternative(bool hasAlternative)
        {
            HasAlternativeOffer = hasAlternative;
            EntityUpdated();
            return this;
        }
        public void UpdateEstimatedValue(Decimal? estimatedValue)
        {
            EstimatedValue = estimatedValue;
            EntityUpdated();
        }
        public Tender UpdateTenderAwardingType(int tenderId, bool? awardingType)
        {
            TenderAwardingType = awardingType;
            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderAwardingNotification(bool? hasGuarantee, decimal? awardingPercentage, int? monthCount, string finalGuaranteeDeliveryAddress)
        {
            HasGuarantee = hasGuarantee;
            AwardingDiscountPercentage = hasGuarantee == true ? awardingPercentage : null;
            AwardingMonths = hasGuarantee == true ? monthCount : null;
            FinalGuaranteeDeliveryAddress = hasGuarantee == true ? finalGuaranteeDeliveryAddress : null;
            EntityUpdated();
            return this;
        }
        public Tender SetTenderRelations(List<TenderActivity> tenderActivities, List<TenderArea> tenderAreas, List<TenderCountry> tenderCountries, List<TenderConstructionWork> constructionWorks, List<TenderMaintenanceRunnigWork> tenderMaintenances)
        {
            TenderActivities = new List<TenderActivity>(tenderActivities);
            TenderAreas = new List<TenderArea>(tenderAreas);
            TenderCountries = new List<TenderCountry>(tenderCountries);
            TenderConstructionWorks = new List<TenderConstructionWork>(constructionWorks);
            TenderMaintenanceRunnigWorks = new List<TenderMaintenanceRunnigWork>(tenderMaintenances);
            EntityUpdated();
            return this;
        }
        public Tender SetTenderReceivedOffers(List<Offer> offers)
        {
            Offers = new List<Offer>(offers);
            EntityUpdated();
            return this;
        }

        public Tender SetTenderHistories(List<TenderHistory> tenderHistories)
        {
            TenderHistories = new List<TenderHistory>(tenderHistories);
            EntityUpdated();
            return this;
        }

        public Tender SetPreviousFramWork(int tenderId)
        {
            TenderId = 0;
            PreviousFramWorkId = tenderId;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            CheckingDateSet = false;
            foreach (var table in TenderQuantityTables)
            {
                table.SetAddedState();
            }
            foreach (var item in Attachments)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderActivities)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderAreas)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderCountries)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderConstructionWorks)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderMaintenanceRunnigWorks)
            {
                item.SetAddedState();
            }
            foreach (var item in TenderAgreementAgencies)
            {
                item.SetAddedState();
            }
            if (TenderConditionsTemplate != null)
            {

                TenderConditionsTemplate.SetAddedState();
                foreach (var item in TenderConditionsTemplate.TemplateCertificates)
                {
                    item.SetAddedState();
                }
                foreach (var item in TenderConditionsTemplate.TenderConditionsTemplateTechnicalOutputs)
                {
                    item.SetAddedState();
                }
                foreach (var item in TenderConditionsTemplate.TenderConditionsTemplateTechnicalDelrations)
                {
                    item.SetAddedState();
                }
                if (TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null)
                    TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.SetAddedState();
            }

            EntityCreated();
            return this;
        }
        public Tender UpdateSecondStageTemplates(TenderConditionsTemplate tenderConditionsTemplate)
        {
            if (tenderConditionsTemplate != null)
            {
                TenderConditionsTemplate = tenderConditionsTemplate;
                TenderConditionsTemplate.SetAddedState();

                foreach (var item in TenderConditionsTemplate.TemplateCertificates)
                {
                    item.SetAddedState();
                }
                foreach (var item in TenderConditionsTemplate.TenderConditionsTemplateTechnicalOutputs)
                {
                    item.SetAddedState();
                }
                foreach (var item in TenderConditionsTemplate.TenderConditionsTemplateTechnicalDelrations)
                {
                    item.SetAddedState();
                }
                if (TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null)
                    TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.SetAddedState();
            }
            EntityUpdated();
            return this;
        }
        public Tender UpdateAwardingStoppingPeriod(int? stoppingPeriod)
        {
            AwardingStoppingPeriod = stoppingPeriod ?? 0;
            EntityUpdated();
            return this;
        }
        public Tender UpdateAwardingReport(string fileName, string fileNameId)
        {
            AwardingReportFileName = fileName;
            AwardingReportFileNameId = fileNameId;
            EntityUpdated();
            return this;
        }
        public void DeleteAwardingReport()
        {
            AwardingReportFileName = null;
            AwardingReportFileNameId = null;
            EntityUpdated();
        }
        public Tender UpdateAttachments(List<TenderAttachment> attachments, int userId, bool addHistory = true)
        {
            foreach (var item in Attachments)
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                Attachments.Add(attachment);
            }
            if (addHistory)
            {
                AddActionHistory((int)Enums.TenderStatus.Established, TenderActions.UpdatePreQualification, "", userId);
            }
            EntityUpdated();
            return this;
        }
        public Tender UpdateLocalContentAttachments(List<TenderAttachment> attachments, int userId, bool addHistory = true)
        {
            Attachments = Attachments.Where(a => a.AttachmentTypeId == (int)Enums.AttachmentType.LocalContent).ToList();
            foreach (var item in Attachments)
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                Attachments.Add(attachment);
            }
            if (addHistory)
            {
                AddActionHistory((int)Enums.TenderStatus.Established, TenderActions.UpdatePreQualification, "", userId);
            }
            EntityUpdated();
            return this;
        }
        public void ResetUnitSecretaryAction()
        {
            IsUnitSecreteryAccepted = null;
            EntityUpdated();
        }

        public void UpdateBeneficiaryAgencies(IList<string> agencyIds)
        {
            if (agencyIds != null)
            {
                //will not cahnge
                var mutualagencies = TenderAgreementAgencies.Where(x => agencyIds.Contains(x.AgencyCode)).ToList();
                var mutualIds = mutualagencies.Select(a => a.AgencyCode).ToList();
                //Will be deleted
                var agenciesToBeDeleted = TenderAgreementAgencies.Where(x => !agencyIds.Contains(x.AgencyCode)).ToList();
                //Will be added
                List<string> agenciesAddedIDs = agencyIds.Where(x => !mutualIds.Contains(x)).ToList();
                if (TenderAgreementAgencies != null)
                {
                    foreach (TenderAgreementAgency item in agenciesToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (agencyIds != null)
                {
                    foreach (var item in agenciesAddedIDs)
                    {
                        TenderAgreementAgencies.Add(new TenderAgreementAgency(item));
                    }
                }
            }
        }
        public void ResetTechnicalCommittee(int committeeId)
        {
            if (committeeId != 0)
                TechnicalOrganizationId = null;
            EntityUpdated();
        }
        public void ResetCheckCommittee(int committeeId)
        {
            if (committeeId != 0)
                OffersCheckingCommitteeId = null;
            EntityUpdated();
        }
        public void UpdateTenderCommittee(int tenderStatusId, int CommitteeTypeId, int committeeId, int userId)
        {

            switch (CommitteeTypeId)
            {
                case (int)Enums.CommitteeType.TechincalCommittee:
                    if (TechnicalOrganizationId != committeeId)
                    {
                        TechnicalOrganizationId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
                case (int)Enums.CommitteeType.OpenOfferCommittee:
                    if (OffersOpeningCommitteeId != committeeId)
                    {
                        OffersOpeningCommitteeId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdateOpeningCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
                case (int)Enums.CommitteeType.CheckOfferCommittee:
                    if (OffersCheckingCommitteeId != committeeId)
                    {
                        OffersCheckingCommitteeId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdateCheckingCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
                case (int)Enums.CommitteeType.PurchaseCommittee:
                    if (DirectPurchaseCommitteeId != committeeId)
                    {
                        DirectPurchaseCommitteeId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdateDirectPurchaseCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
                case (int)Enums.CommitteeType.PreQualificationCommittee:
                    if (PreQualificationCommitteeId != committeeId)
                    {
                        PreQualificationCommitteeId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdatePreQualificationCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
                case (int)Enums.CommitteeType.VROCommittee:
                    if (VROCommitteeId != committeeId)
                    {
                        VROCommitteeId = committeeId;
                        AddActionHistory(tenderStatusId, TenderActions.UpdateVROCommittee, "", userId);
                    }
                    else
                    {
                        throw new BusinessRuleException("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى");
                    }
                    break;
            }
            EntityUpdated();

        }
        public void UpdateTenderInvitationType()
        {
            InvitationTypeId = (int)Enums.InvitationType.Public;
            EntityUpdated();
        }
        public void ResetOpenCommittee(int committeeId)
        {
            if (committeeId != 0)
                OffersOpeningCommitteeId = null;
            EntityUpdated();
        }
        public void UpdateCommittees(int tenderStatusId, int? openingCommitteeId, int? CheckingCommitteeId, int? technicalCommitteeId, int? vROCommitteeId, int? directPurchaseCommitteeId, int? directPurchaseCommitteeMemberId, int userId)
        {
            if (OffersOpeningCommitteeId != openingCommitteeId)
            {
                OffersOpeningCommitteeId = openingCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateOpeningCommittee, "", userId);
            }
            if (OffersCheckingCommitteeId != CheckingCommitteeId)
            {
                OffersCheckingCommitteeId = CheckingCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateCheckingCommittee, "", userId);
            }
            if (TechnicalOrganizationId != technicalCommitteeId)
            {
                TechnicalOrganizationId = technicalCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
            }
            if (VROCommitteeId != vROCommitteeId)
            {
                VROCommitteeId = vROCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
            }
            if (DirectPurchaseCommitteeId != directPurchaseCommitteeId)
            {
                DirectPurchaseCommitteeId = directPurchaseCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
            }
            if (DirectPurchaseMemberId != directPurchaseCommitteeMemberId)
            {
                DirectPurchaseMemberId = directPurchaseCommitteeMemberId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
            }
            EntityUpdated();
        }
        public void UpdatePreQualificationCommittees(int tenderStatusId, int? qualificationCommitteedId, int? technicalCommitteeId, int userId)
        {

            if (PreQualificationCommitteeId != qualificationCommitteedId)
            {
                PreQualificationCommitteeId = qualificationCommitteedId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateCheckingCommittee, "", userId);
            }
            if (TechnicalOrganizationId != technicalCommitteeId)
            {
                TechnicalOrganizationId = technicalCommitteeId;
                AddActionHistory(tenderStatusId, TenderActions.UpdateTechnicalCommittee, "", userId);
            }

            EntityUpdated();
        }
        public void UpdateSamplesDeliveryAddress(string address)
        {
            SamplesDeliveryAddress = address;
            EntityUpdated();
        }

        public void UpdateAreas(IList<int> areaIds, bool? insideKSA)
        {

            if (areaIds.Count > 0)
            {
                //will not cahnge
                var mutualAreas = TenderAreas.Where(x => areaIds.Contains(x.AreaId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.AreaId).ToList();
                //Will be deleted
                var AreasToBeDeleted = TenderAreas.Where(x => !areaIds.Contains(x.AreaId)).ToList();
                //Will be added
                List<int> AreasAddedIDs = areaIds.Where(x => !mutualIds.Contains(x)).ToList<int>();

                if (insideKSA.HasValue && insideKSA.Value && TenderCountries != null)
                {
                    foreach (TenderCountry item in TenderCountries)
                    {
                        item.Delete();
                    }
                }
                if (TenderAreas.Count > 0)
                {
                    foreach (TenderArea item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (areaIds.Count > 0)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        TenderAreas.Add(new TenderArea(item));
                    }
                }
            }

        }

        public void UpdateCountries(IList<int> countriesIds, bool? insideKSA)
        {
            if (countriesIds.Count > 0)
            {
                if (insideKSA.HasValue && !insideKSA.Value && TenderCountries != null)
                {
                    foreach (TenderCountry item in TenderCountries)
                    {
                        item.Delete();
                    }
                }
                //Will be deleted
                var CountriesToBeDeleted = TenderCountries.ToList();
                //Will be added
                List<int> CountriesAddedIDs = countriesIds.ToList();
                if (TenderCountries.Count > 0)
                {
                    foreach (var item in CountriesToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (countriesIds.Count > 0)
                {
                    foreach (var item in CountriesAddedIDs)
                    {
                        TenderCountries.Add(new TenderCountry(item));
                    }
                }
            }
        }

        public void UpdateActivities(IList<int> activityIds)
        {
            if (activityIds.Count > 0)
            {
                //Will be deleted
                var ActivitiesToBeDeleted = TenderActivities.ToList();
                //Will be added
                List<int> ActivitiesAddedIDs = activityIds.ToList();
                if (TenderActivities.Count > 0)
                {
                    foreach (var item in ActivitiesToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (activityIds.Count > 0)
                {
                    foreach (var item in ActivitiesAddedIDs)
                    {
                        TenderActivities.Add(new TenderActivity(item));
                    }
                }
            }
        }
        public void UpdateConstructionWorks(IList<int> constructionWorksIds)
        {
            if (constructionWorksIds.Count > 0)
            {
                var ConstructionToBeDeleted = TenderConstructionWorks.ToList();

                List<int> ConstructionAddedIDs = constructionWorksIds.ToList();

                if (TenderConstructionWorks.Count > 0)
                {
                    foreach (var item in ConstructionToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                if (constructionWorksIds.Count > 0)
                {
                    foreach (var item in ConstructionAddedIDs)
                    {
                        TenderConstructionWorks.Add(new TenderConstructionWork(item));
                    }
                }
            }
        }

        public void UpdateRunningMentainanceWorks(IList<int> mentainanceRunnigWorksIds)
        {
            if (mentainanceRunnigWorksIds.Count > 0)
            {
                var MentainanceToBeDeleted = TenderMaintenanceRunnigWorks.ToList();

                List<int> MentainanceAddedIDs = mentainanceRunnigWorksIds.ToList();

                if (TenderMaintenanceRunnigWorks.Count > 0)
                {
                    foreach (var item in MentainanceToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                if (mentainanceRunnigWorksIds.Count > 0)
                {
                    foreach (var item in MentainanceAddedIDs)
                    {
                        TenderMaintenanceRunnigWorks.Add(new TenderMaintenanceRunnigWork(item));
                    }
                }
            }
        }

        public void UpdateUnregesteredInvitations()
        {
            foreach (var item in UnRegisteredSuppliersInvitation)
                item.UpdateStatus((int)Enums.InvitationStatus.New);
        }
        public void SendInvitations(List<string> suppliers)
        {
            foreach (var invitation in Invitations)
            {
                if (suppliers.Contains(invitation.CommericalRegisterNo))
                {
                    invitation.SendInvitation();
                }
            }
            EntityUpdated();
        }

        public void AddSupplierInvitation(List<string> checkedCommericalRegesterNo, List<string> unCheckedcommericalRegesterNo)
        {
            foreach (var item in Invitations.Where(x => x.IsActive == true && (unCheckedcommericalRegesterNo.Contains(x.CommericalRegisterNo) || x.StatusId == (int)Enums.InvitationStatus.Rejected)).ToList())
            {
                item.Delete();
            }
            foreach (var item in checkedCommericalRegesterNo)
            {

                var cuurentInvitation = Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == item);

                if (cuurentInvitation == null)
                {
                    Invitations.Add(new Invitation(item, InvitationStatus.New, InvitationRequestType.Invitation, false, (int)Enums.InvititedSupplierTypes.HasCR));
                }
                else if (cuurentInvitation != null && cuurentInvitation.StatusId == (int)Enums.InvitationStatus.Withdrawal)
                {
                    cuurentInvitation.UpdateStatusForResend();
                }
            }
            EntityUpdated();
        }

        public void RegisterSupplierEmails(List<string> emails)
        {
            foreach (var item in UnRegisteredSuppliersInvitation.Where(x => x.Email != null))
            {
                item.DeActive();
            }
            foreach (var item in emails)
            {
                UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation(item, (int)Enums.InvitationSendType.Email));
            }
            EntityUpdated();
        }
        public void RegisterSupplierMobilNo(List<string> mobilNoList)
        {
            foreach (var item in UnRegisteredSuppliersInvitation.Where(x => x.MobileNo != null))
            {
                item.DeActive();
            }
            foreach (var item in mobilNoList)
            {
                UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation(item, (int)Enums.InvitationSendType.Mobile));
            }
            EntityUpdated();
        }
        public void AddActionHistory(int statusId, TenderActions action, string rejectionReason, int currentUserId)
        {
            TenderHistories.Add(new TenderHistory(currentUserId, statusId, action, TenderId, rejectionReason));
        }
        public void AddTenderAwardingHistory(string commercialRegisterationNumber, int tenderId, bool? tenderAwardingType, decimal? awardingValue, int awardingIndex)
        {
            TenderAwardingHistories.Add(new TenderAwardingHistory(commercialRegisterationNumber, tenderId, tenderAwardingType, awardingValue, awardingIndex));
            EntityUpdated();
        }
        public void SendRequestForTender(string commericalRegisterNo)
        {
            Invitations.Add(new Invitation(commericalRegisterNo, InvitationStatus.New, InvitationRequestType.Request, false));
            EntityUpdated();
        }
        public void AddInvitationBySupplier(string commericalRegisterNo)
        {
            Invitations.Add(new Invitation(commericalRegisterNo, InvitationStatus.New, InvitationRequestType.Invitation, false));
            EntityUpdated();
        }
        public void UpdateTenderStatus(Enums.TenderStatus tenderStatus, string rejectionReason, int userId, TenderActions tenderAction, bool isActive = true)
        {
            this.TenderStatusId = (int)tenderStatus;
            this.Status = null;
            IsActive = isActive;
            if (tenderStatus == Enums.TenderStatus.Approved)
            {
                this.SubmitionDate = DateTime.Now;
            }
            AddActionHistory(TenderStatusId, tenderAction, rejectionReason, userId);
            EntityUpdated();
        }
        public void UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus status)
        {
            this.ConditionTemplateStageStatusId = (int)status;
            EntityUpdated();
        }
        public void UpdateTenderStatus(Enums.TenderStatus tenderStatus)
        {
            if (TenderStatusId != (int)tenderStatus)
            {
                this.TenderStatusId = (int)tenderStatus;
                this.Status = null;
                EntityUpdated();
            }

        }
        public void UpdateSubmitionDate()
        {
            this.SubmitionDate = DateTime.Now;
        }
        public void SetCreationDate()
        {
            this.CreatedAt = DateTime.Now;
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }


        public void ResetPostQualifications()
        {
            IsActive = false;
            TenderStatusId = (int)Enums.TenderStatus.Canceled;
            EntityUpdated();
        }

        public void UpdateEmarketPlaceStatus()
        {
            IsSendToEmarketPlace = true;
            EntityUpdated();
        }
        public void UpdateFinishedStoppingAwardingPeriodTendersStatus()
        {
            IsNotificationSentForStoppingPeriod = true;
            EntityUpdated();
        }

        public void SetAuditorAgree(bool yes)
        {
            AuditorAgree = yes;
            EntityUpdated();
        }
        public void SetCompetitionIsBudgetedOrNot(bool yes)
        {
            SubmitionDate = DateTime.Now;
            CompetitionIsBudgeted = yes;
            EntityUpdated();
        }

        public void SetReferenceNumber(string referenceNumber)
        {
            this.ReferenceNumber = referenceNumber;
        }

        public void AddBiddingRounds(BiddingRound biddingRound)
        {
            if (BiddingRounds == null)
                BiddingRounds = new List<BiddingRound>();
            BiddingRounds.Add(biddingRound);
        }
        public void AddBiddingRoundOfffer(int biddingRoundId, int offerId, decimal biddingValue)
        {
            var biddingOffer = new BiddingRoundOffer(biddingRoundId, offerId, biddingValue);
            BiddingRounds.FirstOrDefault(r => r.BiddingRoundId == biddingRoundId).BiddingRoundOffers.Add(biddingOffer);
            EntityUpdated();
        }
        public Tender UpdateSecondStageBasicData(string purpose, int? technicalOrganizationId, int? offersOpeningCommitteeId,
        int offerPresentationWayId, int userId, decimal? estimatedValue, List<AgencyBudgetNumber> agencyBudgetNumbers = null)
        {
            Purpose = purpose;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            OfferPresentationWayId = offerPresentationWayId;
            EstimatedValue = estimatedValue;
            AgencyBudgetNumbers = agencyBudgetNumbers;
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, TenderActions.UpdateTender, "", userId);
            EntityUpdated();
            return this;
        }

        #region Plaints

        public bool AddAgencyPlaintRequest(TenderPlaintCommunicationRequestModel model)
        {
            AgencyCommunicationRequest agencyCommunicationRequest = AgencyCommunicationRequests.FirstOrDefault(c => c.TenderId == model.TenderId && c.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint && c.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent);
            if (agencyCommunicationRequest == null)
            {
                agencyCommunicationRequest = new AgencyCommunicationRequest(model.CommunicationRequestId, model.EncryptedTenderId, model.PlaintRequestId, model.EncryptedOfferId, model.PlaintReason, RoleNames.supplier, model.AttachmentList);
                AgencyCommunicationRequests.Add(agencyCommunicationRequest);
            }
            else
            {
                agencyCommunicationRequest.UpdatePlaintAgencyCommunicationRequest(agencyCommunicationRequest.AgencyRequestId, model.EncryptedTenderId, model.PlaintRequestId, model.EncryptedOfferId, model.PlaintReason, model.AttachmentList);
            }
            EntityUpdated();
            return true;
        }
        public bool AddAgencyCommunicationRequest(AgencyCommunicationRequest request)
        {

            this.AgencyCommunicationRequests.Add(request);
            EntityUpdated();
            return true;
        }

        #endregion

        #endregion

        #region Invitations

        public void AddInvitationForAcceptedSuppliersInFirstStage(List<string> commericalRegesterNos)
        {
            if (!Invitations.Any())
            {
                foreach (var item in commericalRegesterNos)
                {
                    Invitations.Add(new Invitation(item, InvitationStatus.ToBeSent, InvitationRequestType.Invitation, false, (int)Enums.InvititedSupplierTypes.HasCR));
                }
                EntityUpdated();
            }
        }
        public void AddSupplierInvitationWhileCreation(List<string> checkedCommericalRegesterNo, int userId)
        {
            foreach (var item in Invitations.Where(x => x.InvitationTypeId == (int)Enums.InvitationRequestType.Invitation && x.StatusId == (int)Enums.InvitationStatus.ToBeSent && x.IsActive == true))
            {
                item.Delete();
            }
            foreach (var item in checkedCommericalRegesterNo)
            {
                Invitations.Add(new Invitation(item, InvitationStatus.ToBeSent, InvitationRequestType.Invitation, false, (int)Enums.InvititedSupplierTypes.HasCR));
            }
            TenderStatusId = (int)Enums.TenderStatus.Established;
            AddActionHistory(TenderStatusId, TenderActions.InviteVendors, "", userId);
            EntityUpdated();
        }

        public void RemoveInvitationsForUnregisteredSupplierByCrAndTenderId(int tenderId, string crnumber)
        {
            foreach (var item in UnRegisteredSuppliersInvitation.Where(x => x.TenderId == tenderId && x.CrNumber == crnumber))
            {
                item.Delete();
            }
            EntityUpdated();
        }
        public void AddInvitationsForUnRegisteredSupplier(string crNumber, int invitationType, string email, string mobile, InvitationStatus invitationStatus, string description)
        {
            if (invitationType == (int)UnRegisteredSuppliersInvitationType.Existed || invitationType == (int)UnRegisteredSuppliersInvitationType.Foriegn)
            {
                if (!Invitations.Select(x => x.CommericalRegisterNo).Contains(crNumber))
                {
                    Invitations.Add(new Invitation(crNumber, invitationStatus, InvitationRequestType.Invitation, false, (int)Enums.InvititedSupplierTypes.Foriegn));
                }
            }
            UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation(crNumber, invitationType, email, mobile, invitationStatus, description));
        }

        #endregion Invitations

        #region Post-Qualification

        public Tender AddPostQualificationInvitations(List<string> commercialnumbers)
        {
            foreach (var item in PostQualificationInvitations)
            {
                item.Delete();
            }
            foreach (var cr in commercialnumbers)
            {
                PostQualificationInvitations.Add(new PostQualificationSuppliersInvitations(cr));
            }
            return this;
        }
        public Tender UpdatePostQualificationData(PreQualificationSavingModel model, List<TenderAttachment> attachments, int userId)
        {
            TenderTypeId = (int)Enums.TenderType.PostQualification;
            TenderStatusId = model.TenderStatusId;
            TenderName = model.TenderName;
            TechnicalOrganizationId = model.TechnicalOrganizationId;
            OffersCheckingCommitteeId = model.OffersCheckingCommitteeId;
            PreQualificationCommitteeId = model.PreQualificationCommitteeId;
            LastEnqueriesDate = model.LastEnqueriesDate;
            LastOfferPresentationDate = model.LastOfferPresentationDate;
            OffersCheckingDate = model.OffersCheckingDate;
            UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, /*userId,*/ model.TenderCountriesIDs, false);
            UpdateAttachments(attachments, userId, false);

            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, TenderActions.UpdatePostQualification, "", userId);
            EntityUpdated();
            return this;
        }

        #endregion Post-Qualification

        public void SetCheckingDateFlagForUnitNotification(bool checkingDateSet)
        {
            this.CheckingDateSet = checkingDateSet;
            EntityUpdated();
        }
        public void SetFinancialCheckingDateFlagForUnitNotification(bool financialCheckingDateSet)
        {
            this.FinancialCheckingDateSet = financialCheckingDateSet;
            EntityUpdated();
        }

        #region ConditionTempalte

        public void CreateConditionsTemplate()
        {
            TenderConditionsTemplate = new TenderConditionsTemplate();
        }

        public void CreateTenderLocalContent()
        {
            TenderLocalContent = new TenderLocalContent();
        }
        public void SetTenderLocalContent(TenderLocalContent localContent)
        {
            TenderLocalContent = localContent;
            EntityUpdated();

        }




        #endregion ConditionTempalte

        public Tender SaveTenderQuantityTables(List<TenderQuantityItemDTO> table, string tableName, long currentItemId, out long itemId, int formId)
        {
            TenderQuantityTable tbl;
            long tableId = table[0].TableId;
            if (tableId != 0)
            {
                tbl = TenderQuantityTables.FirstOrDefault(a => a.Id == tableId);
                tbl = tbl.SaveQuantityTableItems(tableId, table, currentItemId, out itemId);
            }
            else
            {
                TenderQuantityTables.Add(new TenderQuantityTable(table[0].TableId, table, formId));
                itemId = 0;
            }
            EntityUpdated();
            return this;
        }

        public Tender SaveTenderQuantityTableItems(List<TenderQuantityItemDTO> table, string tableName)
        {
            TenderQuantityTable tbl;
            long tableId = table[0].TableId;
            tbl = TenderQuantityTables.Where(a => a.Id == tableId).FirstOrDefault();
            tbl = tbl.SaveQuantityTableBulkItems(tableId, table, tableName);
            EntityUpdated();
            return this;
        }
        public TenderQuantityTable CreateTenderQuantityTables(int tableId, string name, int formId)
        {
            var table = new TenderQuantityTable(tableId, string.IsNullOrEmpty(name) ? name = "اسم الجدول" : name, formId);
            TenderQuantityTables.Add(table);
            EntityUpdated();
            return table;
        }
        public TenderChangeRequest CreateTenderQuantityTableChangeRequest(int userId, string userRole)
        {
            var changeRequest = new TenderChangeRequest(TenderId, (int)Enums.ChangeRequestType.QuantitiesTables, (int)ChangeStatusType.New, userRole, HasAlternativeOffer);
            ChangeRequests.Add(changeRequest);
            AddActionHistory(TenderStatusId, TenderActions.CreateToqRequest, "", userId);
            EntityUpdated();
            return changeRequest;
        }
        public TenderQuantityTableChanges CreateTenderQuantityTablesChanges(int changeId, string name, int formId)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(x => x.TenderChangeRequestId == changeId);
            var table = changeRequest.CreateTenderQuantityTables(0, string.IsNullOrEmpty(name) ? "اسم الجدول" : name, formId);
            EntityUpdated();
            return table;
        }
        public Tender DeleteQuantityTableItems(long tableId, int itemNumber)
        {
            TenderQuantityTable table = TenderQuantityTables.FirstOrDefault(a => a.Id == tableId && a.IsActive == true); //Starting from 1
            if (table != null)
                table.DeleteQuantityTableItems(itemNumber);
            EntityUpdated();
            return this;
        }
        public Tender DeleteQuantityTableItemsChanges(int changeRequestId, long tableId, int itemNumber)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(x => x.TenderChangeRequestId == changeRequestId);
            changeRequest.DeleteQuantityTableItemsChanges(tableId, itemNumber);
            EntityUpdated();
            return this;
        }
        public void DeleteQualificationConfigurations()
        {
            foreach (var item in this.QualificationConfigurations)
            {
                item.Delete();
            }
        }
        public void DeleteQualificationSubCategoryConfigurations()
        {
            foreach (var item in this.QualificationSubCategoryConfigurations)
            {
                item.Delete();
            }
        }
        public void SaveTenderQuantityTableChange(int changeRequestId, List<TenderQuantityItemDTO> table, long currentItemId, out long itemId, long changeTableId)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(a => a.TenderChangeRequestId == changeRequestId);
            changeRequest.SaveTenderQuantityTables(table, currentItemId, out itemId, changeTableId);
            EntityUpdated();
        }
        public long GetTenderQuantityTableLastIndexInEdit(int changeRequestId, long changeTableId)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(a => a.TenderChangeRequestId == changeRequestId);
            return changeRequest.LastItemIndex(changeTableId);
        }
        public void DeleteCancelRequestedByDataEntry(int qualificationId)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(c => c.TenderId == qualificationId && c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && c.RequestedByRoleName == RoleNames.DataEntry);
            if (changeRequest != null)
            {
                changeRequest.DeActive();
            }
        }

        public void AddChangeRequestForUnitTest(TenderChangeRequest tenderChangeRequest)
        {
            this.ChangeRequests.Add(tenderChangeRequest);
            EntityUpdated();
        }

        public void DeletePendingCancelRequests(int qualificationId)
        {
            var changeRequest = ChangeRequests.FirstOrDefault(c => c.TenderId == qualificationId && c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending);
            if (changeRequest != null)
            {
                changeRequest.DeActive();
            }
        }

        public void SetNationalProductPercentage(decimal rate)
        {
            if (ContainSupply || IsTenderContainsTawreedTables.Value)
            {
                NationalProductPercentage = rate;
                EntityUpdated();
            }
        }
        public void SetLocalContenetNationalProductPercentage(bool isTenderHasNationalProductMechanism, decimal nationalProductPercentage)
        {
            if (ContainSupply || IsTenderContainsTawreedTables.Value || isTenderHasNationalProductMechanism)
            {
                TenderLocalContent.SetNationalProductPercentage(nationalProductPercentage);
                EntityUpdated();
            }
        }

        //checks if the tender doesn't have any plaints or escalations active and is finalized
        public bool isTenderFinalAwarded(int esclationPeriod, int plaintReviewingPeriod)
        {
            //DateTime.Now = 31-03-2020
            //25-03-2020
            var tenderAwardingDate = this.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).CreatedAt;
            //25-03-2020 + 5 Days = 30-03-2020
            var awardingDateWithStopPeriod = tenderAwardingDate.AddDays(this.AwardingStoppingPeriod.Value);
            //25-03-2020 + 5 Days + 3 Days = 02-04-2020
            var awardingDateWithEsclationPeriod = tenderAwardingDate.AddDays(this.AwardingStoppingPeriod.Value + esclationPeriod);
            var awardingDateWithPlaintPeriod = tenderAwardingDate.AddDays(this.AwardingStoppingPeriod.Value + plaintReviewingPeriod);
            var result =
               (DateTime.Now > awardingDateWithStopPeriod && (
               !this.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
               || this.AgencyCommunicationRequests.Any(a => a.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Accepted && a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved)
               || this.AgencyCommunicationRequests.Any(a => a.EscalationAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Accepted && a.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.ApprovedByPlaintManager)
               ))

               || this.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
               && a.PlaintRequests.Any(p => !p.IsEscalation
                     && (p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.New || p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Pending)
                     && awardingDateWithPlaintPeriod < DateTime.Now
                     ))

               || this.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
               && a.PlaintRequests.Any(p => !p.IsEscalation
                    && p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Rejected && p.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved
                    && (awardingDateWithEsclationPeriod.AddDays((p.AgencyCommunicationRequest.UpdatedAt.Value.Date - awardingDateWithStopPeriod).TotalDays) < DateTime.Now
                    )))

               || this.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
               && (a.PlaintRequests.Any(p => p.IsEscalation
                    && p.AgencyCommunicationRequest.EscalationAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Rejected && p.AgencyCommunicationRequest.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.ApprovedByPlaintManager
                    || ((p.AgencyCommunicationRequest.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending || p.AgencyCommunicationRequest.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent) &&
               (tenderAwardingDate.AddDays(this.AwardingStoppingPeriod.Value + ((p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Pending || p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.New) ? plaintReviewingPeriod : (p.AgencyCommunicationRequest.UpdatedAt.Value.Date - awardingDateWithStopPeriod).TotalDays) + esclationPeriod + plaintReviewingPeriod)) < DateTime.Now))
               ));
            return result;
        }

        public void DeleteAllCancelRequest()
        {
            this.ChangeRequests.ForEach(c => c.DeActive());
        }
        public void SetTenderType(int tenderTypeId)
        {
            TenderTypeId = tenderTypeId;
        }
        public Tender SetTenderDates(TenderDates tenderDates)
        {
            TenderDates = tenderDates;
            EntityUpdated();
            return this;
        }
        public Tender SetTenderAddress(TenderAddress tenderAddress)
        {
            TenderAddress = tenderAddress;
            EntityUpdated();
            return this;
        }
        public void AddActivityVersion(int activityVersionId)
        {
            TenderVersions.Add(new TenderVersion(activityVersionId));
            EntityUpdated();
        }

        public void AddIsTenderContainsTawreedTables_ForTest(bool isTenderContainsTawreedTables)
        {
            IsTenderContainsTawreedTables = isTenderContainsTawreedTables;

        }


    }
}
