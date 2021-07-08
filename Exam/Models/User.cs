using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}

        [Required]
        [MinLength(2, ErrorMessage ="First name must be at least 2 character")]
        public string FName {get;set;}

        [Required]
        [MinLength(2 ,  ErrorMessage ="Last name must be at least 2 character")]
        public string LName {get; set;}

        [Required]
        [MinLength(3, ErrorMessage ="Username must be at least 3 character")]
        public string UserName {get;set;}
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password must be at least 8 character")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords does not match!")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;

        public List<Hobby> CreatedHobby {get;set;}
        public List<UserHobby> UserHobby {get;set;} 

    }
}