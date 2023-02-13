using System.Threading.Tasks;
using WorkListAPI.Src.Models;

namespace WorkListAPI.Src.Repositories
{

    /// <summary>
    /// <para>summary: Responsable to represent the actions of CRUD in user</para>
    /// <para>Created by: Felipe Flor</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 11/02/2023</para>
    /// </summary>

    public interface IUser
    {
        Task<User> FindUserByEmailAsync(string email);
        Task NewUserAsync(User user);

    }
}
