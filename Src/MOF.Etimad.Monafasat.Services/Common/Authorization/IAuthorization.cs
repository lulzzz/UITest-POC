using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAuthorizationService
    {
        Task HasAccessAsync(string methodName, List<dynamic> parameters);
    }
}
