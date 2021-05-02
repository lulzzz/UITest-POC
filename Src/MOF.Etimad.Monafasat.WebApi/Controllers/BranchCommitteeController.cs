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
    public class BranchCommitteeController : BaseController
    {
       
        private readonly IBranchCommitteeService _branchCommitteeService;
        private readonly IMapper _mapper;
        public BranchCommitteeController(IBranchCommitteeService branchCommitteeService, IMapper mapper)
        {
            _branchCommitteeService = branchCommitteeService;
            _mapper = mapper;
        }

        // GET api/branchcommittee
        [HttpGet]
        [Route("Get")]
        public async Task<List<BranchCommitteeModel>> Get(BranchCommitteeSearchCriteriaModel branchCommitteeSearchCriteriaModel)
        {
            var branchCommitteeSearchCriteria = _mapper.Map<BranchCommitteeSearchCriteria>(branchCommitteeSearchCriteriaModel);
            var branchCommitteeList = await _branchCommitteeService.Find(branchCommitteeSearchCriteria);
             var branchCommitteeModel = _mapper.Map<List<BranchCommitteeModel>>(branchCommitteeList);
            return branchCommitteeModel;
        }

        // GET api/branchcommittee/getbyid
        [HttpGet()]
        [Route("GetById")]
        public async Task<BranchCommitteeModel> GetById(int id)
        {
            var branchCommitteeObject = await _branchCommitteeService.GetById(id);
            var branchCommitteeModel = _mapper.Map<BranchCommitteeModel>(branchCommitteeObject);
            return branchCommitteeModel;
        }

        // POST api/branchcommittee
        [HttpPost]
        public async Task<BranchCommitteeModel> Post(BranchCommitteeModel branchCommitteeModel)
        {
            var branchCommitteeObject = _mapper.Map<BranchCommittee>(branchCommitteeModel);
            var branchCommitteeResult = await _branchCommitteeService.CreateAsync(branchCommitteeObject);
            branchCommitteeModel = _mapper.Map<BranchCommitteeModel>(branchCommitteeResult);
            return branchCommitteeModel;
        }

        // PUT api/branchcommittee
        [HttpPut]
        public async Task<BranchCommitteeModel> Put(BranchCommitteeModel branchCommitteeModel)
        {
            var branchCommitteeObject = _mapper.Map<BranchCommittee>(branchCommitteeModel);
            var branchCommitteeResult = await _branchCommitteeService.UpdateAsync(branchCommitteeObject);
            branchCommitteeModel = _mapper.Map<BranchCommitteeModel>(branchCommitteeResult);
            return branchCommitteeModel;
        }

        // DELETE api/branchcommittee/5
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _branchCommitteeService.DeActiveAsync(id);
        }
    }
}
