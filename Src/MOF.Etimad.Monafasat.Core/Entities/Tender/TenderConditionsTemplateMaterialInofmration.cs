using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConditionsTemplateMaterialsInofmration", Schema = "Tender")]
    public class TenderConditionsTemplateMaterialInofmration : AuditableEntity
    {

        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderConditionsTemplateMaterialInofmrationId { get; private set; }

        public TenderConditionsTemplate TenderConditionsTemplate { get; private set; }

        #region SecondSection

        //[Required]
        public string BasicInformation { get; private set; }

        //[Required] 
        public string RequiredDcoumentationBefore { private set; get; }

        //[Required]
        public string Tests { private set; get; }

        public string IntilizationAndStartWork { private set; get; }

        //[Required]
        public string RequiredDcoumentationAfter { private set; get; }

        //[Required] 
        public string Trainging { private set; get; }

        //[Required] 
        public string Guarantee { private set; get; }

        public string Maintanance { private set; get; }

        public string MachineGuarantee { private set; get; }
        public string MachineMaintanance { private set; get; }

        #endregion SecondSection 


        #endregion

        #region Constructors====================================================

        public TenderConditionsTemplateMaterialInofmration()
        {

        }
        public TenderConditionsTemplateMaterialInofmration(string basicInformation, string requiredDcoumentationBefore, string tests,
            string intilizationAndStartWork, string requiredDcoumentationAfter, string trainging, string guarantee, string maintanance, string machineGuarantee,
            string machineMaintanance)
        {
            BasicInformation = basicInformation;
            RequiredDcoumentationBefore = requiredDcoumentationBefore;
            Tests = tests;
            IntilizationAndStartWork = intilizationAndStartWork;
            RequiredDcoumentationAfter = requiredDcoumentationAfter;
            Trainging = trainging;
            Guarantee = guarantee;
            Maintanance = maintanance;
            MachineGuarantee = machineGuarantee;
            MachineMaintanance = machineMaintanance;
            EntityCreated();
        }
        #endregion

        #region Methods==================================================== 
        public void Update(string basicInformation, string requiredDcoumentationBefore, string tests,
            string intilizationAndStartWork, string requiredDcoumentationAfter, string trainging, string guarantee, string maintanance, string machineGuarantee,
            string machineMaintanance)
        {
            BasicInformation = basicInformation;
            RequiredDcoumentationBefore = requiredDcoumentationBefore;
            Tests = tests;
            IntilizationAndStartWork = intilizationAndStartWork;
            RequiredDcoumentationAfter = requiredDcoumentationAfter;
            Trainging = trainging;
            Guarantee = guarantee;
            Maintanance = maintanance;
            MachineGuarantee = machineGuarantee;
            MachineMaintanance = machineMaintanance;
            EntityUpdated();
        }

        public void SetAddedState()
        {
            TenderConditionsTemplateMaterialInofmrationId = 0;
            EntityCreated();
        }
        #endregion
    }
}