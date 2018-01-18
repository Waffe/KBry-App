using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kbry.Data.Model;

namespace Kbry.MVC.ViewModels
{
    public class SchoolClassViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Number of Students")]
        public int NumberOfStudents { get; set; }
    }
}