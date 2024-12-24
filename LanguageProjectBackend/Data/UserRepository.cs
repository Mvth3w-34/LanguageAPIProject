using LanguageProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Protocol.Plugins;

namespace LanguageProjectBackend.Data
{
    //This class will be used to interact with the users table in the database.
    public class UserRepository : IUserRepo
    {
        readonly LanguageProjectContext _context;
        public UserRepository(LanguageProjectContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserByEmailFrequency(string frequency)
        {
            return _context.Users.Where(p => p.EmailFrequency == frequency).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
