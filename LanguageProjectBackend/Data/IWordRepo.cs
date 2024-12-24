using LanguageProjectBackend.Models;

namespace LanguageProjectBackend.Data
{
    public interface IWordRepo
    {
        void CreateNewWord(NewWord word);
        
        NewWord GetNewWord(int id);

        string GetTranslation(NewWord word, string languagePreference);

        IEnumerable<NewWord> GetAllWords(NewWord word);

        void SaveChanges();

    }
}
