using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGym.Models
{
    public class Payment
    {
        public int MemberID { get; set; }
        public int PaymentID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contactno { get; set; }
        public string EmailID { get; set; }
        public string MemberNo { get; set; }
        public string PlanName { get; set; }
        public string SchemeName { get; set; }
        public DateTime? PaymentFromdt { get; set; }
        public string RenwalDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int WorkouttypeID { get; set; }
        public int PlanID { get; set; }
        public string MemberReg { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int CreateUserID { get; set; }
        public int ModifyUserID { get; set; }
        public String Paymenttype { get; set; }
        public DateTime PaymentTodt { get; set; }
        public DateTime ModifyDate { get; set; }
        public String RecStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime NextRenwalDate { get; set; }
        public string PayPalID { get; set; }
        public string PaymentType { get; set; }


        public IEnumerable<Payment> SearchResults { get; set; }
    }

    public class PaymentAutocomp
    {
        public string MemberNo { get; set; }
        public int MainMemberID { get; set; }
        public string Name { get; set; }
        public int MemID { get; set; }
    }
}