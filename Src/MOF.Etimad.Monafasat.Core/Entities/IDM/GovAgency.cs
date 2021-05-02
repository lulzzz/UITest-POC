using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("GovAgency", Schema = "IDM")]
    public class GovAgency : AuditableEntity
    {
        [StringLength(20)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AgencyCode { get; private set; }

        [StringLength(256)]
        public string NameArabic { get; private set; }

        [StringLength(256)]
        public string NameEnglish { get; private set; }


        public int? CategoryId { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsPrimary { get; private set; }

        public bool IsUGP { get; private set; }
        public bool IsVRO { get; private set; }

        [DefaultValue("1,2,4")]//,Required]
        public string PurchaseMethods { get; private set; }

        public byte[] RowVersion { get; private set; }

        public int? AgencyLogoReferenceId { get; set; }

        public ICollection<Branch> Branches { get; set; }
        public ICollection<Committee> Committees { get; set; }

        public ICollection<Tender> Tenders { get; set; }

        public bool IsOldSystem { get; private set; }
        public bool IsExcepted { get; private set; }

        [StringLength(256)]
        public string MobileNumber { get; private set; }

        [ForeignKey(nameof(VROOfficeCodeRelated))]
        public string VROOfficeCode { get; private set; }
        public virtual GovAgency VROOfficeCodeRelated { get; private set; }

        public GovAgency()
        {
        }

        public GovAgency(string agencyCode, string arabicName, int? agencyLogoReferenceId, bool isVro, int categoryId, string mobileNumber, string vrOfficeCode = null)
        {
            AgencyCode = agencyCode;
            NameArabic = arabicName;
            IsVRO = isVro;
            VROOfficeCode = vrOfficeCode;
            CategoryId = categoryId;
            MobileNumber = mobileNumber;
            if (agencyLogoReferenceId.HasValue)
                AgencyLogoReferenceId = agencyLogoReferenceId;
            if (isVro)
            {
                PurchaseMethods = Convert.ToString((int)Enums.TenderType.NationalTransformationProjects);
            }
            else
            {
                PurchaseMethods = Convert.ToString((int)Enums.TenderType.NewTender + "," + (int)Enums.TenderType.NewDirectPurchase + "," + (int)Enums.TenderType.LimitedTender);
                if (!string.IsNullOrEmpty(vrOfficeCode))
                {
                    PurchaseMethods += "," + Convert.ToString((int)Enums.TenderType.NationalTransformationProjects);
                }
            }
            EntityCreated();
        }

        public void GovAgencyUpdated(string agencyCode, string vrOfficeCode, string arabicName, int categoryId, string mobileNumber)
        {
            AgencyCode = agencyCode;
            VROOfficeCode = vrOfficeCode;
            NameArabic = arabicName;
            CategoryId = categoryId;
            MobileNumber = mobileNumber;
            bool flagiscontainsVRo = PurchaseMethods.Contains(Convert.ToString((int)Enums.TenderType.NationalTransformationProjects));
            if (string.IsNullOrEmpty(vrOfficeCode) && flagiscontainsVRo)
            {
                //remove 
                ArrayList PurchaseMethodsArr = new ArrayList(PurchaseMethods.Split(new char[] { ',' }));
                PurchaseMethodsArr.Remove(Convert.ToString((int)Enums.TenderType.NationalTransformationProjects));
                PurchaseMethods = String.Join(",", PurchaseMethodsArr.ToArray().Select(p => p.ToString()).ToArray());
            }
            else if (!string.IsNullOrEmpty(vrOfficeCode) && !flagiscontainsVRo)
            {
                PurchaseMethods += "," + Convert.ToString((int)Enums.TenderType.NationalTransformationProjects);
            }
            EntityUpdated();
        }
        public void SetExcepted(bool excepted)
        {
            IsExcepted = excepted;
            EntityUpdated();
        }
        public void SetIsOld(bool isOld)
        {
            IsOldSystem = isOld;
            EntityUpdated();
        }
        public void SetPurchaseMethod(string methods)
        {
            PurchaseMethods = methods;
            EntityUpdated();
        }
    }
}
