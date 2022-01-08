﻿using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class ShiftRepository : Repository<Shift>, IShiftRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public ShiftRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<bool> UpdateAsync(Shift shift)
		{
			throw new NotImplementedException();
		}
	}
}
