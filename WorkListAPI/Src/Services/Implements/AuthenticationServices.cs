using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkListAPI.Src.Models;
using WorkListAPI.Src.Repositories;

namespace WorkListAPI.Src.Services.Implements
{
    /// <summary>
    /// <para>summary: Class responsable to implement IAuthentication</para>
    /// <para>Created by: Felipe Flor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 24/02/2023</para>
    /// </summary>

    public class AuthenticationServices : IAuthentication
    {
        #region Attributes
        private IUser _repository;
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructors
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// <para>summary: Method responsable to encrypt password</para>
        /// </summary>
        /// <param name="password">Password to be encrypted</param>
        /// <returns>string</returns>
        public string EncryptedPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>summary: Method async responsable for create user without duplicate</para>
        /// </summary>
        /// <param name="user">Constructor for register user</param>
        public async Task CreateUserWithoutDuplicateAsync(User user)
        {
            var aux = await _repository.FindUserByEmailAsync(user.Email);

            if (aux != null) throw new Exception("Email already been used.");

            user.Password = EncryptedPassword(user.Password);

            await _repository.NewUserAsync(user);
        }

        /// <summary>
        /// <para>summary: Method responsable for generated token JWT</para>
        /// </summary>
        /// <param name="user">User constructor that has parameters of email and password</param>
        /// <returns>string</returns>
        public string GenerateToken(User user)
        {
            var tokenManipulator = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };
            var token = tokenManipulator.CreateToken(tokenDescription);
            return tokenManipulator.WriteToken(token);
        }
        #endregion
    }
}
