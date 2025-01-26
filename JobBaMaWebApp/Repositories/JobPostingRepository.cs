using JobBaMaWebApp.Data;
using JobBaMaWebApp.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace JobBaMaWebApp.Repositories
{
    public class JobPostingRepository : IRepository<JobPosting>
    {
        private readonly ApplicationDbContext _context;
        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(JobPosting entity)
        {
            _context.JobPostings.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.JobPostings.FindAsync(id);
            if (entity == null) 
            {
                throw new KeyNotFoundException();
            }
            _context.JobPostings.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.JobPostings.ToListAsync();
        }

        public async Task<JobPosting> GetByIdAsync(int id)
        {
            var entity = await _context.JobPostings.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            return entity;
        }

        public async Task UpdateAsync(JobPosting entity)
        {
            _context.JobPostings.Update(entity);
           await _context.SaveChangesAsync();
        }
    }
}
