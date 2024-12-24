using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;

namespace LanguageProjectBackend.Models
{
    //This class will model a userWord entity.
    public class UserWord
    {
        [Key]
        public int Id { get; set; } //Primary key

        public int UserId { get; set; } //Foreign Key
        public int WordId { get; set; } // Foreign Key

        public User User { get; set; }   
        public NewWord NewWord { get; set; }




    }
}
