using LanguageProjectBackend.Models;

namespace LanguageProjectBackend.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);

        IEnumerable<User> GetUserByEmailFrequency(string frequency);

        void DeleteUserById(int id);

        void DeleteUserByEmail(string email);

        void SaveChanges();
    }
}
