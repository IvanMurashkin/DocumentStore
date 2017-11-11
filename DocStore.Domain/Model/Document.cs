using Services.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DocStore.Domain.Model {

    public class Document : IDocument {

        [HiddenInput(DisplayValue = false)]
        public virtual long Id { get; set; }

        [Required(ErrorMessage = "Введите название документа")]
        [MinLength(3)]
        [Display(Name = "Имя")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Отсутствует дата создания")]
        [Display(Name = "Дата")]
        public virtual DateTime Date { get; set; }

        public virtual byte[] DocumentData { get; set; }

        public virtual string OriginalName { get; set; }

        [MaxLength(50)]
        public virtual string DocumentMimeType { get; set; }

        public virtual User User { get; set; }

    }

}
