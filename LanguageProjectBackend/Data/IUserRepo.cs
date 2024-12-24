using LanguageProjectBackend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using NuGet.Protocol.Core.Types;
using System.Collections;

namespace LanguageProjectBackend.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        IEnumerable<User> GetUserByEmailFrequency(string frequency);
        void DeleteUser(int id);

        void SaveChanges();
    }
}
