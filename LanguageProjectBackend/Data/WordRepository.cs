using LanguageProjectBackend.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging.Signing;
using System.Collections;
using System.Collections.Immutable;

namespace LanguageProjectBackend.Data
{

    //This class will be used to interact with NewWords table in the database.
    public class WordRepository: IWordRepo
    {
        readonly LanguageProjectContext _context;
        public WordRepository(LanguageProjectContext context)
        {
            _context = context;
        }

       
        // This method generates a new word for the user, ensuring that the word is unique
        public NewWord GetNewWord(int id)
        {
            NewWord newWord = _context.Words.Where(word => !_context.UserWords.Any(uw=> uw.WordId == word.Id && uw.UserId == id)).FirstOrDefault(); //Ensures a user gets a unique word

            return newWord;
        }

        //This method retrieves any existing translation of a specific word from the database.
        public string GetTranslation(NewWord word, string languagePreference)
        {
            string? translation = "";
            switch (languagePreference) 
            {
                case "Swahili":
                    translation = word.Swahili;
                    break;
                case "Arabic":
                    translation = word.Arabic;
                    break;
                default:
                    translation = word.French;
                    break;
            }

            return translation;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        // The following code below is for the case where I decide to expand this project for now it is just in the proof of concept stages.


        //This method will add an english word to the database 
        public void CreateNewWord(NewWord word)
        {
            if (word == null) { 
                throw new ArgumentNullException(nameof(word));
            }

            _context.Words.Add(word);

        }
        
        //This method will be used to retrieve all of the new words in the database
        public IEnumerable<NewWord> GetAllWords(NewWord word)
        {
            return _context.Words.ToList();
        }

    }
}
