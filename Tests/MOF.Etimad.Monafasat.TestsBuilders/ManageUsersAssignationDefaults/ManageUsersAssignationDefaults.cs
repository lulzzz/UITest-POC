using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.ManageUsersAssignationDefaults
{
    public class ManageUsersAssignationDefaults
    {
        public QueryResult<ManageUsersAssignationModel> GetMonafasatUsersModel()
        {
            QueryResult<ManageUsersAssignationModel> Users = new QueryResult<ManageUsersAssignationModel>(new List<ManageUsersAssignationModel>(),10,1,1);
            return Users;
        }
    }
}
