using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Reflection;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class QualificationDefault
    {
        public Tender GetPreQualification()
        {
            Tender generalTender = new Tender("022001000000", 1, (int) Enums.TenderType.PreQualification,
                (int) Enums.InvitationType.Public, "Tender name test", "tender number",
                "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, 1, 1, null, null,
                null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2),
                DateTime.Now.Date.AddDays(3), DateTime.Now, null, null, false, null, 1, "building name", "Floar number",
                "Department Number", null);
            generalTender.SetPointToPassToPostQualification(4, 40, 60, (int) Enums.PreQualificationType.Large);
            generalTender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", 11, Enums.TenderActions.Approve);
            return generalTender;
        }

        public Tender GetQualificationWithConfiguration()
        {
            Tender qualification = GetPreQualification();
            qualification.QualificationConfigurations.Add(GetQualificationConfiguration(7));
            qualification.QualificationSubCategoryConfigurations.Add(GetQualificationSubCategoryConfiguration(7));
            qualification.QualificationCategoryResults.Add(GetQualificationCategoryResult(7));
            qualification.QualificationSubCategoryResults.Add(GetQualificationSubCategoryResult(7));
            qualification.QualificationFinalResults.Add(GetQualificationFinalResult(7));
            qualification.PreQualificationApplyDocuments.Add(GetSupplierPreQualificationDocument());
            return qualification;
        }
        public Tender GetQualificationWithWithFinalPointsAndSupplier()
        {
            Tender qualification = GetPreQualification();
            qualification.QualificationFinalResults.Add(GetQualificationFinalResult(7));
            return qualification;
        }

        public Tender GetPostQualificationWithWithTenderObject()
        {
            Tender postQualification = new Tender("022001000000", 1, (int) Enums.TenderType.PostQualification,
                (int) Enums.InvitationType.Public, "Tender name test", "tender number",
                "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, 1, 1, null, null,
                null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500, null);


            var tender = new Tender("022001000000", 1, (int)Enums.TenderType.NewDirectPurchase, (int)Enums.InvitationType.Specific, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, null, null, 1, null,
                1, null, null, null, null, 1, 1000, null, 1,
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500);


            PropertyInfo propertyInfo = postQualification.GetType().GetProperty("PostQualificationTender");
            propertyInfo.SetValue(postQualification,tender);
            postQualification.PostQualificationInvitations.Add(new PostQualificationSuppliersInvitations("1010000154"));
            return postQualification;
        }

        public List<Tender> GetPostQualifications()
        {
            return new List<Tender>()
            {
                GetPostQualificationWithWithTenderObject()
            };
        }

        public QualificationConfiguration GetQualificationConfiguration(int tenderId)
        {
            QualificationConfiguration qualificationConfiguration = new QualificationConfiguration(0, tenderId, 1, 10, 50, 20);
            return qualificationConfiguration;
        }
        public QualificationSubCategoryConfiguration GetQualificationSubCategoryConfiguration(int tenderId)
        {
            QualificationSubCategoryConfiguration qualificationConfiguration = new QualificationSubCategoryConfiguration(tenderId, 1, 20);
            return qualificationConfiguration;
        }
        public QualificationCategoryResult GetQualificationCategoryResult(int tenderId)
        {
            QualificationCategoryResult qualificationConfiguration = new QualificationCategoryResult(0, 1, tenderId, "1010000154", 7, 20, 20);
            return qualificationConfiguration;
        }

        public QualificationSubCategoryResult GetQualificationSubCategoryResult(int tenderId)
        {
            QualificationSubCategoryResult qualificationConfiguration = new QualificationSubCategoryResult(0, 1, tenderId, "1010052847", 3, 30);
            return qualificationConfiguration;
        }

        public QualificationFinalResult GetQualificationFinalResult(int tenderId)
        {
            QualificationFinalResult qualificationConfiguration = new QualificationFinalResult(tenderId, "1010000154", 4, 14);

            Supplier supplier = new Supplier("1010000154", "Test",new List<UserNotificationSetting>());
            var proprtyInfo = qualificationConfiguration.GetType().GetProperty("Supplier");
            proprtyInfo.SetValue(qualificationConfiguration,supplier);

            return qualificationConfiguration;
        }
        public QualificationLookup GetQualificationLookup()
        {
            QualificationLookup qualificationConfiguration = new QualificationLookup();
            return qualificationConfiguration;
        }

        public PreQualificationApplyDocumentsModel GetApplyDocumentsModelSmallQulaification()
        {
            PreQualificationApplyDocumentsModel applyDocumentsModel = new PreQualificationApplyDocumentsModel()
            {
                QualificationTypeId = (int) Enums.PreQualificationType.Small,
                SupplierId = "5956275283",
                PreQualificationIdString = Util.Encrypt(1),
                QualificationStatusId = 1,
                lstQualificationSupplierDataModel = GetListOfSmallQualificationTechDataModel(),
                preQualificationSupplierAttachmentModels = new List<PreQualificationSupplierAttachmentModel>
                {
                    new PreQualificationSupplierAttachmentModel
                        {FileName = "Test File", FileNetReferenceId = "ddv122122"}
                },
            };

            return applyDocumentsModel;
        }

        private List<QualificationSupplierDataModel> GetListOfSmallQualificationTechDataModel()
        {
            List<QualificationSupplierDataModel> qualificationSupplierDataList =
                new List<QualificationSupplierDataModel>();

            QualificationSupplierDataModel numberOfYearsOfExceprience = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience);
            QualificationSupplierDataModel percentsaudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees);
            QualificationSupplierDataModel employeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfEmployees);
            QualificationSupplierDataModel saudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees);
            QualificationSupplierDataModel cashRateModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.CashRate);
            QualificationSupplierDataModel liquidityRatioModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Small, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.LiquidityRatio);

            qualificationSupplierDataList.Add(numberOfYearsOfExceprience);
            qualificationSupplierDataList.Add(percentsaudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(employeeNumber);
            qualificationSupplierDataList.Add(saudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(cashRateModel);
            qualificationSupplierDataList.Add(liquidityRatioModel);
            return qualificationSupplierDataList;

        }

        public Tender GetGeneralTenderWithQualificationTypeId(int qualificationTypeId)
        {
            Tender tender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, 1, 1, null, null,
                null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500, null);
            tender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
           tender.SetPointToPassToQualification(6,40,60,qualificationTypeId);
            return tender;
        }

        public PreQualificationApplyDocumentsModel GetApplyDocumentsModelMediumQulaification()
        {
            PreQualificationApplyDocumentsModel applyDocumentsModel = new PreQualificationApplyDocumentsModel()
            {
                QualificationTypeId = (int)Enums.PreQualificationType.Medium,
                SupplierId = "5956275283",
                PreQualificationIdString = Util.Encrypt(1),
                QualificationStatusId = 1,
                lstQualificationSupplierDataModel = GetListOfMediumQualificationTechDataModel(),
                preQualificationSupplierAttachmentModels = new List<PreQualificationSupplierAttachmentModel>
                {
                    new PreQualificationSupplierAttachmentModel
                        {FileName = "Test File", FileNetReferenceId = "ddv122122"}
                },
                QualityAssuranceStandardsAttachmentModels = new List<QualificationAttachmentModel>(){new QualificationAttachmentModel(){AttachmentId = 1,FileName = "323",FileNetReferenceId = "32",SupPreQAttachmentId = 2}},
                EnvironmentalHealthSafetyStandardsAttachmentModels = new List<QualificationAttachmentModel>() { new QualificationAttachmentModel() { FileName = "Name",AttachmentId = 1,FileNetReferenceId = "refere",SupPreQAttachmentId = 1} }
            };

            return applyDocumentsModel;
        }

        public PreQualificationApplyDocumentsModel GetApplyDocumentsModelLargeQulaification()
        {
            PreQualificationApplyDocumentsModel applyDocumentsModel = new PreQualificationApplyDocumentsModel()
            {
                QualificationTypeId = (int)Enums.PreQualificationType.Large,
                SupplierId = "1010000154",
                PreQualificationIdString = Util.Encrypt(1),
                QualificationStatusId = 1,
                lstQualificationSupplierDataModel = GetListOfLargeQualificationTechDataModel(),
                preQualificationSupplierAttachmentModels = new List<PreQualificationSupplierAttachmentModel>
                {
                    new PreQualificationSupplierAttachmentModel
                        {FileName = "Test File", FileNetReferenceId = "ddv122122"}
                },
                lstQualificationSupplierProjectModel = new List<QualificationSupplierProjectModel>(){new QualificationSupplierProjectModel(){TenderId = 1,ContractName = "Con Name",ContractValue = 1000,Description = "Des",OwnerName = "Owner Name",StartDate = DateTime.Now,EndDate = DateTime.Now.AddDays(1)}},
                QualityAssuranceStandardsAttachmentModels = new List<QualificationAttachmentModel>() { new QualificationAttachmentModel() { AttachmentId = 1, FileName = "323", FileNetReferenceId = "32", SupPreQAttachmentId = 2 } },
                EnvironmentalHealthSafetyStandardsAttachmentModels = new List<QualificationAttachmentModel>() { new QualificationAttachmentModel() { FileName = "Name", AttachmentId = 1, FileNetReferenceId = "refere", SupPreQAttachmentId = 1 } }
            };

            return applyDocumentsModel;
        }

        private List<QualificationSupplierDataModel> GetListOfMediumQualificationTechDataModel()
        {
            List<QualificationSupplierDataModel> qualificationSupplierDataList =
                new List<QualificationSupplierDataModel>();

            QualificationSupplierDataModel numberOfYearsOfExceprience = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience,0);
            QualificationSupplierDataModel percentsaudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees,0);
            QualificationSupplierDataModel employeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfEmployees,0);
            QualificationSupplierDataModel saudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees,0);
            QualificationSupplierDataModel qualityAssuranceStandards = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards, (int)Enums.QualificationQualityGuaranteeLookup.IsoCertificate);
            QualificationSupplierDataModel environmentalHealthSafetyStandards = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards, (int)Enums.QualificationEnvironmentStandardsLookup.OSHA);

            QualificationSupplierDataModel cashRateModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.CashRate,0);
            QualificationSupplierDataModel liquidityRatioModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.LiquidityRatio,0);
            QualificationSupplierDataModel tradingRatio = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Medium, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.TradingRatio, 0);

            qualificationSupplierDataList.Add(numberOfYearsOfExceprience);
            qualificationSupplierDataList.Add(percentsaudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(employeeNumber);
            qualificationSupplierDataList.Add(saudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(qualityAssuranceStandards);
            qualificationSupplierDataList.Add(environmentalHealthSafetyStandards);

            qualificationSupplierDataList.Add(cashRateModel);
            qualificationSupplierDataList.Add(liquidityRatioModel);
            qualificationSupplierDataList.Add(tradingRatio);
            
            return qualificationSupplierDataList;

        }

        private List<QualificationSupplierDataModel> GetListOfLargeQualificationTechDataModel()
        {
            List<QualificationSupplierDataModel> qualificationSupplierDataList =
                new List<QualificationSupplierDataModel>();

            QualificationSupplierDataModel numberOfYearsOfExceprience = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience, 0, (int)Enums.QualificationItemType.Percentage);
            QualificationSupplierDataModel percentsaudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees, 0, (int)Enums.QualificationItemType.Range);
            QualificationSupplierDataModel employeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfEmployees, 0,(int)Enums.QualificationItemType.Percentage);
            QualificationSupplierDataModel saudiEmoloyeeNumber = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees, 0, (int)Enums.QualificationItemType.Value);
            QualificationSupplierDataModel qualityAssuranceStandards = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards, (int)Enums.QualificationQualityGuaranteeLookup.IsoCertificate, (int)Enums.QualificationItemType.Value);
            QualificationSupplierDataModel environmentalHealthSafetyStandards = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards, (int)Enums.QualificationEnvironmentStandardsLookup.OSHA);
            QualificationSupplierDataModel numberOfProjectsImplementedLastThreeYears = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears, 0);
            QualificationSupplierDataModel totalValueProjectsLastThreeYears = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears, 0);
            QualificationSupplierDataModel insurance = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Technical, (int)Enums.QualificationEvaluationItems.Insurance, 0);

            QualificationSupplierDataModel cashRateModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.CashRate, 0);
            QualificationSupplierDataModel liquidityRatioModel = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.LiquidityRatio, 0, (int)Enums.QualificationItemType.Range);
            QualificationSupplierDataModel tradingRatio = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.TradingRatio, 0, (int)Enums.QualificationItemType.Percentage);
            QualificationSupplierDataModel ratioOfObligations = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.RatioOfObligations, 0);
            QualificationSupplierDataModel rateOfProfitability = GetQualificationSupplierItemData((int)Enums.PreQualificationType.Large, (int)Enums.QualificationItemCategory.Financial, (int)Enums.QualificationEvaluationItems.RateOfProfitability, 0);

            qualificationSupplierDataList.Add(numberOfYearsOfExceprience);
            qualificationSupplierDataList.Add(percentsaudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(employeeNumber);
            qualificationSupplierDataList.Add(saudiEmoloyeeNumber);
            qualificationSupplierDataList.Add(qualityAssuranceStandards);
            qualificationSupplierDataList.Add(environmentalHealthSafetyStandards);
            qualificationSupplierDataList.Add(numberOfProjectsImplementedLastThreeYears);
            qualificationSupplierDataList.Add(totalValueProjectsLastThreeYears);
            qualificationSupplierDataList.Add(insurance);

            qualificationSupplierDataList.Add(cashRateModel);
            qualificationSupplierDataList.Add(liquidityRatioModel);
            qualificationSupplierDataList.Add(tradingRatio);
            qualificationSupplierDataList.Add(rateOfProfitability);
            qualificationSupplierDataList.Add(ratioOfObligations);

            return qualificationSupplierDataList;

        }


        #region  Qualification-Items

        public QualificationSupplierDataModel GetQualificationSupplierItemData(int QTypeId, int categoryId, int itemId,int lookupId = 0,int qualificationItemTypeId = 2)
        {
            QualificationSupplierDataModel model = new QualificationSupplierDataModel()
            {
                QualificationTypeId = QTypeId,
                QualificationStatusId = 1,
                QualificationCategoryId = categoryId,
                IsConfigure = true,
                QualificationItemId = itemId,
                Min = 5,
                Max = 10,
                SupplierValue = 5,
                QualificationLookupId = lookupId,
                QualificationItemTypeId = qualificationItemTypeId
            };
            return model;
        }

        #endregion

        public List<QualificationTypeCategory> GetQualificationTypeCategory()
        {
            QualificationSubCategory qualificationSubCategory = new QualificationSubCategory();
            PropertyInfo qualificationCategoryIdInfo = qualificationSubCategory.GetType().GetProperty("QualificationCategoryId");
            qualificationCategoryIdInfo.SetValue(qualificationSubCategory, Convert.ChangeType((int)Enums.QualificationItemCategory.Technical, qualificationCategoryIdInfo.PropertyType), null);

            List<QualificationTypeCategory> qualificationTypeList = new List<QualificationTypeCategory>();
            QualificationTypeCategory qualificationType = new QualificationTypeCategory();
            
            PropertyInfo propertyInfo = qualificationType.GetType().GetProperty("QualificationSubCategory");
            propertyInfo.SetValue(qualificationType, Convert.ChangeType(qualificationSubCategory, propertyInfo.PropertyType), null);
            
            PropertyInfo qualificationSubCategoryIdInfo = qualificationType.GetType().GetProperty("QualificationSubCategoryId");
            qualificationSubCategoryIdInfo.SetValue(qualificationType, 1, null);

            qualificationTypeList.Add(qualificationType);


            // Financial
            QualificationSubCategory financialQualificationSubCategory = new QualificationSubCategory();

            PropertyInfo finqualificationCategoryIdInfo = financialQualificationSubCategory.GetType().GetProperty("QualificationCategoryId");
            finqualificationCategoryIdInfo.SetValue(financialQualificationSubCategory, Convert.ChangeType((int)Enums.QualificationItemCategory.Financial, finqualificationCategoryIdInfo.PropertyType), null);

            QualificationTypeCategory finQualificationType = new QualificationTypeCategory();

            PropertyInfo finPropertyInfo = finQualificationType.GetType().GetProperty("QualificationSubCategory");
            finPropertyInfo.SetValue(finQualificationType, Convert.ChangeType(financialQualificationSubCategory, finPropertyInfo.PropertyType), null);

            PropertyInfo finQqualificationSubCategoryIdInfo = finQualificationType.GetType().GetProperty("QualificationSubCategoryId");
            finQqualificationSubCategoryIdInfo.SetValue(finQualificationType, 1, null);

            qualificationTypeList.Add(finQualificationType);


            return qualificationTypeList;
        } 
        
        public List<QualificationSubCategoryConfiguration> GeQualificationSubCategoryConfigurations()
        {
            List<QualificationSubCategoryConfiguration> list = new List<QualificationSubCategoryConfiguration>();
            list.Add(GetQualificationSubCategoryConfiguration(1));

            return list;
        }

        public SupplierPreQualificationDocument GetSupplierPreQualificationDocument()
        {
            SupplierPreQualificationDocument supplierPreQualification =
                new SupplierPreQualificationDocument("1010000154", 4, 1, 0, true);
            
            supplierPreQualification.supplierPreQualificationAttachments.Add(
                new SupplierPreQualificationAttachment("File Name", "ddd1", 1));
            var preQualification = GetPreQualification();

            PropertyInfo propertyInfo = supplierPreQualification.GetType().GetProperty("PreQualification");
            propertyInfo.SetValue(supplierPreQualification, Convert.ChangeType(preQualification, propertyInfo.PropertyType), null);


            supplierPreQualification.PreQualification.UpdateTenderCommittee((int) Enums.TenderStatus.Approved,
                (int) Enums.CommitteeType.PreQualificationCommittee, 0, 111);
           
            return supplierPreQualification;
        }

        public List<QualificationSupplierDataYearly> GetQualificationSupplierDataYearlyLsit()
        {
            List<QualificationSupplierDataYearly> qualificationSupplierDataYearlyList =
                new List<QualificationSupplierDataYearly>();
            QualificationSupplierDataYearly qualificationSupplierDataYearly =
                new QualificationSupplierDataYearly(1, 1, 1, "1010000154", 10, 7);

            QualificationSupplierDataYearly qualificationSupplierDataYearly2 =
                new QualificationSupplierDataYearly(1, 1, 2, "1010000154", 10, 7);

            QualificationSupplierDataYearly qualificationSupplierDataYearly3 =
                new QualificationSupplierDataYearly(1, 1, 3, "1010000154", 10, 7);
            qualificationSupplierDataYearlyList.Add(qualificationSupplierDataYearly);
            qualificationSupplierDataYearlyList.Add(qualificationSupplierDataYearly2);
            qualificationSupplierDataYearlyList.Add(qualificationSupplierDataYearly3);
            return qualificationSupplierDataYearlyList;
        }

        public List<QualificationSupplierProject> GetQualificationSupplierProjects()
        {
            List<QualificationSupplierProject> qualificationSupplierProjects = new List<QualificationSupplierProject>()
            {
                new QualificationSupplierProject(1, 7, 1, "Name", "Des", "Owner Name", "100200", "ww@ww.com", "1010000154",
                    DateTime.Now, DateTime.Now.AddDays(1))
            };

            return qualificationSupplierProjects;
        }

        public List<QualificationSupplierData> GetQualificationSupplierDataList()
        {
            List<QualificationSupplierData> qualificationSupplierData = new List<QualificationSupplierData>();
            
            QualificationSupplierData supplierData =
                new QualificationSupplierData(1, 1, 7, 1, 1, 1, 1, 1, "1010000154", 1, 1, DateTime.Now, 1, "");
            supplierData.QualificationConfigurationAttachments.Add(
                new QualificationConfigurationAttachment(1, "File Name", "Idd"));
         
            QualificationItem qualificationItem = GetQualificationItem();
            PropertyInfo qualificationItemInfo = supplierData.GetType().GetProperty("QualificationItem");
            qualificationItemInfo.SetValue(supplierData,qualificationItem);

            Tender tender = GetQualificationWithConfiguration();
            PropertyInfo tenderInfo = supplierData.GetType().GetProperty("Tender");
            tenderInfo.SetValue(supplierData, tender);

            QualificationConfiguration qualificationConfiguration = GetQualificationConfiguration(7);
            PropertyInfo qualificationConfifrationnfo = supplierData.GetType().GetProperty("QualificationConfiguration");
            qualificationConfifrationnfo.SetValue(supplierData, qualificationConfiguration);


            qualificationSupplierData.Add(supplierData);
            return qualificationSupplierData;
        }

        public QualificationItem GetQualificationItem()
        {
            QualificationItem qualificationItem = new QualificationItem();
            QualificationSubCategory subCategory = GeQualificationSubCategory();
            PropertyInfo qualificationSubCatInfo = qualificationItem.GetType().GetProperty("QualificationSubCategory");
            qualificationSubCatInfo.SetValue(qualificationItem, subCategory);

            return qualificationItem;
        }

        public QualificationSubCategory GeQualificationSubCategory()
        {
            QualificationSubCategory qualificationSubCategory = new QualificationSubCategory();
            PropertyInfo qualificationCategoryIdInfo =
                qualificationSubCategory.GetType().GetProperty("QualificationCategoryId");
            qualificationCategoryIdInfo.SetValue(qualificationSubCategory,
                Convert.ChangeType((int) Enums.QualificationItemCategory.Technical,
                    qualificationCategoryIdInfo.PropertyType), null);

            return qualificationSubCategory;
        }

        public QualificationFinalResult GetQualificationFinalResult()
        {
            QualificationFinalResult qualificationFinalResult = new QualificationFinalResult(1, "", 7, 14);

            return qualificationFinalResult;
        }
    }
}
