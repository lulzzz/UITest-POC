using IdentityServer4.Events;
using IdentityServer4.Services;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Data;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common
{
    public class AuditEventService : IEventService
    {
        private readonly AppDbContext _context;

        public AuditEventService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool CanRaiseEventType(EventTypes evtType)
        {
            throw new NotImplementedException();
        }

        public async Task RaiseAsync(Event evt)
        {
            if (evt.Name.Contains("Login"))
            {
                if (evt.EventType == EventTypes.Success)
                {
                    UserLoginSuccessEvent successEvent = evt as UserLoginSuccessEvent;
                    UserAudit user = UserAudit.CreateAuditEvent(successEvent.SubjectId.ToString(), successEvent.Username, successEvent.DisplayName, successEvent.Name, successEvent.Id.ToString(), evt.EventType.ToString(), evt.RemoteIpAddress);
                    await _context.UserAuditEvents.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    UserLoginFailureEvent failEvent = evt as UserLoginFailureEvent;
                    UserAudit user = UserAudit.CreateAuditEvent(failEvent.Username.ToString(), failEvent.Id.ToString(), failEvent.Name, failEvent.EventType.ToString(), evt.RemoteIpAddress);
                    await _context.UserAuditEvents.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
            }
            else if (evt.Name.Contains("Logout"))
            {
                UserLogoutSuccessEvent failEvent = evt as UserLogoutSuccessEvent;
                UserAudit user = UserAudit.CreateAuditEvent(failEvent.SubjectId.ToString(), failEvent.DisplayName, failEvent.DisplayName, failEvent.Name, failEvent.Id.ToString(), evt.EventType.ToString(), evt.RemoteIpAddress);
                await _context.UserAuditEvents.AddAsync(user);
                await _context.SaveChangesAsync();
            }

        }
    }
}
