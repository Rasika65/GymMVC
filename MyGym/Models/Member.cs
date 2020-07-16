using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGym.Models
{
    [MetadataType(typeof(MemberMetaData))]
    public class Member
    {
        public Int64 MemID { get; set; }
        public string MemberNo { get; set; }
        public string MemberFName { get; set; }
        public string MemberLName { get; set; }
        public string MemberMName { get; set; }
        public DateTime? DOB { get; set; }
        public string Age { get; set; }
        public string Contactno { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public int PlantypeID { get; set; }
        public int WorkouttypeID { get; set; }
        public Int64 Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string MemImagename { get; set; }
        public string MemImagePath { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Address { get; set; }
        public Int64 MainMemberID { get; set; }

        public Int64 Amount { get; set; }
        [NotMapped]
        public IEnumerable<Scheme> ListScheme { get; set; }
        [NotMapped]
        public IEnumerable<Plan> ListPlan { get; set; }
    }

    public class MemberMetaData
    {
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[-a-zA-Z]+$", ErrorMessage = "Name field should contain alphabets only")]
        public string MemberFName { get; set; }
                
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[-a-zA-Z]+$", ErrorMessage = "Name field should contain alphabets only")]
        public string MemberLName { get; set; }
                
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[-a-zA-Z]+$", ErrorMessage = "Name field should contain alphabets only")]
        public string MemberMName { get; set; }

        [StringLength(70)]
        [Required]
        public string Address { get; set; }

        //[Required]
        //public DateTime DOB { get; set; }

        [Required]
        [Range(14,150, ErrorMessage = "Minimun age for registration is 14")]
        public string Age { get; set; }

        [Required]
        [StringLength(10,MinimumLength = 10,ErrorMessage="Contact number must contain 10 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter a valid contact number")]
        public string Contactno { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string EmailID { get; set; }

        [Required]
        public DateTime? DOB { get; set; }

        [Required]
        public DateTime? JoiningDate { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}