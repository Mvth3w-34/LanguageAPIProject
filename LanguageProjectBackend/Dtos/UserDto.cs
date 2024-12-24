namespace LanguageProjectBackend.Dtos
{
   
    //This class acts a external version of a user model found internally
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailFrequency { get; set; } 
        public string LanguagePreference { get; set; }

    }
}
