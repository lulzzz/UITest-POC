using FluentValidation;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.Web.TenderValidator
{
   public class CreateAnnouncementModelValidator : AbstractValidator<CreateAnnouncementModel>
   {
      public CreateAnnouncementModelValidator()
      {
         RuleFor(t => t.AnnouncementPeriod)
              .InclusiveBetween(10, 999)
              .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
              .WithMessage(Resources.AnnouncementResources.ErrorMessages.AnnouncementPeriodRangeForDirectPurchase);
         RuleFor(t => t.AnnouncementPeriod)
              .InclusiveBetween(20, 999)
              .When(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender)
              .WithMessage(Resources.AnnouncementResources.ErrorMessages.AnnouncementPeriodRangeForLimitedTender);

      }
   }
}
