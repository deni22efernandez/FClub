using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class AppUserRepository:Repository<AppUser>,IAppUserRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public AppUserRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<bool> UpdateAsync(AppUser appUser)
		{
			throw new NotImplementedException();
		}
	}
}
