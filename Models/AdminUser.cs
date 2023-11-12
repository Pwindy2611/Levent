//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Levent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AdminUser
    {
        [Required(ErrorMessage = "ID not Empty.....")]
        public int ID_User { get; set; }

        [Required(ErrorMessage = "Name not Empty.....")]
        public string User_Name { get; set; }
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Email not Empty....")]
        public string Email_User { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a valid password")]
        public string Password_User { get; set; }
        public Nullable<int> Role { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
