using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IFromToPeriodRepository:IRepository<FromToPeriod>
	{
		Task<bool> UpdateAsync(FromToPeriod fromToPeriod); 
	}
}
