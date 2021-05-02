using Microsoft.EntityFrameworkCore;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Owned]
    public class NotificationBy
    {
        public bool Mobile { get; set; }
        public bool Email { get; set; }

    }
}
