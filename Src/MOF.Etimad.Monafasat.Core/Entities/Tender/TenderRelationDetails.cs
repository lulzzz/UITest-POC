using MOF.Eitimad.SharedKernel;
using MOF.Etimad.Monafasat.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using static MOF.Etimad.Monafasat.Core.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Tender",Schema ="Tender")]
    public class TenderRelationDetails : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderRelationDetailsId { get;private set; }

        public int TenderId { get;private set; }
        [ForeignKey("TenderId")]
        public Tender Tender { get; private set; } = new Tender();
        
        [Required]
        public List<TenderArea> TenderAreas { private set; get; } = new List<TenderArea>();

        [Required]
        public List<QuantitiesTable> QuantitiesTables { get;private set; }=new List<QuantitiesTable>();
         
        [Required]
        public List<TenderActivity> TenderActivities {private set; get; } = new List<TenderActivity>();
        //  Tender Speciality Field
        public List<TenderMentainanceRunnigWork> TenderMentainanceRunnigWorks {private set; get; } = new List<TenderMentainanceRunnigWork>();
        public List<TenderConstructionWork> TenderConstructionWorks {private set; get; } = new List<TenderConstructionWork>();

        public List<Offer> Offers { get; set; }
        //Tender Attachments PDF Or Zip And Maximum 350 MB
        public List<Attachment> Attachments {private set; get; } = new List<Attachment>();
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();
        #endregion

        #region NotMapped
        
        #endregion

        #region Constructors====================================================

        public TenderRelationDetails()
        {
            EntityCreated();
        }

        #endregion

        #region Methods====================================================
        
        public TenderRelationDetails UpdateTenderRelations(IList<int> AreaIds, IList<int> ActivityIds, IList<int> ConstructionWorksIds, IList<int> MentainanceRunnigWorksIds,IList<QuantitiesTable> tables)
        {
            UpdateAreas(AreaIds);
            UpdateActivities(ActivityIds);
            UpdateConstructionWorks(ConstructionWorksIds);
            UpdateRunningMentainanceWorks(MentainanceRunnigWorksIds);
            UpdateQuantitiesTables(tables);
            
            EntityUpdated();
            return this;
        }
        public void UpdateQuantitiesTables(IList<QuantitiesTable> tables )
        {
            //delete quantity tables and its items the add the new ones
            foreach (var item in QuantitiesTables)
            {
                item.DeleteQuantitiesTableAndItems();
            }
            foreach (var table in tables)
            {
                //table.AddQuantityItems(table.QuantitiesTableItems);
                QuantitiesTables.Add(table);
            }
            EntityUpdated();
        }
        public void UpdateAreas(IList<int> AreaIds)
        {
            //will not cahnge
            var mutualAreas = TenderAreas.Where(x => AreaIds.Contains(x.AreaId)).ToList();
            var mutualIds = mutualAreas.Select(a => a.AreaId).ToList<int>();
            //Will be deleted
            var AreasToBeDeleted = TenderAreas.Where(x => !AreaIds.Contains(x.AreaId)).ToList();
            //Will be added
            List<int> AreasAddedIDs = AreaIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
            if (TenderAreas != null)
            {
                foreach (var item in AreasToBeDeleted)
                {
                    item.Delete();
                }
            }
            if (AreaIds != null)
            {
                foreach (var item in AreasAddedIDs)
                {
                    TenderAreas.Add(new TenderArea(item));
                }
            }
        }
        public void UpdateActivities(IList<int> ActivityIds)
        {
            //will not cahnge
            var mutualActivities = TenderActivities.Where(x => ActivityIds.Contains(x.ActivityId)).ToList();
            var mutualActivitiesIds = mutualActivities.Select(a => a.ActivityId).ToList<int>();
            //Will be deleted
            var ActivitiesToBeDeleted = TenderActivities.Where(x => !ActivityIds.Contains(x.ActivityId)).ToList();

            //Will be added
            List<int> ActivitiesAddedIDs = ActivityIds.Where(x => !mutualActivitiesIds.Contains(x)).ToList<int>();
            if (TenderActivities != null)
            {
                foreach (var item in ActivitiesToBeDeleted)
                {
                    item.Delete();
                }
            }
            if (ActivityIds != null)
            {
                foreach (var item in ActivitiesAddedIDs)
                {
                    TenderActivities.Add(new TenderActivity(item));
                }
            }
        }
        public void UpdateConstructionWorks(IList<int> ConstructionWorksIds)
        {
            
            //Construction
            //will not cahnge
            var mutualConstruction = TenderConstructionWorks.Where(x => ConstructionWorksIds.Contains(x.ConstructionWorkId)).ToList();
            var mutualConstructionIds = mutualConstruction.Select(a => a.ConstructionWorkId).ToList<int>();
            //Will be deleted
            var ConstructionToBeDeleted = TenderConstructionWorks.Where(x => !ConstructionWorksIds.Contains(x.ConstructionWorkId)).ToList();

            //Will be added
            List<int> ConstructionAddedIDs = ConstructionWorksIds.Where(x => !mutualConstructionIds.Contains(x)).ToList<int>();

            if (TenderConstructionWorks != null)
            {
                foreach (var item in ConstructionToBeDeleted)
                {
                    item.Delete();
                }
            }
        }
        public void UpdateRunningMentainanceWorks(IList<int> MentainanceRunnigWorksIds)
        {
            //will not cahnge
            var mutualMentainance = TenderMentainanceRunnigWorks.Where(x => MentainanceRunnigWorksIds.Contains(x.MaintenanceRunningWorkId)).ToList();
            var mutualMentainanceIds = mutualMentainance.Select(a => a.MaintenanceRunningWorkId).ToList<int>();
            //Will be deleted
            var MentainanceToBeDeleted = TenderMentainanceRunnigWorks.Where(x => !MentainanceRunnigWorksIds.Contains(x.MaintenanceRunningWorkId)).ToList();

            //Will be added
            List<int> MentainanceAddedIDs = MentainanceRunnigWorksIds.Where(x => !mutualMentainanceIds.Contains(x)).ToList<int>();

            if (TenderMentainanceRunnigWorks != null)
            {
                foreach (var item in MentainanceToBeDeleted)
                {
                    item.Delete();
                }
            }

            if (MentainanceRunnigWorksIds != null)
            {
                foreach (var item in MentainanceAddedIDs)
                {
                    TenderMentainanceRunnigWorks.Add(new TenderMentainanceRunnigWork(item));
                }
            }
        }
    
        public void UpdateAttachments(string description)
        {
            //Description = description;
            
            EntityUpdated(); 
        }
        
        public TenderRelationDetails Update()
        {
            EntityUpdated();
            return this;
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion
    }
}
