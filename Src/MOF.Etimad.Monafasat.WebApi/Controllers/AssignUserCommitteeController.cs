using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Api.Models;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.Monafasat.WebApi.Models;

namespace MOF.Etimad.Monafasat.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssignUserCommitteeController : BaseController
    {
       
        private readonly IAssignUserCommitteeService _assignUserCommitteeService;
        private readonly IMapper _mapper;
        public AssignUserCommitteeController(IAssignUserCommitteeService assignUserCommitteeService, IMapper mapper)
        {
            _assignUserCommitteeService = assignUserCommitteeService;
            _mapper = mapper;
        }

        // GET api/assignusercommittee
        [HttpGet]
        [Route("Get")]
        public async Task<List<AssignUserCommitteeModel>> Get(AssignUserCommitteeSearchCriteriaModel assignUserCommitteeSearchCriteriaModel)
        {
            var assignUserCommitteeSearchCriteria = _mapper.Map<AssignUserCommitteeSearchCriteria>(assignUserCommitteeSearchCriteriaModel);
            var assignUserCommitteeList = await _assignUserCommitteeService.Find(assignUserCommitteeSearchCriteria);
            var assignUserCommitteeModel = _mapper.Map<List<AssignUserCommitteeModel>>(assignUserCommitteeList);
            return assignUserCommitteeModel;
        }

        // GET api/assignusercommittee/getbyid
        [HttpGet()]
        [Route("GetById")]
        public async Task<AssignUserCommitteeModel> GetById(int id)
        {
            var assignUserCommitteeObject = await _assignUserCommitteeService.GetById(id);
            var assignUserCommitteeModel = _mapper.Map<AssignUserCommitteeModel>(assignUserCommitteeObject);
            return assignUserCommitteeModel;
        }

        // POST api/assignusercommittee
        [HttpPost]
        public async Task<AssignUserCommitteeModel> Post(AssignUserCommitteeModel assignUserCommitteeModel)
        {
            var assignUserCommitteeObject = _mapper.Map<AssignUserCommittee>(assignUserCommitteeModel);
            var assignUserCommitteeResult = await _assignUserCommitteeService.CreateAsync(assignUserCommitteeObject);
            assignUserCommitteeModel = _mapper.Map<AssignUserCommitteeModel>(assignUserCommitteeResult);
            return assignUserCommitteeModel;
        }

        // PUT api/assignusercommittee
        [HttpPut]
        public async Task<AssignUserCommitteeModel> Put(AssignUserCommitteeModel assignUserCommitteeModel)
        {
            var assignUserCommitteeObject = _mapper.Map<AssignUserCommittee>(assignUserCommitteeModel);
            var assignUserCommitteeResult = await _assignUserCommitteeService.UpdateAsync(assignUserCommitteeObject);
            assignUserCommitteeModel = _mapper.Map<AssignUserCommitteeModel>(assignUserCommitteeResult);
            return assignUserCommitteeModel;
        }

        // DELETE api/assignusercommittee/5
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _assignUserCommitteeService.DeActiveAsync(id);
        }
    }
}
