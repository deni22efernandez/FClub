﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models
{
	public abstract class Person
	{
		public abstract int Id { get; set; }
		public abstract string Name { get; set; }
		public abstract string LastName { get; set; }
		public string FulName { get { return this.Name + ", " + this.LastName; } }
		public abstract string Email { get; set; }
		public abstract string Mobile { get; set; }
		public abstract string Address { get; set; }
		//[Required]
		public virtual string Username { get { return this.Username; } set { this.Username = value; } }
		//[Required]
		[DataType(DataType.Password)]
		public virtual string Password { get { return this.Password; } set { this.Password = value; } }
	}
}
