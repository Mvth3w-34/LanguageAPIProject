using System.ComponentModel.DataAnnotations;

namespace LanguageProjectFrontend.DtoModels
{
    //This class will be used to model an external representation of a user upon creation.
    public class UserCreateDto
    {

        [Required(ErrorMessage = "The first name field is required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "The language field is required")]
        public string LanguagePreference { get; set; } //The language they want to learn.

        [Required(ErrorMessage = "The email field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Your email must be of format: example123@xyz.com")]
        public string Email { get; set; } //must be unique

        [Required(ErrorMessage = "The email frequency field is required")]
        public string EmailFrequency { get; set; }

    }
}
