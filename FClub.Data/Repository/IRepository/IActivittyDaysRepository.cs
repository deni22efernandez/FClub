using FClub.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IActivittyDaysRepository:IRepository<ActivittyDays>
	{
		Task UpdateAsync(int actId, List<ActivittyDays> activittyDays);
	}
}
