using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IEnrollmentRepository:IRepository<Enrollment>
	{
		Task<bool> UpdateAsync(Enrollment enrollment);
	}
}
