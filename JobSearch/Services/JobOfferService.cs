using JobSearch.Data;
using JobSearch.Services;
using Microsoft.EntityFrameworkCore;

public class JobOfferService : IJobOfferService
{
    private readonly ApplicationDbContext _context;

    public JobOfferService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobOffer>> GetJobOffersAsync(string? location, decimal? minSalary, string? sortBy, EmploymentType? employmentType, JobType? jobType)
    {
        var query = _context.JobOffer.Where(o => o.IsActive).AsQueryable();

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(o => o.Location.ToLower().Contains(location.ToLower()));
        }

        if (minSalary.HasValue && minSalary.Value > 0)
        {
            query = query.Where(o => o.SalaryMin >= minSalary.Value);
        }

      
        if (employmentType.HasValue)
        {
            query = query.Where(o => o.EmplType == employmentType.Value);
        }

        if (jobType.HasValue)
        {
            query = query.Where(o => o.JobType == jobType.Value);
        }
      
        switch (sortBy)
        {
            case "salary_desc":
                query = query.OrderByDescending(o => o.SalaryMin);
                break;
            case "salary_asc":
                query = query.OrderBy(o => o.SalaryMin);
                break;
            default:
                query = query.OrderByDescending(o => o.DatePosted);
                break;
        }

        return await query.ToListAsync();
    }

    public async Task<JobOffer?> GetByIdAsync(int id)
    {
        return await _context.JobOffer.FindAsync(id);
    }

    public async Task CreateAsync(JobOffer jobOffer)
    {
        jobOffer.DatePosted = DateTime.Now;
        _context.JobOffer.Add(jobOffer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobOffer jobOffer)
    {
        _context.Entry(jobOffer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var jobOffer = await _context.JobOffer.FindAsync(id);
        if (jobOffer != null)
        {
            _context.JobOffer.Remove(jobOffer);
            await _context.SaveChangesAsync();
        }
    }
}