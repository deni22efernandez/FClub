using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class InstructorRepository : Repository<Instructor>, IInstructorRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<bool> UpdateAsync(Instructor instructor)
		{
			throw new NotImplementedException();
		}
	}
}
