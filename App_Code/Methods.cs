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
RegisterTime,UnitMeasurementID,UnitMeasurementName,InsertTime,UpdateTime,DeleteTime FROM UnitMeasurements
  where DeleteTime is null", SqlConn);
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
RegisterTime,UnitMeasurementID,UnitMeasurementName,InsertTime,UpdateTime,DeleteTime FROM UnitMeasurements
  where DeleteTime is null  and UnitMeasurementID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType UnitMeasurementInsert(string RegisterTime, string UnitMeasurementName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into UnitMeasurements 
(UserID,RegisterTime,UnitMeasurementName) values (@UserID,@RegisterTime,@UnitMeasurementName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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
    public Types.ProsesType UnitMeasurementUpdate(int UnitMeasurementID, string RegisterTime, string UnitMeasurementName)
    {
        SqlCommand cmd = new SqlCommand(@"update UnitMeasurements set UserID=@UserID,RegisterTime=@RegisterTime,
UnitMeasurementName=@UnitMeasurementName,UpdateTime=getdate() where UnitMeasurementID=@UnitMeasurementID", SqlConn);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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






    public DataTable GetGardens()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by GardenID desc) sn,
GardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementName,Address,Notes 
from Gardens g inner join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID 
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
GardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementName,Address,Notes 
from Gardens g inner join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID 
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














    public DataTable GetZones()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ZoneID desc) sn,
ZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.UnitMeasurementID,u.UnitMeasurementName,z.Notes
FROM Zones z inner join Gardens g on g.GardenID=z.GardenID 
inner join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
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
inner join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
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
    public Types.ProsesType ZoneInsert(string RegisterTime,int GardenID, string ZoneName, string ZoneArea,
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
inner join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID 
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
inner join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
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
inner join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
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
inner join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID 
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


















    public DataTable GetTreeTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeTypeID desc) sn, 
[TreeTypeID]
      ,[UserID],RegisterTime,[TreeTypeName],[InsertTime],[UpdateTime],[DeleteTime]
  FROM [TreeTypes] where DeleteTime is null", SqlConn);
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
[TreeTypeID]
      ,[UserID],RegisterTime,[TreeTypeName],[InsertTime],[UpdateTime],[DeleteTime]
  FROM [TreeTypes] where DeleteTime is null and TreeTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType TreeTypeInsert(string RegisterTime, string TreeTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TreeTypes 
(UserID,RegisterTime,TreeTypeName) values (@UserID,@RegisterTime,@TreeTypeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);

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
    public Types.ProsesType TreeTypeUpdate(int TreeTypeID, string RegisterTime, string TreeTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"update TreeTypes set UserID=@UserID,RegisterTime=@RegisterTime,
TreeTypeName=@TreeTypeName,UpdateTime=getdate() where TreeTypeID=@TreeTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);

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



















    public DataTable GetLines()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by LineID desc) sn,
l.LineID ,l.UserID,l.RegisterTime,l.TreeTypeID,t.TreeTypeName,l.LineName,SectorName,LineArea,l.SectorID,
l.UnitMeasurementID,TreeCount,Sowingtime,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
inner join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
inner join TreeTypes t on t.TreeTypeID=l.TreeTypeID
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null 
and t.DeleteTime is null and l.DeleteTime is null", SqlConn);
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
l.LineID ,l.UserID,l.RegisterTime,l.TreeTypeID,t.TreeTypeName,l.LineName,SectorName,LineArea,l.SectorID,
l.UnitMeasurementID,TreeCount,Sowingtime,l.Notes,g.GardenID,g.GardenName,z.ZoneID,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
inner join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
inner join TreeTypes t on t.TreeTypeID=l.TreeTypeID
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null 
and t.DeleteTime is null and l.DeleteTime is null
and l.LineID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType LineInsert(string RegisterTime, string LineName, int TreeTypeID, string LineArea,
       int SectorID, int UnitMeasurementID, int TreeCount,string Sowingtime, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Lines 
(UserID,RegisterTime,LineName,TreeTypeID,LineArea,SectorID,UnitMeasurementID,TreeCount,Sowingtime,Notes) 
values (@UserID,@RegisterTime,@LineName,@TreeTypeID,@LineArea,@SectorID,@UnitMeasurementID,@TreeCount,@Sowingtime,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineName", LineName);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@LineArea", ConvertTypes.ToParseFloat(LineArea));
        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Sowingtime", ConvertTypes.ToParseDatetime(Sowingtime));
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
    public Types.ProsesType LineUpdate(int LineID, string RegisterTime, string LineName, int TreeTypeID, string LineArea,
       int SectorID, int UnitMeasurementID, int TreeCount, string Sowingtime, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Lines set UserID=@UserID,RegisterTime=@RegisterTime,
LineName=@LineName, TreeTypeID=@TreeTypeID,LineArea=@LineArea,SectorID=@SectorID,
UnitMeasurementID=@UnitMeasurementID,TreeCount=@TreeCount
,Sowingtime=@Sowingtime,Notes=@Notes,UpdateTime=getdate() where LineID=@LineID", SqlConn);

        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@LineName", LineName);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@LineArea", ConvertTypes.ToParseFloat(LineArea));
        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@Sowingtime", ConvertTypes.ToParseDatetime(Sowingtime));
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










    public DataTable GetProducts()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by ProductID desc) sn,
       [ProductID],p.[UserID],p.[RegisterTime],[ProductsName],p.[ProductTypeID],pt.ProductTypeName
      ,p.[BrandID],b.BrandName,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName
      ,[Price],[PriceDiscount],[Notes] from [Products] p 
  inner join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID 
  inner join Brands b on p.BrandID=b.BrandID
  inner join Models m on p.ModelID=m.ModelID
  inner join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
  where p.DeleteTime is null and pt.DeleteTime is null and b.DeleteTime is null and 
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
       [ProductID],p.[UserID],p.[RegisterTime],[ProductsName],p.[ProductTypeID],pt.ProductTypeName
      ,p.[BrandID],b.BrandName,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName
      ,[Price],[PriceDiscount],[Notes] from [Products] p 
  inner join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID 
  inner join Brands b on p.BrandID=b.BrandID
  inner join Models m on p.ModelID=m.ModelID
  inner join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
  where p.DeleteTime is null and pt.DeleteTime is null and b.DeleteTime is null and 
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
    public Types.ProsesType ProductInsert(string RegisterTime, string ProductsName, int ProductTypeID, 
        int BrandID,int ModelID,string Code, int UnitMeasurementID, string Price,
        string PriceDiscount, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Products 
(UserID,RegisterTime,ProductsName,ProductTypeID,BrandID,ModelID,Code,UnitMeasurementID,Price,
PriceDiscount,Notes) values (@UserID,@RegisterTime,@ProductsName,@ProductTypeID,@BrandID,@ModelID,
@Code,@UnitMeasurementID,@Price,@PriceDiscount,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ProductsName", ProductsName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@PriceDiscount", PriceDiscount);
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
    public Types.ProsesType ProductUpdate(int ProductID, string RegisterTime, string ProductsName, int ProductTypeID,
        int BrandID, int ModelID, string Code, int UnitMeasurementID, string Price,
        string PriceDiscount, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update Products set UserID=@UserID,RegisterTime=@RegisterTime,
ProductsName=@ProductsName, ProductTypeID=@ProductTypeID, BrandID=@BrandID,ModelID=@ModelID,
Code=@Code,UnitMeasurementID=@UnitMeasurementID,Price=@Price,PriceDiscount=@PriceDiscount,Notes=@Notes,UpdateTime=getdate() 
where ProductID=@ProductID", SqlConn);
        
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ProductsName", ProductsName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@PriceDiscount", PriceDiscount);
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








    public DataTable GetBrands()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [BrandID] desc) sn,
b.RegisterTime,[BrandID],[BrandName],p.ProductTypeName
  from [Brands] b inner join ProductTypes p on b.ProductTypeID=p.ProductTypeID where b.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetBrandsByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [BrandID] desc) sn,
b.RegisterTime,[BrandID],[BrandName],b.ProductTypeID,p.ProductTypeName
  from [Brands] b inner join ProductTypes p on b.ProductTypeID=p.ProductTypeID where b.DeleteTime is null
and p.DeleteTime is null and BrandID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType BrandInsert(string RegisterTime, string BrandName, int ProductTypeID)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Brands 
(UserID,RegisterTime,BrandName,ProductTypeID) values 
(@UserID,@RegisterTime,@BrandName,@ProductTypeID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@BrandName", BrandName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);

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
    public Types.ProsesType BrandUpdate(int BrandID, string RegisterTime, string BrandName, int ProductTypeID)
    {
        SqlCommand cmd = new SqlCommand(@"update Brands set UserID=@UserID,RegisterTime=@RegisterTime,
BrandName=@BrandName, ProductTypeID=@ProductTypeID,UpdateTime=getdate() 
where BrandID=@BrandID", SqlConn);

        cmd.Parameters.AddWithValue("@BrandID", BrandID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@BrandName", BrandName);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        
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
    public Types.ProsesType DeleteBrand(int id)
    {

        SqlCommand cmd = new SqlCommand(@"Update Brands set deletetime=getdate(),UserID=@UserID 
where BrandID=@BrandID;", SqlConn);
        cmd.Parameters.AddWithValue("@BrandID", id);
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








    public DataTable GetModels()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ModelID] desc) sn,
m.RegisterTime,[ModelID],m.[BrandID],b.BrandName,[ModelName] FROM [Models] m 
inner join Brands b on m.BrandID=b.BrandID where m.DeleteTime is null and b.DeleteTime is null ", SqlConn);
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
m.RegisterTime,[ModelID],m.[BrandID],b.BrandName,[ModelName] FROM [Models] m 
inner join Brands b on m.BrandID=b.BrandID where m.DeleteTime is null and b.DeleteTime is null and ModelID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ModelInsert(string RegisterTime, string ModelName, int BrandID)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Models 
(UserID,RegisterTime,ModelName,BrandID) values 
(@UserID,@RegisterTime,@ModelName,@BrandID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ModelName", ModelName);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);

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
    public Types.ProsesType ModelUpdate(int ModelID, string RegisterTime, string ModelName, int BrandID)
    {
        SqlCommand cmd = new SqlCommand(@"update Models set UserID=@UserID,RegisterTime=@RegisterTime,
ModelName=@ModelName, BrandID=@BrandID,UpdateTime=getdate() 
where ModelID=@ModelID", SqlConn);

        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@ModelName", ModelName);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);

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












    public DataTable GetProductTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ProductTypeID] desc) sn,
[ProductTypeID],RegisterTime,[UserID],[ProductTypeName],[InsertTime],[UpdateTime],[DeleteTime]
  from [ProductTypes]   where DeleteTime is null", SqlConn);
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
[ProductTypeID],RegisterTime,[UserID],[ProductTypeName],[InsertTime],[UpdateTime],[DeleteTime]
  from [ProductTypes]   where DeleteTime is null and ProductTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
   
    public Types.ProsesType ProductTypeInsert(string RegisterTime, string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into ProductTypes 
(UserID,RegisterTime,ProductTypeName) values (@UserID,@RegisterTime,@ProductTypeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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
    public Types.ProsesType ProductTypeUpdate(int ProductTypeID, string RegisterTime, string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"update ProductTypes set UserID=@UserID,RegisterTime=@RegisterTime,
ProductTypeName=@ProductTypeName,UpdateTime=getdate() where ProductTypeID=@ProductTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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











    public DataTable GetWorks()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by WorkID desc) sn,
[WorkID],w.[RegisterTime],w.[WorkTypeID],wt.WorkTypeName,[WorkName]
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
[WorkID],w.[RegisterTime],w.[WorkTypeID],wt.WorkTypeName,[WorkName]
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
    public Types.ProsesType WorkInsert(string RegisterTime, int WorkTypeID, string WorkName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Works 
(UserID,RegisterTime,WorkTypeID,WorkName) 
values (@UserID,@RegisterTime,@WorkTypeID,@WorkName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        cmd.Parameters.AddWithValue("@WorkName", WorkName);

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
    public Types.ProsesType WorkUpdate(int WorkID, string RegisterTime, int WorkTypeID, string WorkName)
    {
        SqlCommand cmd = new SqlCommand(@"update Works set UserID=@UserID,RegisterTime=@RegisterTime,
WorkTypeID=@WorkTypeID, WorkName=@WorkName,UpdateTime=getdate() where WorkID=@WorkID", SqlConn);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
        cmd.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        cmd.Parameters.AddWithValue("@WorkName", WorkName);

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

}