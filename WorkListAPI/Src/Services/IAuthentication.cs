using System.Threading.Tasks;
using WorkListAPI.Src.Models;

namespace WorkListAPI.Src.Services
{
    /// <summary>
    /// <para>summary: Interface responsable to represent authentication actions</para>
    /// <para>Created by: Felipe Flor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 24/02/2023</para>
    /// </summary>
    public interface IAuthentication
    {
        string EncryptedPassword(string password);
        Task CreateUserWithoutDuplicateAsync(User user);
        string GenerateToken(User user);
    }
}
