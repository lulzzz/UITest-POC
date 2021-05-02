using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.AnnouncementDefaults
{
    public class AnnouncementDefaults
    {
        private const string _announcementeName = "test";
        private const string _introAboutAnnouncement = "intro";
        private const bool IsInsideKsa = true;
        private const string Details = "test details";
        private const string ActivityDescription = "activity description";
        private const string AgencyCode = "030001000000";
        private readonly List<int> _activitiesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _areasIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _countriesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> mainteananceWorkIds = new List<int>() {1, 2, 4 };
        private const int _announcementPeriod = 1;
        private const int tenderTypeId = 1;
        private const int branchId = 1;
        private const int tenderReasonId = 1;
        public Announcement ReturnAnnouncementDefaultsForApproval()
        {
            Announcement generalAnnouncement = new Announcement(_announcementeName, _announcementPeriod, tenderTypeId, tenderReasonId, _introAboutAnnouncement,IsInsideKsa, Details, ActivityDescription, branchId, AgencyCode, _activitiesIds, null, mainteananceWorkIds, _areasIds, _countriesIds);
            generalAnnouncement.Test_UpdateStatus(2);
            generalAnnouncement.Test_UpdatePublishDatetooldDate(DateTime.Now.AddDays(5));
            return generalAnnouncement;
        }

        public Announcement ReturnAnnouncementDefaults()
        {
            Announcement generalAnnouncement = new Announcement(_announcementeName, _announcementPeriod, tenderTypeId, tenderReasonId, _introAboutAnnouncement, IsInsideKsa, Details, ActivityDescription, branchId, AgencyCode, _activitiesIds, null, mainteananceWorkIds, _areasIds, _countriesIds);
            return generalAnnouncement;
        }




        public Announcement ReturnAnnouncementDefaultsForReOpen()
        {
            Announcement generalAnnouncement = new Announcement(_announcementeName, _announcementPeriod, tenderTypeId, tenderReasonId, _introAboutAnnouncement, IsInsideKsa, Details, ActivityDescription, branchId, AgencyCode, _activitiesIds, null, mainteananceWorkIds, _areasIds, _countriesIds);
            generalAnnouncement.Test_UpdatePublishDatetooldDate(DateTime.Now.AddDays(5));
            generalAnnouncement.Test_UpdateStatus(4);
            return generalAnnouncement;
        }


        public Announcement ReturnAnnouncementDefaultsForRejection()
        {
            Announcement generalAnnouncement = new Announcement(_announcementeName, _announcementPeriod, tenderTypeId, tenderReasonId, _introAboutAnnouncement, IsInsideKsa, Details, ActivityDescription, branchId, AgencyCode, _activitiesIds, null, mainteananceWorkIds, _areasIds, _countriesIds);
            generalAnnouncement.Test_UpdatePublishDatetooldDate(DateTime.Now.AddDays(-5));
            generalAnnouncement.Test_UpdateStatus(2);
            return generalAnnouncement;
        }
    }
}
