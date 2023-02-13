using Microsoft.EntityFrameworkCore;
using WorkListAPI.Src.Models;

namespace WorkListAPI.Src.Contexts
{
    /// <summary>
    /// <para>summary: Class Context, responsable to charge context and define DbSets</para>
    /// <para>Created by: FelipeFlor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 06/02/2023
    /// </summary>/
    public class Context : DbContext
    {
        #region Attributes
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        #endregion

        #region Constructors
        public Context(DbContextOptions<Context> opt) : base(opt)
        {

        }
        #endregion
    }
}
