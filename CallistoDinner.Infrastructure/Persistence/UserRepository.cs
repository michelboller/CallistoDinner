using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly static List<User> _users = new List<User>();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }
    }
}
