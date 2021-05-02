using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotifayManager
    {
        Task CreateOffer(AuditableEntity entity,string methodName);
        Task UpdateOffer(AuditableEntity entity, string methodName);
        Task DeleteOffer(AuditableEntity entity, string methodName);
    }
}
