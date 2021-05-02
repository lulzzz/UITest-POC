using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("User", Schema = "Lookups")]
    public class User : AuditableEntity
    {
        #region Properties


        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }
        
        public virtual string UserName { get; set; }
        
        public virtual string Email { get; protected set; }

        public virtual string PhoneNumber { get; protected set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; protected set; }

        public string FullName { get; protected set; }

        //[NotMapped]
        //public string FullName { get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; } }

        //public string SelectedCr { get; set; }

        //public string SelectedCrName { get; set; }

        //public int GovAgencyID { get; set; }
        //public GovAgency SelectedGovAgency { get; set; }
        public List<UserAgencyRole> UserAgencyRoles { get; set; }
        public List<Supplier> Suppliers { get; set; }

        public byte[] RowVersion { get; set; }

        #endregion

        #region Constructors

        public User()
        {
        }

        public User(int id)
        {
            this.Id = id;
            EntityCreated();
        }

        public User(ClaimsIdentity oldIdentity)
        {
            if (oldIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
                Id = int.Parse(oldIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            EntityCreated();
        }

        //public User(int id/*, string firstName, string secondName, string thirdName, string lastName, string selectedCr, string selectedCrName*/)
        //{
        //    Id = id;
        //    //FirstName = firstName;
        //    //SecondName = secondName;
        //    //ThirdName = thirdName;
        //    //LastName = lastName;
        //    //SelectedCr = selectedCr;
        //    //SelectedCrName = selectedCrName;
        //    EntityCreated();
        //}

        public User(string fullName, /*string firstName, string secondName, string thirdName, string lastName,*/ bool isActive, string mobile, string email/*, string selectedCr, string selectedCrName*/)
        {
            FullName = fullName;
            //FirstName = firstName;
            //SecondName = secondName;
            //ThirdName = thirdName;
            //LastName = lastName;
            IsActive = isActive;
            Mobile = mobile;
            Email = email;
            //SelectedCr = selectedCr;
            //SelectedCrName = selectedCrName;
            EntityCreated();
        }

        public User(int userID, string fullName, /*string firstName, string secondName, string thirdName, string lastName,*/ string email, string mobile/*, string selectedCr, string selectedCrName*/)
        {
            Id = userID;
            FullName = fullName;
            //FirstName = firstName;
            //SecondName = secondName;
            //ThirdName = thirdName;
            //LastName = lastName;
            Email = email;
            Mobile = mobile;
            IsActive = true;
            //SelectedCr = selectedCr;
            //SelectedCrName = selectedCrName;
            //RowVersion = rowVersion;
            EntityCreated();
        }
        
        #endregion

        #region Operations
        
        public void UpdateProfile(string email, string mobile)
        {
            Email = email;
            Mobile = mobile;
            base.EntityUpdated();
        }

        public User Update(string fullName, /*string firstName, string secondName, string thirdName, string lastName,*/ string email, string mobile/*, string selectedCr, string selectedCrName, byte[] rowVersion*/)
        {
            FullName = fullName;
            //FirstName = firstName;
            //SecondName = secondName;
            //ThirdName = thirdName;
            //LastName = lastName;
            Email = email;
            Mobile = mobile;
            //SelectedCr = selectedCr;
            //SelectedCrName = selectedCrName;
            //RowVersion = rowVersion;
            EntityUpdated();
            return this;
        }
        
        #endregion
    }
}
