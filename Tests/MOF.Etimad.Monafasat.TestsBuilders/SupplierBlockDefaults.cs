using MOF.Etimad.Monafasat.Core;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class SupplierBlockDefaults
    {
        public SupplierBlock ReturnSupplierBlockDefaults()
        {
            var generalSupplierBlockData= new SupplierBlock("1234", "SelectedCr1", "1", 1, 1, "5678", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4), "Punishment", "Offitial bases defimation", "File Name", "File Net Reference ID", "Secretarry File Name", "Secertary File Name Ref Id", "reason", "secertary Block Reason", 1, 1, "9876543221", "origin", true);
            generalSupplierBlockData.SetAgencyForTest();
            return generalSupplierBlockData;


        }

        public Supplier ReturnSupplierDefaults()
        {
            return new Supplier("1234", "Cr Name", new List<Core.Entities.UserNotificationSetting> { new UserNotificationSettingBuilder().ReturnUserNotificationSettingDefaults() });
        }
    }
}
