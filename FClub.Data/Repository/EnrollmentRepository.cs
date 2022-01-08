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
		public void Update(Enrollment enrollment)
		{
			_dbContext.Entry<Enrollment>(enrollment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		}
	}
}
