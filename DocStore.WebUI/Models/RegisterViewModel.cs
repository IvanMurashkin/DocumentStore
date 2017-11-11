using System.ComponentModel.DataAnnotations;

namespace DocStore.WebUI.Models {

    public class RegisterViewModel {

        [Required(ErrorMessage = "Введите логин (не мнее 4-х символов)")]
        [MinLength(4)]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль (не менее 8-и символов)")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

}