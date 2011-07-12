﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Babich.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {

    }

    [Bind(Exclude = "Id")]
    public class ContentValidation
    {
        [Required(ErrorMessage = "* Введите веб-имя страницы")]
        [DisplayName("Веб-имя страницы")]
        public string Name { get; set; }

        [DisplayName("Заголовок в шапке обозревателя")]
        public string PageTitle { get; set; }

        [Required(ErrorMessage = "* Введите заголовок")]
        [DisplayName("Заголовок разворачиваемой области")]
        public string Title { get; set; }

        [DisplayName("Текст над подразделами")]
        public string Description { get; set; }

        [Required(ErrorMessage = "* Введите порядок отображения (целое число)")]
        [DisplayName("Порядок отбражения")]
        public string SortOrder { get; set; }

        [DisplayName("Содержимое страницы")]
        public string Text { get; set; }

        [DisplayName("Ключевые слова этой страницы")]
        public string SeoKeywords { get; set; }

        [DisplayName("Описание страницы")]
        public string SeoDescription { get; set; }

    }
}