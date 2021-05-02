using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults
{
    public class AnnouncementTemplateDefaults
    {
        private const string _announcementSupplierTemplateName = "test";
        private const string _templateExtendMechanism = "test";
        private const string _introAboutAnnouncementTemplate = "intro";
        private const bool IsInsideKsa = true;
        private const int _announcementTemplateSuppliersListTypeId = 1;
        private const string Details = "test details";
        private const string Descriptions = "descriptions";
        private const string ActivityDescription = "activity description";
        private const string AgencyCode = "030001000000";
        private const string AgencyAddress = "agency address";
        private const string AgencyFax = "agency fax";
        private const string AgencyPhone = "agency phone";
        private const string AgencyEmail = "agency email";
        private readonly List<int> _activitiesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _areasIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _countriesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _tenderItemTypes = new List<int>() { 2, 4 };
        private const string _requirementConditionsToJoinList = "requirementConditionsToJoinList";
        private const string _tenderTypesId = "2,4";
        private const string _announcementIdString = "69N3QwUl%208o0yRoVEsRsYA==";
        private const int _announcementId = 100;
        private const string _agencyName = "وزراه المالية";
        private const string _referenceNumber = "123456789";



        public AnnouncementSupplierTemplate ReturnAnnouncementSupplierTemplateDefaults()
        {

            AnnouncementSupplierTemplate generalAnnouncementSupplierTemplate = new AnnouncementSupplierTemplate();
            generalAnnouncementSupplierTemplate.CreateAnnouncementSupplierTemplate(GetAllAnnouncementSupplierTemplateModelData(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);
            generalAnnouncementSupplierTemplate.UpdateAnnouncementStatus_ForTest();
            return generalAnnouncementSupplierTemplate;
        }

        public AnnouncementSupplierTemplate ReturnAnnouncementSupplierTemplateWithLinkedAgencyDefaults()
        {
            AnnouncementSupplierTemplate generalAnnouncementSupplierTemplate = new AnnouncementSupplierTemplate();
            LinkedAgenciesAnnouncementTemplate linkedAgencies = new LinkedAgenciesAnnouncementTemplate(AgencyCode);
            generalAnnouncementSupplierTemplate.CreateAnnouncementSupplierTemplate(GetAllAnnouncementSupplierTemplateModelData(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);
            generalAnnouncementSupplierTemplate.SetLinkedAgencies(linkedAgencies);
            return generalAnnouncementSupplierTemplate;

        }

        public AnnouncementSupplierTemplate ReturnAnnouncementSupplierTemplateDefaultsForUpdateApprovedList()
        {
            AnnouncementSupplierTemplate generalAnnouncementSupplierTemplate = new AnnouncementSupplierTemplate();
            generalAnnouncementSupplierTemplate.UpdateAnnouncementSupplierTemplateListData(GetAllAnnouncementSupplierTemplateModelDataForUpdateList(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);
            return generalAnnouncementSupplierTemplate;

        }
        public CreateAnnouncementSupplierTemplateModel GetCreateAnnouncementSupplierTemplateModel()
        {
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };

            return new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementSupplierTemplateName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                TenderItemTypes = _tenderItemTypes,
                TenderTypesId = _tenderTypesId,
                AnnouncementTemplateSuppliersListTypeId = _announcementTemplateSuppliersListTypeId,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                CountriesIds = _countriesIds,
                AreasIds = _areasIds,
                ConstructionWorkIds = _countriesIds,
                ActivityIds = _activitiesIds,
                AgencyAddress = AgencyAddress,
                AgencyPhone = AgencyPhone,
                AgencyFax = AgencyFax,
                AgencyEmail = AgencyEmail,
                AgencyCode = "022001000000",
                Attachments= attachmentModel,
                MaintenanceRunnigWorkIds=new List<int>() { 1,2,3},
                
            };
        }

        public CreateAnnouncementSupplierTemplateModel GetCreateAnnouncementSupplierTemplateModelWithoutGovAgencyData()
        {
            return new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementSupplierTemplateName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                TenderItemTypes = _tenderItemTypes,
                TenderTypesId = _tenderTypesId,
                AnnouncementTemplateSuppliersListTypeId = _announcementTemplateSuppliersListTypeId,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                CountriesIds = _countriesIds,
                AreasIds = _areasIds,
                ConstructionWorkIds = _countriesIds,
                ActivityIds = _activitiesIds,
                MaintenanceRunnigWorkIds = new List<int>() { 1, 2, 3 }

            };
        }

        

        public CreateAnnouncementSupplierTemplateModel GetAllAnnouncementSupplierTemplateModelData()
        {
            return new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                AnnouncementSupplierTemplateName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                TenderItemTypes = _tenderItemTypes,
                TenderTypesId = _tenderTypesId,
                AnnouncementTemplateSuppliersListTypeId = _announcementTemplateSuppliersListTypeId,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                CountriesIds = _countriesIds,
                AreasIds = _areasIds,
                ConstructionWorkIds = _countriesIds,
                ActivityIds = _activitiesIds,
                AgencyAddress = AgencyAddress,
                AgencyPhone = AgencyPhone,
                AgencyFax = AgencyFax,
                AgencyEmail = AgencyEmail,
                AgencyCode = "022001000000",
                MaintenanceRunnigWorkIds = new List<int>() { 1, 2, 3 }


            };
        }


        public UpdateAnnouncementSupplierTemplateModel GetAllAnnouncementSupplierTemplateModelDataForUpdateList()
        {
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };
            return new UpdateAnnouncementSupplierTemplateModel
            {
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                AnnouncementTemplateSuppliersListTypeId = _announcementTemplateSuppliersListTypeId,
                IsEffectiveList = true,
                EffectiveListDate = DateTime.Now.AddDays(1),
                AgencyAddress = AgencyAddress,
                AgencyPhone = AgencyPhone,
                AgencyFax = AgencyFax,
                AgencyEmail = AgencyEmail,
                AgencyCode = "022001000000",
                Attachments= attachmentModel,
                
            };
        }


        public AnnouncementSuppliersTemplateDetailsModel GetAnnouncementSupplierTemplateDetailsModel()
        {
            return new AnnouncementSuppliersTemplateDetailsModel
            {
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                AgencyAddress = AgencyAddress,
                AgencyPhone = AgencyPhone,
                AgencyFax = AgencyFax,
                AgencyEmail = AgencyEmail,
                AgencyCode = "022001000000",
            };
        }



        public AnnouncementDescriptionModel GetAnnouncementDescriptionModel()
        {
            return new AnnouncementDescriptionModel
            {
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                IsRequiredAttachmentToJoinList = false,
                RequiredAttachment = "RequiredAttachment",


            };
        }



        public AnnouncementBasicDetailModel GetAnnouncementBasicDetailModelData()
        {
            return new AnnouncementBasicDetailModel
            {
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                IsEffectiveList = false,
                Descriptions = Descriptions,
                AnnouncementName = _announcementSupplierTemplateName,
                EffectiveListDate = DateTime.Now.ToString(),
                Details = Details,
                AgencyName = _agencyName,
                IsCreatedByAgncy = true,
                ReferenceNumber = _referenceNumber
            };
        }




        public AnnouncementSuppliersTemplateCancelModel GetAnnouncementSuppliersTemplateCancelModelData()
        {
            return new AnnouncementSuppliersTemplateCancelModel
            {
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                IsEffectiveList = false,
                Descriptions = Descriptions,
                AnnouncementName = _announcementSupplierTemplateName,
                EffectiveListDate = DateTime.Now.ToString(),
                Details = Details,
                AgencyName = _agencyName,
                IsCreatedByAgncy = true,
                ReferenceNumber = _referenceNumber
            };
        }


        

        public AnnouncementTemplateListDetailsModel GetAnnouncementTemplateListDetailsModelData()
        {
            return new AnnouncementTemplateListDetailsModel
            {
              JoinRequestCount=1,
              AcceptedSuppliersCount=1,
              UsingListCount=1,
              UsingListCountInternally=1
            };
        }

        public ExtendAnnouncementSupplierTemplateModel GetExtendAnnouncementSupplierTemplateModelData()
        {
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };
            return new ExtendAnnouncementSupplierTemplateModel
            {
                TemplateExtendMechanism= _templateExtendMechanism,
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                AnnouncementSupplierTemplateName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                TenderItemTypes = _tenderItemTypes,
                TenderTypesId = _tenderTypesId,
                AnnouncementTemplateSuppliersListTypeId = _announcementTemplateSuppliersListTypeId,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                CountriesIds = _countriesIds,
                AreasIds = _areasIds,
                ConstructionWorkIds = _countriesIds,
                ActivityIds = _activitiesIds,
                AgencyAddress = AgencyAddress,
                AgencyPhone = AgencyPhone,
                AgencyFax = AgencyFax,
                AgencyEmail = AgencyEmail,
                AgencyCode = "022001000000",
                Attachments= attachmentModel,
                MaintenanceRunnigWorkIds = new List<int>() { 1, 2, 3 }



            };
        }

        public AnnouncementTemplateDetailsForPrintModel GetGetAnnouncementDetailsByAnnouncementIdForPrintModelData()
        {
            return new AnnouncementTemplateDetailsForPrintModel
            {
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                AnnouncementName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                AgencyCode = "022001000000"
            };
        }

        public AnnouncementTemplateDetailsForSupplierForPrintModel GetGetAnnouncementDetailsByAnnouncementIdForPrintForSupplierModelData()
        {
            return new AnnouncementTemplateDetailsForSupplierForPrintModel
            {
                AnnouncementId = _announcementId,
                AnnouncementIdString = _announcementIdString,
                AnnouncementName = _announcementSupplierTemplateName,
                IntroAboutAnnouncementTemplate = _introAboutAnnouncementTemplate,
                RequirementConditionsToJoinList = _requirementConditionsToJoinList,
                IsEffectiveList = false,
                IsRequiredAttachmentToJoinList = false,
                Descriptions = Descriptions,
                ActivityDescription = ActivityDescription,
                AgencyCode = "022001000000", 
            };
        }

        public AnnouncementSupplierTemplate ReturnAnnouncementSupplierTemplateDefaultsForRequestJoin()
        {
            AnnouncementSupplierTemplate generalAnnouncementSupplierTemplate = new AnnouncementSupplierTemplate();
            generalAnnouncementSupplierTemplate.UpdateAnnouncementSupplierTemplateListData(GetAllAnnouncementSupplierTemplateModelDataForUpdateList(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);
            generalAnnouncementSupplierTemplate.Test_UpdateStatus(3);
            generalAnnouncementSupplierTemplate.Test_AddedCreatedByID();
            return generalAnnouncementSupplierTemplate;

        }



    }
}
