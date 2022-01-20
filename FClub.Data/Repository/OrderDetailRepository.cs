using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Data.Repository
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public OrderDetailRepository(ApplicationDbContext dbContext):base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(OrderDetail orderDetail)
		{
			_dbContext.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		}
	}
}
