using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public AuthorizationService()
        {

        }
        public async Task HasAccessAsync(string methodName, List<dynamic> parameters)
        {
            var result = (Task)GetType().GetMethod("IsValid" + methodName).Invoke(this, new object[] { parameters });
            await result;
        }
    }
}
