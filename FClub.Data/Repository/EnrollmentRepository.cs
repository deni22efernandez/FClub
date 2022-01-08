using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public EnrollmentRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<bool> UpdateAsync(Enrollment enrollment)
		{
			throw new NotImplementedException();
		}
	}
}
