using System.Security.Cryptography;
using System.Text;

namespace Tsu.IndividualPlan.WebApi.Utils;

public static class PasswordUtils
{
    public static string GetPasswordHash(string password)
    {
        var sha = SHA256.Create();
        var byteArray = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(byteArray);
        return Convert.ToBase64String(hash);
    }
}