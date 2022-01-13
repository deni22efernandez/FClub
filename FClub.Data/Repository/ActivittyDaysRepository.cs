using FClub.Data.Repository.IRepository;
using FClub.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Data.Repository
{
	public class ActivittyDaysRepository:Repository<ActivittyDays>,IActivittyDaysRepository
	{
		private readonly ApplicationDbContext _context;
		public ActivittyDaysRepository(ApplicationDbContext context):base(context)
		{
			_context = context;
		}
	}
}
