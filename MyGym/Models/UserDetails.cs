using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace MyGym.Models
{
    public class GymContext : DbContext
    {
        public GymContext()
            : base("DefaultConnection")
        {            
            Database.SetInitializer<GymContext>(null);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Scheme> SchemeList { get; set; }
        public DbSet<Plan> PlanList { get; set; }
    }



    [Table("Users")]
    public class Users
    {
        //[Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
    }

    [Table("webpages_Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }


    public class UserRole
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
        

    [Table("SchemeMaster")]
    public class Scheme
    {
        [Key]
        public int SchemeID { get; set; }
        public string SchemeName { get; set; }
    }

    [Table("PlanMaster")]
    public class Plan
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
    }
}