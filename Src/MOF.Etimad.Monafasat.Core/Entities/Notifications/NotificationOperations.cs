namespace MOF.Etimad.Monafasat.Core.Entities
{
    public static class NotificationOperations
    {
        public static class Auditor
        {
            public static class OperationsOnTheTender
            {
                public static int approvedtender => 29;
                public static int tenderwaitingcancelation => 32;
                public static int sendforcancelationtoq => 33;
                public static int sendforapprovaltoq => 34;
                public static int publishtenderfile => 38;
                public static int approvetenderextenddate => 39;
                public static int removetenderattachment => 43;
                public static int approvetenderattachment => 45;
                public static int vendorsubmitoffer => 158;
                public static int canceltender => 175;
                public static int editTenderFromUnit => 68;
                public static int approveTenderByUnit => 688;
                public static int rejectTenderByUnit => 690;
                public static int ReciveJoinRequest => 570;

            }
            public static class InquiriesAboutTender
            {
                public static int publishfaqanswerbackend => 137;
            }

            public static class OperationsOnQualification
            {
                public static int QualificationWaitingcancelation => 2010;

            }

            public static class OperationsNeedApprove
            {
                public static int approvaloperation => 101;
                public static int tenderwaitingdateextend => 132;
                public static int submittenderforapproval => 136;
                public static int submitPrequalificationforapproval => 670;
            }
            public static class AgencyCommunicationRequest
            {
                public static int SendOffersPostponeRequest => 730;
                public static int SendOffersPostponeRequestForQulification => 953;
            }

            public static class AnnouncementOperations
            {
                public static int SendAnnouncementForApproval => 6000;
            }
        }

        public static class PurshaseSpecialist
        {
            public static class OperationsOnTheTender
            {

                public static int publishopenenvelopesbackend => 919;
                public static int approvetenderawarding => 920;
                public static int removetenderattachment => 921;
                public static int approvedtender => 922;
                public static int sendrelatedvrotender => 923;
                public static int approvetenderattachment => 925;
                public static int approvetoq => 926;
                public static int canceltender => 927;
                public static int rejectopenenvelope => 928;
                public static int rejecttechnicalevaluation => 929;
                public static int rejecttender => 930;
                public static int rejecttenderawarding => 931;
                public static int rejecttendercancellation => 932;
                public static int rejecttoq => 933;
                public static int rejecttenderextenddate => 934;
                public static int joinrequest => 935;
                public static int approvetenderextenddate => 936;
            }
            public static class InquiriesAboutTender
            {
                public static int publishfaqanswerbackend => 937;
            }
            public static class AgencyCommunicationRequest
            {
                public static int SendOffersPostponeRequest => 1102;
            }
        }

        public static class EtimadOfficer
        {
            public static class OperationsOnTheTender
            {
                public static int approvedtender => 909;
                public static int sendrelatedvrotender => 924;
                public static int tenderwaitingcancelation => 910;
                public static int sendforcancelationtoq => 911;
                public static int sendforapprovaltoq => 912;
                public static int publishtenderfile => 913;
                public static int approvetenderextenddate => 914;
                public static int removetenderattachment => 915;
                public static int approvetenderattachment => 916;
                public static int vendorsubmitoffer => 917;
                public static int canceltender => 918;

            }

            public static class InquiriesAboutTender
            {
                public static int publishfaqanswerbackend => 938;
            }
            public static class OperationsNeedApprove
            {
                public static int approvaloperation => 905;
                public static int tenderwaitingdateextend => 939;
                public static int sendopenenvelopesreportforapproval => 940;
                public static int submittenderforapproval => 941;
            }
            public static class AgencyCommunicationRequest
            {
                public static int SendOffersPostponeRequest => 908;
            }
        }



        public static class DataEntry
        {
            public static class OperationsOnTheTender
            {
                public static int publishopenenvelopesbackend => 46;
                public static int approvetenderawarding => 165;
                public static int removetenderattachment => 171;
                public static int approvedtender => 259;
                public static int sendrelatedvrotender => 1022;

                public static int approvedprequalification => 675;
                public static int ReciveJoinRequest => 569;

                public static int approvetenderattachment => 263;
                public static int approvetoq => 265;
                public static int canceltender => 266;
                public static int rejectopenenvelope => 268;
                public static int rejecttechnicalevaluation => 269;
                public static int rejecttender => 271;
                public static int rejecttenderawarding => 272;
                public static int rejecttendercancellation => 273;
                public static int rejecttoq => 274;
                public static int rejecttenderextenddate => 510;
                public static int joinrequest => 666;
                public static int approvetenderextenddate => 3;
                public static int sendprequalificationdocs => 682;
                public static int editTenderFromUnit => 684;
                public static int approveTenderByUnit => 687;
                public static int rejectTenderByUnit => 689;
                public static int sentToUnitForReview => 882;

                public static int vendorsubmitoffer => 7880;
            }




            public static class OperationsNeedApprove
            {
                public static int approvaloperation => 888;

            }


            public static class InquiriesAboutTender
            {
                public static int publishfaqanswerbackend => 141;
            }

            public static class OperationsOnQualification
            {

                public static int ApproveQualificationextenddate => 950;

                public static int RejectPreQualification => 680;
                public static int approveQualificationCancleRequest => 2005;

                public static int RejectQualificationCancleRequest => 2011;

            }
            public static class AgencyCommunicationRequest
            {
                public static int SendOffersPostponeRequest => 1100;
                public static int SendOffersPostponeRequestForQulification => 1101;
            }

            public static class AnnouncementOperations
            {
                public static int ApproveAnnouncement => 6100;
                public static int RejectApproveAnnouncement => 6200;
            }
        }

        public static class OffersOppeningManager
        {
            public static class OperationsOnTheTender
            {
                public static int canceltender => 159;
                public static int publishopenenvelopesbackend => 162;
                public static int tenderwaitingcancelation => 500;
            }
            public static class TransactionsNeedApproval
            {
                public static int sendopenenvelopesreportforapproval => 285;
                public static int sendTechnicalopenenvelopesreportforapproval => 7900;
                public static int approvaloperation => 289;

                public static int sendFinancialOpeningForApproval => 8006;
            }
        }

        public static class OffersOppeningSecretary
        {
            public static class OperationsOnTheTender
            {
                public static int canceltender => 160;
                public static int publishopenenvelopesbackend => 178;
                public static int publishopenTechnicalenvelopesbackend => 8002;
                public static int rejectopenenvelope => 302;
                public static int rejectTechnicalopenenvelope => 8000;
                public static int rejecttendercancellation => 317;
                public static int OffersWillOpenTomorrow => 818;
                public static int ApproveFinancialOpening => 8008;
                public static int RejectFinancialOpening => 8012;
                public static int publishtechnicalevaluation => 555;
            }
        }



        public static class GrievanceSecretary
        {
            public static class AgencyCommunicationRequest
            {
                public static int PlaintEsclationCreated => 773;
                public static int PlaintEsclationRejected => 770;
                public static int PlaintEsclationApproved => 768;

            }
        }
        public static class GrievanceManager
        {
            public static class AgencyCommunicationRequest
            {
                public static int PlaintEsclationSentForApproval => 765;

            }
        }

        public static class Supplier
        {
            public static class AgencyCommunicationRequest
            {
                public static int SendExtendOffersToSuppliers => 721;
                public static int RejectOffersPostponeRequest => 733;
                public static int RejectQualificationPostponeRequest => 732;
                public static int SendNegotiationToSupplier => 746;
                public static int SendSecondNegotiationToSupplier => 1033;
                public static int DisApproveNegotiationFirstSuppliers => 805;
                public static int PlaintRejectedToSupplier => 752;
                public static int PlaintApprovedToSupplier => 7884;
                public static int PlaintEsclationApprovedToSupplier => 755;
                public static int PlaintEsclationRejectedToSupplier => 756;
            }
            public static class OperationsOnTheTender
            {
                public static int approvetenderextenddate => 51;
                public static int publishopenenvelopes => 52;
                public static int publishtechnicalopenenvelopes => 8004;
                public static int publishtechnicalevaluation => 53;
                public static int AddNewRound => 7887;
                public static int invitevendorstotender => 55;
                public static int addtenderattachment => 56;
                public static int AcceptAnnouncementJoinRequest => 567;
                public static int RejectAnnouncementJoinRequest => 568;
                public static int DeleteSupplierFromAnnouncementList => 571;
                public static int unblockvendorbecauseofcrnotices => 100;
                public static int canceltenderapproved => 114;
                public static int SupplierTechnicalOfferRejected => 1993;
                public static int rejectvendorrequest => 116;
                public static int sadadgeneratedbill => 129;
                public static int approvetoq => 131;
                public static int deletevendoroffer => 143;
                public static int acceptvendoroffer => 144;
                public static int acceptvendorrequest => 149;
                public static int sadadpaymentnotification => 155;
                public static int approvetoqwithoutoffer => 320;
                public static int sendprequalificationdocs => 681;
                public static int vendorawardingtender => 54;
                public static int VenderIgnoredFromAwarding => 2049;
                public static int SupplierAwardedNotification => 747;
                public static int SupplierAwardedNotificationNoGurantee => 748;
                public static int RejectCombineRequest => 806;
                public static int ExclusionSupplierFromAwarding => 2075;
                public static int financialOffersOpeningApproved => 8010;
                public static int SupplierFinacialOffersAccepted => 1995;
                public static int SupplierFinacialOffersRejected => 1996;

            }

            public static class InquiriesAboutTender
            {
                public static int publishfaqanswer => 142;
                public static int publishfaqanswerforQualification => 2071;
            }

            public static class OperationsOnPrequalification
            {
                public static int ApproveQualificationExtendDate => 952;
                public static int approvecheckprequalification => 792;
                public static int rejectcheckprequalification => 795;
                public static int ApproveAttachmentChanges => 777;
            }
            public static class OperationsOnPostqualification
            {
                public static int ApprovePostQualification => 704;
                public static int ApplyPostQualificationDocuments => 708;
                public static int AcceptingPostQualificationDocuments => 709;
                public static int RejectingPostQualificationDocuments => 710;

                public static int QualificationCancle => 2014;
            }


            public static class Solidarity
            {
                public static int SendSolidarityInvitation => 825;

                public static int AcceptSolidarityInvitation => 826;
                public static int RejectSolidarityInvitation => 827;


            }

        }

        public static class TechnicalCommitteeSecretary
        {
            public static class InquiriesAboutTender
            {
                public static int publishfaqanswerbackend => 139;
                public static int vendoraskquestion => 138;
                public static int changetendergatid => 140;

                public static int PublishfaqanswerforQualification => 2070;
            }
        }

        public static class UnitManager
        {

            public static class OperationsOnTheTender
            {
                public static int sendToUnitManagerToApprove => 686;
                public static int SendVerificationCode => 7898;

            }
        }

        public static class UnitSecrtaryLevel1
        {
            public static class OperationsOnTheTender
            {
                public static int rejectTenderFromUnitManager => 691;
                public static int reviewTender => 821;
                public static int offerCheckingDateSet => 785;
                public static int sendTenderToAnOtherSpecialist => 787;
                public static int SendVerificationCode => 7999;
            }
            public static class AgencyCommunicationRequest
            {
                public static int SendNegotiationRequestToApprove => 1025;
            }
        }

        public static class UnitSecrtaryLevel2
        {
            public static class OperationsOnTheTender
            {
                public static int rejectTenderFromUnitManager => 692;
                public static int reviewTender => 693;
                public static int offerCheckingDateSet => 7860;
                public static int sendTenderToAnOtherSpecialist => 7861;
                public static int SendVerificationCode => 7888;
            }
            public static class AgencyCommunicationRequest
            {
                public static int SendNegotiationRequestToApprove => 1026;
            }
        }

        public static class BlockSecrtary
        {
            public static class OperationsToBeApproved
            {
                public static int ApproveBlockSecretary => 744;
                public static int ApproveBlockFromManagerToSecretary => 2020;
                public static int sendRejectBlockToSecretary => 2026;



            }
        }

        public static class BlockMonafasatAdmin
        {
            public static class OperationsRequest
            {
                public static int SendRequestBlockFromMonafasatAdminToBlockSecrtary => 2023;
                public static int sendRejectBlockToMonafasatAdmin => 2024;


            }
        }

        public static class BlockManager
        {
            public static class OperationsToBeApproved
            {
                public static int ApproveBlockManager => 745;
                public static int ApproveVerificationBlockManager => 900;
                public static int sendApprovedManagerApprovedBlocked => 2019;

                public static int ApproveBlockFromManagerToMonafasatAdmin => 2021;
                public static int AppoveBlockFromMangerToAccountMangerSpecialist => 2025;

                public static int sendApprovedManagerUnBlocked => 2055;
            }
        }



        public static class CancelAnnouncementTemplate
        {
            public static class OperationsToBeApproved
            {
                public static int sendApprovedCancelAnnouncementTemplate => 3049;
                public static int AcceptJoinedSupplierAnnouncementTemplate => 3050;

            }
        }

        public static class ExtendAnnouncementTemplate
        {
            public static class OperationsOnExtendAnnouncement
            {
                public static int SendNotificationToAcceptedSuppliers => 3051;

            }
        }

        public static class PrePlaningEmployee
        {
            public static class OperationsOnPrePlaning
            {
                public static int ApprovePreplaning => 802;
                public static int RejectPreplaning => 804;
            }
        }
        public static class PrePlaningAuditor
        {
            public static class OperationsOnPrePlaning
            {
                public static int SendToApproval => 801;
                public static int approvaloperation => 902;
            }
        }

        public static class PreQualificationManager
        {
            public static class OperationsOnPreQualification
            {
                public static int PendingApproveCreateQualification => 5002;
                public static int SubmitPreQualificationForCheckingApproval => 1008;
                public static int QualificationVerficationCodeApproval => 1009;
            }
            public static class EditOperationsOnQualification
            {
                public static int approveQualificationattachment => 1015;
                public static int QualificationWaitingCancelation => 2007;
                public static int QualificationWaitingExtendDate => 2039;
            }
        }

        public static class PreQualificationSecretary
        {
            public static class OperationsOnPreQualification
            {
                public static int ApprovePreQualificationChecking => 1006;
                public static int RejectingApprovePreQualificationChecking => 1007;
                public static int ConfirmQualificationCancleRequest => 2004;
                public static int PendingCreateQualificationRequest => 5000;
                public static int RejectCreateQualificationRequestFromManager => 5001;
                public static int ApproveCreateQualificationRequestFromManager => 5003;

            }
            public static class EditOperationsOnQualification
            {
                public static int SendOffersPostponeRequestForQulification => 1101;
                public static int RejectQualificationExtendDate => 1017;
                public static int RejectedQualificationAttachment => 2000;
                public static int RejectQualificationCancleRequest => 2001;

            }
        }


        public static class DirectPurchaseManager
        {
            public static class OperationsOnPostqualification
            {
                public static int SubmitPostQualificationForApproval => 906;
                public static int SubmitPostQualificationForCheckingApproval => 907;

                public static int Qualificationwaitingdateextend => 1014;

                public static int QualificationWaitingCancle => 2009;
            }
            public static class OperationsOnTheTender
            {
                public static int ApproveTenderCheck => 775;
                public static int approvaloperation => 901;
                public static int ApproveTenderFinanceChecking => 784;
                public static int canceltender => 2037;
                public static int tenderwaitingcancelation => 2038;
                public static int approvetenderawarding => 2041;
                public static int sendtechnicalevaluationreportforapproval => 2046;
                public static int TenderAwardingNeedFirstApprove => 2047;
                public static int offersWillCheckingTomorrow => 2060;
                public static int rejecttenderawarding => 2059;
            }



            public static class AgencyCommunicationRequest
            {
                public static int SendSecondNegotitionToApprove => 1024;
                public static int AcceptExtendOffersValidityBySupplier => 7872;
                public static int SendExtendOffersToApprove => 7874;
                public static int PlaintSentToApproval => 7877;
                public static int SendInitialWarantyCopy => 7890;
            }

        }
        public static class DirectPurchaseMember
        {
            public static class OperationsOnTheTender
            {
                public static int offersWillCheckingTomorrowPurchaseMember => 8013;
                public static int ApproveTenderAwardingDirectPurchaseMember => 8014;
                public static int RejectTenderAwardingDirectPurchaseMember => 8015;
                public static int RejectingApprovePostQualificationLowBudget => 1005;


            }

            public static class AgencyCommunicationRequest
            {
                public static int AcceptExtendOffersValidityBySupplierForDirectPurchaseMember => 8016;
                public static int RejectExtendOffersValidityBySupplierForDirectPurchaseMember => 8017;
                public static int SendInitialWarantyCopyToDirectPurchaseMember => 8018;
                public static int RejectApproveSecondNegotiationFromUnitUser => 8025;
                public static int AcceptSecondNegotiationBySupplier => 8030;
                public static int RejectSecondNegotiationBySupplier => 8031;
                public static int AcceptFirstStageNegotiationBySupplier => 8032;
                public static int ApproveFirstStageNegotiationFromDirectPurchaseMember => 8033;
                public static int ApproveSecondStageNegotiationFromDirectPurchaseMember => 8034;


            }

        }
        public static class DirectPurchaseSecretary
        {
            public static class OperationsOnTheTender
            {
                public static int TenderOffersCheckingApproved => 778;
                public static int TenderOffersCheckingRejected => 779;
                public static int rejecttendercancellation => 2035;
                public static int canceltender => 2036;
                public static int TenderOffersTechnicalCheckingApproved => 780;
                public static int TenderOffersTechnicalCheckingRejected => 781;
                public static int TenderOffersFinancialCheckingApproved => 782;
                public static int TenderOffersFinancialCheckingRejected => 783;
                public static int approvetenderawarding => 2040;
                public static int approveInitialAwarding => 2043;
                public static int rejectInitialAwarding => 2048;

                public static int FinishStoppingPeriod => 2057;
                public static int rejecttenderawarding => 2058;
                public static int SendVerificationCode => 7000;

            }

            public static class OperationsOnQualification
            {
                public static int ApprovePostQualification => 1001;
                public static int RejectingApprovePostQualification => 1002;
                public static int ApprovePostQualificationChecking => 1003;
                public static int RejectingApprovePostQualificationChecking => 1004;
                public static int RejectQualificationAttachment => 1016;
                public static int RejectQualificationCancleRequest => 2013;
                public static int approveQualificationextenddate => 1018;
                public static int approveQualificationattachment => 1019;
                public static int approveQualificationCancleRequest => 2003;
            }
            public static class AgencyCommunicationRequest
            {
                public static int PlaintRequestCreated => 7878;
                public static int RejectExtendOffersValidityBySupplier => 7873;
                public static int RejectExtendOffers => 7875;
                public static int PlaintEsclationCreated => 7876;
                public static int ApproveSecondNegotiationSupplier => 1039;
                public static int RecectSecondNegotiationSupplier => 1040;
                public static int ApproveSecondNegotition => 1027;
                public static int SendRejectionToSecretary => 1029;
                public static int AgreeNegotiationFirstSuppliers => 1044;
                public static int RejectUnitSecondNegotition => 1035;
                public static int RejectFirstNegotiation => 7895;
                public static int ApproveSecondNegotitionFromUnit => 1031;
                public static int ApproveExtendOffers => 7870;
                public static int PlaintStoppingPeriodEnd => 7871;
                public static int PlaintRejected => 763;
                public static int PlaintApproved => 761;
                public static int PurchasePlaintRejected => 7886;
                public static int PurchasePlaintApproved => 7883;
                public static int RejectAwarding => 7889;
                public static int ApproveFirstNegotiation => 7897;
            }

        }

        public static class OffersCheckManager
        {

            public static class AgencyCommunicationRequest
            {
                public static int SendExtendOffersToApprove => 711;
                public static int SendExtendOffersToSuppliers => 721;
                public static int SendFirstNegotitionToApprove => 734;
                public static int SendFirstNegotitionToApprovePurchase => 7881;
                public static int SendSecondNegotitionToApprove => 1023;
                public static int PlaintSentToApproval => 759;

            }
            public static class OperationsOnTheTender
            {
                public static int canceltender => 176;
                public static int tenderwaitingcancelation => 501;
                public static int TenderAwardingNeedFirstApprove => 823; //بإنتظار اعتماد الترسية المبدئي
                public static int rejecttenderawarding => 2030;//تم رفض الترسية
                public static int approvetenderawarding => 2031; //تم اعتماد الترسية 

            }

            public static class OperationsNeedToBeAccredited
            {
                public static int tenderawardingwaitingapproval => 288;
                public static int sendtechnicalevaluationreportforapproval => 287;
                public static int SendFinancialCheckingForApproval => 2042;
                public static int approvaloperation => 290;

            }
            public static class OfferStatus
            {
                public static int techEvaluationAction => 502;
            }
            public static class OperationsOnPostqualification
            {
                public static int SubmitPostQualificationForApproval => 700;
                public static int SubmitPostQualificationForCheckingApproval => 707;
                public static int Qualificationwaitingdateextend => 1010;
                public static int approveQualificationattachment => 1011;
                public static int QualificationWaitingCancle => 2008;

            }
        }

        public static class OffersCheckSecretary
        {
            public static class OperationsOnTheTender
            {
                public static int rejecttechnicalevaluation => 183;
                public static int canceltender => 278;
                public static int approvetenderawarding => 164; //تم اعتماد الترسية 
                public static int approveInitialAwarding => 2027; //تم اعتماد الترسية المبدئي
                public static int rejectInitialAwarding => 2028; //تم رفض الترسية المبدئي

                public static int rejecttenderawarding => 284;// تم رفض الترسية

                public static int publishtechnicalevaluation => 297;
                public static int rejecttendercancellation => 303;
                public static int publishopenenvelopesbackend => 503;

                public static int ApproveFinancialChecking => 2044;
                public static int RejectFinancialChecking => 2045;
                public static int BiddingVerficationCode => 1066;
                public static int FinishStoppingPeriod => 2056;

            }

            public static class AgencyCommunicationRequest
            {
                public static int SendRejectionToSecretary => 1030;
                public static int AgreeNegotiationFirstSuppliers => 1090;
                public static int ApproveExtendOffers => 712;
                public static int RejectExtendOffers => 720;
                public static int AcceptExtendOffersValidityBySupplier => 724;
                public static int RejectExtendOffersValidityBySupplier => 725;
                public static int SendInitialWarantyCopy => 729;
                public static int ApproveNegotiationFirstSecratary => 738;
                public static int ApproveSecondNegotiationSupplier => 1037;
                public static int RecectSecondNegotiationSupplier => 1038;
                public static int RejectUnitSecondNegotition => 1036;

                public static int RejectFirstNegotiation => 739;
                public static int PlaintRejected => 763;
                public static int PlaintApproved => 7882;
                public static int PlaintStoppingPeriodEnd => 764;
                public static int PlaintEsclationCreated => 766;
                public static int PlaintRequestCreated => 771;
                public static int ApproveSecondNegotitionFromUnit => 1032;
                public static int PurchasePlaintApproved => 7883;
                public static int TenderPlaintApproved => 7882;

            }

            public static class OperationsOnPostqualification
            {
                public static int ApprovePostQualification => 701;
                public static int RejectingApprovePostQualification => 702;
                public static int ApprovePostQualificationChecking => 705;
                public static int RejectingApprovePostQualificationChecking => 706;

                public static int RejectQualificationAttachment => 1012;

                public static int RejectQualificationExtendDate => 1013;

                public static int approveQualificationCancleRequest => 2002;
                public static int ApproveSecondNegotition => 1028;

                public static int approveQualificationextenddate => 1020;
                public static int approveQualificationattachment => 1021;
                public static int RejectQualificationCancleRequest => 2012;

            }

        }

        public static class VROCheckManager
        {
            public static class OperationsOnTheTender
            {
                public static int SendTenderCheckToApprove => 813;
                public static int approvaloperation => 904;
                public static int sendTechnicalEvaluationOfCompetitionToApprove => 1048;
                public static int sendFinancialEvaluationOfCompetitionToApprove => 1051;
                public static int CancelTenderVRO => 2050;
                public static int SendTenderCancelToApproveVRO => 2052;

                public static int vroAwardingWaitingApproval => 2032;
            }
        }

        public static class VROCheckSecretary
        {
            public static class OperationsOnTheTender
            {
                public static int TenderHasBeenApproved => 814;
                public static int TenderHasBeenRejected => 815;

                public static int TenderOffersFinancialCheckingRejected => 1047;
                public static int TenderOffersFinancialCheckingApproved => 1046;

                public static int TenderOffersTechnicalCheckingRejected => 1050;
                public static int TenderOffersTechnicalCheckingApproved => 1049;
                public static int CancelTenderVRO => 2051;
                public static int RejectCancelTenderVRO => 2053;

                public static int vroApproveTenderAwarding => 2033; //تم اعتماد الترسية   

                public static int vroRejectTenderAwarding => 2034;// تم رفض الترسية


            }
        }

        public static class ApproveTenderAwarding
        {
            public static class OperationsOnTheTender
            {
                public static int approvaloperation => 942;
                public static int sendAwardingToApproveAfterInitialAwarding => 2029;//اعتماد ترسية المنافسة لصاحب الصلاحية - تم اعتماد الترسية المبدئي
            }
        }

        public static class MandatoryListOfficer
        {
            public static class MandatoryListProducts
            {
                public static int ApproveMandatoryList => 999;
                public static int RejectMandatoryList => 1000;
                public static int ApproveUpdateMandatoryList => 996;
                public static int RejectUpdateMandatoryList => 995;
                public static int ApproveDeleteMandatoryList => 992;
                public static int RejectDeleteMandatoryList => 991;
            }
        }
        public static class MandatoryListApprover
        {
            public static class MandatoryListProducts
            {
                public static int SendMandatoryListToApprove => 998;
                public static int SendUpdateMandatoryListToApprove => 997;
                public static int SendDeleteMandatoryListToApprove => 993;
                public static int ApprovalOperation => 990;

            }
        }
    }
}

