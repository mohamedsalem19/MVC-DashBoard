﻿using MVC_Project.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MVC_Project.PL.ViewModels
{
	public class EmployeeViewModel
	{

		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		[MaxLength(50)]
		[MinLength(2)]
		public string Name { get; set; }

		[Range(20, 30)]
		public int? Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$"
			, ErrorMessage = "Address must be like 123-Street-City-Country")]
		public string Address { get; set; }

		
		public decimal Salary { get; set; }

		public bool IsActive { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }

		public DateTime HireDate { get; set; }

		public IFormFile Image { get; set; }

		public string ImageName { get; set; }


		public int? DepartmentId { get; set; }
		public Department Department { get; set; }

	}
}
