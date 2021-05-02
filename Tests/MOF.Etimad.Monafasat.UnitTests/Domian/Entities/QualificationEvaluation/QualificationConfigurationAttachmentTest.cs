using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationConfigurationAttachmentTest
    {
        private const int id = 1;
        private const string FileName = "File";
        private const string FileNetReferenceId = "";
        private const int qualificationSupplierDataId = 1;

        [Fact]
        public void Should_Construct_QualificationConfigurationAttachment()
        {
            _ = new QualificationConfigurationAttachment();
            QualificationConfigurationAttachment qualificationConfigurationAttachment = new QualificationConfigurationAttachment(id, FileName, FileNetReferenceId);
            QualificationConfigurationAttachment _qualificationConfigurationAttachment = new QualificationConfigurationAttachment(FileName, FileNetReferenceId, qualificationSupplierDataId);
            _ = qualificationConfigurationAttachment.QualificationSupplierData;

            qualificationConfigurationAttachment.ShouldNotBeNull();
            _qualificationConfigurationAttachment.ShouldNotBeNull();

        }

        [Fact]
        public void Should_DeActive()
        {
            QualificationConfigurationAttachment qualificationConfigurationAttachment = new QualificationConfigurationAttachment(id, FileName, FileNetReferenceId);
            qualificationConfigurationAttachment.DeActive();
            Assert.False(qualificationConfigurationAttachment.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            QualificationConfigurationAttachment qualificationConfigurationAttachment = new QualificationConfigurationAttachment(id, FileName, FileNetReferenceId);
            qualificationConfigurationAttachment.SetActive();
            Assert.True(qualificationConfigurationAttachment.IsActive);
        }
    }
}
