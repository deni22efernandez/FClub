using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Data.Repository
{
	public class WeekDaysRepository:Repository<WeekDays>,IWeekDaysRepository
	{
		private readonly ApplicationDbContext _context;
		public WeekDaysRepository(ApplicationDbContext context):base(context)
		{
			_context = context;
		}
	}
}
