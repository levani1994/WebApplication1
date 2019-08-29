
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models.Account
{
    public class RegistrationViewModel
    {
       
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}