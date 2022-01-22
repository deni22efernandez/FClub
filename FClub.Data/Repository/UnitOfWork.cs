using FClub.Data.Repository.IRepository;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _contxt;
		public UnitOfWork(ApplicationDbContext contxt)
		{
			_contxt = contxt;
			ActivittyRepository = new ActivittyRepository(contxt);
			AppUserRepository = new AppUserRepository(contxt);
			EnrollmentRepository = new EnrollmentRepository(contxt);
			FromToPeriodRepository = new FromToPeriodRepository(contxt);
			InstructorRepository = new InstructorRepository(contxt);
			ShiftRepository = new ShiftRepository(contxt);
			WeekDaysRepository = new WeekDaysRepository(contxt);
			ActivittyDaysRepository = new ActivittyDaysRepository(contxt);
			OrderDetailRepository = new OrderDetailRepository(contxt);
			OrderHeaderRepository = new OrderHeaderRepository(contxt);
			PeopleRepository = new PeopleRepository(contxt);

		}

		public IActivittyRepository ActivittyRepository { get; private set; }
		public IAppUserRepository AppUserRepository { get; private set; }
		public IEnrollmentRepository EnrollmentRepository { get; private set; }
		public IFromToPeriodRepository FromToPeriodRepository { get; private set; }
		public IInstructorRepository InstructorRepository { get; private set; }
		public IShiftRepository ShiftRepository { get; private set; }

		public IWeekDaysRepository WeekDaysRepository { get; private set; }

		public IActivittyDaysRepository ActivittyDaysRepository { get; private set; }

		public IOrderHeaderRepository OrderHeaderRepository { get; private set; }

		public IOrderDetailRepository OrderDetailRepository { get; private set; }

		public IPeopleRepository PeopleRepository { get; private set; }

		public void Dispose()
		{
			_contxt.Dispose();
		}

		public async Task<bool> SaveAsync()
		{
			var result = await _contxt.SaveChangesAsync();
			return result != 0 ? true : false;
		}
	}
}
