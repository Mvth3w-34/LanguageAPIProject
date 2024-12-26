using System.ComponentModel.DataAnnotations;

namespace LanguageProjectBackend.Models
{
    //This class will model a user entity.
    public class User
    {
        [Key]
        public int Id { get; set; } //Primary Key 

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string LanguagePreference { get; set; } //The language they want to learn.

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+@[A-Za-z0-9]+/.+[^@/s]")]
        public string Email { get; set; }

        [Required]
        public string EmailFrequency { get; set; }

        public List<UserWord> UserWords { get; set; } //Collection navigation property
    }
}
