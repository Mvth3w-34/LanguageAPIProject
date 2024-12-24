using LanguageProjectBackend.Models;

namespace LanguageProjectBackend.Data
{ 
    //This class will be used to interact with the UserWords table in the database.
    public class UserWordRepository : IUserWordRepo
    {
        private readonly LanguageProjectContext _context;

        public UserWordRepository(LanguageProjectContext context)
        {
            _context = context;
        }

        public void CreateUserWord(UserWord userWord)
        {
            if (userWord == null)
            {
                throw new ArgumentNullException(nameof(userWord));
            }
            _context.UserWords.Add(userWord);  
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
