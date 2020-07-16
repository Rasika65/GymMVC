using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGym.Repository;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGym.Controllers
{
    public class ReceiptController : Controller
    {
        //
        // GET: /Receipt/
        private BusinessRules br = new BusinessRules();


        public ActionResult Generatereceipt(string MemberId, string MemberName, string actionType)
        {
            if (string.IsNullOrEmpty(MemberId) && string.IsNullOrEmpty(MemberName))
            {
                if (Request.HttpMethod == "GET")
                {
                    return View();

                }
                else
                    ModelState.AddModelError("Error", "Please enter Member Id or Member Name !");
            }
            
            else
            {
                var Mainno = "";
                if ((MemberId != null) && (MemberId != ""))
                {
                    var x = br.ListofMemberNo(MemberId).ToList();
                    Mainno = x[0].MemID.ToString();
                }
                if ((MemberName != null) && (MemberName != ""))
                {
                    string[] Memno = MemberName.Split('|');
                    Mainno = Memno[3];
                }                

                DataSet ds = br.GenerateRecepitDataset(Mainno);
                ds.Tables[0].TableName = "RecepitDataset";

                if (ds.Tables[0].Rows.Count > 0)
                {

                    var grid = new GridView();
                    grid.DataSource = ds.Tables[0];
                    grid.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=Receipt.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    grid.RenderControl(htw);

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
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
