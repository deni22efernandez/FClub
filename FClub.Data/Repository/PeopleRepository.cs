using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Data.Repository
{
	public class PeopleRepository : Repository<Person>, IPeopleRepository
	{
		private readonly ApplicationDbContext _contxt;
		public PeopleRepository(ApplicationDbContext contxt):base(contxt)
		{
			_contxt = contxt;
		}
	}
}
