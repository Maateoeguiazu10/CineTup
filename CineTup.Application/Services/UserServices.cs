using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Domain.Entities;

namespace CineTup.Application.Services
{
    public class UserServices
    {
        public User Get(string name)
        {
            return new User() { Name = name };
        }
    }
}
