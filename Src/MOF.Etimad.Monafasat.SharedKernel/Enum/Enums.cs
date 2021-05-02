using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public partial class Enums
    {

        public enum LocalContentMechanismPerfermance {
            MechanismForWeighingLocalContentFinancialEvaluation = 1,
            MinimumRequiredMechanismLocalContent = 2,
            ThePricePreferenceMechanismNationalProduct = 3
        }
        public enum TenderType
        {
            NewTender = 1,
            NewDirectPurchase = 2,
            PreQualification = 3,
            LimitedTender = 4,
            ReverseBidding = 5,
            FirstStageTender = 6,
            SecondStageTender = 7,
            PostQualification = 8,
            CurrentTender = 9,
            CurrentDirectPurchase = 10,
            FrameworkAgreement = 11,
            Competition = 12,
            NationalTransformationProjects = 13,
            /// <summary>
            /// Filter Search inSupplier All Tenders
            /// </summary>
            DirectPurchaseTowTypes = 111,
            /// <summary>
            /// Filter Search inSupplier All Tenders
            /// </summary>
            GeneralTernderTwoTypes = 112

        }
        public enum TenderActivityTamplate
        {
            General = 1,
            Roads = 2,
            Buildings = 3,
            MaintananceAndRunning = 4,
            MediacalMaintanance = 5,
            MedicalMaterials = 6,
            Catring = 7,
            Medicines = 8,
            IT = 9,
            EngineeringServicesDesign = 10,
            CitiesCleaning = 11,
            ConsultingServices = 12,
            GeneralMaterials = 13,
            OldSystemAndVRO = 14,
            BayanTemplate = 15,
                  Ration = 20, // إعاشة
            OperationAndMaintenanceMethods = 21, // الصيانة و التشغيل طرق
            GeneralSupply = 22, // توريد عام
            GeneralModel = 23,// نموذج عام
            EngineeringServicesSupervision = 24 //  الخدمات الهندسية (اشراف, )
        }

        public enum ConditionsTemplateCategory
        {
            General = 1, // عام
            RoadConstruction = 2, // إنشاء الطرق 
            BuildingConstruction = 3, // انشاء مباني
            MaintenanceAndOperation = 4, // الصيانة والتشغيل
            MedicalMaintenance = 5, // الصيانة الطبية 
            MedicalSupplies = 6, // المستلزمات الطبية
            Food = 7, // التغذية 
            Pharmaceutical = 8, //  الادوية
            InformationTechnology = 9, // تقنية المعلومات
            EngineeringServicesDesign = 25,  // الخدمات الهندسية (تصميم)
            CleanlinessOfCities = 11, // نظافة المدن
            ConsultingServices = 12, // الخدمات الاستشارية
            GeneralSuppliesSupply = 13, // المستلزمات العامة(التوريد)
            OldSystemAndVRO = 14, // الخدمات الهندسية (اشراف, )
            DataTables = 15, // جداول البيانات
            Ration = 20, // إعاشة
            OperationAndMaintenanceMethods = 21, // الصيانة و التشغيل طرق
            GeneralSupply = 22, // توريد عام
            GeneralModel = 23,// نموذج عام
            EngineeringServicesSupervision = 24, //  الخدمات الهندسية (اشراف, )
            EngineeringServicesDesignAndSupervision = 10  //  ,واشراف الخدمات الهندسية (تصميم)

        }
        public enum ConditionsTemplateSections
        {
            #region Section 8

            WorkForce = 1,
            Materials = 2,
            Equipments = 3,
            MaterialsAdvanced = 4,
            EquipmentAdvanced = 5,
            ImplementaionMethod = 6,
            SafteyDescription = 7,
            QualityDescription = 8,
            ContractBasedOnPerformance = 9,

            #endregion

            #region Section7

            TechnicalDeclerations = 10,
            ProjectWorkScope = 11,
            WorkProgram = 12,
            WorkLocation = 13,
            Outputs = 14

            #endregion
        }
        public enum TenderConditoinsStatus
        {
            GeneralStage = 1,
            PreparteOffers = 2,
            DeliverOffers = 3,
            EvaluateOffers = 4,
            ContractingRequirments = 5,
            TechnicalDeclerations = 6,
            Specifications = 7,
            LocalContent = 8
        }

        public enum LocalContentMechanism
        {
            LocalContentConditionsWeight = 1,
            MinimumBaseline = 2
        }

        public enum VerificationType
        {
            Tender = 1,
            Block = 2,
            AgencyCommunication = 3,
            PrePlanning = 4,
            Negotiation = 5,
            MandatoryList = 6,
            Announcement = 7,
            AnnouncementTemplate = 8

        }
        public enum ConditionTemplateFileType
        {
            Word = 1,
            PDF = 2,
            PowerPoint = 3
        }

        public enum ReasonForLimitedTenderType
        {
            PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers = 1,
            WorksAndPurchasesWithEstimatedValueOfFiveHundredThousandRiyalsOrLess = 2,
            Other = 5
        }

        public enum ReasonForPurchaseTenderType
        {
            WorksAndPurchasesOfWeaponsAndMilitaryEquipmentAndSpareParts = 1,
            BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative = 2,
            BusinessesAndPurchasesLessThanOneHundredThousand = 3,
            UseOfThisMethodNecessaryToProtectNationalSecurityInterests = 4,
            Emergency = 6,
            Other = 7
        }

        public enum OfferPresentationWayId
        {
            OneFile = 1,
            TwoSepratedFiles = 2
        }

        public enum AnnouncementTemplateSuppliersListTypeId
        {
            Public = 1,
            Private = 2
        }





        public enum NotificationType
        {
            AreasChange = 1,
            QuantityTableChange = 2,
            AttachmentsChange = 3,
            FavouriteQuantityTableChange = 4,
            FavouriteTwoDaysRest = 5
        }

        public enum AgencyPlaintStatus
        {
            New = 1,
            Pending = 2,
            Accepted = 3,
            Rejected = 4
        }
        public enum TenderFeesType
        {
            FinantialCostForInvitation = 1,
            FinantialCostForConditionalBooklet = 2,
            ConditionalBookletPrice = 3
        }
        public enum RequestType
        {
            InvitationRequest = 1,
            ConditionalBookletRequest = 2
        }

        public enum InvitationType
        {
            Public = 1,
            Specific = 2
        }

        public enum Durations
        {
            OneDay = 1,
            OneMonth = 1,
            SixMonth = 6
        }

        public enum InvitationRequestType
        {
            Invitation = 1,
            Request = 2
        }
        public enum InvitationStatus
        {
            New = 1,
            Approved = 2,
            Rejected = 3,
            Withdrawal = 4,
            ToBeSent = 5,
            WaitingPayment = 6
        }

        public enum TenderCategory
        {
            ActiveTenders = 2, // المنافسات النشطة
            EndedTenders = 3, // المنافسات المنتهية
        }

        public enum NotifacationStatus
        {
            Send = 1,
            Unsent = 2,
            FailedToSend = 3,
            Read = 4,
            Unread = 5
        }

        public enum StepNames
        {
            DataEntry,
            Open,
            Check,
            Award
        }

        public enum TenderActions
        {
            AcceptInvitation = 1,
            CancelVendorInvitation = 35,
            AcceptOffers = 2,
            AcceptRequestForInvitation = 3, //tender
            Addfile = 4,
            AddUserRole = 8,
            AddVendorToBlacklist = 9,
            Approve = 10,
            WithdrowOffer = 84,
            DeleteOfferFile = 11,
            ApproveAwarding = 12,
            ApproveExtendOfferDate = 13,
            ApproveTenderExtendDates = 206,
            CreateExtendDatesRequest = 209,
            CreateAttachmentsRequest = 213,
            CreateToqRequest = 212,
            ApproveFAQ = 14,
            ApproveOpenEnvelopes = 15,
            ApproveTechnicalEvaluation = 16,
            ApproveTender = 17,
            ApproveTenderCancelation = 18,
            ApproveTOQ = 21,
            ApproveCancelationToq = 23,
            AskQuestion = 24,
            AskForInvitation = 25, //Vendor
            BlockVendorBeacuseOfCRNotes = 27,
            CancelCancellation = 31,
            CancelFile = 32,
            CancelOfferAwarding = 33,
            DeleteFAQInvitation = 76,
            AddFAQInvitation = 106,
            ExtenedTenderDates = 105,// approve extend date\
            OpenEnvelope = 119, // manager open envelop
            OpenEnvelopeFromToq = 119,
            OpenTendersEnvelopes = 120,
            RejectExtendTenderDates = 129,
            RejectInvitation = 130,
            RejectOpenEnvelopes = 131,
            RejectRequestForInvitation = 132,
            RejectTenderCancellation = 133,
            RejecToq = 134,
            RejectCancelationToq = 135,
            RemoveVendorFromBlacklist = 137,
            RequestOfferAwardingApproval = 140,
            RequestTenderCancelation = 141, // طلب إلغاء منافسة
            SendOpenEnvelopeReportForApproval = 145,
            SendTechEvaluationToApproval = 146,
            sendTenderForAwarding = 147,
            SendTenderForAwardingApproval = 148,
            SendForApprovalToq = 149,
            SendForCancelationToq = 150,
            SubmitOffer = 155,
            SubmitTenderForApproval = 156,
            TenderAwardingWasRejected = 164,
            TechnicalEvaluation = 163,
            TenderHasBeenDeleted = 169,
            TenderHasBeenRejected = 170,
            TenderReopenedForAwarding = 172,
            TenderReopenedForEnvelope = 173,
            TenderReopenedForTechnicalEvaluation = 174,
            TenderReopenedForUpdating = 175,
            TenderTechnicalEvaluationWasRejected = 176,
            UNBlockVendorBeacuseOfCRNotes = 177,
            UpdateTender = 178,
            UpdatePreQualification = 221,
            UpdateTenderRegions = 185,
            AddTOQ = 7,
            UpdateToq = 186,
            FillToqTable = 107,
            CancelTOQ = 87,
            CancelAttachements = 210,
            CancelExtend = 75,
            UpdateToqItem = 187,
            DeleteToqItemFile = 89,
            UploadFile = 189,
            UploadTOQFile = 191,
            SendtenderCancelRequest = 208,
            UploadOfferFile = 190,
            CreateTender = 71,
            ApproveTenderFiles = 19, // طلب تعديل الملحقات
            RejectTenderFiles = 204,
            SendTenderFilesToApprove = 207,
            sendExtendDatesToApprove = 101,// ارسال التمديد للاعتماد
            InviteVendors = 116,
            UpdateTechnicalCommittee = 214,
            UpdateOpeningCommittee = 215,
            UpdateCheckingCommittee = 216,
            UpdateDirectPurchaseCommittee = 288,
            UpdatePreQualificationCommittee = 299,
            UpdateVROCommittee = 289,
            PurchaseBooklet = 217,
            DeleteTender = 218,
            UpdateSamplesDeliveryAddress = 219,
            ConvertTenderInvitationToPublic = 220,
            ApproveTenderByUnitManager = 222,
            RejectedFromUnit = 223,
            SendTenderByUnitSecretaryLevelTwoForModification = 224,
            SendTenderByUnitSecretaryForModification = 225,
            CreatePostQualification = 250,
            UpdatePostQualification = 251,
            SendPostQualificationForApprove = 252,
            ApprovePostQualification = 253,
            RejectApprovePostQualification = 254,
            ReopenPostQualification = 255,
            SendPostQulaificationForApproveChecking = 256,
            ApprovePostQualificationChecking = 257,
            RejectPostQulaificationChecking = 258,
            ReopenPostQulaificationChecking = 259,
            SendToUnitForApproval = 226,
            AddNewBiddingRound = 227,
            StartDirectPurchaseTenderCheckingOffers = 265,
            SendDirectPurchaseTenderToApproveChecking = 266,
            SendDirectPurchaseTenderOffersTechnicalCheckingToApprove = 267,
            SendDirectPurchaseTenderOffersCheckingToApprove = 268,
            ApproveDirectPurchaseTenderOffersChecking = 269,
            RejectDirectPurchaseTenderOffersChecking = 270,
            ReopenDirectPurchaseTenderOffersChecking = 271,
            ApproveDirectPurchaseTenderOffersTechnicalChecking = 272,
            RejectDirectPurchaseTenderOffersTechnicalChecking = 273,
            ReopenDirectPurchaseTenderOffersTechnicalChecking = 274,
            SendDirectPurchaseTenderOffersFinanceCheckingToApprove = 275,
            ApproveVROTenderOffersFinanceCheckingAsync = 276,
            RejectDirectPurchaseTenderOffersFinanceChecking = 277,
            ReopenDirectPurchaseTenderOffersFinancialChecking = 278,
            StartOneFileDirectPurchaseTenderChecking = 279,
            SendOneFileDirectPurchaseTenderToAppeoveChecking = 280,
            ApproveCheckgOneFileDirectPurchaseTender = 281,
            RejectCheckOneFilesDirectPurchaseTender = 282,
            ReopenCheckOneFilesDirectPurchaseTender = 283,
            FinancialCheckingStarted = 279,
            SendFinancialEvaluationToApproval = 280,
            ApproveFinancialEvaluation = 281,
            TenderFinancialEvaluationWasRejected = 282,
            TenderReopenedForFinancialEvaluation = 283,
            AddFinancialOfferAttachmentsInDirectPurchase = 284,
            SendAwardTenderToInitialApprove = 285,
            RejectInitialAwardTenderOffer = 286,
            ApproveTenderInitialAwarding = 287,
            StartVROOffersTechnicalChecking = 288,
            SendVROTenderOffersTechnicalCheckingToApproveAsync = 289,
            ReopenVROTenderOffersTechnicalCheckingAsync = 290,
            ApproveVROTenderOffersTechnicalCheckingAsync = 291,
            RejectVROTenderOffersTechnicalCheckingAsync = 292,
            StartVROOffersFinanceChecking = 298,
            SendVROTenderOffersFinanceCheckingToApproveAsync = 293,
            ReopenVROTenderOffersFinancialCheckingAsync = 294,
            RejectVROTenderOffersFinanceCheckingAsync = 295,
            ApproveBidding = 296,
            ReApplyCanciledOffer = 297,
            DownloadBooklet = 300,
            PendingVROAuditerApprove = 301,
            OpenTechnicalEnvelope = 302,
            SendTechnicalOpenEnvelopeReportForApproval = 303,
            SendFinancialOpeningToApproval = 304,
            ApproveFinancialOpening = 305,
            RejectFinancialOpening = 306,
            ReopenFinancialOpening = 307,
            BackForAwardingFromPlaint = 308

        }

        public enum InsideKsa
        {
            [Display(Name = "داخل المملكة")]
            Inside = 1,
            [Display(Name = "خارج المملكة")]
            OutSide = 0
        }

        public enum TenderStatus
        {
            #region [Tender]
            [Display(Name = "UnderEstablishing", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnderEstablishing = 1, // Under Creating Tender
            [Display(Name = "Established", ResourceType = typeof(Resources.TenderResources.Messages))]
            Established = 2, // Tender Had been Created
            [Display(Name = "Pending", ResourceType = typeof(Resources.TenderResources.Messages))]
            Pending = 3, // Pending (Waiting for Approving or Rejection)
            [Display(Name = "Approved", ResourceType = typeof(Resources.TenderResources.Messages))]
            Approved = 4, // Tender Approved
            [Display(Name = "Rejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            Rejected = 5, // Tender Rejected

            // Offers Oppening Stage
            [Display(Name = "OffersOppening", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOppening = 6, // مرحلة فتح العرض
            [Display(Name = "OffersOppenedPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOppenedPending = 7, // Waiting for approval by Open Offer Committee Cheif
            [Display(Name = "OffersOppenedConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOppenedConfirmed = 8, // Approved by Open Offer Committee Cheif
            [Display(Name = "OffersOppenedRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOppenedRejected = 9, // Rejected by Open Offer Committee Cheif


            // Offers Technical Oppening Stage
            [Display(Name = "OffersTechnicalOppening", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalOppening = 78, //   مرحلة فتح العرض الفنية
            [Display(Name = "OffersTechnicalOppeningPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalOppeningPending = 80, // Waiting for approval by Open technical Offer Committee Cheif
            [Display(Name = "OffersTechnicalOppeningConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalOppeningConfirmed = 82, // Approved by Open technical Offer Committee Cheif
            [Display(Name = "OffersTechnicalOppeningRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalOppeningRejected = 84, // Rejected by Open Offer technical Committee Cheif




            //[Display(Name = "OffersOpenningPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            //OffersOpenningPending = 37,

            // Offers Checking Stage 
            [Display(Name = "OffersChecking", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersChecking = 18, // مرحلة فحص العروض   This Status Has Canceled
            [Display(Name = "OffersCheckedPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersCheckedPending = 10, // Waiting for approval by Open Offer Committee Cheif
            [Display(Name = "OffersCheckedConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersCheckedConfirmed = 11, // Approved by Check Offer Committee Cheif
            [Display(Name = "OffersCheckedRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersCheckedRejected = 12, // Rejected by Check Offer Committee Cheif,

            // Offer checking stage for reverse bidding
            [Display(Name = "BiddingOffersCheckedPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            BiddingOffersCheckedPending = 74, // Bidding Waiting for approval by Open Offer Committee Cheif
            [Display(Name = "BiddingOffersCheckedConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            BiddingOffersCheckedConfirmed = 75, // Bidding Approved by Check Offer Committee Cheif
            [Display(Name = "BiddingOffersCheckedRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            BiddingOffersCheckedRejected = 76, // Bidding Rejected by Check Offer Committee Cheif,

            //offers technical checking
            [Display(Name = "OffersTechnicalChecking", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalChecking = 90, //  Offer technical Committee Checking

            [Display(Name = "OffersTechnicalCheckingPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalCheckingPending = 28, // بانتظار اعتماد التقييم الفني
            [Display(Name = "OffersTechnicalCheckingConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalCheckingConfirmed = 29, // تم اعتماد التقييم الفني
            [Display(Name = "OffersTechnicalCheckingRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersTechnicalCheckingRejected = 30, // تم رفض التقييم الفني

            //offers financial checking
            [Display(Name = "OffersOpenFinancialStage", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOpenFinancialStage = 46, // مرحلة فتح العروض المالية
            [Display(Name = "OffersOpenFinancialStagePending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOpenFinancialStagePending = 85, // بانتظار اعتماد تقرير فتح العروض المالية
            [Display(Name = "OffersOpenFinancialStageApproved", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOpenFinancialStageApproved = 86, // تم فتح العروض المالية
            [Display(Name = "OffersOpenFinancialStageRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersOpenFinancialStageRejected = 87, // تم رفض تقرير فتح العروض المالية
            [Display(Name = "OffersFinancialChecking", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersFinancialChecking = 31, // مرحلة تقييم العروض المالية
            [Display(Name = "OffersFinancialOfferCheckingPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersFinancialOfferCheckingPending = 32, //بانتظار اعتماد تقييم العروض المالية
            [Display(Name = "OffersTechnicalCheckingApproved", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersFinancialOfferCheckingApproved = 33, // تم إعتماد تقييم العروض المالية
            [Display(Name = "OffersFinancialOfferCheckingRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersFinancialOfferCheckingRejected = 34, // تم رفض تقييم العروض المالية

            //direct purchase offers checking
            [Display(Name = "DirectPurchaseOffersCheckingPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            DirectPurchaseOffersCheckingPending = 25, // بانتظار تأكيد مرحلة فحص عروض الشراء المباشر
            [Display(Name = "DirectPurchaseOffersChecking", ResourceType = typeof(Resources.TenderResources.Messages))]
            DirectPurchaseOffersChecking = 26, // مرحلة فحص عروض الشراء المباشر
            [Display(Name = "DirectPurchaseOffersCheckingApprovePending", ResourceType = typeof(Resources.TenderResources.Messages))]
            DirectPurchaseOffersCheckingApprovePending = 27, // بانتظار اعتماد فحص عروض الشراء المباشر
            [Display(Name = "DirectPurchaseOffersCheckingApproved", ResourceType = typeof(Resources.TenderResources.Messages))]
            DirectPurchaseOffersCheckingApproved = 35, // تم إعتماد فحص العروض للشراء المباشر
            [Display(Name = "DirectPurchaseOffersCheckingRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            DirectPurchaseOffersCheckingRejected = 36, // تم رفض فحص العروض للشراء المباشر

            // bidding
            [Display(Name = "Bidding", ResourceType = typeof(Resources.TenderResources.Messages))]
            Bidding = 48, // مرحلة المزايدة
            [Display(Name = "BiddingApproved", ResourceType = typeof(Resources.TenderResources.Messages))]
            BiddingApproved = 49, // انتهاء المزايدة المباشرة
            // end bidding


            // Offers Awawrding Stage

            [Display(Name = "OffersAwarding", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersAwarding = 13, // مرحلة ترسية العرض

            [Display(Name = "OffersInitialAwardedPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersInitialAwardedPending = 38, // Waiting for approval by Award Offer Committee Cheif
            [Display(Name = "OffersInitialAwardedConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersInitialAwardedConfirmed = 39, // Approved by Award Offer Committee Cheif
            [Display(Name = "OffersInitialAwardedRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersInitialAwardedRejected = 40, // Rejected by Award Offer Committee Cheif

            [Display(Name = "OffersAwardedPending", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersAwardedPending = 14, // Waiting for approval by Award Offer Committee Cheif
            [Display(Name = "OffersAwardedConfirmed", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersAwardedConfirmed = 15, // Approved by Award Offer Committee Cheif
            [Display(Name = "OffersAwardedRejected", ResourceType = typeof(Resources.TenderResources.Messages))]
            OffersAwardedRejected = 16, // Rejected by Award Offer Committee Cheif

            // Tender Cancel
            [Display(Name = "Canceled", ResourceType = typeof(Resources.TenderResources.Messages))]
            Canceled = 17, //  Tender Canceled

            [Display(Name = "UnitStaging", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnitStaging = 23,
            [Display(Name = "ReturnedToAgencyForEdit", ResourceType = typeof(Resources.TenderResources.Messages))]
            ReturnedFromUnitToAgencyForEdit = 47,

            [Display(Name = "RejectedFromUnit", ResourceType = typeof(Resources.TenderResources.Messages))]
            RejectedFromUnit = 24,
            #endregion
            #region [Qualification status]
            [Display(Name = "DocumentChecking", ResourceType = typeof(Resources.QualificationResources.Messages))]
            DocumentChecking = 19, // مرحلة فحص العرض   This Status Has Canceled
            [Display(Name = "DocumentCheckPending", ResourceType = typeof(Resources.QualificationResources.Messages))]
            DocumentCheckPending = 20, // Waiting for approval by Open Offer Committee Cheif
            [Display(Name = "DocumentCheckConfirmed", ResourceType = typeof(Resources.QualificationResources.Messages))]
            DocumentCheckConfirmed = 21, // Approved by Check Offer Committee Cheif
            [Display(Name = "DocumentCheckRejected", ResourceType = typeof(Resources.QualificationResources.Messages))]
            DocumentCheckRejected = 22, // Rejected by Check Offer Committee Cheif
            [Display(Name = "StartNegotiation", ResourceType = typeof(Resources.TenderResources.Messages))]
            StartNegotiation = 45, //  Tender Canceled

            #endregion
            // Tender Is Pending VRO Auditor Approve
            [Display(Name = "RejectedFromUnit", ResourceType = typeof(Resources.TenderResources.Messages))]
            PendingVROAuditerApprove = 50, //  Pending VRO Auditor Approve
            QualificationUnderEstablishingFromCommittee = 73,
            QualificationCommitteeApproval = 70,
            PendingQualificationCommitteeManagerApproval = 71,
            RejectedQualificationApprovalByCommitteeManager = 72,
            #region VRO Statuses 


            [Display(Name = "VROOffersTechnicalChecking", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersTechnicalChecking = 51,
            [Display(Name = "VROOffersCheckingAndTechnicalEval", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersCheckingAndTechnicalEval = 60,
            [Display(Name = "VROOffersTechnicalCheckingPending", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersTechnicalCheckingPending = 52,
            [Display(Name = "VROOffersTechnicalCheckingRejected", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersTechnicalCheckingRejected = 53,
            [Display(Name = "VROOffersTechnicalCheckingApproved", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersTechnicalCheckingApproved = 58,

            [Display(Name = "VROFinancialCheckingOpening", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROFinancialCheckingOpening = 59,
            [Display(Name = "VROOffersFinancialChecking", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersFinancialChecking = 54,
            [Display(Name = "VROOffersFinancialCheckingPending", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersFinancialCheckingPending = 55,
            [Display(Name = "VROOffersFinancialCheckingApproved", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersFinancialCheckingApproved = 56,
            [Display(Name = "VROOffersFinancialCheckingRejected", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            VROOffersFinancialCheckingRejected = 57,


            [Display(Name = "BackForCheckingFromPlaint", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            BackForCheckingFromPlaint = 92,

            [Display(Name = "BackForAwardingFromPlaint", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
            BackForAwardingFromPlaint = 94,

            #endregion VRO Statuses 
        }

        public enum UserRole
        {
            [Display(Name = "RoleName", ResourceType = typeof(Resources.BranchResources.DisplayInputs))]

            NewMonafasat_DataEntry = 1,
            NewMonafasat_CustomerService = 2,//خدمة عملاء
            NewMonafasat_Auditer = 3,
            NewMonafasat_Supplier = 4,
            NewMonafasat_OffersOpeningManager = 5, //رئيس لجنة فتح العروض 
            NewMonafasat_OffersOpeningSecretary = 6,//سكرتير لجنة فتح العروض
            NewMonafasat_OffersCheckManager = 7, //رئيس لجنة فحص العروض
            NewMonafasat_OffersCheckSecretary = 8,  //سكرتير لجنة فحص العروض
            NewMonafasat_TechnicalCommitteeUser = 9,
            NewMonafasat_AccountManager = 10,
            NewMonafasat_ManagerBlockCommittee = 11,
            NewMonafasat_Admin = 12, // مدير منافسات الجهة
            Anonymous = 13,
            DefaultNotificationSetting = 19,
            NewMonafasat_UnitManager = 20,
            NewMonafasat_UnitSpecialistLevel1 = 21,
            NewMonafasat_UnitSpecialistLevel2 = 22,
            NewMonafasat_ManagerDirtectPurshasingCommittee = 23,
            NewMonafasat_SecretaryDirtectPurshasingCommittee = 24,
            NewMonafasat_ApproveTenderAward = 25,
            NewMonafasat_SecretaryBlockCommittee = 26,
            NewMonafasat_ManagerGrievanceCommittee = 27,
            NewMonafasat_SecretaryGrievanceCommittee = 28,
            NewMonafasat_PreQualificationCommitteeSecretary = 30,
            NewMonafasat_PreQualificationCommitteeManager = 29,
            NewMonafasat_PlanningOfficer = 31,
            NewMonafasat_PlanningApprover = 32,
            NewMonafasat_UnitBusinessManagement = 33,
            NewMonafasat_OffersOpeningAndCheckManager = 34,
            NewMonafasat_OffersOpeningAndCheckSecretary = 35,
            NewMonafasat_EtimadOfficer = 36,
            NewMonafasat_PurshaseSpecialist = 37,
            NewMonafasat_AccountsManagementSpecialist = 38,
            NewMonafasat_ProductionManager = 39,
            NewMonafasat_ProductionManagerDisplay = 40,
            LC_ProductsOfficer = 41,
            LC_ProductsApprover = 42,
            NewMonafasat_LocalContentOfficer = 43,
            NewMonafasat_FinancialSupervisor = 44,
            CR_DirectPurchaseMember = 100,
        }
        public enum NotificationEntityType
        {
            Tender = 1,
            Offer = 2,
            PreQualification = 3,
            PostQualification = 3,
            Block = 4,
            prePlanning = 5,
            MandatoryList = 6,
            Announcement = 7,
        }

        public enum SubscriptionType
        {
            Free = 1,
            Basic = 2,
            Partial150 = 3,
            Partial2000 = 4,
            Full = 5
        }

        public enum UserCommitteeType
        {
            NewMonafasat_TechnicalCommitteeUser = 1,
            NewMonafasat_OffersOpeningSecretary = 2,
            NewMonafasat_OffersOpeningManager = 3,
            NewMonafasat_OffersCheckSecretary = 4,
            NewMonafasat_OffersCheckManager = 5,
            NewMonafasat_ManagerBlockCommittee = 6,
            None = 7,
            NewMonafasat_ManagerDirtectPurshasingCommittee = 8,
            NewMonafasat_SecretaryDirtectPurshasingCommittee = 9,
            NewMonafasat_PreQualificationCommitteeSecretary = 29,
            NewMonafasat_PreQualificationCommitteeManager = 30
        }

        public enum CommitteeType
        {
            TechincalCommittee = 1,
            OpenOfferCommittee = 2,
            CheckOfferCommittee = 3,
            BlockCommittee = 4,
            PurchaseCommittee = 5,
            PreQualificationCommittee = 6,
            VROCommittee = 7
        }

        public enum NotifayType
        {

            OfferStatus = 1,
            InquiriesAboutTender = 2,
            OperationsOnYourAccount = 3,
            OperationsOnTheTender = 4,
            PurchaseInvoiceNumber = 5,
            ChangePassword = 6,
            ForgotYourPassword = 7,
            TenderRelatedToYourActivity = 8,
            ReceiveTheAmountOfTheBooklet = 9,
            AccessCodeOnTheSystem = 11,
            OperationsNeedToBeAccredited = 12,
            DefaultNotify = 13,
        }

        public enum ChangeRequestType
        {
            ExtendDates = 1,
            QuantitiesTables = 2,
            Attachments = 3,
            Canceling = 4
        }

        public enum OperationsNeedsApproval
        {
            ExtendDates = 1,
            QuantitiesTables = 2,
            Attachments = 3,
            Canceling = 4,
            Negotiation = 5,
            PlaintRequest = 6,
            Escalation = 7,
            ExtendOffersValidity = 8,

            NegotiationFirstStage = 9
        }

        public enum AddressType
        {
            OfferOpeningAddress = 2,
            ConditionsBookletDeliveryAddress = 3
        }

        public enum ChangeStatusType
        {
            New = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4
        }

        public enum EnquiryReplyStatus
        {
            New = 0,
            Pending = 1,
            Approved = 2
        }

        public enum OfferStatus
        {
            UnderEstablishing = 1,// تحت الانشاء
            Established = 2,// مكتمل فى انتظار الارسال
            Received = 4,
            Accepted = 5,//  مطابق للتاهيل
            Rejected = 7, //  غير مطابق للتاهيل
            Excluded = 8, // مستبعد
            Canceled = 6, // غير مطابق
            ChangeByQT = 9
        }
        public enum QualificationStatus
        {
            Draft = 1,// مسودة
            Received = 4, // مستلم

        }

        public enum BlockType
        {
            Permenant = 1,
            Tamporary = 2

        }

        public enum OfferTechnicalEvaluationStatusId
        {

            //الفحص الفنى
            IdenticalOffer = 1,     // مطابق
            NotIdenticalOffer = 2   // غير مطابق
        }

        public enum ConitionalBookletPriceList

        {
            LessThanMilion = 999999,
            MoreThanMilion = 1000000,
            [Display(Name = "MustBeEqualOrGreaterThan5000000", ResourceType = typeof(Resources.TenderResources.Messages))]
            MoreThanFifeMilion = 5000000,
            MoreThanTenMilion = 10000000,
            MoreThanTwentyMilion = 20000000,
            MoreThanTwentyFiveMilion = 25000000,
            MoreThanFiftyMilion = 50000000,
        }

        public enum PeriodOfTime
        {
            Daily = 1,
            Weekly = 2,
            Monthly = 3,
            Yearly = 4
        }

        public enum DeleteModule
        {
            Tender = 1,
            QuantityTable = 2,
            Offer = 3,
            Block = 4,
            TechniciansReport = 5,
            TenderAttachementChanges = 6,
            Qualification = 7
        }

        public enum PageSize
        {
            Five = 5,
            Six = 6,
            Ten = 10,
            Fifteen = 15,
            twenty = 20,
            Twelve = 12,
            Eighteen = 18,
            TwentyFour = 24,
            Fifty = 50
        }

        public enum IsDeleted
        {
            Deleted = 1,
            Active = 2
        }

        public enum IsActive
        {
            Active = 1
        }
        public enum OfferAcceptanceStatusId
        {
            AcceptedOffer = 1, //مقبول ماليا 
            RejectedOffer = 2  // غير مقبول ماليا
        }

        public enum QuantityTableChangeStatus
        {
            Add = 1,
            Remove = 2
        }
        public enum QuantityTableReviewStatus
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3
        }

        public enum PreQSupplierDocumentStatus
        {
            NotApplied = 0, // تستخدم فى حاله انه لم يقدم عرض 
            Delivered = 1,// مستلم
        }

        public enum PreQualificationRequestResult
        {
            Matched = 1, // تستخدم فى حاله انه لم يقدم عرض 
            NotMatched = 2,// مستلم
        }

        public enum BillStatus
        {
            /// <summary>
            /// Bill Created
            /// </summary>
            New = 1,
            /// <summary>
            /// Bill Waiting for upload to sadad :: Request Sent Successfully
            /// </summary>
            UnderProcess = 2,
            SuccessUploaded = 3,
            Paid = 4,
            Rejected = 5,
            Expired = 6
        }

        public enum BillActionStatus
        {
            UpdateBill = 1,
            CancelBill = 2,
            SuccessUpdateBill = 3,
            SuccessCancelBill = 4
        }

        public enum BranchAddressType
        {
            MainBranchAddress = 1,
            DeliverOfferAddress = 2,
            OpenOfferAddress = 3,
            DeliverSpecificationBookOfferAddress = 4

        }
        public enum MessageStatus
        {
            Pending = 1,
            Sent = 2
        }

        public enum AgencyType
        {
            /// <summary>
            /// كل الجهات
            /// </summary>
            None = 0,
            /// <summary>
            /// قطاع خاص
            /// </summary>
            PrivateSector = 1,
            /// <summary>
            /// شبه حكومي
            /// </summary>
            SemiGovAgency = 2,
            /// <summary>
            /// حكومي خدمي
            /// </summary>
            GovVendor = 3,
            /// <summary>
            /// جهة حكومية
            /// </summary>
            Agency = 4,
            /// <summary>
            /// مكتب تحقيق رؤية
            /// </summary>
            VRO = 5,

        }

        public enum IDMUserCategory
        {
            //جهة حكومية
            Agency = 1,
            //مالية قطاعات
            MOF = 2,
            //ترشيد الانفاق قطاعات
            ExpenseControl = 3,
            //ديوان الرقابة
            AuditCourt = 4,
            //وحدة التدخل السريع
            DeliveryAndRapidInterventionCenter = 5,
            // قطاع خاص
            PrivateSector = 6,
            //مالية رقابة تنظيمات
            MOFAudit = 7,
            //جهة مستفيدة
            GovVendor = 8,
            //
            CustomerCare = 9,
            //تحقيق الرؤية تنظيمات
            VRO = 10,
            // العمل لدائم قطاعات
            PWT = 11,
            // المحتوى قطاعات
            LocalContent = 12,
            // فريق تشغيل إعتماد
            EtimadOperationTeam = 14,
            // قطاع خاص لا يملك سجل تجاري
            PrivateSectorHasNotCR = 15,
            // وحدة الشراء الإستراتيجي
            StrategicPurchasingUnit = 16
        }
        public enum TableChangeStatus
        {
            // إضافة جدول جديد
            Add = 1,
            // تعديل على جدول موجود
            Edit = 2,
            // حذف جدول
            Delete = 3
        }
        public enum SupplierInfoType
        {
            Success = 1,
            Warnning = 2,
            Wrong = 3
        }

        public enum ISICActivityLevel
        {
            /// <summary>
            /// main activity
            /// </summary>
            ISICActivityParent = 1,
            /// <summary>
            /// second level activity
            /// </summary>
            ISICActivityChild = 2,
            /// <summary>
            /// third level activity
            /// </summary>
            ISICActivityGrandChild = 3,
        }

        public enum SupplierType
        {
            PrivateSector = 1,// قطاع// خاص
            SemiGovAgency = 2,// شبه حكومي
            GovVendor = 3,// حكومي خدمي
            PrivateSectorHasNoCR = 4// قطاع خاص لا يملك سجل تجاري
        }
        public enum AssignedRoleLevelType
        {
            NotAssigned = 0,
            Branch = 1,
            Committee = 2
        }

        public enum AgencyCommunicationRequestType
        {
            Plaint = 1,
            Negotiation = 2,
            SupplierOfferExtendDates = 3,
            ExtendOfferValidtiy = 4,
            Enquiry = 5
        }

        public enum RequestRejectionType
        {
            Plaint = 1,
            Escalation = 2,
        }
        public enum AgencyCommunicationRequestStatus
        {
            UnderUpdate = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4,
            UnderNegotiation = 10,
            Finished = 11,
            PendingOnPlaintManager = 5,
            ApprovedByPlaintManager = 6,
            RejectedByPlaintManager = 7,
            RequestSent = 8,
            PendingEnquiryForReply = 9,
            ReplyOnEnquiry = 10,
            Ended = 11,
            UnderRevision = 12,
            TenderExtendedDates = 13
        }

        public enum AwardingConstants
        {
            AwaredingStoppingPeriod = 5
        }

        public enum TenderPlaintRequestProcedure
        {
            ReOpenTenderChecking = 1,
            ReOpenTenderAwarding = 2,
            Other = 3
        }
        public enum NegotiationType
        {
            FirstStage = 15,
            SecondStage = 10
        }
        public enum BlockStatus
        {
            NewAdmin = 1,
            NewSecretary = 2,
            ApprovedSecertary = 3,
            RejectedSecertary = 4,
            ApprovedManager = 5,
            RejectedManager = 6,
            RemoveBlock = 7
        }

        public enum YearQuarters
        {
            FirstQuarters = 1,
            SecondQuarters = 2,
            ThirdQuarters = 3,
            FourthQuarters = 4
        }

        public enum PrePlanningStatus
        {
            UnderUpdate = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4
        }

        public enum TenderUnitStatus
        {
            [Display(Name = "WaitingUnitSecretaryReview", ResourceType = typeof(Resources.TenderResources.Messages))]
            WaitingUnitSecretaryReview = 1,
            [Display(Name = "UnderUnitReviewLevelOne", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnderUnitReviewLevelOne = 6,
            [Display(Name = "TenderTransferdToLevelTwo", ResourceType = typeof(Resources.TenderResources.Messages))]
            TenderTransferdToLevelTwo = 8,
            [Display(Name = "UnderUnitReviewLevelTwo", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnderUnitReviewLevelTwo = 9,
            [Display(Name = "ReturnedToAgencyForEdit", ResourceType = typeof(Resources.TenderResources.Messages))]
            ReturnedToAgencyForEdit = 7,
            [Display(Name = "UnderReviewing", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnderReviewing = 2,
            [Display(Name = "WaitingManagerApprove", ResourceType = typeof(Resources.TenderResources.Messages))]
            WaitingManagerApprove = 3,
            [Display(Name = "UnderManagerReviewing", ResourceType = typeof(Resources.TenderResources.Messages))]
            UnderManagerReviewing = 10,
            [Display(Name = "ApprovedByManager", ResourceType = typeof(Resources.TenderResources.Messages))]
            ApprovedByManager = 4,
            [Display(Name = "RejectedByManager", ResourceType = typeof(Resources.TenderResources.Messages))]
            RejectedByManager = 5,
        }
        public enum UnitSpecialistLevel
        {
            UnitSpecialistLevelOne = 1,
            UnitSpecialistLevelTwo = 2,
            UnitManager = 3
        }

        public enum SupplierExtendOffersValidityStatus
        {
            Sent = 1,
            UnderProcessing = 5,
            Accepted = 10,
            AcceptedInitially = 16,
            Rejected = 15
        }
        public enum NegotiationFirstStageRejectionReasons
        {
            HighPriceThanMarket = 1,
            HighPriceThanBudget = 2
        }

        public enum SupplierSolidarityStatus
        {
            New = 1,
            Approved = 2,
            Rejected = 3,
            ToBeSent = 4
        }

        public enum InvitationSendType
        {
            Mobile = 1,
            Email = 2
        }

        #region ApplyOffer
        public enum enAjaxResponseMessageType
        {
            success = 1,
            warnning = 2,
            danger = 3
        }
        public enum AttachmentType
        {
            TenderBookletAttachment = 1,
            QuantityTableAttachment = 2,
            OfferFile = 3,
            GuaranteeLetter = 4,
            TenderPurchaseEnvoice = 5,
            CertificateOfVisitation = 6,
            ClassificationCertificate = 7,
            VATCertificate = 8,
            Socialinsurancecertificate = 9,
            SaudizationCertificate = 10,
            CommercialRegister = 11,
            ChamberofCommerceCertificate = 12,
            OfferLetter = 13,
            OfferCopy = 14,
            SupplierOriginalAttachment = 15,
            TenderOtherFile = 16,
            SupplierCombinedAttachment = 17,
            SupplierFinancialProposalAttachment = 18,
            SupplierTechnicalProposalAttachment = 19,
            PlainRequest = 20,
            InitialGuarantee = 22,
            UnitModificationsAttachmentsToDataEntry = 23,
            Negotiation = 24,
            Escalation = 25,
            TechnicianReport = 26,
            SupplierTechnicalAndFinancialProposalAttachment = 27,
            LocalContent = 28 
        }

        public enum enCombinedSupplierinviteWay
        {
            /// <summary>
            /// تمت الدعوه بالسجل
            /// </summary>
            CR = 1,
            /// <summary>
            /// الدعوه بالايميل
            /// </summary>
            Email = 2,
            /// <summary>
            /// الدعوه برقم الموبايل
            /// </summary>
            Mobile = 3
        }
        public enum AgreementType
        {
            Closed = 1,
            Opened = 2
        }

        public enum CombinedType
        {
            Registered = 1
                , Unregistered = 2
                , Owner = 3

        }

        #endregion

        #region [Negotiation Status]
        public enum enNegotiationStatus
        {
            New = 3,
            UnderUpdate = 5,
            CheckManagerPendingApprove = 10,
            CheckManagerReject = 15,
            UnitSpecialestPendingApproved = 20, //Approved from CheckManager
            UnitSpecialistReject = 25,
            SupplierAgreed = 35,
            SupplierNotAgreed = 40,
            SentToSuppliers = 45,
            SupplierAgreedWithExtraDiscount = 50,

        }

        public enum enNegotiationFirstStageRejectionReasons
        {
            HighPriceThanMarket = 1,
            HighPriceThanBudget = 2
        }

        /// <summary>
        /// please keep variance to be add between(5)
        /// </summary>
        public enum enNegotiationType
        {
            FirstStage = 15,
            SecondStage = 10,


        }
        public enum enNegotiationSupplierStatus
        {
            NotSent = 5,
            PendeingSupplierReply = 10,
            Agree = 15,
            DisAgree = 20,
            NoReply = 25,
            AgreeWithExtraDiscount = 30,

        }
        #endregion
        public enum BiddingRoundStatus
        {
            Started = 1,
            Stopped = 2,
            Approved = 3,
            New = 4
        }
        public enum TenderUnitUpdateType
        {
            MainInformation = 1,
            Dates = 2,
            RelationStep = 3,
            TenderActivities = 4,
            QuantityTables = 5,
            Attachments = 6,
            LocalContent = 7
        }

        public enum enOperationType
        {
            Add = 1,
            Update = 2,
            Delete = 3
        }
        public enum enSubmitActionType
        {
            SaveAndSend = 1,
            Preview = 2,
            SaveOnly = 3,
            Submit = 4,
            SaveAndSendSecondNegotiation = 5

        }
        public enum TenderCreatedByType
        {
            VRO = 1,
            AgenciesRelatedByVRO = 2,
            ExceptionalAgencies = 3,
        }
        public enum UnRegisteredSuppliersInvitationType
        {
            Existed = 1,
            Foriegn = 2,
            HasCR = 3,
            HasLicience = 4,
            SolidarityLeader = 5
        }

        public enum InvititedSupplierTypes
        {
            HasCR = 1,
            Foriegn = 2,
            HasLicience = 3,
            Other = 4

        }

        public enum SolidarityInvitationType2
        {
            Leader = 1,
            RegisteredSupplier = 2,
            HasCR = 3,
            HasLicience = 4,
            ForignSupplier = 5
        }

        public enum ReferenceNumberType
        {
            Monafasat = 39,
            Qualification = 40,
            Announcement = 41,
            AnnouncementTemplate = 42,
        }

        #region QualificationEnums

        public enum QualificationLookupNames
        {
            QualityGuarantie = 1,
            EnvironmentalAndHealthInsurance = 2,
            AvaiableQuestion = 3,
            AcceptAndRejectQuestion = 4,
            RateOfProfitability = 5,
            ResultOfRehabilitation = 6
        }
        public enum QuantityTableRowType
        {
            Available = 1,
            NotAvailable = 2,
            Free = 3
        }


        /// <summary>
        ///  ids from db 
        /// </summary>
        public enum Points
        {
            OnePoint = 1,
            ThreePoint = 3,
            SevenPoint = 7
        }

        public enum QualificationItemCategory
        {
            Technical = 1,
            Financial = 2

        }

        public enum QualificationItemType
        {
            Select = 1,
            Range = 2,
            Percentage = 3,
            Value = 4
        }

        #region Qualification lookups

        public enum QualificationQualityGuaranteeLookup
        {
            [Display(Name = "IsoCertificate", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            IsoCertificate = 1,
            [Display(Name = "QualityGuarantee", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            QualityGuarantee = 2,
            [Display(Name = "GuaranteeNotAvailible", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            GuaranteeNotAvailible = 3

        }

        public enum QualificationEnvironmentStandardsLookup
        {
            [Display(Name = "OSHA", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            OSHA = 4,
            [Display(Name = "EnvironmentGuide", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            EnvironmentGuide = 5,
            [Display(Name = "EnvironmentGuideNotAvailible", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            EnvironmentGuideNotAvailible = 6
        }

        public enum QualificationAvailibleNotAvailibleLookup
        {
            [Display(Name = "Availible", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Availible = 7,
            [Display(Name = "NotAvailible", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            NotAvailible = 8
        }

        public enum QualificationYesOrNoLookup
        {
            [Display(Name = "Yes", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Yes = 9,
            [Display(Name = "No", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            No = 10
        }

        public enum QualificationProfitDirAverageLookup
        {
            [Display(Name = "High", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            High = 11,
            [Display(Name = "Stable", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Stable = 12,
            [Display(Name = "Low", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Low = 13
        }

        public enum QualificationResultLookup
        {
            [Display(Name = "Succeeded", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Succeeded = 14,
            [Display(Name = "Failed", ResourceType = typeof(Resources.QualificationResources.DisplayInputs))]
            Failed = 15
        }

        #endregion Qualification lookups


        public enum QualificationConfig
        {
            PointToPass = 7,
            WeigthDefaultValue = 1
        }

        public enum QualificationYear
        {
            CurrentYear = 1,
            SecondYear = 2,
            ThirdYear = 3,
        }

        public enum PreQualificationType
        {
            Small = 1,
            Medium = 2,
            Large = 3,
        }

        public enum QualificationEvaluationItems
        {
            NumberOfYearsOfExperience = 1, // عدد سنوات الخبرة في مجال طلب التأهيل
            NumberOfProjectsImplementedLastThreeYears = 2, // عدد المشاريع المنفذة خلال الثلاث سنوات الأخيرة في مجال طلب التأهيل
            TotalValueProjectsLastThreeYears = 3, // إجمالي قيمة المشاريع خلال الثلاث سنوات الأخيرة في مجال طلب التأهيل            
            QualityAssuranceStandards = 6, // ما هي معايير ضمان الجودة
            EnvironmentalHealthSafetyStandards = 7, // ما هي معايير ضمان البيئة والصحة والسلامة
            NumberOfExistingProjects = 8, // عدد المشاريع القائمة
            ValueOfExistingProjects = 9, // قيمة المشاريع القائمة
            NumberOfEmployees = 10, // عدد الموظفين
            NumberOfSaudiEmployees = 11, // عدد الموظفين السعوديين
            PercentageOfSaudiEmployees = 12, // نسبة الموظفين السعوديين
            InsuranceOfProfessionalCompensation = 13, // تأمين التعويض المهني
            LiabilityInsurance = 14, // تأمين المسؤولية ضد الغير
            InsuranceOfGeneralCommercialResponsibility = 15, // تأمين المسؤولية التجارية العامة
            CashEquivalents = 16, // النقدية ومكافئات النقدية
            AccountsReceivable = 17, // الحسابات مستحقة القبض
            CurrentLiabilities = 18, // الالتزامات المتداولة
            CashRate = 19, // نسبة النقدية (النقدية ومكافئات النقدية\الالتزامات المتداولة)
            LiquidityRatio = 20, // نسبة السيولة السريعة ((النقدية ومكافئات النقدية+الحسابات المستحقة القبض)/الالتزامات المتداولة)
            TradingRatio = 21, // نسبة التداول (الأصول المتداولة\الالتزامات المتداولة)
            ConfirmAbilityToSubmitLastThreeyearsAuditedFinancialStatements = 22, // يرجى تأكيد القدرة على تقديم آخر 3 سنوات من البيانات المالية المدققة، إذا طلب من الجهة
            CurrentAssets = 23, // الموجودات المتداولة او الاصول المتداولة
            TotalAssets = 24, // مجموع الموجودات
            TotalLiabilities = 25, // مجموع المطلوبات
            TotalRevenue = 26, // مجموع الإيرادات
            NetProfit = 27, // صافي الأرباح
            RatioOfObligations = 28, // نسبة الالتزامات (مجموع المطلوبات\مجموع الموجودات)
            RateOfProfitability = 29,  //معدل التغيير التراكمي لمعدل الربحية,
            Insurance = 30

        }

        public enum QualificationSubCategory
        {
            PreviousExperienceYear = 1,
            ExistingContractualObligations = 2,
            HumanResource = 3,
            Quality = 4,
            EnviromentAndHealthy = 5,
            Insurance = 6,
            FinancialStatements = 7,
            FinancialPerformanceIndicators = 8,
            BalanceSheetStatement = 9,
            IncomeList = 10

        }
        public enum AttachmentTypes
        {
            // شهادة الزكاة
            ZakatCertificate = 1,
            // شهادة الضريبة
            TaxCertificate = 2,
            // التأمينات الاجتماعية
            SocialSecurity = 3,
            // شهادة السعودة
            SaudizationCertificate = 4,
            // رخصة البلدية
            ShopCertificate = 5,
            // السجل التجاري
            CommercialRegistration = 6,
            // تصنيف المقاولين
            ContractorsClassification = 7,
            // شهادة الانتساب للغرفة التجارية
            EnrollmentCertificateForChamberOfCommerce = 8,
            // رخصة الاستثمار
            InvestmentLicense = 9,
            // شهادة انتساب إلى الهيئة السعودية للمقاولين
            EnrollmentCertificateForContractorsSaudiCommitte = 10,
            // شهادة انتساب إلى الهيئة السعودية للمهندسين
            EnrollmentCertificateForEngineersSaudiCommitte = 11,
            // شهادة من هيئة المنشآت الصغيرة والمتوسطة
            SmallMediumInstitutionsCommitteCertificate = 12,
            // رخصة محاماة
            LawFirmLicense = 13,
            // مرفقات داعمة
            SupportDocuments = 14,
        }
        #endregion

        #region IDMEnums

        public enum IDMInstitutionTypeEnum
        {
            Company = 1,
            Orgnization = 2,
            Other = 3
        }

        #endregion

        public enum FormCategoryEnum
        {
            Workforce = 1,
            Material = 2,
            Equipment = 3,
            Services = 4
        }

        public enum ColumnTypeEnum
        {
            Title = 1,
            Serial = 2,
            Data = 3,
            Price = 4,
            TotalCost = 5,
            TotalCostAfterDiscount = 6,
            Description = 7,
            Attachment = 8,
            RowType = 9,
            MandatoryList = 10,
            IsMandatoryList = 11,
            Countries = 12,
            IsVerifiedMandatoryList = 13,
            Name = 14,
            Unit = 15,
            VAT = 16,
            DiscountPercentage = 17,
            Quantity = 18
        }

        public enum ColumnValueEnum
        {
            Name = 1,
            Unit = 2,
            VAT = 3,
            DiscountPercentage = 4,
            Description = 5,
            Price = 6,
            Quantity = 7,
            ItemId = 8
        }


        public enum MandatoryListStatus
        {
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.UnderEstablishing), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            UnderEstablishing = 1,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.WaitingApproval), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            WaitingApproval = 2,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.Rejected), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            Rejected = 3,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.Approved), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            Approved = 4,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.WaitingCancelApproval), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            WaitingCancelApproval = 5,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.CancelRejected), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            CancelRejected = 6,
            [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.Canceled), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
            Canceled = 7
        }

        public enum MandatoryListChangeRequestStatus
        {
            WaitingApproval = 1,
            Rejected = 2,
            Approved = 3,
            Closed = 4,
        }
        public enum AwardingReturnType
        {
            HasNoPreQualification = 1,
            HasPendingPreQualification = 2,
            HasExpiredPreQualification = 3,
            SupplierFailedInPreQualification = 4,
            SupplierHasNoPostQualification = 5,
            HasPendingPostQualification = 6,
            HasApprovedPostQualification = 7,
            SupplierPassedInPostQualification = 8,
            SupplierFailedInPostQualification = 9,
            NoPreQualificationNoPostQualificatoin = 10,
            HasValidPreQualification = 11
        }

        public enum QuantityTableVersion
        {
            Version1 = 1,
            Version2 = 5000
        }
        public enum AwardingHistoryIndex
        {
            One = 1,
            Two = 2
        }

        public enum AnnouncementStatus
        {
            UnderCreation = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4,
            Ended = 5
        }

        public enum AnnouncementSupplierTemplateStatus
        {
            UnderCreation = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4,
            Ended = 5,
            ReadyForApproval = 6,
            Canceled = 7
        }
        public enum AnnouncementSupplierTemplateInsertionType
        {
            FromSaveDraft = 1,
            FromCreation = 2,
        }


        public enum AnnouncementPeriodRange
        {
            Min = 10,
            Max = 999
        }
        public enum AnnouncementJoinRequestStatus
        {
            Sent = 1,
            WithDraw = 2

        }
        public enum AnnouncementSuppliers
        {
            One = 1,
            Five = 5

        }


        public enum QTSStatusCode
        {
            OK = 200,
            NoContent = 204,
            Unauthorized = 401,
            Exception = 405,
            ExcutionFailed = 417

        }

        public enum TimeMessagesType
        {
            TimeOfferChecking = 1,
            LastOfferPresentationDate = 2,
            Qualification = 3,
            TimeOffersOpeningDate = 4,
            SamplesDeliveryTime = 5,
            OffersDeliveryTime = 6


        }

        public enum TenderBudget
        {
            LowBudget = 30000
        }

        public enum AnnouncementTemplateJoinRequestStatus
        {
            Sent = 1,
            PendingAcceptance = 2,
            PendingRejection = 3,
            Accepted = 4,
            Rejected = 5,
            Withdrawn = 6,
            Deleted = 7

        }

        public enum AnnouncementTemplateJoinRequestResult
        {
            Accepted = 1,
            Rejected = 2
        }
        public enum BankGuaranteeType
        {
            Primary = 1,
            Final = 2,
        }

        public enum VersionType
        {
            TenderActivity = 1,
        }

        public enum ActivityTemplate
        {
            General = 1,
            ConsultingServices = 12,
            Tawreed = 13,
            E3asha = 20,
            TawreedActivityVersion4 = 22
        }

        public enum ActivityVersions
        {
            OldActivities = 3,
            Sprint7Activities = 4
        } 
        public enum ConfigurationSetting
        {
            LocalContent = 1
        }
         
    }
}
