using System.ComponentModel.DataAnnotations;

namespace GoseiVn.DemoApp.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}