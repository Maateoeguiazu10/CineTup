using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Exceptions;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Infraestucture.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CineTupDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(CineTupDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.Where(x => !x.IsDeleted).ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public virtual T Add(T entity)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            _dbSet.Add(entity);
            SaveChanges();
            return entity;
        }

        public virtual void Update(T entity)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            _dbSet.Update(entity);
            SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletedDateTime = DateTime.UtcNow;
                entity.UpdateDateTime = DateTime.UtcNow;
                _dbSet.Update(entity);
                SaveChanges();


            }
        }
        protected void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("Error al acceder a la base de datos.", ex);
            }
        }
    }
}
