using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSupplierDataTest
    {
        private const int id = 1;
        private const int qualificationConfigurationId = 1;
        private const int tenderId = 1;
        private const int qualificationItemId = 1;
        private const decimal supplierValue = 1;
        private const decimal pointValue = 1;
        private const int qualificationCategoryId = 1;
        private const decimal weight = 20;
        private const string supplierSelectedCr = "0000";
        private const int qualificationSubCategoryId = 1;

        private const int attachmentId = 1;
        private const string FileName = "File";
        private const string FileNetReferenceId = "";
        private const int qualificationSupplierDataId = 1;

        [Fact]
        public void Should_Construct_QualificationSupplierData()
        {
            QualificationSupplierData qualificationSupplierData = new QualificationSupplierData(id, qualificationConfigurationId, tenderId, supplierValue, pointValue,
                qualificationItemId, qualificationCategoryId, weight, supplierSelectedCr, qualificationSubCategoryId);
            _ = qualificationSupplierData.QualificationConfiguration;
            _ = qualificationSupplierData.QualificationItem;
            _ = qualificationSupplierData.Tender;
            _ = qualificationSupplierData.Supplier;
            _ = qualificationSupplierData.QualificationLookup;
            _ = qualificationSupplierData.QualificationConfigurationAttachments;
            _ = qualificationSupplierData.QualificationSupplierProjects;

            _ = new QualificationSubCategoryResult();

            qualificationSupplierData.ShouldNotBeNull();

        }

        [Fact]
        public void Should_InsertSupplierProject()
        {
            QualificationSupplierData qualificationSupplierData = new QualificationSupplierData(id, qualificationConfigurationId, tenderId, supplierValue, pointValue,
                qualificationItemId, qualificationCategoryId, weight, supplierSelectedCr, qualificationSubCategoryId);

            qualificationSupplierData.InsertSupplierProject(new List<QualificationSupplierProject>() { new QualificationSupplierProject() });
            Assert.NotEmpty(qualificationSupplierData.QualificationSupplierProjects);

        }

        [Fact]
        public void Should_DeActive()
        {
            QualificationSupplierData qualificationSupplierData = new QualificationSupplierData(id, qualificationConfigurationId, tenderId, supplierValue, pointValue,
                qualificationItemId, qualificationCategoryId, weight, supplierSelectedCr, qualificationSubCategoryId);

            qualificationSupplierData.DeActive();
            Assert.False(qualificationSupplierData.IsActive);

        }

        [Fact]
        public void Should_InsertQualificationConfigurationAttachments()
        {
            QualificationSupplierData qualificationSupplierData = new QualificationSupplierData(id, qualificationConfigurationId, tenderId, supplierValue, pointValue,
                qualificationItemId, qualificationCategoryId, weight, supplierSelectedCr, qualificationSubCategoryId);

            QualificationConfigurationAttachment qualificationConfigurationAttachment = new QualificationConfigurationAttachment(FileName, FileNetReferenceId, qualificationSupplierDataId);

            qualificationSupplierData.InsertQualificationConfigurationAttachments(new List<QualificationConfigurationAttachment>() { qualificationConfigurationAttachment });
            Assert.NotEmpty(qualificationSupplierData.QualificationConfigurationAttachments);

        }
    }
}
