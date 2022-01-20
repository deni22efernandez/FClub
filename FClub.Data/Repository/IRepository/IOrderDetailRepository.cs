using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Data.Repository.IRepository
{
	public interface IOrderDetailRepository:IRepository<OrderDetail>
	{
		void Update(OrderDetail orderDetail);
	}
}
