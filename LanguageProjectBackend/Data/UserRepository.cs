using LanguageProjectBackend.Models;

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
            SaveChanges();
        }

        public void DeleteUserById(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            _context.Users.Remove(user); // Cascade delete the user.
            SaveChanges();
        }

        public void DeleteUserByEmail(string email)
        {
            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();

            _context.Users.Remove(user); // Cascade delete the user.
            SaveChanges();
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
