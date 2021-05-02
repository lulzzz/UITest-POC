using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults
{
    public class BranchDefaults
    {

        public readonly string _agencyCode = "022001000000";
        public readonly List<BranchAddress> _branchAddresses = new List<BranchAddress>();
        public readonly string _branchName = "newbranch";
        public readonly int addressTypeId = 1;
        public readonly string position = "position";
        public readonly string address = "address";
        public readonly string phone = "0533286913";
        public readonly string fax = "0533286913";
        public readonly string phone2 = "0533286913";
        public readonly string fax2 = "0533286913";
        public readonly string email = "hsawak@etimad.sa";
        public readonly string description = "description";
        public readonly string addressName = "addressName";
        public readonly string cityCode = "11111";
        public readonly string postalCode = "11111";
        public readonly string zipCode = "11111";
        public readonly bool? isActive = true;
        public readonly string _nationalId = "1070814676";

        public Branch ReturnBranchDefaults()
        {
            Branch generalBranch = new Branch(_agencyCode, _branchName, _branchAddresses);
            return generalBranch;
        }

        public BranchModel GetBranchModel()
        {
            List<OtherBranchAddressModel> otherBranchAddressModels = new List<OtherBranchAddressModel>();
            otherBranchAddressModels.Add(new OtherBranchAddressModel() { AddressName = "address" });

            List<int> committieeIds = new List<int>() {1,2};

            MainBranchAddressModel mainBranchAddressModel = new MainBranchAddressModel{ 
            Position= "Position",
            Email="hsawak@etimad.sa",
            Address="address",
            Phone="0533286913",
            Fax= "0533286913"
            };
            return new BranchModel
            {
                BranchName = "test branch",
                AgencyName = "AgencyName",
                AgencyCode = "022001000000",
                RelatedAgencyCode = "022001000000",
                MainBranchAddress = mainBranchAddressModel,
                AddressesList = otherBranchAddressModels,
                CommittieeIds = committieeIds
            };
        }

        public List<BranchModel> GetBranchModels()
        {
            List<BranchModel> branchModels = new List<BranchModel>();
            branchModels.Add(GetBranchModel());
            return branchModels;
        }

        public BranchModel GetBranchModelForUpdate()
        {
            int branchId = 1;
            MainBranchAddressModel mainBranchAddressModel = new MainBranchAddressModel
            {
                Position = "Position",
                Email = "hsawak@etimad.sa",
                Address = "address",
                Phone = "0533286913",
                Fax = "0533286913"
            };
            return new BranchModel
            {
                BranchName = "test branch",
                AgencyName = "AgencyName",
                BranchIdString = Util.Encrypt(branchId),
                AgencyCode = "022001000000",
                RelatedAgencyCode = "022001000000",
                MainBranchAddress = mainBranchAddressModel,
                BranchId= branchId,
                techcommitteeIds=new List<int>() { 1,2}
            };
        }

        public BranchUserModel GetBranchUserModel()
        {
         
            return new BranchUserModel
            {
                RelatedAgencyCode = "022001000000",
                RoleName= "NewMonafasat_DataEntry",
                UserName= "1087287072",
                
            };
        }

        public List<BranchUserAssignModel> GetAllBranchesUserAssignModel()
        {
            List<BranchUserAssignModel> branchUserAssignModel = new List<BranchUserAssignModel>();
            branchUserAssignModel.Add(GetBranchUserAssignModel());
            return branchUserAssignModel;
        }


        public BranchUserAssignModel GetBranchUserAssignModel()
        {
            return new BranchUserAssignModel
            {
                RelatedAgencyCode = "022001000000",
                RoleName = "NewMonafasat_DataEntry"
            };
        }

        public BranchCommitteeModel GetBranchCommitteeModel()
        {
            return new BranchCommitteeModel
            {
                BranchId=1,
                CommitteeIdsString = "1,2,3"
            };
        }


       
        public BranchAddress ReturnBranchAddressDefaults()
        {
            BranchAddress branchAddress = new BranchAddress(addressTypeId, position, address, phone, fax, phone2, fax2, email, description, addressName, cityCode, postalCode, zipCode, isActive);
            return branchAddress;
        }


        public List<ManageUsersAssignationModel> GetUsersAssignation()
        {
            List<RoleModel> roles = new List<RoleModel>();
            roles.Add(new RoleModel() { RoleNameAr= "NewMonafasat_OffersCheckSecretary", RoleName= "NewMonafasat_OffersCheckSecretary" });
            List<ManageUsersAssignationModel> manageUsersAssignationModels = new List<ManageUsersAssignationModel>();
            ManageUsersAssignationModel manageUsersAssignationModel = new ManageUsersAssignationModel() {
                userId = 102,
                userIdString = "Ahc1Ou5BrJi7B*@@**WUaxDxaQ==",
                nationalId = _nationalId,
                nationalIdString = "",
                roles = roles,
                AgencyNames="الديوان العام"
            };
            manageUsersAssignationModels.Add(manageUsersAssignationModel);
            return manageUsersAssignationModels;
        }


        

        public List<EmployeeIntegrationModel> GetEmployeesData()
        {
            List<IDMRolesModel> roles = new List<IDMRolesModel>();
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersCheckSecretary", NormalizedName = "NewMonafasat_OffersCheckSecretary" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersOpeningAndCheckSecretary", NormalizedName = "NewMonafasat_OffersOpeningAndCheckSecretary" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_TechnicalCommitteeUser", NormalizedName = "NewMonafasat_TechnicalCommitteeUser" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersOpeningManager", NormalizedName = "NewMonafasat_OffersOpeningManager" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_PreQualificationCommitteeManager", NormalizedName = "NewMonafasat_PreQualificationCommitteeManager" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_ManagerDirtectPurshasingCommittee", NormalizedName = "NewMonafasat_ManagerDirtectPurshasingCommittee" });


            List<Integration.AgencyModel> agencies = new List<Integration.AgencyModel>();
            agencies.Add(new Integration.AgencyModel() { agencyName = "وزارة الصحة", agencyCode = "022001000000" });
            List<EmployeeIntegrationModel> manageUsersAssignationModels = new List<EmployeeIntegrationModel>();
            EmployeeIntegrationModel manageUsersAssignationModel = new EmployeeIntegrationModel()
            {
                userId = 102,
                nationalId = _nationalId,
                roles = roles,
                agencies = agencies,
                dateOfBirth = "2003-07-23 00:00:00.0000000",
                email = "h@y.com",
                fullName = "aaa"
            };
            manageUsersAssignationModels.Add(manageUsersAssignationModel);
            return manageUsersAssignationModels;
        }

        public QueryResult<EmployeeIntegrationModel> GetEmployeeIntegrationModel()
        {
            
            List<Integration.AgencyModel> agencies = new List<Integration.AgencyModel>();
            agencies.Add(new Integration.AgencyModel() {agencyName="وزارة الصحة",agencyCode= "022001000000" });
            List<IDMRolesModel> roles = new List<IDMRolesModel>();
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersCheckSecretary", NormalizedName = "NewMonafasat_OffersCheckSecretary" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersOpeningAndCheckSecretary", NormalizedName = "NewMonafasat_OffersOpeningAndCheckSecretary" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_TechnicalCommitteeUser", NormalizedName = "NewMonafasat_TechnicalCommitteeUser" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_OffersOpeningManager", NormalizedName = "NewMonafasat_OffersOpeningManager" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_PreQualificationCommitteeManager", NormalizedName = "NewMonafasat_PreQualificationCommitteeManager" });
            roles.Add(new IDMRolesModel() { Name = "NewMonafasat_ManagerDirtectPurshasingCommittee", NormalizedName = "NewMonafasat_ManagerDirtectPurshasingCommittee" });

            


            List<EmployeeIntegrationModel> manageUsersAssignationModels = new List<EmployeeIntegrationModel>();
            EmployeeIntegrationModel manageUsersAssignationModel = new EmployeeIntegrationModel()
            {
                userId = 102,
                nationalId = _nationalId,
                roles = roles,
                agencies= agencies,
                dateOfBirth= "2003-07-23 00:00:00.0000000",
                email="h@y.com",
                fullName="aaa"
            };
            manageUsersAssignationModels.Add(manageUsersAssignationModel);
            return new  QueryResult<EmployeeIntegrationModel>(manageUsersAssignationModels, manageUsersAssignationModels.Count,1,6);
        }

        public QueryResult<ContactOfficerModel> GetContactOfficerModel()
        {
            List<ContactOfficerModel> contactOfficerModel = new List<ContactOfficerModel>();
            return new QueryResult<ContactOfficerModel>(contactOfficerModel, contactOfficerModel.Count, 1, 6);
        }

        

    }
}
