using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class ActivittyRepository : Repository<Activitty>, IActivittyRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public ActivittyRepository(ApplicationDbContext dbContext):base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Activitty activitty)
		{
			_dbContext.Entry<Activitty>(activitty).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
		}

	}
}
