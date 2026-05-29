using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Domain.Entities;

namespace CineTup.Infraestructure.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public User? Get(string name) 
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }
    }
}
