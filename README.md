# GymMVC
Gym management system in .NET MVC

Database Part
-------------------
1)	Create Database with Name: - GYMONEDBMVC.

2)	After Creating Database now make changes to ConnectionStrings in Web.Config
    Update this connectionStrings with your Own Data Source and Sql UserName and Password.

3)	After making changes in connectionStrings, Run the Project and it will create Simple Membership Tables as follows:
    1.	Users
    2.	webpages_Membership
    3.	webpages_OAuthMembership
    4.	webpages_Roles
    5.	webpages_UsersInRoles

4)	Now Run Script GYMMVC.sql Script.

5)	If you Get Error saying "Membership Table already Exists", just remove Create Scripts for below listed tables 
    1.	Users
    2.	webpages_Membership
    3.	webpages_OAuthMembership
    4.	webpages_Roles
    5.	webpages_UsersInRoles
    Please don't remove Insert Script of these Tables.
6)	Login Details
    1.	Admin 
    User ID: Admin 
    Password: 123456
    2.	System User
    User ID: User
    Password: 123456

---------------- IIS Link ----------------
http://localhost/GymOne



      
  


