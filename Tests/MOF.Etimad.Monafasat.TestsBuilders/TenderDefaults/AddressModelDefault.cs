using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults
{
    public class AddressModelDefault
    {
        public List<AddressModel> BuildAddressModelList()
        {
            var AddressesModel = new List<AddressModel>()
            {

            };
            AddressesModel.Add(BuildAddressModelObject());
            return AddressesModel;
        }
        public AddressModel BuildAddressModelObject()
        {
            return new AddressModel()
            {
                AddressId = 1,
                AddressName = "test",
                AddressTypeId = 2
            };
        }
    }
}
