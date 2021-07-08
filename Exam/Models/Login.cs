using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Username")]
        public string LoginUserName {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPassword {get;set;}
        
    }
}