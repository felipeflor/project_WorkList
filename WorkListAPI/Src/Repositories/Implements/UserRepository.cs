using System.Threading.Tasks;
using WorkListAPI.Src.Models;
using WorkListAPI.Src.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkListAPI.Src.Repositories;

namespace WorkListAPI.Src.Repositories.Implements
{
    /// <summary>
    /// <para>summary: Class responsable to implement IUser</para>
    /// <para>Created by: Felipe Flor</para>
    /// <para>Versin: 1.0</para>
    /// <para>Date: 11/02/2023</para>
    /// </summary>
    public class UserRepository : IUser
    {
        #region Attributes
        private readonly Context _context;
        #endregion

        #region Constructors
        public UserRepository(Context context)
        {
            _context = context;
        }
        #endregion

        #region 
        /// <summary>
        /// <para>summary: Method async to get an user by email</para>
        /// <para>Created by: Felipe Flor</para>
        /// <para>Version: 1.0</para>
        /// <para>Date: 11/02/2023</para>
        /// </summary>
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>summary: Method async to save a new user</para>
        /// <para>Created by: Felipe Flor</para>
        /// <para>Version: 1.0</para>
        /// <para>Date: 11/02/2023</para>
        /// </summary>
        public async Task NewUserAsync(User user)
        {
            await _context.Users.AddAsync(
                new User
                {
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    Type = user.Type
                });
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
