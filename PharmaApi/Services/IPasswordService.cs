namespace PharmaApi.Services
{
    public interface IPasswordService
    {
        string GetHashPassword(string password);
        bool VerifyHash(string userProvidedPassword, string passwordHashDb);
    }
}
