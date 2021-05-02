using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.PrePlanningDefaults
{
    public class PrePlanningDefaults
    {
        public readonly string _agencyCode = "022001000000";
        public readonly string _projectName = "projectName";
        public readonly string _projectNature = "projectNature";
        public readonly string _projectDescription = "projectDescription";
        public readonly string _duration = "1";
        public readonly int _durationInDays = 1;
        public readonly int _rejectStatusId = 4;
        public readonly int _statusId = 1;
        public readonly int _durationInMonths = 1;
        public readonly int _durationInYears = 1;
        public readonly int _yearQuarterId = 1;
        public readonly int _projectTypeId = 1;
        public readonly int _branchId = 1;
        public readonly int _prePlanningId = 0;
        public readonly int _prePlanningId_Update = 1;
        public readonly bool _insideKSA = true;
        public readonly string _rejectionReason = "rejectionReason";
        public readonly string _verificationCode = "1234";
        public readonly List<int> _areaIDs = new List<int> { 1, 2, 3 };
        public readonly List<int> _countriesIds = new List<int> { 1, 2, 3 };
        public readonly List<int> _projectTypeIds = new List<int> { 1, 2, 3 };
        public readonly string _rejectReasonMoreThan500Charachers = "rejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreasonrejectreason";

        public PrePlanning ReturnPrePlanningDefaults()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId, _agencyCode,_branchId,_projectName,_projectNature,_projectTypeId
             ,_projectDescription,_insideKSA,_statusId,_duration,_durationInDays,_durationInMonths,_durationInYears,_yearQuarterId);
            return generalPrePlanning;
        }

        public PrePlanning ReturnPrePlanningDefaults_Update()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId_Update, _agencyCode, _branchId, _projectName, _projectNature, _projectTypeId
             , _projectDescription, _insideKSA, _statusId, _duration, _durationInDays, _durationInMonths, _durationInYears, _yearQuarterId);
            return generalPrePlanning;
        }


        public PrePlanningModel GetPrePlanningModelData()
        {
            return new PrePlanningModel()
            {
                AgencyCode = _agencyCode,
                ProjectName=_projectName,
                ProjectDescription=_projectDescription
            
            };
        }

        public PrePlanning GetPrePlanningData()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId_Update, _agencyCode, _branchId, _projectName, _projectNature, _projectTypeId
             , _projectDescription, _insideKSA, 3, _duration, _durationInDays, _durationInMonths, _durationInYears, _yearQuarterId);
            generalPrePlanning.SetDeActiveRequest(true);
            return generalPrePlanning;
        }

        public PrePlanning GetPrePlanningDataForApproval()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId_Update, _agencyCode, _branchId, _projectName, _projectNature, _projectTypeId
             , _projectDescription, _insideKSA, 2, _duration, _durationInDays, _durationInMonths, _durationInYears, _yearQuarterId);
            generalPrePlanning.SetDeActiveRequest(true);
            return generalPrePlanning;
        }

        public PrePlanning GetPrePlanningDataForReOpen()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId_Update, _agencyCode, _branchId, _projectName, _projectNature, _projectTypeId
             , _projectDescription, _insideKSA, 4, _duration, _durationInDays, _durationInMonths, _durationInYears, _yearQuarterId);
            generalPrePlanning.SetDeActiveRequest(true);
            return generalPrePlanning;
        }

        public PrePlanning GetPrePlanningDataReOpenForCancelation()
        {
            PrePlanning generalPrePlanning = new PrePlanning(_prePlanningId_Update, _agencyCode, _branchId, _projectName, _projectNature, _projectTypeId
             , _projectDescription, _insideKSA, 4, _duration, _durationInDays, _durationInMonths, _durationInYears, _yearQuarterId);
            generalPrePlanning.SetDeActiveRequest(true);
            generalPrePlanning.SetDeleteRejectionReason("rejectReason");
            return generalPrePlanning;
        }
    }


}
