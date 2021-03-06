﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AirlineResSystem.BusinessLayer.Model
{
    public class UserDetailDO
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = " *")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " *")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be atleast 6 characters in length")]
        public string Password { get; set; }

        [Required(ErrorMessage = " *")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords to not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " *")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " *")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " *")]
        [StringLength(2, ErrorMessage = "Enter only country code")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = " *")]
        [StringLength(10, ErrorMessage = "Max of 10 numbers")]
        [Phone(ErrorMessage = "Only numbers allowed")]
        public string Mobile { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = " *")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = " *")]
        [EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        public string PassportNo { get; set; }

        public string MiscInfo { get; set; }

    }


}
