using JobSearch.Data;
using JobSearch.Services;

public interface IJobOfferService
{
   
    Task<List<JobOffer>> GetJobOffersAsync(string? location, decimal? minSalary, string? sortBy, EmploymentType? employmentType, JobType? jobType);

    Task<JobOffer?> GetByIdAsync(int id);
    Task CreateAsync(JobOffer jobOffer);
    Task UpdateAsync(JobOffer jobOffer);
    Task DeleteAsync(int id);
}