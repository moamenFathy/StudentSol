using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem_Model.Models
{
  public class EndUser
  {
    public int EndUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] Password { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
  }
}
