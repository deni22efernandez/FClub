using FClub.Data.Repository.IRepository;
using FClub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class ActivittyDaysRepository:Repository<ActivittyDays>,IActivittyDaysRepository
	{
		private readonly ApplicationDbContext _context;
		public ActivittyDaysRepository(ApplicationDbContext context):base(context)
		{
			_context = context;
		}
		public async Task UpdateAsync(int actId, List<ActivittyDays> days)
		{
			var actDay = await _context.ActivittyDays.Where(x => x.ActivittyId == actId).ToListAsync();
			_context.ActivittyDays.RemoveRange(actDay);
			_context.ActivittyDays.UpdateRange(days);
		}
	}
}
