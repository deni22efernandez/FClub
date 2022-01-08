﻿using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class FromToPeriodRepository : Repository<FromToPeriod>, IFromToPeriodRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public FromToPeriodRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<bool> UpdateAsync(FromToPeriod fromToPeriod)
		{
			throw new NotImplementedException();
		}
	}
}
