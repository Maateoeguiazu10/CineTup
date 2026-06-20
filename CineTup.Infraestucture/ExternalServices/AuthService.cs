using CineTup.Application.Abstractions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CineTup.Infraestucture.ExternalServices
{
    public class AuthService : IAuthService
    {
        private readonly CineTupDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(CineTupDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public AuthResponse? SingUp(SignUpRequest request)
        {
            bool emailExists = _context.Clients.Any(c => c.Email == request.Email)
                           || _context.Admins.Any(a => a.Email == request.Email)
                           || _context.SysAdmins.Any(u => u.Email == request.Email);
            if (emailExists)
            {
                return null;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            string rol;
            int userId;

            if (request.Rol == "Admin")
            {
                var admin = new Admin
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = hashedPassword
                };
                _context.Admins.Add(admin);
                _context.SaveChanges();

                userId = admin.Id;
                rol = "Admin";
            }
            else if (request.Rol == "Client")
            {
                var client = new Client
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = hashedPassword
                };
                _context.Clients.Add(client);
                _context.SaveChanges();
                userId = client.Id;
                rol = "Client";
            }
            else
            {
                var sysAdmin = new SysAdmin
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = hashedPassword
                };
                _context.SysAdmins.Add(sysAdmin);
                _context.SaveChanges();
                userId = sysAdmin.Id;
                rol = "SysAdmin";
            }

            return new AuthResponse
            {
                Token = GenerateToken(userId, request.Email, rol),
                Rol = rol,
                UserId = userId,
                Email = request.Email
            };
        }
        public AuthResponse? SingIn(SignInRequest request)
        {
            int userId;
            string rol;
            var client = _context.Clients.FirstOrDefault(c => c.Email == request.Email);
            if (client != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
                {
                    return null;
                }
                userId = client.Id;
                rol = "Client";
            }
            else
            {
                var admin = _context.Admins.FirstOrDefault(a => a.Email == request.Email);
                if (admin != null)
                {
                    if (!BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
                    {
                        return null;
                    }
                    userId = admin.Id;
                    rol = "Admin";

                }
                else
                {
                    var sysAdmin = _context.SysAdmins.FirstOrDefault(u => u.Email == request.Email);
                    if (sysAdmin != null)
                    {
                        if (!BCrypt.Net.BCrypt.Verify(request.Password, sysAdmin.Password))
                        {
                            return null;
                        }
                        userId = sysAdmin.Id;
                        rol = "SysAdmin";
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            return new AuthResponse
            {
                Token = GenerateToken(userId, request.Email, rol),
                Rol = rol,
                UserId = userId,
                Email = request.Email
            };
        }
        private string GenerateToken(int userId, string email, string rol)
        {
            string key = _configuration["Jwt:Key"]!;
            string issuer = _configuration["Jwt:Issuer"]!;
            string audience = _configuration["Jwt:Audience"]!;
            int expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]!);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), 
                    ClaimValueTypes.Integer64)
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
