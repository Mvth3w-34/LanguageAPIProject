using System.ComponentModel.DataAnnotations;

namespace LanguageProjectFrontend.DtoModels
{
    //This class will be used to model an external representation of a user upon creation.
    public class UsersCreateDto
    {

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string LanguagePreference { get; set; } //The language they want to learn.

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+@[A-Za-z0-9]+/.+[^@/s]")]
        public string Email { get; set; } //must be unique

        [Required]
        public string EmailFrequency { get; set; }

    }
}
