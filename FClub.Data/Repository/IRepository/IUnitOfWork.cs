using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IUnitOfWork:IDisposable
	{
		Task<bool> SaveAsync();
		IActivittyRepository ActivittyRepository { get;  }
		IAppUserRepository AppUserRepository { get;}
		IEnrollmentRepository EnrollmentRepository { get;}
		IFromToPeriodRepository FromToPeriodRepository { get;  }
		IInstructorRepository InstructorRepository { get;}
		IShiftRepository ShiftRepository { get;  }
		IWeekDaysRepository WeekDaysRepository { get; }
		IActivittyDaysRepository ActivittyDaysRepository { get; }
		IOrderHeaderRepository OrderHeaderRepository { get; }
		IOrderDetailRepository OrderDetailRepository { get; }
		IPeopleRepository PeopleRepository { get; }
	}
}
