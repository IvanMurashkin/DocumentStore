using Services.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DocStore.Domain.Model {

    public class User : IUser {

        [HiddenInput(DisplayValue = false)]
        public virtual long Id { get; set; }

        [Required(ErrorMessage = "Введите логин (не менее 4-х символов)")]
        [MinLength(4)]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль (не мнее 8-и символов)")]
        [MinLength(8)]
        public virtual string Password { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public User() {
            Documents = new List<Document>();
        }

    }

}
