using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services
{
	public interface IJobOfferService
	{
		Task<List<JobOffer>> GetAllAsync(CancellationToken ct = default);
		Task<JobOffer?> GetByIdAsync(int id, CancellationToken ct = default);
		Task<int> CreateAsync(JobOffer offer, CancellationToken ct = default);
		Task UpdateAsync(JobOffer offer, CancellationToken ct = default);
		Task DeleteAsync(int id, CancellationToken ct = default);
	}
}
