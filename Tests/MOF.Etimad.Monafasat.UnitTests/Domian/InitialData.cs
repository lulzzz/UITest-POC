using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.NotificationDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.UnitTests
{
    public static class InitialData
    {
        private static readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        public static readonly AppDbContext context;

        static InitialData()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestDb");
            context = new AppDbContext(optionsBuilder.Options, _mockHttpContextAccessor.Object);
            initial(context);
        }

        private static void initial(AppDbContext context)
        {

            context.TenderTypes.AddRange(new List<TenderType>()
            {
                new TenderType()
                {
                    TenderTypeId = 1, NameAr = " منافسة عامة (جديد)", BuyingCost = 500.0m, InvitationCost = 200.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 2, NameAr = "شراء مباشر (جديد)", BuyingCost = 0.0m, InvitationCost = 200.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 3, NameAr = "تأهيل مسبق", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 4, NameAr = "منافسة محدودة", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 5, NameAr = "المزايدة العكسية الإلكترونية", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 6, NameAr = "المنافسة على مرحلتين(المرحلة الاولى)", BuyingCost = 0.0m,
                    InvitationCost = 0.0m, CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 7, NameAr = "المنافسة على مرحلتين(المرحلة الثانية)", BuyingCost = 0.0m,
                    InvitationCost = 0.0m, CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 8, NameAr = "تأهيل لاحق", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 9, NameAr = "منافسة عامة (حالى)", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 10, NameAr = " شراء مباشر (حالى)", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 11, NameAr = "منافسة اتفاقية اطارية", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 12, NameAr = "مسابقة", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                },
                new TenderType()
                {
                    TenderTypeId = 13, NameAr = "مشاريع التحول الوطني", BuyingCost = 0.0m, InvitationCost = 0.0m,
                    CreatedAt = DateTime.Now
                }
            });
            context.Tenders.AddRange(new List<Tender>()
            {
                new Tender(0, 1, "Test Tender 1", null, null, null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4),
                    DateTime.Now.AddDays(5), DateTime.Now.AddDays(6), (int) Enums.TenderStatus.Approved, "1", 1, 1,
                    null),
                new Tender(0, (int) Enums.TenderType.NationalTransformationProjects, "Test Tender 2", null, null, null,
                    DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), DateTime.Now.AddDays(5), DateTime.Now.AddDays(6),
                    (int) Enums.TenderStatus.Approved, "1", 1, 1, null),
                new Tender(0, 1, "Test Tender 3", null, null, null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4),
                    DateTime.Now.AddDays(5), DateTime.Now.AddDays(6), (int) Enums.TenderStatus.UnderEstablishing, "1",
                    1, 1, null),
                new Tender(0, 1, "Test Tender 3", null, null, null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4),
                    DateTime.Now.AddDays(5), DateTime.Now.AddDays(6), (int) Enums.TenderStatus.OffersAwardedConfirmed,
                    "1", 1, 1, null),
                new Tender(60, 1, "Test Tender 3", null, null, null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4),
                    DateTime.Now.AddDays(5), DateTime.Now.AddDays(6), (int) Enums.TenderStatus.OffersAwardedConfirmed,
                    "1", 1, 1, null),

                    new Tender(50, "Test Tender 50", "1","purpose", null, null,null,1,50,"agencycode",1,50,1,"",1,null),


        //public Tender(int tenderTypeId, string tenderName, string tenderNumber, string purpose, int? technicalOrganizationId, int? offersCheckingCommitteeId,
        // int? offersOpeningCommitteeId, int offerPresentationWayId, int? tenderFirstStageId, string agencyCode, int branchId, decimal? estimatedValue, int? quantitiyTableVersionId
        // , string intialGurantteeAddress, decimal? initialGuaranteePercentage, List<AgencyBudgetNumber> agencyBudgetNumbers = null)
            });

            context.TenderUnitAssigns.AddRange(new List<TenderUnitAssign>()
            {
                new TenderUnitAssign(1,1,true,(int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne)
            });

            context.AnnouncementSupplierTemplate.AddRange(new List<AnnouncementSupplierTemplate>()
            {
                new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults()
            });

            context.AnnouncementRequestSupplierTemplate.AddRange(new List<AnnouncementJoinRequestSupplierTemplate>()
            {
                new AnnouncementJoinRequestDefaults().GetJoinRequestWithAnnouncementDefaultData()
            });
            context.Offers.AddRange(new List<Offer>()
            {
                new Offer(1, "1299659801", null, 1, false),
                new Offer(2, "1299659801", null, 1, false),
                new OfferDefaults().GetOfferDefaultsByOfferId(1020)
            });

            context.OfferSolidarities.AddRange(new List<OfferSolidarity>()
            {
                new OfferSolidarity(1,"1299659801")
            });

            context.SupplierCombinedDetails.AddRange(new List<SupplierCombinedDetail>()
            {
                new SupplierCombinedDetailDefaults().BuildSupplierCombinedDetail()
            });
            context.NegotiationFirstStageSuppliers.AddRange(new List<NegotiationFirstStageSupplier>()
            {
                new NegotiationDefaults().GetNegotiationFirstStageSupplier(1,"")
            });
            context.TenderAgreementAgencies.AddRange(new List<TenderAgreementAgency>()
            {
                new TenderAgreementAgency("1", 60)
            });
            context.Committees.AddRange(new List<Committee>()
            {
                new Committee(1, "1", "committeeName1", "any address", "0545698754", "0547895643",
                    "committee@gmail.com", "45875", "78548", true),
                new Committee(2, "1", "committeeName2", "any address", "0545698754", "0547895643",
                    "committee@gmail.com", "45875", "78548", true),
                new Committee(3, "1", "committeeName3", "any address", "0545698754", "0547895643",
                    "committee@gmail.com", "45875", "78548", true),
                new Committee(4, "1", "committeeName4", "any address", "0545698754", "0547895643",
                    "committee@gmail.com", "45875", "78548", true),
                new Committee(5, "1", "committeeName5", "any address", "0545698754", "0547895643",
                    "committee@gmail.com", "45875", "78548", true),

            });

            context.Invitations.AddRange(new List<Invitation>()
            {
                new Invitation("1299659801", Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Invitation, false),
                new Invitation("1299659801", Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Request, false),
                new Invitation("1299659801", Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Invitation, true),
                new Invitation("1299659801", Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Request, true)
            });
            context.ConditionsBooklets.AddRange(new List<ConditionsBooklet>()
            {
                new ConditionsBooklet("1299659801", new BillInfo(0.00m, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "1"))
            });
            context.CommitteeUsers.AddRange(new List<CommitteeUser>()
            {
                new CommitteeUser(1, 1, 1),
                new CommitteeUser(2, 2, 2)

            });




            context.UserProfiles.AddRange(new List<UserProfile>()
            {
                new UserProfile(1, "username", "fullname", "mobile", "email", new List<UserNotificationSetting>())
            });


            context.UserNotificationSettings.AddRange(new List<UserNotificationSetting>()
            {
                new UserNotificationSetting("1", "1", 1, false, false, 1),
                new UserNotificationSetting("1",1, 1, false, false, 1)
            });


            context.PanelNotifications.AddRange(new List<NotificationPanel>()
            {
                new NotificationPanel("1","title","content",1,"link")
            });



            context.NotificationOperationCodes.AddRange(new List<NotificationOperationCode>()
            {
                 new NotificationsDefaults().GetNotificationOperationCodeData()
            });

            context.Notifications.AddRange(new List<BaseNotification>()
            {
                new NotificationEmail(1,"hsawak@etimad.sa","testTitle","TestContent",1,"link","key")
            });


            context.SupplierBlock.AddRange(new List<SupplierBlock>()
            {
                new SupplierBlock("1", "SelectedCr1", "1", 1, 1, "1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true),
                new SupplierBlock("2", "SelectedCr2", "1", 1, 1, "2", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true),
                new SupplierBlock("3", "SelectedCr3", "1", 1, 1, "3", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true),
                new SupplierBlock("4", "SelectedCr4", "1", 1, 1, "4", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true),
                new SupplierBlock("5", "SelectedCr5", "1", 1, 1, "5", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true),
                new SupplierBlock("6", "SelectedCr6", "1", 1, 1, "6", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4),
                    "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID",
                    "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1,
                    "9876543221", "origin", true)

            });

            context.Suppliers.AddRange(new List<Supplier>()
            {
                new Supplier("SelectedCr1", "SelectedCrName1", new List<UserNotificationSetting>()),
                new Supplier("SelectedCr78", "SelectedCrName2", new List<UserNotificationSetting>()),
                new Supplier("SelectedCr45", "SelectedCrName3", new List<UserNotificationSetting>()),
                new Supplier("SelectedCr12", "SelectedCrName4", new List<UserNotificationSetting>()),
                new Supplier("SelectedCr98", "SelectedCrName5", new List<UserNotificationSetting>())

            });

            context.BlockTypes.AddRange(new List<BlockType>()
            {
                new BlockType(1, "BlockType1", "BlockType1"),
                new BlockType(2, "BlockType2", "BlockType2"),
                new BlockType(3, "BlockType3", "BlockType3")
            });
            context.BlockStatus.AddRange(new List<BlockStatus>()
            {
                new BlockStatus(1, "BlockType1", "BlockType1"),
                new BlockStatus(2, "BlockType2", "BlockType2"),
                new BlockStatus(3, "BlockType3", "BlockType3")
            });

            context.GovAgencies.AddRange(new List<GovAgency>()
            {
                new GovAgency("1", "شركه 1", 100, true, 1, "05657898765", "123"),
                new GovAgency("2", "شركه 2", 100, true, 1, "05657898765", "123"),
                new GovAgency("3", "شركه 3", 100, true, 1, "05657898765", "123"),
                new GovAgency("4", "شركه 4", 100, true, 1, "05657898765", "123"),
                new GovAgency("5", "شركه 5", 100, true, 1, "05657898765", "123"),
                new GovAgency("6", "شركه 6", 100, true, 1, "05657898765", "123"),
            });

            #region [Supplier Document]

            //(string SupplierId,int PreQualificationId, int PreQualificationResult, bool? isActive = true)
            context.SupplierPreQualificationDocument.AddRange(new List<SupplierPreQualificationDocument>()
            {
                new SupplierPreQualificationDocument("5956275283", 555, 6, 1),
                new SupplierPreQualificationDocument("5956275283", 555, 4, 1),
                new SupplierPreQualificationDocument("5956275283", 555, 5, 1),
                new SupplierPreQualificationDocument("5956275283", 555, 3, 1),
                new SupplierPreQualificationDocument("5956275283", 555, 2, 1)
            });

            context.Tenders.AddRange(new List<Tender>()
            {
                new Tender("00220033", 1, 1, 1, "Tender name test", "tender number",
                    "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                    null, 1, 1, 1, null, null,
                    null, null, null, null, null, 1, 1000, 100, 1,
                    null, null, null, null, null, null, null,
                    null, null, false, null, null, 500, null),

                new Tender("00220033", 1, 1, 1, "Tender name test", "tender number",
                    "Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse Purpuse ",
                    null, 1, 1, 1, null, null,
                    null, null, null, null, null, 1, 1000, 100, 1,
                    null, null, null, null, null, null, null,
                    null, null, false, null, null, 500, null),
                new TenderDefault().GetPreQualificationWithChangeRequest(),
                new QualificationDefault().GetPostQualificationWithWithTenderObject(),
               // new QualificationDefault().GetQualificationWithWithFinalPointsAndSupplier(),

            });
            context.Branch.AddRange(new List<Branch>()
            {
                new Branch(1, "00220033", "branchName", new List<BranchAddress>()),
                new Branch(2, "00220033", "branchName", new List<BranchAddress>()),
                new Branch(3, "00220033", "branchName", new List<BranchAddress>()),

            });

            context.BranchCommittees.AddRange(new List<BranchCommittee>()
            {
                new BranchCommittee(1, 1),
                new BranchCommittee(2, 2),
            });

            context.BranchUsers.AddRange(new List<BranchUser>()
            {
                new BranchUser(1, 1, 1, "00220033", 1, 1),
            });
            context.TenderStatus.AddRange(new List<TenderStatus>()
            {
                new TenderStatus() {TenderStatusId = 1, NameEn = "UnderEstablishing", NameAr = "تحت الإنشاء"},
                new TenderStatus() {TenderStatusId = 2, NameEn = "Established", NameAr = "منشاء"},
                new TenderStatus() {TenderStatusId = 3, NameEn = "Pending", NameAr = "بإنتظار الاعتماد"},
                new TenderStatus() {TenderStatusId = 4, NameEn = "Approved", NameAr = "معتمد"},
                new TenderStatus() {TenderStatusId = 5, NameEn = "Rejected", NameAr = "مرفوض"},
                new TenderStatus() {TenderStatusId = 6, NameEn = "OffersOppening", NameAr = "فتح العروض"},
                new TenderStatus()
                    {TenderStatusId = 7, NameEn = "OffersOppenedPending", NameAr = "بانتظار اعتماد فتح العروض"},
                new TenderStatus()
                    {TenderStatusId = 8, NameEn = "OffersOppenedConfirmed", NameAr = "اعتماد فتح العروض"},
                new TenderStatus() {TenderStatusId = 9, NameEn = "OffersOppenedRejected", NameAr = "رفض العروض"}
            });

            #endregion

            BillInfoModel billInfoModel = new BillInfoModel()
            {

                AmountDue = 10,
                DueDate = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(1),
                AgencyCode = "0410010000000002000",
                ActionStatus = 0,
                ChapterNumber = "000",
                NumOfSubSections = "000",
                SequenceNumber = "000",
                NumOfSubDepartments = "000",
                SectionId = "000",
                BankBranchId = "000",
                BillInvoiceNumber = "93991132000",
                BillStatusId = 3,
                ConditionBookletId = 270,
                BillReferenceInfo = "93991132000",
                CreatedBy = "hhh",
                CreatedAt = DateTime.Now,
                PurchaseDate = DateTime.Now,
            };
            context.BillInfos.AddRange(new List<BillInfo>()
            {
                new BillInfo(billInfoModel)
            });

            context.PrePlannings.AddRange(new List<PrePlanning>()
            {
                new PrePlanning(1, "0410010000000002000", 1, "test project", "project nature", 1, "project description",
                    true),
                new PrePlanning(2, "0410010000000002000", 1, "test project", "project nature", 1, "project description",
                    true),
                new PrePlanning(3, "0410010000000002000", 1, "test project", "project nature", 1, "project description",
                    true, 1, "1", 1, 1, 1, 1),
            });

            context.YearQuarters.AddRange(new List<YearQuarter>()
            {
                new YearQuarter(1, "test", "test"),
            });


            context.PrePlanningCountries.AddRange(new List<PrePlanningCountry>()
            {
                new PrePlanningCountry(1)
            });

            context.PrePlanningAreas.AddRange(new List<PrePlanningArea>()
            {
                new PrePlanningArea(1)
            });

            context.PrePlanningProjectTypes.AddRange(new List<PrePlanningProjectType>()
            {
                new PrePlanningProjectType(1)
            });


            context.AgencyCommunicationRequests.AddRange(new List<AgencyCommunicationRequest>()
            {
                new AgencyCommunicationRequest(1, (int) Enums.AgencyCommunicationRequestType.Plaint,
                    (int) Enums.AgencyCommunicationRequestStatus.RequestSent, "role1")
            });
            context.AgencyCommunicationRequests.AddRange(new List<AgencyCommunicationRequest>()
            {
                new AgencyCommunicationRequest(1, (int) Enums.AgencyCommunicationRequestType.Plaint,
                    (int) Enums.AgencyCommunicationRequestStatus.Approved, "role1")
            });

            context.TenderHistories.AddRange(new List<TenderHistory>()
            {
                new TenderHistory(1, (int) Enums.TenderStatus.OffersAwardedConfirmed, Enums.TenderActions.AcceptOffers,
                    4, "role1")
            });
            context.PlaintRequests.AddRange(new List<PlaintRequest>()
            {
                new PlaintRequest(1, 1, "1", new List<CommunicationAttachmentModel>(), false),
            });

            context.QualificationSupplierData.AddRange(new QualificationDefault().GetQualificationSupplierDataList());

            context.QualificationConfiguration.AddRange(new List<QualificationConfiguration>()
            {
                new QualificationConfiguration(0, 1, 1, 10, 50, 20)
            });
            context.QualificationSupplierProject.AddRange(new QualificationDefault()
                .GetQualificationSupplierProjects());
            context.QualificationSupplierDataYearly.AddRange(new QualificationDefault()
                .GetQualificationSupplierDataYearlyLsit());

            context.QualificationFinalResult.AddRange(
                new QualificationDefault().GetQualificationFinalResult(),
                new QualificationFinalResult(7, "1010000154", 6, (int)Enums.QualificationResultLookup.Succeeded)
                , new QualificationFinalResult(1, "1010000154", 6, (int)Enums.QualificationResultLookup.Failed));


            context.ConfigurationSettings.AddRange(
                new ConfigurationSetting()
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Description = "default Setting"
                }
                ) ;

            context.MandatoryLists.AddRange(new List<MandatoryList>()
            {
               new MandatoryListDefault().GetMandatoryListWithProdcuts()
            });


           
           context.MandatoryListProducts.AddRange(new List<MandatoryListProduct>()
            {
               new MandatoryListDefault().GetMandatoryListProdcut()
            });

            context.OfferlocalContentDetails.AddRange(new List<OfferlocalContentDetails>()
            {
                new OfferlocalContentDetails(){ OfferId = 1 },
                new OfferlocalContentDetails(){ OfferId = 2 }

            });

            context.SaveChanges();
        }
    }
}
