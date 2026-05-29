using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CineTup.Infraestructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly bool isTestingEnviroment;

        public ApplicationContext(DbContextOptions <ApplicationContext> options, bool isTestingEnviroment = false): base(options)
        {
            this.isTestingEnviroment=isTestingEnviroment;
        }
    }
}
