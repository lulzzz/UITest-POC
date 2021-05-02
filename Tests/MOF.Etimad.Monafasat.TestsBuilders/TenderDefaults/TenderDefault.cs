using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults
{
    public class TenderDefault
    {
        public Tender GetNewDirectPurchasePrivate()
        {
            Tender newDirectPurchasePrivate = new Tender("022001000000", 1, (int)Enums.TenderType.NewDirectPurchase, (int)Enums.InvitationType.Specific, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
     null, 1, null, null, 1, null,
                1, null, null, null, null, 1, 1000, null, 1,
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500);

            newDirectPurchasePrivate.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), null, DateTime.Now.Date.AddDays(3), null, null, false, null, 1, "building name", "Floar number", "Department Number", null);

            return newDirectPurchasePrivate;
        }

        public List<Tender> GetGenerlTenders()
        {
            List<Tender> tenders = new List<Tender>() { GetGeneralTender() };
            return tenders;
        }
        public List<Tender> GetDirectPurchaseTenders()
        {
            List<Tender> tenders = new List<Tender>() { GetNewDirectPurchasePrivate() };
            return tenders;
        }
        public List<Tender> GetDirectPurchaseLowBudgetTenders()
        {
            List<Tender> tenders = new List<Tender>() { GetNewDirectPurchaseLowValue() };
            return tenders;
        }

        public TenderQuantityTable GetTenderQuantityTable()
        {
            TenderQuantityTable quantityTable = new TenderQuantityTable(1, "table 1", 1);
            quantityTable.QuantitiyItemsJson.TenderQuantityTableItems.Add(new TenderQuantityTableItem(1, 1, 1, 1, "Column Name", "Value", 1));
            return quantityTable;
        }

        public Tender GetNewDirectPurchaseLowValue()
        {
            Tender newDirectPurchaseLowValue = new Tender("022001000000", 1, (int)Enums.TenderType.NewDirectPurchase, (int)Enums.InvitationType.Specific, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, null, null, null, null,
                1, null, null, null, null, 1, 1000, null, 1,
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500, null, true, 1);

            newDirectPurchaseLowValue.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), null, DateTime.Now.Date.AddDays(3), null, null, false, null, 1, "building name", "Floar number", "Department Number", null);

            return newDirectPurchaseLowValue;
        }
        public Tender GetGeneralTender()
        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
null, 1, 1, 1, null, null,
              null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            return generalTender;
        }
        public Tender GetVROTenderData()

        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NationalTransformationProjects,
                (int)Enums.InvitationType.Public, "Tender name test", "1", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
null, 1, null, null, null, 1,
              null, null, null, null, null, (int)Enums.OfferPresentationWayId.OneFile, 1000, null, 1, // replace conditon book vale with null
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            return generalTender;
        }
        public Tender GetGeneralTenderWithCommittees()
        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
