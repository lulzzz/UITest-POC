using System;

namespace MOF.Etimad.Monafasat.Services
{
    public class VerificationCodeService : IVerificationCodeService
    {
        public string GenerateVerificationCodeAsync()
        {
            return DateTime.Now.ToString("hhmmss");
        }
    }
}
