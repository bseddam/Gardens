using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Web.UI;
using System.Data.SqlClient;


public class Methods
{
    public static SqlConnection SqlConn
    {
        get { return new SqlConnection(@"Data Source = SQL5105.site4now.net; Initial Catalog = db_a83176_gardens; User Id = db_a83176_gardens_admin; Password = Gardens1"); }
    }
    public DataTable User(string Login, string Pass)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"Select * from Users where Login=@Login 
and Password=@Password and DeleteTime is null", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@Login", Login);
            da.SelectCommand.Parameters.AddWithValue("@Password", Pass);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetUserByid(int userid)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users where UserID=@userid", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@userid", userid);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GetUsers()
    {
        try
        {
            //string status = " ";
            //if (HttpContext.Current.Session["StatusID"].ToParseStr() == "2")
            //{
            //    status = " and a.ID=" + HttpContext.Current.Session["UsersID1"].ToParseStr();
            //}
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT  row_number() 
over(order by u.UserID desc) sn,c.Sname+' '+c.Name+' '+c.FName fullname,
us.UserStatusName,g.GardenName,u.* FROM [Users] u
inner join UserStatus us on u.UserStatusID=us.UserStatusID
left join Gardens g on g.GardenID=u.GardenID
left join Cadres c on u.cadreid=c.cadreid where u.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType UserInsert(int GardenID, int CadreID, string Login,
        string Password, int UserStatusID)
    {


        SqlCommand cmd = new SqlCommand(@"insert into Users (GardenID,CadreID,Login,Password,
UserStatusID) values (@GardenID,@CadreID,@Login,@Password,@UserStatusID)", SqlConn);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@Login", Login);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@UserStatusID", UserStatusID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    
    public Types.ProsesType UserUpdate(int id, int GardenID, int CadreID, string Login,
        string Password, int UserStatusID)
    {
        SqlCommand cmd = new SqlCommand(@"update Users set 
GardenID=@GardenID, CadreID=@CadreID, Login=@Login,Password=@Password,UserStatusID=@UserStatusID  where UserID=@id", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@Login", Login);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@UserStatusID", UserStatusID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteUser(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update Users set DeleteTime=GetDate() where UserID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public DataTable GetUsersPermissions()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"Select m2.SiteMapID,m1.MenuName+' '+m2.MenuName name from (select * from SiteMap s where s.menusubid is null) m1 inner join 
(select * from SiteMap s1 where s1.menusubid is not null) m2 on m1.SiteMapID=m2.MenuSubID", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType UserInsertPermissions(int UserID, string[] SiteMapID)
    {
        SqlCommand cmddel = new SqlCommand(@"Delete from PermissionUser Where UserID=@UserID", SqlConn);
        cmddel.Parameters.AddWithValue("@UserID", UserID);
        try
        {
            cmddel.Connection.Open();
            cmddel.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmddel.Connection.Close();
            cmddel.Dispose();
        }


        if (SiteMapID != null)
        {
            if (SiteMapID.Length > 1)
            {
                foreach (string i in SiteMapID)
                {
                    SqlCommand cmd = new SqlCommand(@"insert into PermissionUser (UserID,SiteMapID) 
values (@UserID,@SiteMapID)", SqlConn);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@SiteMapID", i.ToParseInt());


                    try
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return Types.ProsesType.Error;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }
        }
        return Types.ProsesType.Succes;

    }

    public DataTable GetCompanies()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by CompanyID desc) sn,
CompanyID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes from Companies c 
where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCompanyById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by CompanyID desc) sn,
CompanyID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes from 
Companies c where DeleteTime is null and CompanyID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType CompaniesInsert(string RegisterTime, string CompanyName, string CompanyVoen,
        string BankAccount, string PhoneNumbers, string Email, string Adress, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Companies 
(UserID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes) 
values (@UserID,@RegisterTime,@CompanyName,@CompanyVoen,@BankAccount,@PhoneNumbers,@Email,@Adress,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
        cmd.Parameters.AddWithValue("@CompanyVoen", CompanyVoen);
        cmd.Parameters.AddWithValue("@BankAccount", BankAccount);
        cmd.Parameters.AddWithValue("@PhoneNumbers", PhoneNumbers);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Adress", Adress);
        cmd.Parameters.AddWithValue("@Notes", Notes);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType CompanyUpdate(int CompanyID, string RegisterTime, string CompanyName, string CompanyVoen,
        string BankAccount, string PhoneNumbers, string Email, string Adress, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Companies set UserID=@UserID,RegisterTime=@RegisterTime,
CompanyName=@CompanyName, CompanyVoen=@CompanyVoen, BankAccount=@BankAccount,
PhoneNumbers=@PhoneNumbers, Email=@Email, Adress=@Adress,Notes=@Notes,UpdateTime=getdate() where CompanyID=@CompanyID", SqlConn);

        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
        cmd.Parameters.AddWithValue("@CompanyVoen", CompanyVoen);
        cmd.Parameters.AddWithValue("@BankAccount", BankAccount);
        cmd.Parameters.AddWithValue("@PhoneNumbers", PhoneNumbers);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Adress", Adress);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteCompany(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Companies set deletetime=getdate(),UserID=@UserID where CompanyID=@CompanyID;", SqlConn);
        cmd.Parameters.AddWithValue("@CompanyID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    //olkeler
    public DataTable GetCountries()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by CountryID desc) sn,
* FROM Countries where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCountryByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by CountryID desc) sn,
* FROM Countries where DeleteTime is null and CountryID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType CountryInsert(string CountryName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Countries 
(UserID,CountryName) values (@UserID,@CountryName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CountryName", CountryName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType CountryUpdate(int CountryID, string CountryName)
    {
        SqlCommand cmd = new SqlCommand(@"update Countries set UserID=@UserID,
CountryName=@CountryName,UpdateTime=getdate() where CountryID=@CountryID", SqlConn);
        cmd.Parameters.AddWithValue("@CountryID", CountryID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CountryName", CountryName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteCountry(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Countries set deletetime=getdate(),
UserID=@UserID where CountryID=@CountryID;", SqlConn);
        cmd.Parameters.AddWithValue("@CountryID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }




















    //Ölçü vahidi
    public DataTable GetUnitMeasurements()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by UnitMeasurementID desc) sn,
* FROM UnitMeasurements where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetUnitMeasurementByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by UnitMeasurementID desc) sn,
* FROM UnitMeasurements where DeleteTime is null  and UnitMeasurementID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType UnitMeasurementInsert(string UnitMeasurementName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into UnitMeasurements 
(UserID,UnitMeasurementName) values (@UserID,@UnitMeasurementName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@UnitMeasurementName", UnitMeasurementName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType UnitMeasurementUpdate(int UnitMeasurementID, string UnitMeasurementName)
    {
        SqlCommand cmd = new SqlCommand(@"update UnitMeasurements set UserID=@UserID,
UnitMeasurementName=@UnitMeasurementName,UpdateTime=getdate() where UnitMeasurementID=@UnitMeasurementID", SqlConn);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@UnitMeasurementName", UnitMeasurementName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteUnitMeasurement(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update UnitMeasurements set deletetime=getdate(),
UserID=@UserID where UnitMeasurementID=@UnitMeasurementID;", SqlConn);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }




    public DataTable GetStocks()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by StockID) sn,
StockID,StockName from Stocks s where s.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }




    //Bağlar
    public DataTable GetGardens()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by GardenID desc) sn,
GardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementName,Address,Notes 
from Gardens g left join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID 
where  g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetGardenById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by GardenID desc) sn,
GardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementID,u.UnitMeasurementName,Address,Notes 
from Gardens g left join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID 
where  g.DeleteTime is null and u.DeleteTime is null and GardenID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType GardensInsert(string RegisterTime, string GardenName, string GardenArea,
        int UnitMeasurementID, string Address, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Gardens 
(UserID,RegisterTime,GardenName,GardenArea,UnitMeasurementID,Address,Notes) 
values (@UserID,@RegisterTime,@GardenName,@GardenArea,@UnitMeasurementID,@Address,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@GardenName", GardenName);
        cmd.Parameters.AddWithValue("@GardenArea", ConvertTypes.ToParseFloat(GardenArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType GardensUpdate(int GardenID, string RegisterTime, string GardenName, string GardenArea,
        int UnitMeasurementID, string Address, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Gardens set UserID=@UserID,RegisterTime=@RegisterTime,
GardenName=@GardenName, GardenArea=@GardenArea, UnitMeasurementID=@UnitMeasurementID,
Address=@Address,Notes=@Notes,UpdateTime=getdate() where GardenID=@GardenID", SqlConn);

        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@GardenName", GardenName);
        cmd.Parameters.AddWithValue("@GardenArea", ConvertTypes.ToParseFloat(GardenArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteGarden(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Gardens set deletetime=getdate(),UserID=@UserID where GardenID=@GardenID;", SqlConn);
        cmd.Parameters.AddWithValue("@GardenID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //Zonalar
    public DataTable GetZones()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ZoneID desc) sn,
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.UnitMeasurementID,u.UnitMeasurementName,z.Notes
FROM Zones z inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
where z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetZoneById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ZoneID desc) sn,
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.GardenID,z.UnitMeasurementID,u.UnitMeasurementName,
z.Notes from Zones z  inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
where z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and z.ZoneID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ZoneInsert(string RegisterTime, int GardenID, string ZoneName, string ZoneArea,
        int UnitMeasurementID, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Zones 
(UserID,RegisterTime,GardenID,ZoneName,ZoneArea,UnitMeasurementID,Notes) 
values (@UserID,@RegisterTime,@GardenID,@ZoneName,@ZoneArea,@UnitMeasurementID,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ZoneName", ZoneName);
        cmd.Parameters.AddWithValue("@ZoneArea", ConvertTypes.ToParseFloat(ZoneArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType ZoneUpdate(int ZoneID, string RegisterTime, int GardenID, string ZoneName, string ZoneArea,
        int UnitMeasurementID, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Zones set UserID=@UserID,RegisterTime=@RegisterTime,
GardenID=@GardenID, ZoneName=@ZoneName, ZoneArea=@ZoneArea,
UnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() where ZoneID=@ZoneID", SqlConn);

        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));

        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ZoneName", ZoneName);
        cmd.Parameters.AddWithValue("@ZoneArea", ConvertTypes.ToParseFloat(ZoneArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteZone(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Zones set deletetime=getdate(),UserID=@UserID where ZoneID=@ZoneID;", SqlConn);
        cmd.Parameters.AddWithValue("@ZoneID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetZonesByGardenID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ZoneID desc) sn,
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.UnitMeasurementID,u.UnitMeasurementName,
z.Notes,g.GardenID FROM Zones z inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
where z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and g.GardenID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //Sektorlar
    public DataTable GetSectors()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by SectorID desc) sn,
s.SectorID
      ,s.UserID
      ,s.RegisterTime
      ,SectorName
      ,s.ZoneID
      ,SectorArea
      ,s.UnitMeasurementID
      ,s.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Sectors s 
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetSectorById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by SectorID desc) sn,
s.SectorID,s.UserID,s.RegisterTime,SectorName,s.ZoneID,SectorArea,s.UnitMeasurementID,s.Notes,g.GardenID,
g.GardenName,ZoneName,u.UnitMeasurementName FROM Sectors s 
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null 
and s.SectorID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType SectorInsert(string RegisterTime, int ZoneID, string SectorName, string SectorArea,
        int UnitMeasurementID, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Sectors 
(UserID,RegisterTime,ZoneID,SectorName,SectorArea,UnitMeasurementID,Notes) 
values (@UserID,@RegisterTime,@ZoneID,@SectorName,@SectorArea,@UnitMeasurementID,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
        cmd.Parameters.AddWithValue("@SectorName", SectorName);
        cmd.Parameters.AddWithValue("@SectorArea", ConvertTypes.ToParseFloat(SectorArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType SectorUpdate(int SectorID, string RegisterTime, int ZoneID, string SectorName, string SectorArea,
        int UnitMeasurementID, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Sectors set UserID=@UserID,RegisterTime=@RegisterTime,
ZoneID=@ZoneID, SectorName=@SectorName, SectorArea=@SectorArea,
UnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() where SectorID=@SectorID", SqlConn);

        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
        cmd.Parameters.AddWithValue("@SectorName", SectorName);
        cmd.Parameters.AddWithValue("@SectorArea", ConvertTypes.ToParseFloat(SectorArea));
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteSector(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Sectors set deletetime=getdate(),UserID=@UserID where SectorID=@SectorID;", SqlConn);
        cmd.Parameters.AddWithValue("@SectorID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetSectorsByZoneID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by SectorID desc) sn,
s.SectorID
      ,s.UserID
      ,s.RegisterTime
      ,SectorName
      ,s.ZoneID
      ,SectorArea
      ,s.UnitMeasurementID
      ,s.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Sectors s 
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null
and z.ZoneID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //Siralar
    public DataTable GetLines()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by LineID desc) sn,
l.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,l.SectorID,
l.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and l.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetLineById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by LineID desc) sn,
l.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,z.zoneid,l.SectorID,
l.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null 
and u.DeleteTime is null and l.DeleteTime is null and l.LineID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetLineBySectorID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by LineID desc) sn,
l.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,l.SectorID,
l.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and l.DeleteTime is null and s.SectorID=@ID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("ID", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType LineInsert(string RegisterTime, string LineName, string LineArea,
       int SectorID, int UnitMeasurementID, int TreeCount, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Lines 
(UserID,RegisterTime,LineName,LineArea,SectorID,UnitMeasurementID,TreeCount,Notes) 
values (@UserID,@RegisterTime,@LineName,@LineArea,@SectorID,@UnitMeasurementID,@TreeCount,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineName", LineName);
        cmd.Parameters.AddWithValue("@LineArea", ConvertTypes.ToParseFloat(LineArea));
        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType LineUpdate(int LineID, string RegisterTime, string LineName, string LineArea,
       int SectorID, int UnitMeasurementID, int TreeCount, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Lines set UserID=@UserID,RegisterTime=@RegisterTime,
LineName=@LineName,LineArea=@LineArea,SectorID=@SectorID,
UnitMeasurementID=@UnitMeasurementID,TreeCount=@TreeCount
, Notes=@Notes,UpdateTime=getdate() where LineID=@LineID", SqlConn);

        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineName", LineName);
        cmd.Parameters.AddWithValue("@LineArea", ConvertTypes.ToParseFloat(LineArea));
        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteLine(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Lines set deletetime=getdate(),UserID=@UserID 
where LineID=@LineID;", SqlConn);
        cmd.Parameters.AddWithValue("@LineID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetTreeSitiuations()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreesSitiuationID desc) sn, 
 * FROM [TreesSitiuation] t where t.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //agaclar
    public DataTable GetTrees()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeID desc) sn, 
t.*  FROM [Trees] t where t.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetTreesByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeID desc) sn, 
* FROM [Trees] where DeleteTime is null and TreeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType TreeInsert(string TreeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Trees (UserID,TreeName) values 
(@UserID,@TreeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TreeName", TreeName);
     
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType TreeUpdate(int TreeID, string TreeName)
    {
        SqlCommand cmd = new SqlCommand(@"update Trees set UserID=@UserID,
TreeName=@TreeName,UpdateTime=getdate() where TreeID=@TreeID", SqlConn);
        cmd.Parameters.AddWithValue("@TreeID", TreeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TreeName", TreeName);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteTree(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Trees set deletetime=getdate(),UserID=@UserID 
where TreeID=@TreeID;", SqlConn);
        cmd.Parameters.AddWithValue("@TreeID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    public DataTable GetTreeTypeByTreeCountryID(int TreeID,int CountryID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"
select row_number() over(order by TreeTypeID desc) sn, 
tp.*,c.CountryName,t.TreeName  FROM [TreeTypes] tp left join Countries c on tp.CountryID=c.CountryID
left join Trees t on tp.TreeID=t.TreeID where  tp.DeleteTime is null and tp.TreeID=@TreeID 
and c.CountryID=@CountryID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("TreeID", TreeID);
            da.SelectCommand.Parameters.AddWithValue("CountryID", CountryID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }







    //Ağaclar tipleri
    public DataTable GetTreeTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeTypeID desc) sn, 
tp.*,c.CountryName,t.TreeName  FROM [TreeTypes] tp left join Countries c on tp.CountryID=c.CountryID
left join Trees t on tp.TreeID=t.TreeID where  tp.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetTreeTypesByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeTypeID desc) sn, 
* FROM [TreeTypes] where DeleteTime is null and TreeTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType TreeTypeInsert(int CountryID,int TreeID, string TreeTypeName, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TreeTypes (UserID,CountryID,TreeID,TreeTypeName,
Coefficient) values (@UserID,@CountryID,@TreeID,@TreeTypeName,@Coefficient)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CountryID", CountryID);
        cmd.Parameters.AddWithValue("@TreeID", TreeID);
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
        cmd.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType TreeTypeUpdate(int TreeTypeID, int CountryID, int TreeID, string TreeTypeName, 
        string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"update TreeTypes set UserID=@UserID,CountryID=@CountryID,TreeID=@TreeID,
TreeTypeName=@TreeTypeName,Coefficient=@Coefficient,UpdateTime=getdate() where TreeTypeID=@TreeTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CountryID", CountryID);
        cmd.Parameters.AddWithValue("@TreeID", TreeID);
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
        cmd.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteTreeType(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update TreeTypes set deletetime=getdate(),UserID=@UserID 
where TreeTypeID=@TreeTypeID;", SqlConn);
        cmd.Parameters.AddWithValue("@TreeTypeID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //Mallar
    public DataTable GetProducts()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ProductID desc) sn,
       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName
      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName
      ,[Notes] from [Products] p 
  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID 
 
  left join Models m on p.ModelID=m.ModelID
  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
  where p.DeleteTime is null and pt.DeleteTime is null  and 
m.DeleteTime is null and u.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetProductById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ProductID desc) sn,
       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName
      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName
      ,[Notes] from [Products] p 
  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID 
  left join Models m on p.ModelID=m.ModelID
  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
  where p.DeleteTime is null and pt.DeleteTime is null and 
m.DeleteTime is null and u.DeleteTime is null and p.ProductID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ProductInsert(string ProductsName, int ProductTypeID,
        int ModelID, string Code, int UnitMeasurementID,  string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Products 
(UserID,ProductsName,ProductTypeID,ModelID,Code,UnitMeasurementID,
Notes) values (@UserID,@ProductsName,@ProductTypeID,@ModelID,
@Code,@UnitMeasurementID,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductsName", ProductsName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType ProductUpdate(int ProductID, string ProductsName, int ProductTypeID,
         int ModelID, string Code, int UnitMeasurementID,  string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Products set UserID=@UserID,
ProductsName=@ProductsName, ProductTypeID=@ProductTypeID, ModelID=@ModelID,
Code=@Code,UnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() 
where ProductID=@ProductID", SqlConn);

        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductsName", ProductsName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
    
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Notes", Notes);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteProduct(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Products set deletetime=getdate(),UserID=@UserID 
where ProductID=@ProductID;", SqlConn);
        cmd.Parameters.AddWithValue("@ProductID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    //Modeller
    public DataTable GetModels()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ModelID] desc) sn,
[ModelID],[ModelName],pt.ProductTypeName FROM [Models] m 
left join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public DataTable GetModelByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ModelID] desc) sn,
[ModelID],[ModelName],pt.ProductTypeName,m.ProductTypeID FROM [Models] m 
inner join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null  and m.ModelID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ModelInsert(int ProductTypeID, string ModelName)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Models 
(UserID,ProductTypeID,ModelName) values (@UserID,@ProductTypeID,@ModelName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@ModelName", ModelName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType ModelUpdate(int ProductTypeID, int ModelID,string ModelName)
    {
        SqlCommand cmd = new SqlCommand(@"update Models set UserID=@UserID,ProductTypeID=@ProductTypeID,
ModelName=@ModelName,UpdateTime=getdate() 
where ModelID=@ModelID", SqlConn);

        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@ModelName", ModelName);
      

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteModel(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Models set deletetime=getdate(),UserID=@UserID 
where ModelID=@ModelID;", SqlConn);
        cmd.Parameters.AddWithValue("@ModelID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetModelsByProductTypeID(int ProductTypeID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ModelID] desc) sn,
[ModelID],[ModelName],pt.ProductTypeName FROM [Models] m 
inner join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null and pt.ProductTypeID=@ProductTypeID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("ProductTypeID", ProductTypeID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }






//    public DataTable GetProductGeneralTypes()
//    {
//        try
//        {
//            DataTable dt = new DataTable();
//            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() 
//over(order by [ProductGeneralTypeID] desc) sn,
//* FROM [ProductGeneralTypes] where DeleteTime is null", SqlConn);
//            da.Fill(dt);
//            return dt;
//        }
//        catch (Exception ex)
//        {
//            return null;
//        }
//    }

    //Mallarin tipi
    public DataTable GetProductTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"
SELECT row_number() over(order by [ProductTypeID] desc) sn,
[ProductTypeID],[UserID],[ProductTypeName]
  from [ProductTypes] pt  where pt.DeleteTime is null ", SqlConn);



            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetProductTypeByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ProductTypeID] desc) sn,
[ProductTypeID],[UserID],[ProductTypeName]
  from [ProductTypes] pt  where pt.DeleteTime is null and ProductTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ProductTypeInsert(string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into ProductTypes 
(UserID,ProductTypeName) values (@UserID,@ProductTypeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
      
        cmd.Parameters.AddWithValue("@ProductTypeName", ProductTypeName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType ProductTypeUpdate(int ProductTypeID, string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"update ProductTypes set UserID=@UserID,
ProductTypeName=@ProductTypeName,UpdateTime=getdate() where ProductTypeID=@ProductTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
      
        cmd.Parameters.AddWithValue("@ProductTypeName", ProductTypeName);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteProductType(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update ProductTypes set deletetime=getdate(),UserID=@UserID 
where ProductTypeID=@ProductTypeID;", SqlConn);
        cmd.Parameters.AddWithValue("@ProductTypeID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    //işlər
    public DataTable GetWorks()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by WorkID desc) sn,
[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],w.Price
FROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID 
where w.DeleteTime is null and wt.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetWorkById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by WorkID desc) sn,
[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price
FROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID 
where w.DeleteTime is null and wt.DeleteTime is null and w.WorkID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GetWorkByWorkTypeId(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by WorkID desc) sn,
[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price
FROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID 
where w.DeleteTime is null and wt.DeleteTime is null and w.WorkTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType WorkInsert(int WorkTypeID, string WorkName, string Price)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Works 
(UserID,WorkTypeID,WorkName,Price) 
values (@UserID,@WorkTypeID,@WorkName,@Price)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        cmd.Parameters.AddWithValue("@WorkName", WorkName);
        cmd.Parameters.AddWithValue("@Price", Price);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType WorkUpdate(int WorkID, int WorkTypeID, string WorkName, string Price)
    {
        SqlCommand cmd = new SqlCommand(@"update Works set UserID=@UserID,
WorkTypeID=@WorkTypeID,WorkName=@WorkName,Price=@Price,UpdateTime=getdate() where WorkID=@WorkID", SqlConn);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        cmd.Parameters.AddWithValue("@WorkName", WorkName);
        cmd.Parameters.AddWithValue("@Price", Price);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteWork(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Works set deletetime=getdate(),UserID=@UserID where WorkID=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    //İşlərin tipləri
    public DataTable GetWorkTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [WorkTypeID] desc) sn,
[WorkTypeID],[WorkTypeName],[InsertTime],[UpdateTime],[DeleteTime] from WorkTypes  
where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    //İşlərin statusu
    public DataTable GetWorkStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [WorkStatusID] desc) sn,
[WorkStatusID]
      ,[WorkStatusName]
      ,[InsertTime]
      ,[Updatetime]
      ,[DeleteTime] from [WorkStatus]  
where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    //Kadr tipləri
    public DataTable GetCadreType()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT  [CadreTypeID]
      ,[CadreTypeName]
  FROM [Gardens].[dbo].[CadreType]", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    //Kartlar
    public DataTable GetCards()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by c.CardID desc) sn,
c.* FROM Cards c  where c.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
//    public DataTable GetCardsByGardenID(int GardenID)
//    {  //and c.cardid not in (select cardid from cadres)
//        try
//        {
//            DataTable dt = new DataTable();
//            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by c.CardID desc) sn,
//c.CardID,c.UserID,c.CardNumber,c.InsertTime,c.Updatetime,c.DeleteTime,
//g.GardenName,c.GardenID FROM Cards c inner join Gardens g on g.GardenID=c.GardenID 
//where c.DeleteTime is null and c.GardenID=@GardenID", SqlConn);
//            da.SelectCommand.Parameters.AddWithValue("GardenID", GardenID);
//            da.Fill(dt);
//            return dt;
//        }
//        catch (Exception ex)
//        {
//            return null;
//        }
//    }
    public DataTable GetCardsByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [CardID] desc) sn,
*
  FROM [Cards] where DeleteTime is null and CardID=@CardID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("CardID", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType DeleteCards(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Cards set deletetime=getdate(),UserID=@UserID where CardID=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType CardsInsert(int UserID, string CardNumber, string CardBarcode)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Cards (UserID,CardNumber,CardBarcode)  
values (@UserID,@CardNumber,@CardBarcode)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
        cmd.Parameters.AddWithValue("@CardBarcode", CardBarcode);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType CardsUpdate(int CardID, int UserID, string CardNumber, string CardBarcode)
    {
        SqlCommand cmd = new SqlCommand(@"update Cards set UserID=@UserID,CardNumber=@CardNumber,
CardBarcode=@CardBarcode where CardID=@CardID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
        cmd.Parameters.AddWithValue("@CardBarcode", CardBarcode);
        cmd.Parameters.AddWithValue("@CardID", CardID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    //Cins
    public DataTable GetGenders()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [GenderID]) sn,
[GenderID],[GenderName] FROM [Genders] ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //Texnikalar
    public DataTable GetTechniqueSituations()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [TechniqueSituationID] desc) sn,
[TechniqueSituationID] ,[TechniqueSituationName] FROM [TechniqueSituation] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetTechniqueById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from Techniques where TechniqueID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType TechniqueInsert(int UserID,int GardenID, int ModelID,string RegisterNumber,string SerieNumber,int Motor,int CompanyID,
        int TechniqueSituationID, int GPS, string GPSLogin, string GPSPassword, int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, string BoughtDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Techniques (UserID,GardenID,  ModelID,  RegisterNumber,  SerieNumber, 
Motor,  CompanyID,  TechniqueSituationID,  GPS, GPSLogin, GPSPassword,  ProductionYear,  Photo,  Birka,  TechniquesName,  
Passport,  BoughtDate)    Values( @UserID,@GardenID,  @ModelID,  @RegisterNumber,  @SerieNumber,  @Motor,  @CompanyID,  @TechniqueSituationID,  @GPS, @GPSLogin, @GPSPassword, @ProductionYear,  @Photourl,  @Birka,  @TechniquesName,  @Passport,  @BoughtDate)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        cmd.Parameters.AddWithValue("@SerieNumber", SerieNumber);
        cmd.Parameters.AddWithValue("@Motor", Motor);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        cmd.Parameters.AddWithValue("@GPS", GPS);
        cmd.Parameters.AddWithValue("@GPSLogin", GPSLogin);
        cmd.Parameters.AddWithValue("@GPSPassword", GPSPassword);
        cmd.Parameters.AddWithValue("@ProductionYear", ProductionYear);
        cmd.Parameters.AddWithValue("@Photourl", Photourl);
        cmd.Parameters.AddWithValue("@Birka", Birka);
        cmd.Parameters.AddWithValue("@TechniquesName", TechniquesName);
        cmd.Parameters.AddWithValue("@Passport", Passport);
        cmd.Parameters.AddWithValue("@BoughtDate", ConvertTypes.ToParseDatetime(BoughtDate));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType TechniqueUpdate(int TechniqueID, int UserID,int GardenID, int ModelID, string RegisterNumber, 
        string SerieNumber, int Motor, int CompanyID, int TechniqueSituationID, int GPS, string GPSLogin,string GPSPassword,
        int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, 
        string BoughtDate)
    {
        SqlCommand cmd = new SqlCommand(@"update Techniques set UserID=@UserID,
GardenID=@GardenID,ModelID=@ModelID,RegisterNumber=@RegisterNumber, SerieNumber=@SerieNumber, 
Motor=@Motor,CompanyID=@CompanyID, TechniqueSituationID=@TechniqueSituationID, GPS=@GPS, 
GPSLogin=@GPSLogin, GPSPassword=@GPSPassword, ProductionYear=@ProductionYear, Photo=@Photourl, 
Birka=@Birka,TechniquesName=@TechniquesName, Passport=@Passport,BoughtDate=@BoughtDate where TechniqueID=@TechniqueID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        cmd.Parameters.AddWithValue("@SerieNumber", SerieNumber);
        cmd.Parameters.AddWithValue("@Motor", Motor);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        cmd.Parameters.AddWithValue("@GPS", GPS);
        cmd.Parameters.AddWithValue("@GPSLogin", GPSLogin);
        cmd.Parameters.AddWithValue("@GPSPassword", GPSPassword);
        cmd.Parameters.AddWithValue("@ProductionYear", ProductionYear);
        cmd.Parameters.AddWithValue("@Photourl", Photourl);
        cmd.Parameters.AddWithValue("@Birka", Birka);
        cmd.Parameters.AddWithValue("@TechniquesName", TechniquesName);
        cmd.Parameters.AddWithValue("@Passport", Passport);
        cmd.Parameters.AddWithValue("BoughtDate", ConvertTypes.ToParseDatetime(BoughtDate));
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType DeleteTechnique(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update Techniques set DeleteTime=GetDate() where TechniqueID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //texniki service
    public DataTable GetTechniquesServices()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by t.TechniqueServiceID desc) sn,
t.*,g.GardenName,w.WorkName,p.ProductTypeID,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,
 m1.ModelID modeltexid,m1.ModelName modeltex,m2.ModelID modelprodid, m2.ModelName modelprod
FROM [TechniqueService] t 
left join Techniques tc on t.TechniqueID=tc.TechniqueID
left join Models m1 on m1.ModelID=tc.ModelID
left join Gardens g on t.GardenID=g.GardenID 
left join Works w on t.WorkID=w.WorkID
left join Products p on p.ProductID=t.ProductID
left join Models m2 on m2.ModelID=p.ModelID
left join UnitMeasurements u on u.UnitMeasurementID=t.UnitMeasurementID where t.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public DataTable GetTechniquesServiceById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by t.TechniqueServiceID desc) sn,
t.*,g.GardenName,w.WorkName,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,
 m1.ModelID modeltexid,m1.ModelName modeltex,m2.ModelID modelprodid,pt1.ProductTypeID ProductTypeID1,pt2.ProductTypeID ProductTypeID2,m2.ModelName modelprod
FROM [TechniqueService] t 
left join Techniques tc on t.TechniqueID=tc.TechniqueID
left join Models m1 on m1.ModelID=tc.ModelID
left join ProductTypes pt1 on m1.ProductTypeID=pt1.ProductTypeID

left join Gardens g on t.GardenID=g.GardenID 
left join Works w on t.WorkID=w.WorkID
left join Products p on p.ProductID=t.ProductID
left join Models m2 on m2.ModelID=p.ModelID
left join ProductTypes pt2 on m2.ProductTypeID=pt2.ProductTypeID

left join UnitMeasurements u on u.UnitMeasurementID=t.UnitMeasurementID where t.DeleteTime is null 
and TechniqueServiceID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType InsertTechniquesService(int TechniqueID, int GardenID, int WorkID,
        int ProductID, string Price, int ProductSize, int UnitMeasurementID, string Amount,
       string ServicePrice, int Odometer, string Note, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TechniqueService 
(UserID,TechniqueID,GardenID,WorkID,ProductID,Price,ProductSize,
UnitMeasurementID,Amount,ServicePrice,Odometer,Note,RegisterTime) 
Values (@UserID,@TechniqueID,@GardenID,@WorkID,@ProductID,@Price,@ProductSize,
@UnitMeasurementID,@Amount,@ServicePrice,@Odometer,@Note,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@ProductSize", ProductSize);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@ServicePrice", ConvertTypes.ToParseFloat(ServicePrice));
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@Note", Note);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Types.ProsesType UpdateTechniquesService(int TechniqueServiceID, int TechniqueID, int GardenID, int WorkID,
        int ProductID, string Price, int ProductSize, int UnitMeasurementID, string Amount,
       string ServicePrice, int Odometer, string Note, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update TechniqueService set 
UserID=@UserID, TechniqueID=@TechniqueID, GardenID=@GardenID, 
WorkID=@WorkID, ProductID=@ProductID,Price=@Price,ProductSize=@ProductSize,
UnitMeasurementID=@UnitMeasurementID,Amount=@Amount,ServicePrice=@ServicePrice,
Odometer=@Odometer,Note=@Note,RegisterTime=@RegisterTime 
where TechniqueServiceID=@TechniqueServiceID", SqlConn);
        cmd.Parameters.AddWithValue("@TechniqueServiceID", TechniqueServiceID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@ProductSize", ProductSize);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@ServicePrice", ConvertTypes.ToParseFloat(ServicePrice));
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@Note", Note);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

















    public DataTable GetTechnique()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() 
over(order by t.TechniqueID desc) sn,c.Sname+' '+c.Name UsingUsers,
t.TechniquesName, m.ModelName, cp.CompanyName,t.Motor,t.RegisterNumber,t.SerieNumber,t.Passport,t.ProductionYear,t.Birka,ts.TechniqueSituationName,t.GPS,t.GPSLogin,t.GPSPassword,t.BoughtDate,t.Photo,t.TechniqueID,t.ModelID,t.UserID ID,t.CompanyID,t.TechniqueSituationID
from Techniques t 
left join Users u on t.UserID=u.UserID
left join Cadres c on u.CadreID=c.CadreID
left join Models m on m.ModelID=t.ModelID
left join Companies cp on cp.CompanyID=t.CompanyID 
left join TechniqueSituation ts on ts.TechniqueSituationID=t.TechniqueSituationID 
where m.DeleteTime is null order by t.UserID desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //-------Suvarma sistemleri
    public DataTable GetWateringSystems()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over (order by w.WateringSystemID desc) sn, m.ModelName,w.WateringSystemName, 
w.Notes,ts.TechniqueSituationName, w.RegisterTime,w.WateringSystemID,w.ModelID,w.UserID,w.WateringSystemID
from WateringSystems w left join Models m on m.ModelID=w.ModelID
left join TechniqueSituation ts on ts.TechniqueSituationID=w.TechniqueSituationID where w.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteWateringSystems(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update WateringSystems set DeleteTime=GetDate() where WateringSystemID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetWateringSystemsById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from WateringSystems where WateringSystemID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType WateringSystemsInsert(int UserID, int ModelID,
        string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into WateringSystems 
(UserID, ModelID, WateringSystemName, Notes, TechniqueSituationID, RegisterTime) 
Values (@UserID, @ModelID, @WateringSystemName, @Notes, @TechniqueSituationID, @RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
     
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@WateringSystemName", WateringSystemName);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType WateringSystemsUpdate(int WateringSystemID, int UserID,int ModelID,
        string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update WateringSystems set 
UserID=@UserID,ModelID=@ModelID, WateringSystemName=@WateringSystemName, 
Notes=@Notes, TechniqueSituationID=@TechniqueSituationID, RegisterTime=@RegisterTime 
where WateringSystemID=@WateringSystemID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@WateringSystemName", WateringSystemName);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    //  Emek kitabcasi
    public DataTable GetEmploymentHistoryById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from EmploymentHistory where EmploymentHistoryID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType EmploymentHistoryInsert(int UserID, int StructureID, int CadreID, int GardenID, int CadreTypeID, int PositionID, string EmploymentNumber, string EntryDate, string ExitDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into EmploymentHistory (UserID,StructureID,CadreID,GardenID,CadreTypeID,PositionID,EmploymentNumber,EntryDate,ExitDate)  
Values(@UserID,@StructureID,@CadreID,@GardenID,@CadreTypeID,@PositionID,@EmploymentNumber,@EntryDate,@ExitDate)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);        
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
        cmd.Parameters.AddWithValue("@EmploymentNumber", EmploymentNumber);
        cmd.Parameters.AddWithValue("@EntryDate", ConvertTypes.ToParseDatetime(EntryDate));
        cmd.Parameters.AddWithValue("@ExitDate", ConvertTypes.ToParseDatetime(ExitDate));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType EmploymentHistoryUpdate(int EmploymentHistoryID, int UserID, int StructureID, int CadreID, int GardenID, int CadreTypeID, int PositionID, string EmploymentNumber, string EntryDate, string ExitDate)
    {
        SqlCommand cmd = new SqlCommand(@"update EmploymentHistory set UserID=@UserID,StructureID=@StructureID,
CadreID=@CadreID,GardenID=@GardenID,CadreTypeID=@CadreTypeID,PositionID=@PositionID,
EmploymentNumber=@EmploymentNumber,EntryDate=@EntryDate,ExitDate=@ExitDate 
where EmploymentHistoryID=@EmploymentHistoryID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
        cmd.Parameters.AddWithValue("@EmploymentNumber", EmploymentNumber);
        cmd.Parameters.AddWithValue("@EntryDate", ConvertTypes.ToParseDatetime(EntryDate));
        cmd.Parameters.AddWithValue("@ExitDate", ConvertTypes.ToParseDatetime(ExitDate));
        cmd.Parameters.AddWithValue("@EmploymentHistoryID", EmploymentHistoryID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType DeleteEmploymentHistory(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update EmploymentHistory set DeleteTime=GetDate() where EmploymentHistoryID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetEmploymentHistory()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"Select ROW_NUMBER() over(order by c.EmploymentHistoryID desc) sn,c.UserID,s.StructureID, s.StructureName, k.CardID,
 c.CadreID,c.CadreTypeID,ct.CadreTypeName,c.EmploymentHistoryID, k.CardNumber, g.GenderName,gd.GardenName,
c.EntryDate,c.ExitDate,c.EmploymentNumber, p.PositionName, 
cd.Gender, isnull(k.CardNumber,'')+' '+cd.Sname+' '+ cd.Name+' '+ cd.FName NameDDL
from EmploymentHistory c 
left join Structure s on s.StructureID=c.StructureID 
left join Cadres cd on cd.CadreID=c.CadreID
left join Cards k on cd.CardID=k.CardID
left join Positions p on c.PositionID=p.PositionID
left join CadreType ct on ct.CadreTypeID=c.CadreTypeID
left join Gardens gd on gd.GardenID=c.GardenID
left join Genders g on g.GenderID=cd.Gender where c.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    // Kadrlar
    public DataTable GetCadresById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from Cadres where CadreID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType CadresInsert(int UserID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string RegstrDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Cadres (UserID, CardID, Sname, Name, FName, Gender, PassportN, PIN, PhoneNumber, Photo, Email, Address)  
Values(@UserID, @CardID, @Sname, @Name, @FName, @Gender, @PassportN, @PIN, @PhoneNumber, @Photo, @Email, @Address)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardID", CardID);
        cmd.Parameters.AddWithValue("@Sname", Sname);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@FName", FName);
        cmd.Parameters.AddWithValue("@Gender", Gender);
        cmd.Parameters.AddWithValue("@PassportN", PassportN);
        cmd.Parameters.AddWithValue("@PIN", PIN);
        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        cmd.Parameters.AddWithValue("@Photo", Photo);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Address", Address);
        try
        {
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType CadresUpdate(int CadreID, int UserID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string RegstrDate)
    {
        SqlCommand cmd = new SqlCommand(@"update Cadres set UserID=@UserID, CardID=@CardID,Sname=@Sname, Name=@Name, FName=@FName,Gender=@Gender, PassportN=@PassportN, PIN=@PIN, 
PhoneNumber=@PhoneNumber, Photo=@Photo, Email=@Email, Address=@Address where CadreID=@CadreID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardID", CardID);
        cmd.Parameters.AddWithValue("@Sname", Sname);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@FName", FName);
        cmd.Parameters.AddWithValue("@Gender", Gender);
        cmd.Parameters.AddWithValue("@PassportN", PassportN);
        cmd.Parameters.AddWithValue("@PIN", PIN);
        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        cmd.Parameters.AddWithValue("@Photo", Photo);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        try
        {
            cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
        cmd.Dispose();
        }
    }

    public Types.ProsesType DeleteCadres(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update Cadres set DeleteTime=GetDate() where CadreID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetCadres()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"Select ROW_NUMBER() over(order by c.CadreID desc) sn, k.CardNumber, 
c.Sname, c.Name, c.FName, g.GenderName, c.PassportN, c.PIN, c.[Address], c.PhoneNumber,c.Email,case when c.Photo='' then 'avatar.png' else c.Photo end Photo,
c.RegisterTime, c.CadreID, c.UserID, c.CardID, c.Gender, isnull(k.CardNumber,'')+' '+c.Sname+' '+ c.Name+' '+ c.FName NameDDL
from Cadres c 
left join Cards k on c.CardID=k.CardID 
left join Genders g on g.GenderID=c.Gender where c.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    //-------Struktur kateqoriyasi
    public DataTable GetStructure()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by s.StructureSort) sn,s.StructureName, s.StructureSort, s.StructureID from Structure s  where s.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteStructure(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update Structure set DeleteTime=GetDate() where StructureID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetStructureById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from Structure where StructureID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType StructureInsert(int UserID, string StructureName, float StructureSort)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Structure (UserID, StructureName, StructureSort)  Values(@UserID, @StructureName, @StructureSort)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureName", StructureName);
        cmd.Parameters.AddWithValue("@StructureSort", StructureSort);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType StructureUpdate(int StructureID, int UserID, string StructureName, float StructureSort)
    {
        SqlCommand cmd = new SqlCommand(@"update Structure set UserID=@UserID, StructureName=@StructureName, StructureSort=@StructureSort where StructureID=@StructureID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureName", StructureName);
        cmd.Parameters.AddWithValue("@StructureSort", StructureSort);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    //-------Positions kateqoriyasi
    public DataTable GetPositions()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by p.PositionID) sn,p.PositionName, p.PositionID from Positions p  where p.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeletePosition(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update Positions set DeleteTime=GetDate() where PositionID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetPositionById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from Positions where PositionID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType PositionInsert(int UserID, string PositionName)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Positions (UserID, PositionName)  Values(@UserID, @PositionName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@PositionName", PositionName);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType PositionUpdate(int PositionID, int UserID, string PositionName)
    {
        SqlCommand cmd = new SqlCommand(@"update Positions set UserID=@UserID, PositionName=@PositionName where PositionID=@PositionID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@PositionName", PositionName);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    //Kadrlar uzre emeliyyat
    public DataTable GetOperationCadre()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by w.WorkDoneID) sn,
c.Name+' '+c.Sname+' '+c.FName fullname, s.WorkName,w.Salary,
g.GardenName,z.ZoneName,sc.SectorName,l.LineName,w.TreeCount,
w.Notes,w.RegisterTime,w.WorkDoneID,w.UserID,w.LinesID,w.CadreID,w.WorkID,
g.GardenID,z.ZoneID,sc.SectorID,wc.WeatherConditionName+' ('+cast(wc.Coefficient as varchar)+')' as WeatherConditionName1
, tt.TreeTypeName+' ('+cast(tt.Coefficient as varchar)+')' TreeTypeName1,
cast(tta.FirstAge as varchar)+' - '+cast(tta.LastAge as varchar)+' yaş ('+cast(tta.Coefficient as varchar)+')' TreeAgeName1
from WorkDone w
left join Cadres c on c.CadreID=w.CadreID
left join Works s on s.WorkID=w.WorkID
left join Lines l on l.LineID=w.LinesID
left join Sectors sc on sc.SectorID=l.SectorID
left join Zones z on z.ZoneID=sc.ZoneID
left join Gardens g on g.GardenID=z.GardenID 
left join WeatherCondition wc on wc.WeatherConditionID=w.WeatherConditionID
left join TreeTypes tt on tt.TreeTypeID=w.TreeTypeID
left join TariffTreeAge tta on tta.TariffAgeID=w.TariffAgeID
where w.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationCadre(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update WorkDone set DeleteTime=GetDate() where WorkDoneID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetOperationCadreByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by w.WorkDoneID) sn,
c.Name+' '+c.Sname+' '+c.FName fullname, s.WorkName,
g.GardenName,z.ZoneName,sc.SectorName,l.LineName,w.TreeCount,
w.Notes,w.RegisterTime,w.WorkDoneID,w.UserID,w.LinesID,w.CadreID,
g.GardenID,z.ZoneID,sc.SectorID,w.WorkID,w.WeatherConditionID,w.TreeTypeID,TariffAgeID,w.Salary
from WorkDone w
left join Cadres c on c.CadreID=w.CadreID
left join Works s on s.WorkID=w.WorkID
left join Lines l on l.LineID=w.LinesID
left join Sectors sc on sc.SectorID=l.SectorID
left join Zones z on z.ZoneID=sc.ZoneID
left join Gardens g on g.GardenID=z.GardenID 
where w.DeleteTime is null and w.WorkDoneID=@WorkDoneID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@WorkDoneID", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType OperationCadreInsert(int UserID, int CadreID, int WorkID, int LinesID, int WeatherConditionID, int TreeTypeID, int TariffAgeID, int TreeCount, float Salary, string Notes, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into WorkDone (UserID,CadreID,WorkID,LinesID,WeatherConditionID,TreeTypeID,TariffAgeID,TreeCount,Salary,Notes,RegisterTime)
                                            Values(@UserID, @CadreID,@WorkID,@LinesID,@WeatherConditionID,@TreeTypeID,@TariffAgeID,@TreeCount,@Salary,@Notes,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@LinesID", LinesID);
        cmd.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Salary", Salary);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType OperationCadreUpdate(int WorkDoneID, int UserID, int CadreID, int WorkID, int LinesID, 
        int WeatherConditionID, int TreeTypeID, int TariffAgeID, int TreeCount, float Salary, string Notes, 
        string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update WorkDone set UserID=@UserID, CadreID=@CadreID,WorkID=@WorkID,
LinesID=@LinesID,WeatherConditionID=@WeatherConditionID,TreeTypeID=@TreeTypeID,TariffAgeID=@TariffAgeID,
TreeCount=@TreeCount,Salary=@Salary,Notes=@Notes,RegisterTime=@RegisterTime where WorkDoneID=@WorkDoneID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@LinesID", LinesID);
        cmd.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Salary", Salary);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@WorkDoneID", WorkDoneID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //Texnikilarin gorduyu isler uzre emeliyyat
    public DataTable GetOperationTechniqueWorkDone()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by t.TechniquesWorkDoneID) sn,t.*,c.CompanyName,sc.SectorID,sc.SectorName,
z.ZoneID,z.ZoneName,g.GardenID,g.GardenName,tc.TechniqueID,tc.TechniqueID,tc.TechniquesName+' '+Birka as TechniquesName,s.WorkName,z.ZoneName,g.GardenName,sc.SectorName,l.LineName
from TechniquesWorkDone t
left join Companies c on c.CompanyID=t.CompanyID
left join Works s on s.WorkID=t.WorkID
left join Lines l on l.LineID=t.LineID
left join Sectors sc on sc.SectorID=l.SectorID
left join Zones z on z.ZoneID=sc.ZoneID
left join Gardens g on g.GardenID=z.GardenID 
left join Techniques tc on tc.TechniqueID=t.TechniqueID
where t.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationTechniqueWorkDone(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update TechniquesWorkDone set DeleteTime=GetDate() where TechniquesWorkDoneID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetTechniqueByModelId(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from Techniques where ModelID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetOperationTechniqueWorkDoneByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by t.TechniquesWorkDoneID) sn,t.*,c.CompanyName,sc.SectorID,sc.SectorName,
z.ZoneID,z.ZoneName,g.GardenID,g.GardenName,tc.TechniqueID,tc.TechniqueID,tc.TechniquesName+' '+Birka as TechniquesName,
tc.ModelID,m.ModelName,m.ProductTypeID,
l.SectorID,sc.ZoneID,z.GardenID 
from TechniquesWorkDone t
left join Companies c on c.CompanyID=t.CompanyID
left join Works s on s.WorkID=t.WorkID
left join Lines l on l.LineID=t.LineID
left join Sectors sc on sc.SectorID=l.SectorID
left join Zones z on z.ZoneID=sc.ZoneID
left join Gardens g on g.GardenID=z.GardenID 
left join Techniques tc on tc.TechniqueID=t.TechniqueID
left join Models m on tc.ModelID=m.ModelID
where t.DeleteTime is null and t.TechniquesWorkDoneID=@TechniquesWorkDoneID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@TechniquesWorkDoneID", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType OperationTechniqueWorkDoneInsert(int UserID, int TechniqueID, int CompanyID, int WorkID, 
        int Odometer, int LineID, int TreeCount, string Notes, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into TechniquesWorkDone (UserID,TechniqueID,CompanyID,WorkID,Odometer,LineID,TreeCount,Notes,RegisterTime)
                                            Values(@UserID,@TechniqueID,@CompanyID,@WorkID,@Odometer,@LineID,@TreeCount,@Notes,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@LineID", LineID);        
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType OperationTechniqueWorkDoneUpdate(int TechniquesWorkDoneID, int UserID, int TechniqueID, 
        int CompanyID, int WorkID, int Odometer, int LineID, int TreeCount, string Notes, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update TechniquesWorkDone set UserID=@UserID, TechniqueID=@TechniqueID,CompanyID=@CompanyID,WorkID=@WorkID,Odometer=@Odometer,LineID=@LineID,TreeCount=@TreeCount,Notes=@Notes,RegisterTime=@RegisterTime where TechniquesWorkDoneID=@TechniquesWorkDoneID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@TechniquesWorkDoneID", TechniquesWorkDoneID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //-------Texnikalar uzre emeliyyat

    public DataTable GetOperationTechniques()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by w.TechniquesWorkDoneID) sn,
t.TechniquesName, s.WorkName,g.GardenName,z.ZonaName,sc.SectorName,
l.LineName,w.Odometer,
w.Note,w.RegstrTime,w.TechniquesWorkDoneID,w.UserID,w.LineID,w.WorkID,
g.GardenID,z.ZoneID,sc.SectorsID
from TechniquesWorkDone w
left join Techniques t on t.TechniqueID=w.TechniqueID
left join Works s on s.WorkID=w.WorkID
left join Lines l on l.LineID=w.LineID
left join Sectors sc on sc.SectorsID=l.SektorID
left join Zones z on z.ZoneID=sc.ZonaID
left join Gardens g on g.GardenID=z.GardenID where w.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationTechniques(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update TechniquesWorkDone set DeleteTime=GetDate() where TechniquesWorkDoneID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetTechniquesWorkDoneById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from TechniquesWorkDone where TechniquesWorkDoneID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    //-------Suvarma sistemleri uzre emeliyyat

    public DataTable GetOperationWateringSystems()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by w.WateringSystemWorkID) sn,
wt.WateringSystemName, g.GardenName,Ws.ProductOperationTypeName, w.WateringSystemSize,
um.UnitMeasurementName,w.Notes,w.RegisterTime,w.WateringSystemWorkID,
w.UserID,w.UnitMeasurementID,g.GardenID
from WateringSystemWorkDone w
left join Gardens g on g.GardenID=w.GardenID
left join WateringSystems wt on wt.WateringSystemID=w.WateringSystemID
left join ProductOperationTypes Ws on Ws.ProductOperationTypeID=W.EntryExitStatus
left join UnitMeasurements um on um.UnitMeasurementID=W.UnitMeasurementID
where w.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationWateringSystems(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update WateringSystemWorkDone set DeleteTime=GetDate() 
where WateringSystemWorkID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetOperationWateringSystemsById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from WateringSystemWorkDone where WateringSystemWorkID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Types.ProsesType OperationWateringSystemsWorkDoneInsert(int UserID, int GardenID, int WateringSystemID, 
        string WateringSystemSize, int UnitMeasurementID, int EntryExitStatus, string Notes, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into WateringSystemWorkDone (UserID,GardenID,WateringSystemID,WateringSystemSize,
UnitMeasurementID,EntryExitStatus,Notes,RegisterTime)
 Values(@UserID,@GardenID,@WateringSystemID,@WateringSystemSize,@UnitMeasurementID,@EntryExitStatus,@Notes,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        cmd.Parameters.AddWithValue("@WateringSystemSize", WateringSystemSize);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType OperationWateringSystemsWorkDoneUpdate(int WateringSystemWorkID, int UserID, int GardenID, int WateringSystemID, string WateringSystemSize, int UnitMeasurementID, int EntryExitStatus, string Notes, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update WateringSystemWorkDone set UserID=@UserID, GardenID=@GardenID,
WateringSystemID=@WateringSystemID,WateringSystemSize=@WateringSystemSize,UnitMeasurementID=@UnitMeasurementID,
EntryExitStatus=@EntryExitStatus,Notes=@Notes,RegisterTime=@RegisterTime where WateringSystemWorkID=@WateringSystemWorkID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        cmd.Parameters.AddWithValue("@WateringSystemSize", WateringSystemSize);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@WateringSystemWorkID", WateringSystemWorkID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    //hava seraiti
    public DataTable GetWeatherCondition()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [WeatherConditionID] desc) sn,
* FROM [WeatherCondition] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetWeatherConditionByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [WeatherConditionID] desc) sn,
* FROM [WeatherCondition] where DeleteTime is null and WeatherConditionID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    
    //agac yaslari tarifi
    public DataTable GetTariffTreeAge()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [TariffAgeID] desc) sn,
*,cast(FirstAge as varchar)+' - '+cast(LastAge as varchar)+' yaş ('+cast(Coefficient as varchar)+')' TariffAgeName1 FROM [TariffTreeAge] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetTariffTreeAgeByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() 
over(order by [TariffAgeID] desc) sn,* FROM [TariffTreeAge] where DeleteTime is null 
and TariffAgeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType TariffTreeAgeInsert(int FirstAge, int LastAge, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TariffTreeAge 
(UserID,FirstAge,LastAge,Coefficient) values (@UserID,@FirstAge,@LastAge,@Coefficient)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@FirstAge", FirstAge);
        cmd.Parameters.AddWithValue("@LastAge", LastAge);
        cmd.Parameters.AddWithValue("@Coefficient", ConvertTypes.ToParseFloat(Coefficient));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType TariffTreeAgeUpdate(int TariffAgeID,
        int FirstAge, int LastAge, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"update TariffTreeAge set UserID=@UserID,
FirstAge=@FirstAge,LastAge=@LastAge,Coefficient=@Coefficient,
UpdateTime=getdate() where TariffAgeID=@TariffAgeID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        cmd.Parameters.AddWithValue("@FirstAge", FirstAge);
        cmd.Parameters.AddWithValue("@LastAge", LastAge);
        cmd.Parameters.AddWithValue("@Coefficient", ConvertTypes.ToParseFloat(Coefficient));
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteTariffTreeAge(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update TariffTreeAge set deletetime=getdate(),
UserID=@UserID where TariffAgeID=@TariffAgeID;", SqlConn);
        cmd.Parameters.AddWithValue("@TariffAgeID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Types.ProsesType WeatherConditionInsert(string WeatherConditionName, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"insert into WeatherCondition 
(UserID,WeatherConditionName,Coefficient) values (@UserID,@WeatherConditionName,@Coefficient)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@WeatherConditionName", WeatherConditionName);
        cmd.Parameters.AddWithValue("@Coefficient", Coefficient);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType WeatherConditionUpdate(int WeatherConditionID, string WeatherConditionName, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"update WeatherCondition set UserID=@UserID,
WeatherConditionName=@WeatherConditionName,Coefficient=@Coefficient,
UpdateTime=getdate() where WeatherConditionID=@WeatherConditionID", SqlConn);
        cmd.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@WeatherConditionName", WeatherConditionName);
        cmd.Parameters.AddWithValue("@Coefficient", Coefficient);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Types.ProsesType DeleteWeatherCondition(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update WeatherCondition set deletetime=getdate(),
UserID=@UserID where WeatherConditionID=@WeatherConditionID;", SqlConn);
        cmd.Parameters.AddWithValue("@WeatherConditionID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }




    //giris cixis
    public DataTable GetEntryExits()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"
select t.[Bağın adı],t.[İşçinin adı],t.Barkod,t.[Kartın nömrəsi],t.[Tarix], case when t.[sn] % 2 =0 then N'Çıxış' 
else N'Giriş' end [Status],EntryExitID,
row_number() over( order by t.Tarix desc) [Sıra nömrəsi] from (

SELECT row_number() over(PARTITION BY c1.Sname,c1.Name,c1.FName order by c1.Sname,c1.Name,c1.FName,e.InsertTime) [sn],
c2.CardNumber [Kartın nömrəsi],c1.Sname+' '+c1.Name+' '+c1.FName [İşçinin adı],e.InsertTime Tarix,e.EntryExitID,g.GardenName [Bağın adı], c2.CardBarcode [Barkod]
from [EntryExit] e 
left join Cadres c1 on e.CadreID=c1.CadreID 
left join Cards c2 on c2.CardID=c1.CardID 
left join Users u on u.UserID=e.UserID
left join Gardens g on u.GardenID=g.GardenID
where e.DeleteTime is null and c1.DeleteTime is null  and 
c2.DeleteTime is null and u.DeleteTime is null and g.DeleteTime is null

) t
", SqlConn);
           
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
//    //giris cixis
//    public DataTable GetEntryExits()
//    {
//        try
//        {
//            DataTable dt = new DataTable();
//            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [EntryExitID] desc) sn,
//e.*,c2.CardNumber,c1.Sname+' '+c1.Name+' '+c1.FName fullname,case when e.EntryExitStatus =1 then N'Giriş' 
//when e.EntryExitStatus =2 then N'Çıxış' else N'Xəta baş verdi!' end EntryExitName from [EntryExit] e 
//left join Cadres c1 on e.CadreID=c1.CadreID 
//left join Cards c2 on c2.CardID=c1.CardID  where e.DeleteTime is null and c1.DeleteTime is null  and c2.DeleteTime is null ", SqlConn);
//            da.Fill(dt);
//            return dt;
//        }
//        catch (Exception ex)
//        {
//            return null;
//        }
//    }
    public DataTable GetCadreByCardnumber(string cardnumber)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT  CadreID,c2.CardNumber,c1.Sname+' '+c1.Name+' '+c1.FName fullname from   Cadres c1 
left join Cards c2 on c2.CardID=c1.CardID  where c1.DeleteTime is null
and c2.DeleteTime is null and c2.CardNumber=@cardnumber", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("cardnumber", cardnumber);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public Types.ProsesType EntryExitInsert(int CadreID, int EntryExitStatus)
    {

        SqlCommand cmd = new SqlCommand(@"insert into EntryExit 
(UserID,CadreID,EntryExitStatus) values (@UserID,@CadreID,@EntryExitStatus)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
        cmd.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Types.ProsesType DeleteEntryExit(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update EntryExit set deletetime=getdate(),UserID=@UserID 
where EntryExitID=@EntryExitID;", SqlConn);
        cmd.Parameters.AddWithValue("@EntryExitID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }







    public DataTable GetUserStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT *
  FROM [UserStatus] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GetProductOperationTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT *
  FROM [ProductOperationTypes] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetStockOperationReasonsByProductOperationTypeID(int ProductOperationTypeID)
    {  //and c.cardid not in (select cardid from cadres)
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT *
  FROM [StockOperationReasons] where deletetime is null and ProductOperationTypeID=@ProductOperationTypeID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public DataTable GetProductStockInputOutput()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,s.StockName,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,
sor.ReasonName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName FROM [ProductStockInputOutput] psio 
left join ProductOperationTypes pot on psio.ProductOperationTypeID=pot.ProductOperationTypeID
left join StockOperationReasons sor on psio.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID
left join Products p on psio.ProductID=p.ProductID
left join Models m on m.ModelID=p.ModelID
left join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID
left join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID
left join Stocks s on s.StockID=psio.StockID
 where psio.DeleteTime is null
", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public DataTable GetProductStockInputOutputByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,s.StockName,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,
sor.ReasonName,p.ProductsName,um.UnitMeasurementID,um.UnitMeasurementName,case when m.ModelID is null then 0 else m.ModelID end ModelID,
m.ModelName FROM [ProductStockInputOutput] psio 
left join ProductOperationTypes pot on psio.ProductOperationTypeID=pot.ProductOperationTypeID
left join StockOperationReasons sor on psio.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID
left join Products p on psio.ProductID=p.ProductID
left join Models m on m.ModelID=p.ModelID
left join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID
left join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID
left join Stocks s on s.StockID=psio.StockID
 where psio.DeleteTime is null
 and ProductStockInputOutputID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType ProductStockInputOutputInsert(int StockID, int ProductOperationTypeID,
        int StockOperationReasonID, int ProductID, string ProductSize,
        string Price, string PriceDiscount, string Amount, string AmountDiscount, string RegisterTime, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into ProductStockInputOutput 
(UserID,StockID,ProductOperationTypeID,StockOperationReasonID,ProductID,
ProductSize,Price,PriceDiscount,Amount,AmountDiscount,RegisterTime,Notes) values 
(@UserID,@StockID,@ProductOperationTypeID,@StockOperationReasonID,@ProductID,
@ProductSize,@Price,@PriceDiscount,@Amount,@AmountDiscount,@RegisterTime,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@StockID", StockID);
        cmd.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        cmd.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        
        cmd.Parameters.AddWithValue("@ProductSize", ConvertTypes.ToParseFloat(ProductSize));
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@PriceDiscount", ConvertTypes.ToParseFloat(PriceDiscount));
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@AmountDiscount", ConvertTypes.ToParseFloat(AmountDiscount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@Notes", Notes);
        //try
        //{
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        //}
        //catch (Exception ex)
        //{
        //    return Types.ProsesType.Error;
        //}
        //finally
        //{
        //    cmd.Connection.Close();
        //    cmd.Dispose();
        //}
    }



    public Types.ProsesType ProductStockInputOutputUpdate(int ProductStockInputOutputID,int StockID,
        int ProductOperationTypeID, int StockOperationReasonID, int ProductID, 
        string ProductSize, string Price, string PriceDiscount, string Amount,
        string AmountDiscount, string RegisterTime, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update ProductStockInputOutput set UserID=@UserID,
ProductOperationTypeID=@ProductOperationTypeID,StockID=@StockID,StockOperationReasonID=@StockOperationReasonID,
ProductID=@ProductID,ProductSize=@ProductSize,
Price=@Price,PriceDiscount=@PriceDiscount,Amount=@Amount,AmountDiscount=@AmountDiscount,
RegisterTime=@RegisterTime,Notes=@Notes,UpdateTime=getdate() where ProductStockInputOutputID=@ProductStockInputOutputID", SqlConn);
        cmd.Parameters.AddWithValue("@ProductStockInputOutputID", ProductStockInputOutputID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@StockID", StockID);
        cmd.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        cmd.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@ProductSize", ConvertTypes.ToParseFloat(ProductSize));
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@PriceDiscount", ConvertTypes.ToParseFloat(PriceDiscount));
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@AmountDiscount", ConvertTypes.ToParseFloat(AmountDiscount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@Notes", Notes);
        //try
        //{
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        //}
        //catch (Exception ex)
        //{
        //    return Types.ProsesType.Error;
        //}
        //finally
        //{
        //    cmd.Connection.Close();
        //    cmd.Dispose();
        //}
    }


    public Types.ProsesType DeleteProductStockInputOutput(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update ProductStockInputOutput set deletetime=getdate(),UserID=@UserID 
where ProductStockInputOutputID=@ProductStockInputOutputID;", SqlConn);
        cmd.Parameters.AddWithValue("@ProductStockInputOutputID", id);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            //LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }













    public DataTable GetProductByModelProductId(int ModelID,int ProductTypeID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ProductID desc) sn,
       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName
      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName
      ,[Notes] from [Products] p 
  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID
  left join Models m on p.ModelID=m.ModelID
  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
  where p.DeleteTime is null and pt.DeleteTime is null and 
m.DeleteTime is null and u.DeleteTime is null and p.ProductTypeID=@ProductTypeID and p.ModelID=@ModelID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("ModelID", ModelID);
            da.SelectCommand.Parameters.AddWithValue("ProductTypeID", ProductTypeID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }




    public DataTable GetTechniquesByModelId(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TechniqueID desc) sn,
 p.* from Techniques p  left join Models m on p.ModelID=m.ModelID where p.DeleteTime is null  and 
 m.DeleteTime is null  and m.ModelID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }




    public DataTable GetExpenses()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() 
over(order by OtherExpenseID desc) sn,e.* from OtherExpenses e where e.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetExpenseByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() 
over(order by OtherExpenseID desc) sn,e.* from OtherExpenses e where e.DeleteTime is null 
and OtherExpenseID=@id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType InsertExpense(string OtherExpenseName, string Amount, string Note, 
        string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into OtherExpenses 
(UserID,OtherExpenseName,Amount,Note,RegisterTime) values 
(@UserID,@OtherExpenseName,@Amount,@Note,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@OtherExpenseName", OtherExpenseName);
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@Note", Note);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Types.ProsesType UpdateExpense(int OtherExpenseID, string OtherExpenseName,
        string Amount, string Note, string RegisterTime)
    {
 

        SqlCommand cmd = new SqlCommand(@"update OtherExpenses set UserID=@UserID,
OtherExpenseName=@OtherExpenseName,Amount=@Amount,RegisterTime=@RegisterTime,Note=@Note,
UpdateTime=getdate() 
where OtherExpenseID=@OtherExpenseID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@OtherExpenseID", OtherExpenseID);
        cmd.Parameters.AddWithValue("@OtherExpenseName", OtherExpenseName);
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@Note", Note);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    public Types.ProsesType DeleteExpense(int id)
    {
        SqlCommand cmd = new SqlCommand(@"Update OtherExpenses set DeleteTime=GetDate() where OtherExpenseID=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetSiteMap()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select lm.* from SiteMap lm 
where  lm.SiteMapID not in (
select sm.SiteMapID from Users u 
inner join PermissionUser p on u.UserID = p.UserID
inner join SiteMap sm on p.SiteMapID = sm.SiteMapID where p.UserID=@UserID)", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("UserID", HttpContext.Current.Session["UserID"].ToParseStr());
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GetPermisionByUserID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select * from PermissionUser p 
where p.UserID=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("UserID", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }











    public DataTable GetTreesCounts()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"

SELECT row_number() 
over(order by TreeCountID desc) sn,tc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,ts.TreesSitiuationName,
tt.TreeTypeName,t.TreeName,c.CountryName
  FROM [TreesCount] tc 
  left join Lines l on  tc.LineID=l.LineID 
  left join Sectors s on s.SectorID=l.SectorID
  left join Zones z on z.ZoneID=s.ZoneID
  left join Gardens g on g.GardenID=z.GardenID 
  left join TreesSitiuation ts on tc.TreeSitiuation=ts.TreesSitiuationID
  left join TreeTypes tt on tc.TreeTypeID=tt.TreeTypeID
  left join Countries c on c.CountryID=tt.CountryID
  left join Trees t on t.TreeID=tt.TreeID
  where tc.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GetTreesCountsByID(int id)
    {
        //try
        //{
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"

SELECT row_number() 
over(order by TreeCountID desc) sn,tc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,ts.TreesSitiuationName,
tt.TreeTypeName,t.TreeName,c.CountryName,s.SectorID,g.GardenID,z.ZoneID,c.CountryID,t.TreeID,ts.TreesSitiuationID
  FROM [TreesCount] tc 
  left join Lines l on  tc.LineID=l.LineID 
  left join Sectors s on s.SectorID=l.SectorID
  left join Zones z on z.ZoneID=s.ZoneID
  left join Gardens g on g.GardenID=z.GardenID 
  left join TreesSitiuation ts on tc.TreeSitiuation=ts.TreesSitiuationID
  left join TreeTypes tt on tc.TreeTypeID=tt.TreeTypeID
  left join Countries c on c.CountryID=tt.CountryID
  left join Trees t on t.TreeID=tt.TreeID
  where tc.DeleteTime is null and TreeCountID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        //}
        //catch (Exception ex)
        //{
        //    return null;
        //}
    }





    public Types.ProsesType TreeCountInsert(int LineID, int TreeTypeID,
    int TreeCount, int TreeSitiuation, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into TreesCount 
(UserID,RegisterTime,LineID,TreeTypeID,TreeCount,TreeSitiuation) 
values (@UserID,@RegisterTime,@LineID,@TreeTypeID,@TreeCount,@TreeSitiuation)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@TreeSitiuation", TreeSitiuation);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }




    public Types.ProsesType TreeCountUpdate(int TreeCountID, int LineID, int TreeTypeID,
    int TreeCount, int TreeSitiuation, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update TreesCount set UserID=@UserID,
RegisterTime=@RegisterTime,LineID=@LineID,TreeTypeID=@TreeTypeID,TreeCount=@TreeCount,
TreeSitiuation=@TreeSitiuation,UpdateTime=getdate() where TreeCountID=@TreeCountID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TreeCountID", TreeCountID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@TreeSitiuation", TreeSitiuation);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception ex)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    //Anbar Kocurmeleri
    public DataTable GetProductStock()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by ps.StockID desc) sn,ps.ProductID,s.StockID,s.StockName,
pt.ProductTypeID,pt.ProductTypeName,
p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName
      ,[productsizesum]
  FROM [dbo].[vProductStock] ps 
left join Products p on ps.ProductID=p.ProductID
left join Models m on m.ModelID=p.ModelID
left join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID
left join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID
left join Stocks s on s.StockID=ps.StockID", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetProductStockByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT * from ProductStock where DeleteTime is null and ProductStockID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public Types.ProsesType ProductStockInsertTransfer(int StockFromID, int UserID,int ProductID, int StockToID, 
        string ProductSize,string RegisterTime)
    {
    
        SqlCommand cmd2 = new SqlCommand(@"Insert into ProductStockTransfer (UserID,StockFromID,StockToID,ProductID,
ProductSize,RegisterTime) 
Values (@UserID,@StockFromID,@StockToID,@ProductID,@ProductSize,@RegisterTime)", SqlConn);
        cmd2.Parameters.AddWithValue("@UserID", UserID);
        cmd2.Parameters.AddWithValue("@StockFromID", StockFromID);
        cmd2.Parameters.AddWithValue("@StockToID", StockToID);
        cmd2.Parameters.AddWithValue("@ProductID", ProductID);
        cmd2.Parameters.AddWithValue("@ProductSize", ConvertTypes.ToParseFloat(ProductSize));
        cmd2.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
       

        //try
        //{

            cmd2.Connection.Open();
            cmd2.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        //}
        //catch (Exception ex)
        //{
        //    return Types.ProsesType.Error;
        //}
        //finally
        //{
            
        //    cmd2.Connection.Close();
        //    cmd2.Dispose();
        //}
    }


}