using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace LanguageProjectBackend.Models
{
    //This class will act as a model for the NewWord entity
    public class NewWord
    {
        [Key]
        public int Id { get; set;} //Primary Key

        public string Word { get; set;}

        public string Swahili { get; set; }

        public string Arabic { get; set; }

        public string French { get; set; }
        public List<UserWord> UserWords { get; set; } //Collection navigation property
    }
}
