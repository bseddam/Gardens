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

    //Ölçü vahidi
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
l.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,l.SectorID,
l.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName
FROM Lines l 
inner join Sectors s on l.SectorID=s.SectorID
inner join Zones z on s.ZoneID=z.ZoneID
inner join Gardens g on g.GardenID=z.GardenID 
left join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID 
where s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and l.DeleteTime is null and l.LineID=@id", SqlConn);
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


    //Ağaclar
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
    public Types.ProsesType TreeTypeInsert( string TreeTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TreeTypes 
(UserID,TreeTypeName) values (@UserID,@TreeTypeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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
    public Types.ProsesType TreeTypeUpdate(int TreeTypeID, string TreeTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"update TreeTypes set UserID=@UserID,
TreeTypeName=@TreeTypeName,UpdateTime=getdate() where TreeTypeID=@TreeTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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

          
//Mallar
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
  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
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
  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID 
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



//Markalar
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
    public Types.ProsesType BrandInsert(string BrandName, int ProductTypeID)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Brands 
(UserID,BrandName,ProductTypeID) values 
(@UserID,@BrandName,@ProductTypeID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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
    public Types.ProsesType BrandUpdate(int BrandID,  string BrandName, int ProductTypeID)
    {
        SqlCommand cmd = new SqlCommand(@"update Brands set UserID=@UserID,
BrandName=@BrandName, ProductTypeID=@ProductTypeID,UpdateTime=getdate() 
where BrandID=@BrandID", SqlConn);

        cmd.Parameters.AddWithValue("@BrandID", BrandID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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

    
    //Madeller
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
    public DataTable GetModelsByBrandID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ModelID] desc) sn,
m.RegisterTime,[ModelID],m.[BrandID],b.BrandName,[ModelName] FROM [Models] m 
inner join Brands b on m.BrandID=b.BrandID where m.DeleteTime is null and b.DeleteTime is null and m.BrandID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
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
    public Types.ProsesType ModelInsert(string ModelName, int BrandID)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Models 
(UserID,ModelName,BrandID) values 
(@UserID,@ModelName,@BrandID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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
    public Types.ProsesType ModelUpdate(int ModelID, string ModelName, int BrandID)
    {
        SqlCommand cmd = new SqlCommand(@"update Models set UserID=@UserID,
ModelName=@ModelName, BrandID=@BrandID,UpdateTime=getdate() 
where ModelID=@ModelID", SqlConn);

        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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


    //Mallarin tipi
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
    public Types.ProsesType WorkInsert( int WorkTypeID, string WorkName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into Works 
(UserID,WorkTypeID,WorkName) 
values (@UserID,@WorkTypeID,@WorkName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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
    public Types.ProsesType WorkUpdate(int WorkID, int WorkTypeID, string WorkName)
    {
        SqlCommand cmd = new SqlCommand(@"update Works set UserID=@UserID,
WorkTypeID=@WorkTypeID, WorkName=@WorkName,UpdateTime=getdate() where WorkID=@WorkID", SqlConn);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
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
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [CardID] desc) sn,
[CardID],[UserID],[CardNumber],[InsertTime],[Updatetime],[DeleteTime]
  FROM [Cards] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
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
[TechniqueSituationID]
      ,[TechniqueSituationName]
  FROM [TechniqueSituation]
where DeleteTime is null", SqlConn);
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
    public Types.ProsesType TechniqueInsert(int UserID, int BrandID, int ModelID, string RegisterNumber, string SerieNumber, int Motor, int CompanyID, int TechniqueSituationID, int GPS, string GPSLogin, string GPSPassword, int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, string BoughtDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Techniques (UserID,  BrandID,  ModelID,  RegisterNumber,  SerieNumber,  Motor,  CompanyID,  TechniqueSituationID,  GPS, GPSLogin, GPSPassword,  ProductionYear,  Photo,  Birka,  TechniquesName,  Passport,  BoughtDate)    Values( @UserID,  @BrandID,  @ModelID,  @RegisterNumber,  @SerieNumber,  @Motor,  @CompanyID,  @TechniqueSituationID,  @GPS, @GPSLogin, @GPSPassword, @ProductionYear,  @Photourl,  @Birka,  @TechniquesName,  @Passport,  @BoughtDate)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
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

    public Types.ProsesType TechniqueUpdate(int TechniqueID, int UserID, int BrandID, int ModelID, string RegisterNumber, string SerieNumber, int Motor, int CompanyID, int TechniqueSituationID, int GPS, string GPSLogin, string GPSPassword, int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, string BoughtDate)
    {
        SqlCommand cmd = new SqlCommand(@"update Techniques set UserID=@UserID, BrandID=@BrandID, ModelID=@ModelID,RegisterNumber=@RegisterNumber, SerieNumber=@SerieNumber, Motor=@Motor,CompanyID=@CompanyID, TechniqueSituationID=@TechniqueSituationID, GPS=@GPS, GPSLogin=@GPSLogin, GPSPassword=@GPSPassword, ProductionYear=@ProductionYear, Photo=@Photourl, Birka=@Birka,TechniquesName=@TechniquesName, Passport=@Passport,BoughtDate=@BoughtDate where TechniqueID=@TechniqueID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
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

    public DataTable GetTechnique()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() 
over(order by t.TechniqueID desc) sn,c.Sname+' '+c.Name UsingUsers,
t.TechniquesName,b.BrandName, m.ModelName, cp.CompanyName,t.Motor,t.RegisterNumber,t.SerieNumber,t.Passport,t.ProductionYear,t.Birka,ts.TechniqueSituationName,t.GPS,t.GPSLogin,t.GPSPassword,t.BoughtDate,t.Photo,t.TechniqueID,t.ModelID,t.UserID ID,t.BrandID,t.CompanyID,t.TechniqueSituationID
from Techniques t 
left join Users u on t.UserID=u.UsersID
left join Cadres c on u.CadreID=c.CadreID
left join Brands b on b.BrandID=t.BrandID
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
            SqlDataAdapter da = new SqlDataAdapter(@"select  row_number() over (order by w.WateringSystemID desc) sn,b.BrandName, m.ModelName,w.WateringSystemName, 
w.Notes,ts.TechniqueSituationName, w.RegisterTime,w.WateringSystemID,w.ModelID,w.UserID,w.BrandID,w.WateringSystemID
from WateringSystems w left join Brands b on b.BrandID=w.BrandID left join Models m on m.ModelID=w.ModelID
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

    public Types.ProsesType WateringSystemsInsert(int UserID, int BrandID, int ModelID, 
        string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into WateringSystems 
(UserID, BrandID, ModelID, WateringSystemName, Notes, TechniqueSituationID, RegisterTime) 
Values (@UserID, @BrandID, @ModelID, @WateringSystemName, @Notes, @TechniqueSituationID, @RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
        cmd.Parameters.AddWithValue("@ModelID", ModelID);
        cmd.Parameters.AddWithValue("@WateringSystemName", WateringSystemName);
        cmd.Parameters.AddWithValue("@Notes", Notes);
        cmd.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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

    public Types.ProsesType WateringSystemsUpdate(int WateringSystemID, int UserID, int BrandID, int ModelID,
        string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update WateringSystems set 
UserID=@UserID, BrandID=@BrandID, ModelID=@ModelID, WateringSystemName=@WateringSystemName, 
Notes=@Notes, TechniqueSituationID=@TechniqueSituationID, RegisterTime=@RegisterTime 
where WateringSystemID=@WateringSystemID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@BrandID", BrandID);
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

    public Types.ProsesType CadresInsert(int UserID, int StructureID, int PositionID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string JobEntryDate, string JobExitDate, int CadreTypeID, string RegstrDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Cadres (UserID, StructureID, PositionID, CardID, Sname, Name, FName, Gender, PassportN, PIN, PhoneNumber, Photo, Email, Address, JobEntryDate, 
JobExitDate, CadreTypeID)  Values(@UserID, @StructureID, @PositionID, @CardID, @Sname, @Name, @FName, @Gender, @PassportN, @PIN, @PhoneNumber, @Photo, @Email, @Address, @JobEntryDate, @JobExitDate, @CadreTypeID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
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
        cmd.Parameters.AddWithValue("@JobEntryDate", ConvertTypes.ToParseDatetime(JobEntryDate));
        cmd.Parameters.AddWithValue("@JobExitDate", ConvertTypes.ToParseDatetime(JobExitDate));
        cmd.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
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

    public Types.ProsesType CadresUpdate(int CadreID, int UserID, int StructureID, int PositionID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string JobEntryDate, string JobExitDate, int CadreTypeID, string RegstrDate)
    {
        SqlCommand cmd = new SqlCommand(@"update Cadres set UserID=@UserID, StructureID=@StructureID, PositionID=@PositionID,
CardID=@CardID,Sname=@Sname, Name=@Name, FName=@FName,Gender=@Gender, PassportN=@PassportN, PIN=@PIN, 
PhoneNumber=@PhoneNumber, Photo=@Photo, Email=@Email, Address=@Address,JobEntryDate=@JobEntryDate, 
JobExitDate=@JobExitDate,CadreTypeID=@CadreTypeID where CadreID=@CadreID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
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
        cmd.Parameters.AddWithValue("@JobEntryDate", ConvertTypes.ToParseDatetime(JobEntryDate));
        cmd.Parameters.AddWithValue("@JobExitDate", ConvertTypes.ToParseDatetime(JobExitDate));
        cmd.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
        cmd.Parameters.AddWithValue("@CadreID", CadreID);
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
        cmd.Connection.Close();
        cmd.Dispose();
        //}
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
            SqlDataAdapter da = new SqlDataAdapter(@"Select ROW_NUMBER() over(order by c.CadreID desc) sn, s.StructureName,p.PositionName, k.CardNumber, 
c.Sname, c.Name, c.FName, g.GenderName, c.PassportN, c.PIN, c.[Address], c.PhoneNumber,c.Email,case when c.Photo='' then 'avatar.png' else c.Photo end Photo,c.JobEntryDate,c.JobExitDate,
j.CadreTypeName,c.RegisterTime, c.CadreID, c.UserID, c.StructureID, c.PositionID, c.CardID, c.Gender, c.CadreTypeID,c.Salary,isnull(s.StructureName,'') +' '+isnull(p.PositionName,'')+' '+ isnull(k.CardNumber,'')+' '+ 
c.Sname+' '+ c.Name+' '+ c.FName NameDDL
from Cadres c 
left join Structure s on c.StructureID=s.StructureID
left join Positions p on c.PositionID=p.PositionID
left join Cards k on c.CardID=k.CardID 
left join Genders g on g.GenderID=c.Gender
left join CadreType j on j.CadreTypeID=c.CadreTypeID where c.DeleteTime is null", SqlConn);
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
c.Name+' '+c.Sname+' '+c.FName fullname, s.WorkName,
g.GardenName,z.ZonaName,sc.SectorName,l.LineName,w.TreeCount,
w.Notes,w.RegstrTime,w.WorkDoneID,w.UserID,w.LinesID,w.CadreID,
g.GardenID,z.ZoneID,sc.SectorsID
from WorkDone w
left join Cadres c on c.CadreID=w.CadreID
left join Works s on s.WorkID=w.WorkID
left join Lines l on l.LineID=w.LinesID
left join Sectors sc on sc.SectorsID=l.SektorID
left join Zones z on z.ZoneID=sc.ZonaID
left join Gardens g on g.GardenID=z.GardenID where w.DeleteTime is null ", SqlConn);
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


    //-------Texnikalar uzre emeliyyat

    public DataTable GetOperationTechniques()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select ROW_NUMBER() over( order by w.TechniquesWorkDoneID) sn,
t.TechniquesName, s.WorkName,g.GardenName,z.ZonaName,sc.SectorName,
l.LineName,w.Odometer,w.FactualCount,w.OfficialCount,
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
wt.WateringSystemName, g.GardenName,Ws.StatusExitEntry, w.WateringSystemSize,um.UnitMeasurementName,w.Note,w.RegstrTime,w.WateringSystemWorkID,
w.UserID,w.UnitMeasurementID,w.WateringSystemGardenID,g.GardenID
from WateringSystemWork w
left join WateringSystemGarden s on s.WateringSystemGardenID=w.WateringSystemGardenID
left join Gardens g on g.GardenID=s.GardenID
left join WateringSystems wt on wt.WateringSystemID=s.WateringSystemID
left join WateringSystemStatus Ws on Ws.StatusID=W.EntryExitStatus
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
        SqlCommand cmd = new SqlCommand(@"Update WateringSystemWork set DeleteTime=GetDate() where WateringSystemWorkID=@id ", SqlConn);
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
            SqlDataAdapter da = new SqlDataAdapter(@"select * from WateringSystemWork where WateringSystemWorkID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }





}