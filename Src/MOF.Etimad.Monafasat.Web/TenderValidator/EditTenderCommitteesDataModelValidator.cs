using FluentValidation;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.Web.TenderValidator
{
   public class EditTenderCommitteesDataModelValidator : AbstractValidator<EditeCommitteesModel>
   {
      public EditTenderCommitteesDataModelValidator()
      {
         RuleFor(t => t.DirectPurchaseCommitteeMemberId)
        .NotNull().NotEmpty()
        .When(t => t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && t.IsLowBudgetDirectPurchase == true)
        .WithMessage(Resources.TenderResources.DisplayInputs.SelectDirectPurchaseCommitteeMember);
      }
   }
}
