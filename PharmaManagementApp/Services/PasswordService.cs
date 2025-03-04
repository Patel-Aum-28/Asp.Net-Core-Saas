namespace PharmacyManagementApp.Services
{
    public class PasswordService : IPasswordService
    {
        public string GetHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyHash(string userProvidedPassword, string passwordHashDb)
        {
            return BCrypt.Net.BCrypt.Verify(userProvidedPassword, passwordHashDb);
        }
    }
}
