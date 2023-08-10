using WebUygulamaProje.Context;

namespace WebUygulamaProje.Models
{
	public class HireRepository : Repository<Hire>, IHireRepository
	{
		private ApplicationDbContext _applicationDbContext;
		public HireRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(Hire hire)
		{
			_applicationDbContext.Update(hire);
		}
	}
}
