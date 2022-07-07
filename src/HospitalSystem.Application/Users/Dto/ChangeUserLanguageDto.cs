using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}