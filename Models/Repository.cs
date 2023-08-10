using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebUygulamaProje.Context;

namespace WebUygulamaProje.Models
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _applicationDbContext;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			this.dbSet = _applicationDbContext.Set<T>();
			_applicationDbContext.Books.Include(x => x.BookType).Include(x => x.BookTypeId);
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filtre);

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> query = dbSet;

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach(var includeProp  in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.ToList();
		}
	}
}
