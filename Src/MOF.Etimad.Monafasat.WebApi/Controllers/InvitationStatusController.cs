using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOF.Eitimd.SharedKernel;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("Api/[controller]")]
    public class InvitationStatusController : Controller
    {
        private readonly ITenderService _tenderService;
        private IAppDbContext _context;
        public InvitationStatusController(IAppDbContext context, ITenderService tenderService)
        {
            this._tenderService = tenderService;
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public  IEnumerable<InvitationStatus> Get()
        {
            List<InvitationStatus> invitations =   _context.InvitationStatuses.ToList();
            return invitations;
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public InvitationStatus GetById(int id)
        {
            InvitationStatus invitations = _context.InvitationStatuses.Find(id);
            return invitations;
        }

        [HttpGet]
        [Route("UpdateStatus/{id}/{statusId}")]
        public Invitation UpdateStatus(int id,int statusId)
        {
            Invitation invitations = _context.Invitations.Find(id);
            invitations.UpdateStatus(statusId);
             _context.SaveChangesAsync();
            return invitations;
        }
        // Api / Tender/GetTenderInformationByInvitationId
        // Get Tender Information By Invitation ID

        [HttpPost]
        [Route("Create")]
        public async Task<InvitationStatus> Create([FromBody] InvitationStatusModel invitationStatusmodel)
        {
            Check.ArgumentNotNull(nameof(invitationStatusmodel), invitationStatusmodel);
            InvitationStatus invitationStatus = new InvitationStatus(invitationStatusmodel.Name);
            _context.InvitationStatuses.Add(invitationStatus);
            await _context.SaveChangesAsync();
            return invitationStatus;
        }
        [HttpPost]
        [Route("Update/{id}")]
        public async Task<InvitationStatus> Update(int id,[FromBody] InvitationStatusModel invitationStatusmodel)
        {
            InvitationStatus invitationStatus = await _context.InvitationStatuses.FindAsync(id) ;
            invitationStatus.Update(invitationStatusmodel.Name);
            await _context.SaveChangesAsync();
            return invitationStatus;
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<InvitationStatus> Delete(int id)
        {
            InvitationStatus invitationStatus = await _context.InvitationStatuses.FindAsync(id);
            invitationStatus.DeActive();
            await _context.SaveChangesAsync();
            return invitationStatus;
        }
    }
}
