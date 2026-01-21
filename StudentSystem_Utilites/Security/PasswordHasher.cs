using System.Security.Cryptography;

namespace StudentSystem.Security
{
  public static class PasswordHasher
  {
    public static (byte[] hash, byte[] salt) HashPassword(string password)
    {
      using var hmac = new HMACSHA256();
      var salt = hmac.Key;
      var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      return (hash, salt);
    }

    public static bool Verify(string password, byte[] storedHash, byte[] storedSalt)
    {
      using var hmac = new HMACSHA256(storedSalt);
      var computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      return CryptographicOperations.FixedTimeEquals(computed, storedHash);
    }
  }
}
