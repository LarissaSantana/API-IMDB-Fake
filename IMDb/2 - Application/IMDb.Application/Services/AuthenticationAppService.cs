using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels.User;
using IMDb.Domain.Core.Security;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IMDb.Application.Services
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurity _security;

        public AuthenticationAppService(IUserRepository userRepository, ISecurity security)
        {
            _userRepository = userRepository;
            _security = security;
        }

        public string Authenticate(UserLoginViewModel viewModel, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(viewModel.Password) || string.IsNullOrWhiteSpace(viewModel.Name))
            {
                error = "Invalid username or password.";
                return null;
            }

            var hashPassword = _security.Encrypt(viewModel.Password, viewModel.Name);
            var user = _userRepository.GetByFilters(user =>
                                        user.Name.ToLower().Equals(viewModel.Name.ToLower()) &&
                                        user.Password.ToLower().Equals(hashPassword))?.FirstOrDefault();
            if (user == null)
            {
                error = "Invalid username or password.";
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("64620a926ff863676225015795ee096944eb85452ffb2d48ffc194852ac2769f");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
