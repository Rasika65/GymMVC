using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MyGym.Models;

using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MyGym.Repository
{

    public class BusinessRules
    {
        public string GetUserID_By_UserName(string UserName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserName", UserName);
                return con.Query<string>("Usp_UserIDbyUserName", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public List<UserRole> DisplayAllUserAndRoles()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_DisplayAllUser_And_Roles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var datareader = cmd.ExecuteReader();

                List<UserRole> rlist = new List<UserRole>();

                while (datareader.Read())
                {
                    UserRole r = new UserRole();
                    r.UserName = Convert.ToString(datareader["UserName"]);
                    r.RoleName = Convert.ToString(datareader["RoleName"]);
                    rlist.Add(r);
                }
                return rlist;
            }            
        }

        public IEnumerable<Member> RegistrationSelectList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                return con.Query<Member>("sprocMemberRegistrationSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Payment> AllPaymentDetails()
        {
            using (SqlConnection con = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
            {
                return con.Query<Payment>("Usp_ALLPaymentDetailinfo", null, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public int InsertMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MemID", member.MemID);
                para.Add("@MemberFName", member.MemberFName);
                para.Add("@MemberLName", member.MemberLName);
                para.Add("@MemberMName", member.MemberMName);
                para.Add("@Address", member.Address);
                para.Add("@DOB", member.DOB);
                para.Add("@Age", member.Age);
                para.Add("@Contactno", member.Contactno);
                para.Add("@EmailID", member.EmailID);
                para.Add("@Gender", member.Gender);
                para.Add("@PlantypeID", member.PlantypeID);
                para.Add("@WorkouttypeID", member.WorkouttypeID);
                para.Add("@Createdby", member.Createdby);
                para.Add("@JoiningDate", member.JoiningDate);
                para.Add("@ModifiedBy", 0);
                para.Add("@MemIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("SprocMemberRegistrationInsertUpdateSingleItem", para, null, 0, CommandType.StoredProcedure);
                int MemID = para.Get<int>("MemIDOUT");
                return MemID;
            }
        }

        public string GetAmount(string PlanID, string WorkTypeID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@PlanID", PlanID);
                para.Add("@SchemeID", WorkTypeID);
                return val = con.Query<string>("Usp_GetAmount_reg", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public IEnumerable<Plan> GetPlanByWorkTypeID(string SchemeID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@SchemeID", SchemeID);
                var Plan_list = con.Query<Plan>("Usp_GetPlanByWorkTypeID", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }

        public void DeleteMember(string MemID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@MemID", MemID);
                val = con.Query<string>("sprocMemberRegistrationDeleteSingleItem", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int InsertPaymentDetails(Payment payment)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@PaymentID", payment.PaymentID);
                paramater.Add("@PlanID", payment.PlanID);
                paramater.Add("@WorkouttypeID", payment.WorkouttypeID);
                paramater.Add("@Paymenttype", payment.PaymentType);
                paramater.Add("@PaymentFromdt", payment.PaymentFromdt);
                paramater.Add("@PaymentAmount", payment.PaymentAmount);
                paramater.Add("@CreateUserID", payment.CreateUserID);
                paramater.Add("@ModifyUserID", payment.ModifyUserID);
                paramater.Add("@RecStatus", "A");
                paramater.Add("@MemberID", payment.MemberID);
                paramater.Add("@PayPalID", payment.PayPalID);
                paramater.Add("@PaymentIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("sprocPaymentDetailsInsertUpdateSingleItem", paramater, null, 0, CommandType.StoredProcedure);
                int PaymentID = paramater.Get<int>("PaymentIDOUT");
                return PaymentID;
            }
        }

        public int UpdatePaymentDetails(Payment payment)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@PaymentID", payment.PaymentID);
                paramater.Add("@PlanID", payment.PaymentID);
                paramater.Add("@WorkouttypeID", payment.WorkouttypeID);
                paramater.Add("@Paymenttype", "Cash");
                paramater.Add("@PaymentFromdt", payment.PaymentFromdt);
                paramater.Add("@PaymentAmount", payment.PaymentAmount);
                paramater.Add("@ModifyUserID", payment.ModifyUserID);
                paramater.Add("@RecStatus", "A");
                paramater.Add("@MemberID", payment.MemberID);
                paramater.Add("@PaymentIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("sprocMemberRegistrationUpdateSingleItem", paramater, null, 0, CommandType.StoredProcedure);
                int PaymentID = paramater.Get<int>("PaymentIDOUT");
                return PaymentID;
            }

        }
                
        public IEnumerable<Payment> AllPaymentDetailsByName(string MemberName)
        {
            using (SqlConnection con = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
            {
                var param = new DynamicParameters();
                param.Add("@MemberFName", MemberName);
                return con.Query<Payment>("Usp_PaymentDetailinfo_Name", param, null, true, 0, commandType: CommandType.StoredProcedure);
            }

        }

        public IEnumerable<PaymentAutocomp> ListofMemberNo(string Memberno)
        {
            using (SqlConnection con = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", Memberno);
                return con.Query<PaymentAutocomp>("USP_listofMemberno", param, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<PaymentAutocomp> ListofMemberName(string Membername)
        {
            using (SqlConnection con = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
            {
                var param = new DynamicParameters();
                param.Add("@MemberFName", Membername);
                return con.Query<PaymentAutocomp>("USP_listofMemberName", param, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public DataSet GenerateRecepitDataset(string MemberID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GenerateRecepit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Memberid", MemberID);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }

                    else
                    {
                        return ds = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Dispose();
                }

            }
        }

        public IEnumerable<SchemeMasterDTO> GetSchemes()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var Listscheme = con.Query<SchemeMasterDTO>("select * from SchemeMaster", null, null, true, 0, CommandType.Text).ToList();

                return Listscheme;
            }
        }

        public IEnumerable<PlanMasterDTO> GetPlan()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var ListofPlan = con.Query<PlanMasterDTO>("sprocPlanMasterSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofPlan;
            }
        }

        public RenewalDATA GetDataofMember(string MemberID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MainMemberID", MemberID);
                return con.Query<RenewalDATA>("Usp_GetDataofMemberbyID", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string Get_PeriodID_byPlan(string PlanID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PlanID", PlanID);
                return con.Query<string>("Usp_getPlanPeriodID", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
    }
}