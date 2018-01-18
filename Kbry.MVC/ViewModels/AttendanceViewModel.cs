using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Kbry.MVC.ViewModels
{
    public class AttendanceViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Date { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string StudentName => $"{StudentLastName}, {StudentFirstName}";

        public string StudentClass { get; set; }
    }
}