using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class QualificationSupplierProjectModel : AuditableEntity
    {

        public int ID { get; set; }
        public int TenderId { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        public string EndDateStr { get; set; }
        public string StartDateStr { get; set; }
        public string ContractName { get; set; }
        public string Description { get; set; }
        public string PerformanceEvaluation { get; set; }
        public decimal ContractValue { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SupplierSelectedCr { get; set; }

        public string EndDateHijri { get; set; }

        public string StartDateHijri { get; set; }

        public void Created()
        {
            EntityCreated();
        }


    }
}
