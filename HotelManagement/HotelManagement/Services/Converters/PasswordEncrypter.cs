using Konscious.Security.Cryptography;
using System.Text;

namespace HotelManagement.BusinessLogic.Converters;

public static class PasswordEncrypter
{
    public static string Hash(string password)
    {
        var hasher = new Argon2id(Encoding.UTF8.GetBytes(password));
        hasher.Iterations = 1;
        hasher.MemorySize = 32;
        hasher.DegreeOfParallelism = 1;
        byte[] hashedBytes = hasher.GetBytes(64);

        return Convert.ToBase64String(hashedBytes);
    }
}
