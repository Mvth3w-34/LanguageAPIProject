using LanguageProjectBackend.Models;
using System.CodeDom;
using System.Xml.Serialization;

namespace LanguageProjectBackend.Data
{
    public interface IUserWordRepo
    {
        void CreateUserWord(UserWord userWord);

        void SaveChanges();
    }
}
