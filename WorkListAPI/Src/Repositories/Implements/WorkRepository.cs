using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkListAPI.Src.Contexts;
using WorkListAPI.Src.Models;

namespace WorkListAPI.Src.Repositories.Implements
{
    /// <summary>
    /// <para>summary: Class responsable to implement IWork</para>
    /// <para>Created By: Felipe Flor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 23/02/2023</para>
    /// </summary>
    public class WorkRepository : IWork
    {
        #region Attributes
        private readonly Context _context;
        #endregion

        #region Constructors
        public WorkRepository(Context context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// <para>summary: Method async to get all works</para>
        /// </summary>
        /// <return>List WorkModel></return>
        public async Task<List<Work>> FindAllWorksAsync()
        {
            return await _context.Works
                .Include(w => w.Creator)
                .ToListAsync();
        }

        /// <summary>
        /// <para>summary: Method async to find a work by Id</para>
        /// </summary>
        /// <param name="id">Id of work</param>
        /// <return>WorkModel</return>
        /// <exception cref="Exception">Id can't br null</exception>
        public async Task<Work> FindWorkByIdAsync(int id)
        {
            if (!ExistId(id)) throw new Exception("Work Id not found!");

            return await _context.Works
                .Include(w => w.Creator)
                .FirstOrDefaultAsync(w => w.Id == id);

            bool ExistId(int id)
            {
                var aux = _context.Works.FirstOrDefaultAsync(u => u.Id == id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>summary: Method async to save a new work</para>
        /// </summary>
        /// <param name="work">Constructor to register work</param>
        /// <exception cref="Exception">Id can't be null</exception>
        public async Task NewWorkAsync(Work work)
        {
            if (!ExistUserId(work.Creator.Id)) throw new Exception("User Id not found");

            await _context.Works.AddAsync(
                new Work
                {
                    Title = work.Title,
                    Description = work.Description,
                    Status = work.Status,
                    Creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == work.Creator.Id)
                });

            await _context.SaveChangesAsync();

            //Auxiliary function
            bool ExistUserId(int id)
            {
                var aux = _context.Users.FirstOrDefault(u => u.Id == id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>summary: Method async to update work</para>
        /// </summary>
        /// <param name="work">Constructor to update work</param>
        /// <exception cref="Exception">Id can't be null</exception>
        public async Task UpdateWorkAsync(Work work)
        {
            var workExist = await FindWorkByIdAsync(work.Id);
            workExist.Title = work.Title;
            workExist.Description = work.Description;
            workExist.Status = work.Status;

            _context.Works.Update(workExist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkAsync(int id)
        {
            _context.Works.Remove(await FindWorkByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
