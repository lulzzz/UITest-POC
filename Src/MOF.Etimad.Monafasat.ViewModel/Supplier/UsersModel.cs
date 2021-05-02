using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UsersModel
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string address { get; set; }
        public string role { get; set; }
        public string CommitteeId { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string selectedAgency { get; set; }
        public string userCategory { get; set; }
        public string selectedCR { get; set; }
        public static List<UsersModel> GetUsers()
        {
            return new List<UsersModel>()
            {
                new UsersModel{
                SubjectId="1",
                Username="DataEntryUser",
                Password="password",
                given_name="DataEntry",
                family_name="Mohamed",
                address="cairo",
                 role="dataEntry",
                 mobile="0504583476",
                 email="eazzam@thiqah.sa",
                 selectedAgency="1",
                 userCategory="1",// قطاع حكومي
                 selectedCR=""
                },
                 new UsersModel{
                    SubjectId="2",
                    Username="AuditerUser",
                    Password="password",
                    given_name="AuditerUser",
                    family_name="Mohamed",
                    role="Auditer",
                    mobile="0504583476",
                    email="eazzam@thiqah.sa",
                    selectedAgency="1",
                    userCategory="1",// قطاع حكومي
                    selectedCR=""
                },
                new UsersModel{
                    SubjectId="3",
                    Username="SupplierUser",
                    Password="password",
                        given_name="supplier1",
                        family_name="Mohamed",
                        role="NewMonafasat_Supplier",
                        mobile="0540814154",
                        //email="eazzam@thiqah.sass",
                        email="rhassan@thiqah.sa",
                        selectedAgency="",
                        userCategory="6",// قطاع خاص
                        selectedCR="1010157114"
                },
                new UsersModel{
                    SubjectId="4",
                    Username="DataEntryUser2",
                    Password="password",
                        given_name="DataEntry2",
                        family_name="Mohamed",
                        address="cairo",
                        role="dataEntry",
                        mobile="0540814154",
                        email="eazzam@thiqah.saqw",
                        selectedAgency="2",
                        userCategory="1",// قطاع حكومي
                        selectedCR=""
                },
                     new UsersModel{
                    SubjectId="5",
                    Username="AuditerUser2",
                    Password="password",
                        given_name="AuditerUser2",
                        family_name="Mohamed",
                        role="Auditer",
                        mobile="0540814154",
                        email="mohammed.youssef1123@gmail.com",
                        selectedAgency="2",
                        userCategory="1",// قطاع حكومي
                        selectedCR=""
                },
                new UsersModel{
                    SubjectId="6",
                    Username="SupplierUser2",
                    Password="password",
                        given_name="supplier2",
                        family_name="Mohamed",
                        role="NewMonafasat_Supplier",
                        mobile="0540814154",
                        email="eazzam@thiqah.sa",
                        selectedAgency="",
                        userCategory="6",// قطاع خاص
                        selectedCR="1010245397"
                },
                  new UsersModel{
                  SubjectId="7",
                  Username="OffersCheckManagerUser",
                  Password="password",
                      given_name="OffersCheckManager",
                      family_name="Mohamed",
                      address="cairo",
                      role="OffersCheckManager",
                      mobile="0540814154",
                      email="eazzam@thiqah.sa",
                      selectedAgency="1",
                      userCategory="1",// قطاع حكومي
                      selectedCR=""
                },
                new UsersModel{
                    SubjectId="8",
                    Username="OffersCheckSecretaryUser",
                    Password="password",
                     given_name="OffersCheckSecretary",
                     family_name="Mohamed",
                     address="cairo",
                     role="OffersCheckSecretary",
                     mobile="0540814154",
                     email="eazzam@thiqah.sa",
                     selectedAgency="1",
                     userCategory="1",// قطاع حكومي
                     selectedCR=""
                },
                  new UsersModel{
                  SubjectId="9",
                  Username="OffersOppeningManagerUser",
                  Password="password",
                      given_name="OffersOppeningManager",
                      family_name="Mohamed",
                      address="cairo",
                      role="OffersOppeningManager",
                      mobile="0540814154",
                      email="eazzam@thiqah.sa",
                      selectedAgency="1",
                      userCategory="1",// قطاع حكومي
                      selectedCR=""
                },
                new UsersModel{
                    SubjectId="10",
                    Username="OffersOppeningSecretaryUser",
                    Password="password",

                        given_name="OffersOppeningSecretary",
                        family_name="Mohamed",
                        address="cairo",
                        role="OffersOppeningSecretary",
                        mobile="0540814154",
                        email="eazzam@thiqah.sa",
                        selectedAgency="1",
                        userCategory="1",// قطاع حكومي
                        selectedCR=""
                },
                new UsersModel{
                    SubjectId="11",
                    Username="MonafasatAdmin",//مدير منافسات في الجهة الحكومية
                    Password="password",

                        given_name="MonafasatAdmin",
                        family_name="Mohamed",
                        role="MonafasatAdmin",
                        mobile="0540814154",
                        email="eazzam@thiqah.sa",
                        selectedAgency="2",
                        userCategory="1",// قطاع خاص
                        selectedCR="1010245397"
                },
                new UsersModel{
                    SubjectId="12",
                    Username="MonafasatAccountManager", //مدير الحساب باعتماد
                    Password="password",

                        given_name="MonafasatAccountManager",
                        family_name="Mohamed",
                        role="MonafasatAccountManager",
                        mobile="0540814154",
                        email="eazzam@thiqah.sa",
                        selectedAgency="2",
                        userCategory="1",// قطاع خاص
                        selectedCR="1010245397"
                },
                new UsersModel{
                    SubjectId="15",
                    Username="CustomerServiceUser",
                    Password="password",

                        given_name="CustomerService",
                        family_name="Mohamed",
                        address="cairo",
                        role="CustomerService",
                        mobile="0540814154",
                        email="eazzam@thiqah.sa",
                        selectedAgency="1",
                        userCategory="1",// قطاع حكومي
                        selectedCR=""
                },
                new UsersModel{
                    SubjectId="16",
                    Username="TechnicalCommitteeUser",
                    Password="password",
                        given_name="TechnicalCommitteeUser",
                        family_name="Mohamed",
                        role="TechnicalCommitteeUser",
                        CommitteeId="13",
                        mobile="0540814154",
                        //email="eazzam@thiqah.sass",
                        email="rhassan@thiqah.sa",
                        selectedAgency="1",
                        userCategory="1",// قطاع حكومي
                        selectedCR=""
                }
            };
        }
    }
}
