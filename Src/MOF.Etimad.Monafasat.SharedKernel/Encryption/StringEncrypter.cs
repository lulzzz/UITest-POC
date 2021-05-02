namespace MOF.Etimad.Monafasat.SharedKernel.Encryption
{
    public class StringEncrypter
    {
        public static IEncryptString UrlEncrypter { get; private set; }


        static StringEncrypter()
        {
            UrlEncrypter = new ConfigurationBasedStringEncrypter();
        }
    }
}
