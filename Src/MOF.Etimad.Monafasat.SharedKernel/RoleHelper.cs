using System;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public static class RoleHelper
    {
        public static Array AuditRoles()
        {
            string[] rolesList = { RoleNames.Auditer, RoleNames.OffersOppeningManager, RoleNames.OffersCheckManager };
            return rolesList;
        }

        public static Array EntryRoles()
        {
            string[] rolesList = { RoleNames.DataEntry, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary };
            return rolesList;
        }

        public static string GetHigherAuthorityByRoleName(String whoRoleName)
        {
            string higherAuthorityRoleName = "";
            switch (whoRoleName)
            {
                case RoleNames.OffersOppeningSecretary:
                    higherAuthorityRoleName = RoleNames.OffersOppeningManager;
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    higherAuthorityRoleName = RoleNames.OffersPurchaseManager;
                    break;
                case RoleNames.OffersPurchaseManager:
                    higherAuthorityRoleName = RoleNames.OffersPurchaseManager;
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    higherAuthorityRoleName = RoleNames.PreQualificationCommitteeManager;
                    break;
                case RoleNames.OffersCheckSecretary:
                    higherAuthorityRoleName = RoleNames.OffersCheckManager;
                    break;
                case RoleNames.OffersOpeningAndCheckSecretary:
                    higherAuthorityRoleName = RoleNames.OffersOpeningAndCheckManager;
                    break;
                default:
                case RoleNames.DataEntry:
                    higherAuthorityRoleName = RoleNames.Auditer;
                    break;
                case RoleNames.PurshaseSpecialist:
                    higherAuthorityRoleName = RoleNames.EtimadOfficer;
                    break;
            }
            return higherAuthorityRoleName;
        }

    }
}
