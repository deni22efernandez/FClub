using FClub.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FClub.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{

		}
		public DbSet<Activitty> Activities { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<FromToPeriod> FromToPeriods { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Person> People { get; set; }
		public DbSet<Shift> Shifts { get; set; }
	}
}
