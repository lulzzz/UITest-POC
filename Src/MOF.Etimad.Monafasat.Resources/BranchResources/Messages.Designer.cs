//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOF.Etimad.Monafasat.Resources.BranchResources
{
    using System;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MOF.Etimad.Monafasat.Resources.BranchResources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to تم إضافة فرع الجهة الحكومية بنجاح.
        /// </summary>
        public static string BranchAddedSuccessfully
        {
            get
            {
                return ResourceManager.GetString("BranchAddedSuccessfully", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to تم  تحديث فرع الجهة الحكومية بنجاح.
        /// </summary>
        public static string BranchUpdatedSuccessfully
        {
            get
            {
                return ResourceManager.GetString("BranchUpdatedSuccessfully", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to تأكيد الحذف.
        /// </summary>
        public static string ConfirmDeletion
        {
            get
            {
                return ResourceManager.GetString("ConfirmDeletion", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to هل انت متاكد من انك تريد حذف هذه المعلومات ؟.
        /// </summary>
        public static string ConfirmSupplierDeletion
        {
            get
            {
                return ResourceManager.GetString("ConfirmSupplierDeletion", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to عزيزنا المستخدم، نود تنبيهكم بانة تم تعديل الصلاحية المسندة اليكم.
        /// </summary>
        public static string EmailBody
        {
            get
            {
                return ResourceManager.GetString("EmailBody", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to تعديل صلاحية.
        /// </summary>
        public static string EmailSubject
        {
            get
            {
                return ResourceManager.GetString("EmailSubject", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to البت بالترسية للاعمال التي تزيد تكلفتها عن 10 ملايين مستخدم يملك صلاحية: اعتماد ترسية المنافسة لصاحب الصلاحية.
        /// </summary>
        public static string EstimatedValueWarning
        {
            get
            {
                return ResourceManager.GetString("EstimatedValueWarning", resourceCulture);
            }
        }
    }
}
