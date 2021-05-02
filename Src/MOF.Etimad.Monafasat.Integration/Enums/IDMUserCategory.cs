using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Integration.Enums
{
    public enum IDMUserCategory
    {
        //جهة حكومية
        Agency = 1,
        //مالية قطاعات
        MOF = 2,
        //ترشيد الانفاق قطاعات
        ExpenseControl = 3,
        //ديوان الرقابة
        AuditCourt = 4,
        //وحدة التدخل السريع
        DeliveryAndRapidInterventionCenter = 5,
        // قطاع خاص
        PrivateSector = 6,
        //مالية رقابة تنظيمات
        MOFAudit = 7,
        //جهة مستفيدة
        GovVendor = 8,
        //
        CustomerCare = 9,
        //تحقيق الرؤية تنظيمات
        VRO = 10,
        // العمل لدائم قطاعات
        PWT = 11,
        // المحتوى قطاعات
        LocalContent = 12,
        // فريق تشغيل إعتماد
        EtimadOperationTeam = 14,
        // قطاع خاص لا يملك سجل تجاري
        PrivateSectorHasNotCR = 15,
        // وحدة الشراء الإستراتيجي
        StrategicPurchasingUnit = 16
    }
}
