using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exam.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public string Description {get; set;}

        public int UserId {get;set;}
        public User Creator {get;set;}
        public List<UserHobby> UserHobby {get;set;} 

        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;
    }
}