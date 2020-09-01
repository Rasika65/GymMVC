using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGym.Models;
using MyGym.Repository;


namespace MyGym.Controllers
{
    public class MemberController : Controller
    {
        private GymContext db = new GymContext();
        private Member member = new Member();
        private BusinessRules br = new BusinessRules();
        private Payment payment = new Payment();
        //
        // GET: /Member/

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult RegisterMember()
        {
            member.ListScheme = db.SchemeList.ToList();
            ViewBag.planlist = Enumerable.Empty<SelectListItem>();
            return View(member);
        }

        //[HttpPost]
        //[Authorize(Roles = "User,Admin")]
        //[ActionName("RegisterMember")]
        //public ActionResult RegisterMember1(Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        member.Createdby = Convert.ToInt32(Session["UserId"]);
        //        int MemberID = br.InsertMember(member);
        //        if (MemberID > 0)
        //        {
        //            int payresult = Pay(member, MemberID);
        //            if (payresult > 0)
        //            {
        //                TempData["Message"] = "Member Created Successfully.";
        //            }
        //            else
        //            {
        //                TempData["Message"] = "Some thing went wrong while Member Created .";
        //            }
        //        }
        //        else
        //        {
        //            TempData["Message"] = "Some thing went wrong while Member Created .";
        //        }
        //        return RedirectToAction("GetAllMembers");
        //    }
        //    member.ListScheme = db.SchemeList.ToList();
        //    ViewBag.planlist = new SelectList(db.PlanList, "PlanID", "PlanName");
        //    return View(member);
        //}

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        [ActionName("RegisterMember")]
        public ActionResult RegisterMember1(Member member)
        {
            if (ModelState.IsValid)
            {
                TempData["memberdata"] = member;
                return RedirectToAction("ConfirmPayment");
            }
            member.ListScheme = db.SchemeList.ToList();
            ViewBag.planlist = new SelectList(db.PlanList, "PlanID", "PlanName");
            return View(member);
        }


        [HttpGet]
        public ActionResult GetAllMembers()
        {
            BusinessRules br = new BusinessRules();
            return View(br.RegistrationSelectList());
        }              

        [HttpGet]
        public ActionResult Delete(string MemberId)
        {
            br.DeleteMember(MemberId);
            return RedirectToAction("GetAllMembers");
        }

        
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult ConfirmPayment()
        {
            Member member = (Member)TempData.Peek("memberdata");
            member.ListScheme = db.SchemeList.ToList();
            member.ListPlan = db.PlanList.ToList();
            
            return View(member);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        [ActionName("ConfirmPayment")]
        public ActionResult ConfirmPayment1()
        {
            Member member = (Member)TempData["memberdata"];

            member.Createdby = Convert.ToInt32(Session["UserId"]);
            int MemberID = br.InsertMember(member);
            if (MemberID > 0)
            {
                int payresult = Pay(member, MemberID, null);
                if (payresult > 0)
                {
                    TempData["Message"] = "Member Created Successfully.";
                    return RedirectToAction("GetAllMembers");
                }
                else
                {
                    TempData["Message"] = "Some thing went wrong! Please try again.";
                    return RedirectToAction("RegisterMember");
                }
            }
            else
            {
                TempData["Message"] = "Some thing went wrong! Please try again.";
                return RedirectToAction("RegisterMember");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult PaymentSucccess(string tran_Id)
        {
            Member member = (Member)TempData["memberdata"];

            member.Createdby = Convert.ToInt32(Session["UserId"]);
            int MemberID = br.InsertMember(member);
            if (MemberID > 0)
            {
                int payresult = Pay(member, MemberID, tran_Id);
                if (payresult > 0)
                {
                    TempData["Message"] = "Member Created Successfully.";
                    return RedirectToAction("GetAllMembers");
                }
                else
                {
                    TempData["Message"] = "Some thing went wrong! Please try again.";
                    return RedirectToAction("RegisterMember");
                }
            }
            else
            {
                TempData["Message"] = "Some thing went wrong! Please try again.";
                return RedirectToAction("RegisterMember");
            }            
        }

        
        [NonAction]
        public int Pay(Member obj, int MemberID, string paypal_Id)
        {
            try
            {
                payment.PaymentID = 0;
                payment.PlanID = Convert.ToInt32(obj.PlantypeID);
                payment.WorkouttypeID = Convert.ToInt32(obj.WorkouttypeID);

                string[] joing = obj.JoiningDate.ToString().Split('/');
                int year2 = Convert.ToInt32(obj.JoiningDate.Value.Year);
                int month2 = Convert.ToInt32(obj.JoiningDate.Value.Month);
                int day2 = Convert.ToInt32(obj.JoiningDate.Value.Day);
                DateTime joining = new DateTime(year2, month2, day2);
                payment.PaymentFromdt = joining;
                payment.PaymentAmount = Convert.ToDecimal(obj.Amount);
                payment.CreateUserID = Convert.ToInt32(Session["UserID"]);
                payment.ModifyUserID = 0;
                payment.MemberID = MemberID;
                payment.PayPalID = paypal_Id;

                if (!string.IsNullOrEmpty(payment.PayPalID))
                {
                    payment.PaymentType = "Online_paypal";
                }
                else
                {
                    payment.PaymentType = "Cash";
                }

                int payresult = br.InsertPaymentDetails(payment);

                return payresult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public JsonResult GetAmount(string PlanID, string WorkTypeID)
        {
            var amount = br.GetAmount(PlanID, WorkTypeID);
            return Json(amount, JsonRequestBehavior.AllowGet);
        }
                
        public JsonResult GetPlan(string WorkTypeID)
        {
            var plandata = br.GetPlanByWorkTypeID(WorkTypeID);
            return Json(plandata,JsonRequestBehavior.AllowGet);
        }
    }
}
