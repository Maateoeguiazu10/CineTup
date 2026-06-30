using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CineTup.Infraestucture.ExternalServices
{
    public class UserService : IUserService
    {
        private readonly CineTupDbContext _context;

        public UserService(CineTupDbContext context)
        {
            _context = context;
        }

        public List<UserResponse> GetAllUsers()
        {
            var clients = _context.Clients
                .Where(c => !c.IsDeleted)
                .Select(c => new UserResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    AvatarUrl = c.AvatarUrl,
                    Rol = "Client"
                }).ToList();

            var admins = _context.Admins
                .Where(a => !a.IsDeleted)
                .Select(a => new UserResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    AvatarUrl = a.AvatarUrl,
                    Rol = "Admin"
                }).ToList();

            var sysAdmins = _context.SysAdmins
                .Where(s => !s.IsDeleted)
                .Select(s => new UserResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    AvatarUrl = s.AvatarUrl,
                    Rol = "SysAdmin"
                }).ToList();

            return clients.Concat(admins).Concat(sysAdmins).ToList();
        }

        public void AssignRole(int userId, string currentRole, string newRole)
        {
            if (string.Equals(currentRole, newRole, StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidationException("El usuario ya tiene asignado ese rol.");
            }

            var validRoles = new[] { "Client", "Admin", "SysAdmin" };
            if (!validRoles.Contains(newRole) || !validRoles.Contains(currentRole))
            {
                throw new ValidationException("Rol no válido. Los roles válidos son: Client, Admin, SysAdmin.");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    User? sourceUser = null;

                    // 1. Obtener y eliminar del rol actual
                    if (string.Equals(currentRole, "Client", StringComparison.OrdinalIgnoreCase))
                    {
                        var client = _context.Clients.Include(c => c.Tickets).FirstOrDefault(c => c.Id == userId && !c.IsDeleted);
                        if (client == null) throw new NotFoundException("Usuario no encontrado con rol Client.");

                        // Liberar tickets asociados al cliente si pasa a ser Admin o SysAdmin
                        foreach (var ticket in client.Tickets)
                        {
                            ticket.ClientId = null;
                            ticket.IsAvailable = true;
                            ticket.PurchaseDate = null;
                        }

                        sourceUser = client;
                        _context.Clients.Remove(client);
                    }
                    else if (string.Equals(currentRole, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var admin = _context.Admins.FirstOrDefault(a => a.Id == userId && !a.IsDeleted);
                        if (admin == null) throw new NotFoundException("Usuario no encontrado con rol Admin.");
                        sourceUser = admin;
                        _context.Admins.Remove(admin);
                    }
                    else if (string.Equals(currentRole, "SysAdmin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Prevenir quedarse sin ningún SysAdmin en el sistema
                        if (_context.SysAdmins.Count(s => !s.IsDeleted) <= 1)
                        {
                            throw new ValidationException("No se puede eliminar o cambiar el rol del único SysAdmin en el sistema.");
                        }

                        var sysAdmin = _context.SysAdmins.FirstOrDefault(s => s.Id == userId && !s.IsDeleted);
                        if (sysAdmin == null) throw new NotFoundException("Usuario no encontrado con rol SysAdmin.");
                        sourceUser = sysAdmin;
                        _context.SysAdmins.Remove(sysAdmin);
                    }

                    if (sourceUser == null)
                    {
                        throw new NotFoundException("Usuario no encontrado.");
                    }

                    // Validar que el email no exista en la tabla destino
                    bool emailExists = false;
                    if (string.Equals(newRole, "Client", StringComparison.OrdinalIgnoreCase))
                        emailExists = _context.Clients.Any(c => c.Email == sourceUser.Email && !c.IsDeleted);
                    else if (string.Equals(newRole, "Admin", StringComparison.OrdinalIgnoreCase))
                        emailExists = _context.Admins.Any(a => a.Email == sourceUser.Email && !a.IsDeleted);
                    else if (string.Equals(newRole, "SysAdmin", StringComparison.OrdinalIgnoreCase))
                        emailExists = _context.SysAdmins.Any(s => s.Email == sourceUser.Email && !s.IsDeleted);

                    if (emailExists)
                    {
                        throw new ConflictException("Ya existe un usuario con ese email en el rol de destino.");
                    }

                    // 2. Crear y guardar en el nuevo rol
                    User targetUser;
                    if (string.Equals(newRole, "Client", StringComparison.OrdinalIgnoreCase))
                    {
                        targetUser = new Client
                        {
                            Name = sourceUser.Name,
                            Email = sourceUser.Email,
                            Password = sourceUser.Password,
                            UpdateDateTime = DateTime.UtcNow
                        };
                        _context.Clients.Add((Client)targetUser);
                    }
                    else if (string.Equals(newRole, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        targetUser = new Admin
                        {
                            Name = sourceUser.Name,
                            Email = sourceUser.Email,
                            Password = sourceUser.Password,
                            UpdateDateTime = DateTime.UtcNow
                        };
                        _context.Admins.Add((Admin)targetUser);
                    }
                    else
                    {
                        targetUser = new SysAdmin
                        {
                            Name = sourceUser.Name,
                            Email = sourceUser.Email,
                            Password = sourceUser.Password,
                            UpdateDateTime = DateTime.UtcNow
                        };
                        _context.SysAdmins.Add((SysAdmin)targetUser);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteUser(int userId)
        {
            var client = _context.Clients.FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
            if (client != null)
            {
                client.IsDeleted = true;
                client.DeletedDateTime = DateTime.UtcNow;
                _context.SaveChanges();
                return;
            }

            var admin = _context.Admins.FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
            if (admin != null)
            {
                admin.IsDeleted = true;
                admin.DeletedDateTime = DateTime.UtcNow;
                _context.SaveChanges();
                return;
            }

            var sysAdmin = _context.SysAdmins.FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
            if (sysAdmin != null)
            {
                sysAdmin.IsDeleted = true;
                sysAdmin.DeletedDateTime = DateTime.UtcNow;
                _context.SaveChanges();
                return;
            }

            throw new NotFoundException("Usuario no encontrado.");
        }
    }
}
