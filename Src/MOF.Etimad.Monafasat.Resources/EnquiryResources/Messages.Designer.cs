﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOF.Etimad.Monafasat.Resources.EnquiryResources
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MOF.Etimad.Monafasat.Resources.EnquiryResources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to الرجاء التأكيد على اعتماد الرد وارسال تنبيه للموردين.
        /// </summary>
        public static string ApproveReplyMsg
        {
            get
            {
                return ResourceManager.GetString("ApproveReplyMsg", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to لا يمكن إضافة استفسار بعد موعد أخر  لاستلام الاستفسارت.
        /// </summary>
        public static string CannotAddEnquiryAfterDeadLine
        {
            get
            {
                return ResourceManager.GetString("CannotAddEnquiryAfterDeadLine", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to لا يمكن ضم هذه اللجنه أكثر من مرة على نفس الإستفسار.
        /// </summary>
        public static string CannotJoinCommittee
        {
            get
            {
                return ResourceManager.GetString("CannotJoinCommittee", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to الرجاء التأكيد على عملية الحذف .
        /// </summary>
        public static string DeleteReplyMsg
        {
            get
            {
                return ResourceManager.GetString("DeleteReplyMsg", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to المشرف من الجهة الحكومية سيقوم بالرد على سؤالك, وسيتم إشعارك فور الرد على السؤال.
        /// </summary>
        public static string EnquiryReplyMsg
        {
            get
            {
                return ResourceManager.GetString("EnquiryReplyMsg", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to رجاء كتابة التعليق.
        /// </summary>
        public static string EnterComment
        {
            get
            {
                return ResourceManager.GetString("EnterComment", resourceCulture);
            }
        }
    }
}