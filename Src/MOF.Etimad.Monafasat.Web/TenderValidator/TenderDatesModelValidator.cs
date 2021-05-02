using FluentValidation;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.Web.TenderValidator
{
   public class TenderDatesModelValidator : AbstractValidator<TenderDatesModel>
   {
      public TenderDatesModelValidator()
      {

         RuleFor(t => t.InitialGuaranteeAddress)
           .NotEmpty()
           .When(x => x.NeedInitialGuarantee == true && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
           .WithMessage(Resources.TenderResources.Messages.EnterInitialGuranteePresentationAddress);

         RuleFor(t => t.InitialGuaranteePercentage)
           .NotEmpty()
           .When(x => x.NeedInitialGuarantee == true && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
           .WithMessage(Resources.TenderResources.Messages.EnterInitialGuranteeValue);
         RuleFor(t => t.InitialGuaranteePercentage)
           .InclusiveBetween(1, 2)
           .When(x => x.NeedInitialGuarantee && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
           .WithMessage(Resources.TenderResources.Messages.WrongInitialGuranteePercent);

         RuleFor(t => t.FinalGuaranteePercentage)
           .NotEmpty()
           .When(x => x.VersionId >= 4 && !x.IsOldTender && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.Competition)
           .WithMessage(Resources.TenderResources.ErrorMessages.FinalGuaranteePercentageRequired);

         RuleFor(t => t.FinalGuaranteePercentage)
           .InclusiveBetween(5, 100)
           .When(x => x.VersionId >= 4 && !x.IsOldTender && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.Competition)
           .WithMessage(Resources.TenderResources.ErrorMessages.FinalGuaranteeNotValidValue);

         RuleFor(t => t.AwardingStoppingPeriod)
         .NotEmpty()
         .When(x => x.VersionId >= 4 && !x.IsOldTender && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.Competition)
         .WithMessage(Resources.TenderResources.ErrorMessages.StoppingPeriodRequired);

         RuleFor(t => t.AwardingStoppingPeriod)
         .InclusiveBetween(5, 10)
         .When(x => x.VersionId >= 4 && !x.IsOldTender && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.Competition)
         .WithMessage(Resources.TenderResources.DisplayInputs.AwardingStoppingMustBeEqualOrGreaterThen5);

         RuleFor(t => t.SamplesDeliveryAddress)
           .NotEmpty()
           .When(x => (x.SupplierNeedSample == true || x.SupplierNeedSample.ToString().ToLower() == "true") && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
           .WithMessage(Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress);

         RuleFor(t => t.OffersOpeningAddressId)
           .NotNull()
           .When(t => t.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.FirstStageTender && t.TenderTypeId != (int)Enums.TenderType.Competition && t.OffersOpeningAddress == null)
           .WithMessage(Resources.TenderResources.DisplayInputs.PleaseEnterOpenLocation);

        // RuleFor(t => t.AwardingStoppingPeriod)
        //.NotEmpty()
        //.When(x => x.VersionId >= 4 && !x.IsOldTender && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.Competition)
        //.WithMessage(Resources.TenderResources.ErrorMessages.StoppingPeriodRequired);

      }
   }
}
