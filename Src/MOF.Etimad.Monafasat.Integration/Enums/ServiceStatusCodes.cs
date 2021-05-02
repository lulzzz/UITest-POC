namespace MOF.Etimad.Monafasat.Integration.Enums
{
    public static class ServiceStatusCodes
    {
        public const string SuccessString = "Success";
        public const string Success = "I000000";
        public const string NoDataFound = "E002001";
        public const string WrongTypeCode = "E002002";
        public const string UnrecoverableDatabaseError = "E999998";
        public const string UnrecoverableIntegrationError = "E999999";
        public const string WathiqNoDataFound = "E004402";
        public const string WrongInputData = "E004404";

        #region Billing with Sadad
        //---------------A4SADAD Errors-----------------
        public const string MonafasatA4SADADNoDataFound = "E006300";
        public const string MonafasatA4SADADTimeOut = "E006301";
        public const string UnknownMonafasatA4SADADError = "E006399";
        //---------------Tahseel Errors-----------------
        public const string TahseelNoDataFound = "E006400";
        public const string TahseelTimeOut = "E006401";
        public const string UnknownTahseelError = "E006499";
        public const string AgencyGatewayGeneralFailure = "E006402";
        public const string SignatureVerificationFailed = "E006403";
        public const string FailedtoEstablishaBacksideConnection = "E006403";
        public const string InvalidXMLSchemaValidationFailed = "E006405";
        public const string InvalidMessageFromBackend = "E006406";
        public const string BacksideConnectionFailed = "E006404";
        #endregion

        #region MCI CR Validation
        public const string NoRecordsFoundValidateMCICR = "E001101";
        public const string CRIsCancelledValidateMCICR = "E001102";
        public const string BannedFromMinistryOfJusticeValidateMCICR = "E001103";
        public const string GSB_MCI_TimeoutValidateMCICR = "E001198";
        public const string UnknowGSB_MCI_Error = "E001199";
        #endregion

        #region Zakat Certificate Inquiry
        public const string NoRecordsFoundZakatCertificateInquiry = "E001201";
        public const string GSB_Zakat_Timeout = "E001298";
        public const string UnknowGSB_Zakat_Error = "E001299";
        #endregion

        #region GOSI Certificate Inquiry
        /// <summary>
        /// A general failure has occurred while executing the service. Please contact the GSB support team
        /// </summary>
        public const string GeneralFailureHasOccurred = "E001301";
        /// <summary>
        /// The service return multiple record for the input parameters used
        /// </summary>
        public const string MultipleRecordsFoundForInputParameters = "E001303";
        public const string GSB_GOSI_Timeout = "E001398";
        public const string UnknowGSB_GOSI_Error = "E001399";
        #endregion

        #region Nitaqat Information Inquiry
        public const string EstablishmentNotExistent = "E001401";
        public const string GSB_Nitaqat_Timeout = "E001498";
        public const string UnknowGSB_Nitaqat_Error = "E001499";
        #endregion

        #region COC Subscription Inquiry
        public const string NoRecordsFoundCOCSubsciption = "E003102";
        public const string InvalidInputParametersCOCSubsciption = "E003101";
        public const string COCTimeoutCOCSubsciption = "E003198";
        public const string UnknownCOCErrorCOCSubsciption = "E003199";
        #endregion

        #region License Details Inquiry
        public const string NoRecordsFoundLicenseDetails = "E000203";
        public const string NorecordsfoundforCompanyIdLicenseNumber = "E005500";
        public const string AuthenticationError = "E005501";
        public const string InternalSystemError = "E005502";

        #endregion

        #region Contractor Details Inquiry
        public const string NoRecordsFoundContractorDetails = "E006200";
        public const string NoRecordsFoundContractorDetailsE006299 = "E006299";
        public const string MultipleRecordsFound = "E006201";
        public const string InvalidInputparameters = "E006202";
        public const string Unexpectederrorwithdescriptiondependsontheerror = "E006203";
        public const string BackendTimeOut = "E006204";
        #endregion

        #region Etimad tender contract link
        public const string ContractSuccessString = "Success";
        public const string FailedString = "Failed";
        public const string TenderCanBeLinked = "1000";
        public const string InvalidTenderReference = "1001";
        public const string StillInAwardingProcess = "1002";
        public const string StillInStoppingPeriod = "1003";
        public const string TenderNotRelatedToAgency = "1004";
        #endregion

        #region LocalContentContract
        public const string LCGPATimeOut = "E007000";
        public const string LCGPAAuthenticationFailure = "E007001";
        public const string LCGPAInvalidInputParams = "E007002";
        public const string LCGPAErrorWhileProcessingRequest = "E007003";
        public const string LCGPASupplierIdRequired = "E007005";
        public const string LCGPASupplierIdRequiredInvalidReportType = "E007006";
        public const string LCGPASupplierIdRequiredInvalidTenderReference = "E007007";
        public const string LCGPAUnExpectedError = "E007099";

        #endregion
    }
}