null, 1, 1, 1, 1, 1,
              null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdatePreQualificationCommittees(4, 1, 1, 1);
            return generalTender;
        }

        public PreQualificationSavingModel GetPreQualificationSavingModel()
        {
            PreQualificationSavingModel qualificationSavingModel = new PreQualificationSavingModel()
            {
                TenderTypeId = (int)Enums.TenderType.PostQualification,
                TenderStatusId = 4,
                TenderName = "Tender Name",
                TechnicalOrganizationId = 1,
                OffersCheckingCommitteeId = 1,
                PreQualificationCommitteeId = 1,
                LastEnqueriesDate = DateTime.Now.Date,
                LastOfferPresentationDate = DateTime.Now.Date,
                OffersCheckingDate = DateTime.Now.Date,
            };

            return qualificationSavingModel;
        }
        public Tender GetGeneralTenderWithPrequalification()
        {
            Tender generalTenderWithPrequalification = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Specific, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
       null, 1, 1, 1, null, null,
              null, null, null, 1, null, 1, 1000, null, 1,
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);

            generalTenderWithPrequalification.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);

            return generalTenderWithPrequalification;

        }
        public Tender GetGeneralTenderWithLocalContent()
        {
            Tender tender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Specific, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
       null, 1, 1, 1, null, null,
              null, null, null, 1, null, 1, 1000, null, 1,
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);

            tender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            TenderLocalContent tenderLocal = new TenderLocalContent();
            tender.SetTenderLocalContent(tenderLocal);
            tender.CreateTenderLocalContent();

            return tender;

        }

        public Tender GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus status)
        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
       null, 1, 1, 1, null, null,
              null, null, null, null, null, 1, 1000, null, 1,
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            generalTender.CreateConditionsTemplate();
            generalTender.AddIsTenderContainsTawreedTables_ForTest(false);
            generalTender.UpdateTenderConditoinsStatus(status);
            return generalTender;
        }

        public Tender GetGeneralTenderForConditoinTemplatesTenderMoreThan50Million()
        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
       null, 1, 1, 1, null, null,
              null, null, null, null, null, 1, 500000000, null, 1,
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            generalTender.CreateConditionsTemplate();
            generalTender.AddIsTenderContainsTawreedTables_ForTest(false);
            return generalTender;
        }


        public CreateTenderBasicDataModel GetBasicTenderModel()
        {
            return new CreateTenderBasicDataModel
            {
                TenderName = "tender created by unit test",
                TenderTypeId = 1,
                TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing,
                ConditionsBookletPrice = 1000,
                AgencyCode = "022001000000",
                BranchId = 1,
                TechnicalOrganizationId = 1,
                OffersOpeningCommitteeId = 1,
                OffersCheckingCommitteeId = 1,
                EstimatedValue = 10000,
                SupplierNeedSample = false,
                OfferPresentationWayId = 1,
                AgreementTypeId = 1,
                SpendingCategoryId = 1,
                InvitationTypeId = (int)Enums.InvitationType.Public,
                Purpose = "tender created by unit test" + "   Unit Test Unit Test Unit Test Unit Test Unit Test Unit Test Unit Test Unit Test "
            };
        }

        public TenderRelationsModel GetTenderRelationsDefaultData()
        {
            int tenderId = 1;

            return new TenderRelationsModel
            {
                TenderIdString = Util.Encrypt(tenderId),
                TenderActivitieIDs = new List<int> { 1, 2 },
                TenderAreaIDs = new List<int> { 1, 2 },
                InsideKSA = true,
            };
        }


        public EditConditionTemplateSecondSectionModel GetIntroductionToConditionsTemplateDefaultData()
        {
            int tenderId = 1;
            return new EditConditionTemplateSecondSectionModel
            {
                Description = "يتم تعريف وتحديد الغرض من الكراسة ويمكن للجهة إضافة أي مقدمات أو شرح عنها أو عن المشروع كما تراه مناسباً.",
                CerificatesIDs = new List<int> { 1, 2 },
                AgentName = "اسم ممثل الجهه",
                AgentPhone = "0533286913",
                AgentFax = "0533286913",
                AgentJob = "وظيفة ممثل الجهه",
                AgentEmail = "hsawak@etimad.sa",
                AgencyDecalration = "تعريف الجهه",
                EncryptedTenderId = Util.Encrypt(tenderId)
            };
        }
        public EditConditionTemplateThridSectionModel GetPreparingOffersToConditionsTemplateDefaultData()
        {
            int tenderId = 1;
            return new EditConditionTemplateThridSectionModel
            {
                ConfirimJoiningTheTender = "يحق للجهة إلغاء هذه المادة إذا كانت الشروط محققة عند شراء الكراسة",
                MaxTimeToAnswerQuestions = 1,
                AlternativeEmailForCommuncation = "hsawak@etimad.sa",
                DocumentStyle = "DocumentStyle",
                AllowancePeriodToAddOffersIfNotAddedBeofre = 1,
                AllowedOfferSiningDays = 1,
                WritePrice = "لا يجوز لمقدم العرض ترك أي بند من بنود المنافسة دون تسعير إلّا إذا أجازت شروط المنافسة ذلك",
                FinancialOfferDocuments = "1#",
                TechnicalOfferDocuments = "1#",
                EncryptedTenderId = Util.Encrypt(tenderId)

            };
        }
        public EditConditionTemplateThridSectionModel GetPreparingOffersNewVersionDefaultData()
        {
            int tenderId = 1;
            return new EditConditionTemplateThridSectionModel
            {
                ConfirimJoiningTheTender = "يحق للجهة إلغاء هذه المادة إذا كانت الشروط محققة عند شراء الكراسة",
                MaxTimeToAnswerQuestions = 1,
                AlternativeEmailForCommuncation = "hsawak@etimad.sa",
                DocumentStyle = "DocumentStyle",
                AllowancePeriodToAddOffersIfNotAddedBeofre = 1,
                AllowedOfferSiningDays = 1,
                WritePrice = "لا يجوز لمقدم العرض ترك أي بند من بنود المنافسة دون تسعير إلّا إذا أجازت شروط المنافسة ذلك",
                FinancialOfferDocuments = "1#",
                TechnicalOfferDocuments = "1#",
                ParticipationConfirmationLetterDate = DateTime.Now.Date.AddDays(1),
                OffersChecking = "OffersChecking",
                OffersEvaluationCriteria = "OffersEvaluationCriteria",
                VersionId = (int)Enums.ActivityVersions.Sprint7Activities,
                EncryptedTenderId = Util.Encrypt(tenderId)

            };
        }

        public EditConditionTemplateSeventhSectionModel GetTechnicalDeclerationsToConditionsTemplateDefaultData()
        {
            int tenderId = 1;
            return new EditConditionTemplateSeventhSectionModel
            {
                ProjectsScope = "في هذا البند يتم توضيح نطاق العمل الخاص بالمشروع والتفاصيل التي يجب مراعاتها عند تقديم الخدمة من المتعاقد.",
                WorksProgram = "WorksProgram",
                WorkLocationDetails = "WorkLocationDetails",
                ListOfSections = "10,11,12,13,2,3,7,8,14",
                IsTemplateHasOutput = false,
                ShowGeneralOnly = false,
                EncryptedTenderId = Util.Encrypt(tenderId),
                TechnicalDeclrations = new List<TenderConditionsTemplateTechnicalDeclrationViewModel>()
                {
                    new TenderConditionsTemplateTechnicalDeclrationViewModel()
                    {
                        TenderConditionsTemplateTechnicalDeclrationId = 1,
                        Term = "term",
                        Decleration= "Declaration"
                    }
                },
                TenderConditionsTemplateOutputs = new List<TenderConditionsTemplateOutputsViewModel>()
                {
                    new TenderConditionsTemplateOutputsViewModel()
                    {
                        TenderConditionsTemplateTechnicalOutputId = 1,
                        OutputName = "outputName",
                        OutputDescriptions = "OutputDescriptions",
                        OutputStage = "OutputStage",
                        OutputDeliveryTime = "Delivery time",
                    }
                }
            };
        }

        public LocalContentModel GetLocalContentModelData()
        {
            return new LocalContentModel()
            {
                EncryptedTenderId = "9FqqzWC1*@@**FVAt Mcj3sYjw==",
                TenderId = 1,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                IsApplyProvisionsMandatoryList = true,
                StudyMinimumTargetFileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000081036",
                LocalContentMechanismIDs = new List<int>() { 1, 3 },
                TenderAttachments = new List<TenderAttachmentModel>(),
            };
        }

        public EditConditionTemplateEighthSectionModel GetDescriptionToConditionsTemplateDefaultData()
        {
            int tenderId = 1;
            List<TenderAttachmentModel> tenderAttachments = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };
            return new EditConditionTemplateEighthSectionModel
            {
                MaterialsSpecifications = "MaterialsSpecifications",
                EquipmentsSpecifications = "EquipmentsSpecifications",
                QualitySpecifications = "QualitySpecifications",
                SafetySpecifications = "SafetySpecifications",
                SpecialConditions = "SpecialConditions",
                TenderAttachments = tenderAttachments,
                Attachments = "Attachments",
                AttachmentReference = ",idd_2CE19608-84B1-C336-8521-6875EB300000020206:vue-js.pdf",
                IsTemplateHasOutput = false,
                ListOfSections = "10,11,12,13,2,3,7,8",
                ShowGeneralOnly = false,
                TemplateIds = "13",
                EncryptedTenderId = Util.Encrypt(tenderId)
            };
        }

        public InvitationsInCreateTenderModel GetInvitationsInCreateTenderModelDefaultData()
        {
            List<CrModel> suppliers = new List<CrModel>();
            suppliers.Add(new CrModel() { CrName = "1010052847", CrNumber = "مؤسسه عبدالله حسين المزيد للمقاولات" });
            suppliers.Add(new CrModel() { CrName = "1010254223", CrNumber = "مؤسسة الحلول الراسخه لتقنية المعلومات" });
            return new InvitationsInCreateTenderModel
            {
                TenderId = 1,
                InvitedSuppliers = suppliers
            };
        }

        public TenderOffersStepModel GetTenderOffersStepModelData()
        {
            return new TenderOffersStepModel()
            {
                TenderId = 1,
                TenderTypeId = 9,
                OffersOpeningAddressId = 1,
                SupplierNeedSample = false,
                LastEnqueriesDate = DateTime.Parse("2019-09-07T00:00:00"),
                LastOfferPresentationDate = DateTime.Parse("2020-06-20T00:00:00"),
                OffersOpeningDate = DateTime.Parse("2020-02-12T00:00:00"),
                TenderStatusId = 8,
                ChangeRequestTypeId = 0
            };
        }




        public GetConditionTemplateModel GetConditionTemplateModelModelData()
        {
            return new GetConditionTemplateModel()
            {
                AttachmentReferenceLst = new List<string>() { "idd_2CE19608-84B1-C336-8521-6875EB300000081036" },
                BranchId = 4,
                TenderId = 1,
                TenderTypeId = 9,
                SupplierNeedSample = false,
                TenderStatusId = 8,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                IsApplyProvisionsMandatoryList = true,
                StudyMinimumTargetFileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000081036",
                LocalContentMechanismIDs = new List<int>() { 1, 3 },
                LocalContentAttachmentReferenceLst = new List<string>()
            };
        }

        public Tender GetTenderWithStatus(Enums.TenderStatus tenderStatus)
        {
            Tender generalTender = new Tender("022001000000", 1, (int)Enums.TenderType.NewTender, (int)Enums.InvitationType.Public, "Tender name test", "tender number", "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
        null, 1, 1, 1, null, null,
              null, null, null, null, null, 1, 1000, null, 1, // replace conditon book vale with null
              null, null, null, null, null, null, null,
              null, null, false, null, null, 500, null);
            generalTender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), null, null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            generalTender.UpdateTenderStatus(tenderStatus);
            return generalTender;
        }

        public Tender GetPreQualificationWithChangeRequest()
        {
            Tender tender = new Tender("022001000000", 12, (int)Enums.TenderType.PreQualification,
                (int)Enums.InvitationType.Public, "Qualification name test", "tender number",
                "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                null, 1, 1, 1, null, null,
                null, null, null, null, null, 1, 1000, null, 1,
                null, null, null, null, null, null, null,
                null, null, false, null, null, 500, null);

            var tenderId = 55;
            var cr = "1010000154";
            //  tender.FavouriteSupplierTenders.Add(new FavouriteSupplierTender(tenderId,cr));
            tender.ChangeRequests.Add(new TenderChangeRequest(tenderId, 1, 1, RoleNames.DataEntry, false));
            tender.ChangeRequests.Add(new TenderChangeRequest(tenderId, 2, 1, RoleNames.DataEntry, true));
            tender.PreQualificationApplyDocuments.Add(new SupplierPreQualificationDocument(cr, 4, tenderId, 14, true));
            return tender;
        }


        public List<Template> GetTemplateData()
        {
            return new List<Template>();
        }



        public LocalContentModel GetLocalContentModelDefaultData()
        {
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };

            int tenderId = 1;
            return new LocalContentModel
            {
                IsApplyProvisionsMandatoryList = true,
                LocalContentMechanismIDs = new List<int>() { 1, 3 },
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                EncryptedTenderId = Util.Encrypt(tenderId),
                TenderAttachments = attachmentModel
            };
        }

    }
}
