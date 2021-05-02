namespace MOF.Etimad.Monafasat.SharedKernel.Encryption
{
    public interface IEncryptString
    {
        string Encrypt(string value);
        string Decrypt(string value);
        string Hash(string value);
        bool CompaireHash(string hashedText, string plainText);
        string Prefix { get; }
        int HashIterationCounts { get; }
        bool KeyIVExist { get; }
    }
}
