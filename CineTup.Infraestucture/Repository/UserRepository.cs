using CineTup.Application.Abstractions;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Infraestucture.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CineTupDbContext context) : base(context)
        {
        }
    }
}