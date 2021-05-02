using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.MandatoryListEntities
{
    public class MandatoryListProductTest
    {
        [Fact]
        public void ShouldCreateNewMandatoryListProdcut()
        {
            MandatoryListProduct product = new MandatoryListProduct();

            product.Add();

            Assert.Equal(ObjectState.Added, product.State);
        }

        [Fact]
        public void ShouldDeActivateMandatoryListProduct()
        {
            MandatoryListProduct product = new MandatoryListProduct();

            product.SetInActive();

            Assert.False(product.IsActive);
            Assert.Equal(ObjectState.Modified, product.State);
        }

        [Fact]
        public void ShouldUpdateMandatoryListProduct()
        {
            MandatoryListProduct product = new MandatoryListProduct();
            MandatoryListProduct updatedProduct = new MandatoryListDefault().GetMandatoryListProdcut();

            product.Update(updatedProduct);

            Assert.Equal(updatedProduct.NameAr, product.NameAr);
            Assert.Equal(updatedProduct.NameEn, product.NameEn);
            Assert.Equal(updatedProduct.CSICode, product.CSICode);
            Assert.Equal(updatedProduct.DescriptionAr, product.DescriptionAr);
            Assert.Equal(updatedProduct.DescriptionEn, product.DescriptionEn);
            Assert.Equal(updatedProduct.PriceCelling, product.PriceCelling);
            Assert.Equal(ObjectState.Modified, product.State);
        }

        [Fact]
        public void ShouldUpdateMandatoryListProductChangeRequest()
        {
            MandatoryListProductChangeRequest product = new MandatoryListProductChangeRequest();
            MandatoryListProductChangeRequest updatedProduct = new MandatoryListDefault().GetMandatoryListProductChangeRequest();

            product.Update(updatedProduct);

            Assert.Equal(updatedProduct.NameAr, product.NameAr);
            Assert.Equal(updatedProduct.NameEn, product.NameEn);
            Assert.Equal(updatedProduct.CSICode, product.CSICode);
            Assert.Equal(updatedProduct.DescriptionAr, product.DescriptionAr);
            Assert.Equal(updatedProduct.DescriptionEn, product.DescriptionEn);
            Assert.Equal(updatedProduct.PriceCelling, product.PriceCelling);
            Assert.Equal(ObjectState.Modified, product.State);
        }


        [Fact]
        public void ShouldAddNewMandatoryListProductChangeRequest()
        {
            MandatoryListProductChangeRequest product = new MandatoryListProductChangeRequest();

            product.Add();

            Assert.Equal(ObjectState.Added, product.State);
        }

    }
}
