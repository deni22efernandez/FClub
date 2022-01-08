﻿using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IAppUserRepository:IRepository<AppUser>
	{
		Task<bool> UpdateAsync(AppUser appUser);
	}
}
