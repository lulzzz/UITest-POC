using FluentValidation;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.Web.TenderValidator
{
   public class CreateTenderBasicDataModelValidator : AbstractValidator<CreateTenderBasicDataModel>
   {
      public CreateTenderBasicDataModelValidator()
      {
         RuleFor(t => t.EstimatedValue.ToString())
              .Length(0, 15)
              .WithMessage(Resources.TenderResources.DisplayInputs.PleaseEnterRealValue);
         RuleFor(t => t.EstimatedValue)
              .NotNull()
              .When(t => t.TenderTypeId != (int)Enums.TenderType.FirstStageTender && t.TenderTypeId != (int)Enums.TenderType.CurrentTender && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && !(t.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && t.IsVRO))
              .WithMessage(Resources.TenderResources.DisplayInputs.PleaseEnterRealValue);
         RuleFor(t => t.EstimatedValue)
            .InclusiveBetween(0, 5000000)
            .When(t => t.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            .WithMessage(Resources.TenderResources.Messages.MaximumValueIsFiveMillions);

         RuleFor(t => t.EstimatedValue)
            .InclusiveBetween(0, 100000)
            .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && t.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.BusinessesAndPurchasesLessThanOneHundredThousand)
            .WithMessage(Resources.TenderResources.ErrorMessages.EstimationValueShouldNotBrGreaterThan100000);

         RuleFor(t => t.EstimatedValue)
            .ExclusiveBetween(0, 999999999999.99m)
            .When(t => t.TenderTypeId != (int)Enums.TenderType.FirstStageTender && t.TenderTypeId != (int)Enums.TenderType.CurrentTender && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
            .WithMessage(Resources.SharedResources.ErrorMessages.InvalidValue);

         RuleFor(t => t.EstimatedValue)
            .ExclusiveBetween(0, 30000.01m)
            .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && t.IsLowBudgetDirectPurchase.Value)
            .WithMessage(Resources.TenderResources.DisplayInputs.LowBudgetError);

         RuleFor(t => t.PreQualificationId)
          .NotEmpty()
          .When(x => x.HasQualification)
          .WithMessage(Resources.TenderResources.DisplayInputs.RequiredPrequalification);


         RuleFor(t => t.PreAnnouncementId)
             .NotEmpty()
             .When(x => x.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative && x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
             .WithMessage(Resources.TenderResources.DisplayInputs.SelectPreAnnouncement);

         RuleFor(t => t.SamplesDeliveryAddress)
          .NotEmpty()
          .When(x => (x.SupplierNeedSample == true || x.SupplierNeedSample.ToString().ToLower() == "true") && x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && x.IsAgencyRelatedByVRO)
          .WithMessage(Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress);

         RuleFor(t => t.OfferPresentationWayId)
           .Equal((int)Enums.OfferPresentationWayId.TwoSepratedFiles)
           .When(x => x.EstimatedValue.HasValue && x.EstimatedValue >= (int)Enums.ConitionalBookletPriceList.MoreThanFifeMilion && x.TenderTypeId != (int)Enums.TenderType.ReverseBidding && x.TenderTypeId != (int)Enums.TenderType.CurrentTender && x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.TenderTypeId != (int)Enums.TenderType.Competition && x.TenderTypeId != (int)Enums.TenderType.FirstStageTender)
           .WithMessage(Resources.TenderResources.DisplayInputs.RequiredTwoFilesMoreThanFiveMillion);


         RuleFor(t => t.TenderTypeId)
            .NotEmpty()
            .WithMessage(Resources.TenderResources.DisplayInputs.SelectTenderType);
         RuleFor(t => t.ReasonForPurchaseTenderTypeId)
            .NotEmpty()
            .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            .WithMessage(Resources.TenderResources.DisplayInputs.RequiredTenderTypeSelectionReason);

         RuleFor(t => t.ReasonForLimitedTenderTypeId)
            .NotEmpty()
            .When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender)
            .WithMessage(Resources.TenderResources.DisplayInputs.RequiredTenderTypeSelectionReason);

         RuleFor(t => t.TenderAgreementAgencyIDs)
            .NotEmpty()
            .When(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.IsUnitAgency)
            .WithMessage(Resources.TenderResources.DisplayInputs.EnterAgreementAgencies);

         RuleFor(t => t.TenderTypeOtherSelectionReason)
           .NotEmpty()
           .When(t => (t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && t.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.Other) || (t.TenderTypeId == (int)Enums.TenderType.LimitedTender && t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.Other))
           .WithMessage(Resources.TenderResources.DisplayInputs.RequiredOtherReason);

         RuleFor(t => t.ConditionsBookletPrice)
           .NotEmpty()
           .When(t => t.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.Competition && t.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
           .WithMessage(Resources.TenderResources.ErrorMessages.EnterConditionBook);


         RuleFor(t => t.TechnicalOrganizationId)
            .NotNull()
            .When(t => t.IsAgancyHasTechnicalCommittee == true && !(t.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && t.IsAgencyRelatedByVRO) /*&& t.BranchHasTechnicalCommittees*/)
            .WithMessage(Resources.TenderResources.ErrorMessages.TechnicalOrganizationRequired);

         RuleFor(t => t.DirectPurchaseCommitteeId)
           .NotEmpty()
           .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && (!t.IsLowBudgetDirectPurchase.HasValue || t.IsLowBudgetDirectPurchase == false))
           .WithMessage(Resources.TenderResources.DisplayInputs.SelectDirectPurchaseCommittee);

         RuleFor(t => t.DirectPurchaseCommitteeMemberId)
        .NotEmpty()
        .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && t.IsLowBudgetDirectPurchase == true)
        .WithMessage(Resources.TenderResources.DisplayInputs.SelectDirectPurchaseCommitteeMember);

         RuleFor(t => t.AgreementMonthes)
           .NotNull()
           .When(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
           .WithMessage(Resources.TenderResources.DisplayInputs.EnterAgreementMonthes);
         RuleFor(t => t.AgreementDays)
              .NotNull()
              .When(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
              .WithMessage(Resources.TenderResources.DisplayInputs.EnterAgreementDays);
         RuleFor(t => t.AgreementYears)
           .NotNull()
           .When(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
           .WithMessage(Resources.TenderResources.DisplayInputs.EnterAgreementYears);


         //RuleFor(t => t.IsLinkedToAnnouncement)
         //.Equal(true)
         //.When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender
         //&& t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers
         //&& t.PreAnnouncementId == null
         //&& t.AnnouncementTemplateId == null
         //)
         //.WithMessage(Resources.AnnouncementSupplierTemplateResources.DisplayInputs.HaveToSelectAnnouncement);

         RuleFor(t => t.AnnouncementTemplateId)
           .NotNull()
           .When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender &&
           t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers
           && t.PreAnnouncementId == null && !t.IsLinkedToAnnouncement
           )
           .WithMessage(Resources.AnnouncementSupplierTemplateResources.DisplayInputs.RequiredAnnouncementTemplteWhenReasonLimtedNumber);

         RuleFor(t => t.PreAnnouncementId)
         .NotNull()
         .When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender &&
         t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers
         && t.AnnouncementTemplateId == null && t.IsLinkedToAnnouncement
         )
         .WithMessage(Resources.AnnouncementSupplierTemplateResources.DisplayInputs.RequiredAnnouncementWhenReasonLimtedNumber);

         RuleFor(t => t.PreAnnouncementId)
     .Null()
     .When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender &&
     t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers
     && t.PreAnnouncementId != null && t.AnnouncementTemplateId != null && t.IsLinkedToAnnouncement
     )
     .WithMessage(Resources.AnnouncementSupplierTemplateResources.DisplayInputs.RequiredOneAnnouncementWhenReasonLimtedNumber);



      }
   }
}
