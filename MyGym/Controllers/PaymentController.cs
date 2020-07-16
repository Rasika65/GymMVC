using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGym.Models;
using MyGym.Repository;

namespace MyGym.Controllers
{
    public class PaymentController : Controller
    {
        private BusinessRules br = new BusinessRules();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllMemberList()
        {            
            return View(br.AllPaymentDetails());
        }

        public ActionResult PaymentDetails(string SearchbyId, string SearchbyName)
        {
            if ((SearchbyId != null) && (SearchbyId!=""))
            {
                IEnumerable<Payment> PaymentList = br.AllPaymentDetails();
                return View(PaymentList.Where(x => x.MemberReg == SearchbyId));                
            }
            if ((SearchbyName != null) && (SearchbyName != ""))
            {
                if (SearchbyName.Contains("|"))
                {
                    string str = SearchbyName.Substring(0, (SearchbyName.LastIndexOf("|") - 1));
                    return View(br.AllPaymentDetailsByName(str));
                }
                else
                {
                    return View(br.AllPaymentDetailsByName(SearchbyName));
                }
                
            }
            
            return View();
        }

        public JsonResult GetPaymentbyId(string term)
        {
            List<string> list = br.ListofMemberNo(term).Select(y => y.MemberNo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaymentbyName(string term)
        {
            List<string> list = br.ListofMemberName(term).Select(y => y.Name).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
