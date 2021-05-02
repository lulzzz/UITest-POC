using System;
namespace MOF.Etimad.Monafasat.Integration
{
    public class UserAPIModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public ICollection<UserRoleAPIModel> UserRoles { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; } }
        public string Mobile { get; set; }
        public int? UserCategoryID { get; set; }
        public int? UserTypeID { get; set; }
        public bool IsAdmin { get; protected set; }
        public bool IsFirstTimeLogin { get; private set; }
        //public ICollection<UserDepartmentsAPIModel> UserDepartments { get; set; }
        //public ICollection<UserSectorAPIModel> UserSectors { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
