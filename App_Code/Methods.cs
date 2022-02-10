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
    //public SqlConnection SqlConn = new SqlConnection(@"Data Source=(local);Initial Catalog=ETES_DB;Integrated Security=true;");

    public static SqlConnection SqlConn
    {
         get { return new SqlConnection(@"Data Source=(local);Initial Catalog=Gardens;Integrated Security=true;Max Pool Size=1024;Pooling=true;"); }

    }
    public DataTable User (string Login, string Pass)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"Select * from Users where UserName=@UserName and Password=@Password", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@UserName", Login);
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
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users where id=@userid", SqlConn);
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
            string status = " ";
            if (HttpContext.Current.Session["StatusID"].ToParseStr() == "2")
            {
                status = " and a.ID=" + HttpContext.Current.Session["UsersID1"].ToParseStr();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select 
ROW_NUMBER() over (order by a.id desc, a.FullName) SN,a.*,e.Adi ElmiShura 
from Users a inner join TeElmiShura e on a.ElmiShuraID=e.ID where 1=1 " + status +
" order by a.id desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType UsersUpdate(int id,int ElmiShuraID, string UserName, string Password, string FullName)
    {
        SqlCommand cmd = new SqlCommand(@"update Users set 
UserName=@UserName, Password=@Password, ElmiShuraID=@ElmiShuraID,FullName=@FullName where ID=@id", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@ElmiShuraID", ElmiShuraID);
        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@FullName", FullName);
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
    public Types.ProsesType UsersInsert(int ElmiShuraID, string UserName, string Password,string FullName)
    {

 
        SqlCommand cmd = new SqlCommand(@"insert into Users (FullName,UserName,Password,ElmiShuraID) values 
(@FullName,@UserName,@Password,@ElmiShuraID)", SqlConn);
        cmd.Parameters.AddWithValue("@ElmiShuraID", ElmiShuraID);
        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@FullName", FullName);

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






    public DataTable GetUnitMeasurements()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by UnitMeasurementID desc) sn,
UnitMeasurementID,UnitMeasurementName,InsertTime,UpdateTime,DeleteTime FROM UnitMeasurements
  where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }




    public DataTable GetGardens()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by GardenID desc) sn,
GardenID,UserID,RegisterTime,GardenName,GardenArea,u.UnitMeasurementName,Address,Notes 
  from Gardens g left join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID 
  where g.DeleteTime is null", SqlConn);
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
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over(order by GardenID desc) sn,
GardenID,UserID,RegisterTime,GardenName,GardenArea,UnitMeasurementID,Address,Notes,InsertTime,UpdateTime,DeleteTime
  from Gardens where DeleteTime is null and GardenID=@id", SqlConn);
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






    public DataTable GetZones()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ZoneID desc) sn,
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZonaName,ZonaArea,z.UnitMeasurementID,u.UnitMeasurementName,z.Notes
FROM Zones z left join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
where z.DeleteTime is null", SqlConn);
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
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZonaName,ZonaArea,z.GardenID,z.UnitMeasurementID,u.UnitMeasurementName,z.Notes
from Zones z  left join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
where z.DeleteTime is null and z.ZoneID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ZoneInsert(string RegisterTime,int GardenID, string ZonaName, string ZonaArea,
        int UnitMeasurementID, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Zones 
(UserID,RegisterTime,GardenID,ZonaName,ZonaArea,UnitMeasurementID,Notes) 
values (@UserID,@RegisterTime,@GardenID,@ZonaName,@ZonaArea,@UnitMeasurementID,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ZonaName", ZonaName);
        cmd.Parameters.AddWithValue("@ZonaArea", ConvertTypes.ToParseFloat(ZonaArea));
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
    public Types.ProsesType ZoneUpdate(int ZoneID, string RegisterTime, int GardenID, string ZonaName, string ZonaArea,
        int UnitMeasurementID, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Zones set UserID=@UserID,RegisterTime=@RegisterTime,
GardenID=@GardenID, ZonaName=@ZonaName, ZonaArea=@ZonaArea,
UnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() where ZoneID=@ZoneID", SqlConn);

        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));

        cmd.Parameters.AddWithValue("@GardenID", GardenID);
        cmd.Parameters.AddWithValue("@ZonaName", ZonaName);
        cmd.Parameters.AddWithValue("@ZonaArea", ConvertTypes.ToParseFloat(ZonaArea));
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



}