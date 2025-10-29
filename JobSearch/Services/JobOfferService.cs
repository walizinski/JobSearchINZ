using JobSearch.Data;
using JobSearch.Services;
using Microsoft.EntityFrameworkCore;

public sealed class JobOfferService : IJobOfferService
{
	private readonly IDbContextFactory<ApplicationDbContext> _factory;
	public JobOfferService(IDbContextFactory<ApplicationDbContext> factory) => _factory = factory;

	public async Task<int> CreateAsync(JobOffer offer, CancellationToken ct = default)
	{
		await using var db = await _factory.CreateDbContextAsync(ct);
		db.JobOffer.Add(offer);
		await db.SaveChangesAsync(ct);
		return offer.Id;
	}

	public async Task<List<JobOffer>> GetAllAsync(CancellationToken ct = default)
	{
		await using var db = await _factory.CreateDbContextAsync(ct);
		return await db.JobOffer.AsNoTracking().OrderByDescending(x => x.DatePosted).ToListAsync(ct);
	}

	public async Task<JobOffer?> GetByIdAsync(int id, CancellationToken ct = default)
	{
		await using var db = await _factory.CreateDbContextAsync(ct);
		return await db.JobOffer.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
	}

	public async Task UpdateAsync(JobOffer offer, CancellationToken ct = default)
	{
		await using var db = await _factory.CreateDbContextAsync(ct);
		db.JobOffer.Attach(offer);
		db.Entry(offer).State = EntityState.Modified;
		await db.SaveChangesAsync(ct);
	}

	public async Task DeleteAsync(int id, CancellationToken ct = default)
	{
		await using var db = await _factory.CreateDbContextAsync(ct);
		var entity = await db.JobOffer.FirstOrDefaultAsync(x => x.Id == id, ct);
		if (entity is null) return;
		db.JobOffer.Remove(entity);
		await db.SaveChangesAsync(ct);
	}
}
