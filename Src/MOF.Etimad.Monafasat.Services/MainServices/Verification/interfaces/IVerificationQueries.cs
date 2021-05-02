using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IVerificationQueries
    {
        Task<VerificationCode> FindVerificationCode(int codeTypeId, int userId, int TypeId);

    }
}
