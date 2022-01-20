using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _contxt;
		public OrderHeaderRepository(ApplicationDbContext contxt):base(contxt)
		{
			_contxt = contxt;
		}
		public void Update(OrderHeader orderHeader)
		{
			 _contxt.Entry<OrderHeader>(orderHeader).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		}
	}
}
