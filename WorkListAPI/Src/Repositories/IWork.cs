using System.Collections.Generic;
using System.Threading.Tasks;
using WorkListAPI.Src.Models;

namespace WorkListAPI.Src.Repositories
{
    /// <summary>
    /// <para>summary: Responsable to represent the actions of a "work" CRUD</para>
    /// <para>Created By: Felipe Flor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 23/02/2023</para>
    /// </summary>

    public interface IWork
    {
        Task<List<Work>> FindAllWorksAsync();
        Task<Work> FindWorkByIdAsync(int id);
        Task NewWorkAsync(Work work);
        Task UpdateWorkAsync(Work work);
        Task DeleteWorkAsync(int id);
    }
}
