using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyGym.Models
{
    public class SchemeMasterDTO
    {
        [Key]
        public int SchemeID { get; set; }
        [Required(ErrorMessage = "Please enter Scheme Name")]
        [Remote("SchemeNameExists", "Scheme", ErrorMessage = "Scheme Name Already Exists ")]
        public string SchemeName { get; set; }
        public string Createdby { get; set; }
        public DateTime Createddate { get; set; }
    }
}