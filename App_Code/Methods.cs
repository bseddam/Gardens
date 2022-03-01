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
UserStatusID) values 
(@GardenID,@CadreID,@Login,@Password,@UserStatusID)", SqlConn);
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


    //Ağaclar
    public DataTable GetTreeTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"select row_number() over(order by TreeTypeID desc) sn, 
*  FROM [TreeTypes] where DeleteTime is null", SqlConn);
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
    public Types.ProsesType TreeTypeInsert(string TreeTypeName, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"insert into TreeTypes 
(UserID,TreeTypeName,Coefficient) values (@UserID,@TreeTypeName,@Coefficient)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
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
    public Types.ProsesType TreeTypeUpdate(int TreeTypeID, string TreeTypeName, string Coefficient)
    {
        SqlCommand cmd = new SqlCommand(@"update TreeTypes set UserID=@UserID,
TreeTypeName=@TreeTypeName,Coefficient=@Coefficient,UpdateTime=getdate() where TreeTypeID=@TreeTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
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
        int BrandID, int ModelID, string Code, int UnitMeasurementID, string Price,
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


    public DataTable GetBrandsByProductTypeID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [BrandID] desc) sn,
b.RegisterTime,[BrandID],[BrandName],b.ProductTypeID,p.ProductTypeName
  from [Brands] b inner join ProductTypes p on b.ProductTypeID=p.ProductTypeID where b.DeleteTime is null
and p.DeleteTime is null and b.ProductTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
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
    public Types.ProsesType BrandUpdate(int BrandID, string BrandName, int ProductTypeID)
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


    public DataTable GetProductGeneralTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() 
over(order by [ProductGeneralTypeID] desc) sn,
* FROM [ProductGeneralTypes] where DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    //Mallarin tipi
    public DataTable GetProductTypes()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [ProductTypeID] desc) sn,
[ProductTypeID],RegisterTime,[UserID],[ProductTypeName],pgt.ProductGeneralTypeID,ProductGeneralTypeName
  from [ProductTypes] pt inner join ProductGeneralTypes pgt on 
pt.ProductGeneralTypeID=pgt.ProductGeneralTypeID   where pt.DeleteTime is null and pgt.DeleteTime is null", SqlConn);
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
[ProductTypeID],RegisterTime,[UserID],[ProductTypeName],pgt.ProductGeneralTypeID,ProductGeneralTypeName
  from [ProductTypes] pt inner join ProductGeneralTypes pgt on 
pt.ProductGeneralTypeID=pgt.ProductGeneralTypeID where pt.DeleteTime is null and ProductTypeID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Types.ProsesType ProductTypeInsert(int ProductGeneralTypeID, string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"insert into ProductTypes 
(UserID,ProductGeneralTypeID,ProductTypeName) values (@UserID,@ProductGeneralTypeID,@ProductTypeName)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductGeneralTypeID", ProductGeneralTypeID);
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
    public Types.ProsesType ProductTypeUpdate(int ProductTypeID, int ProductGeneralTypeID, string ProductTypeName)
    {
        SqlCommand cmd = new SqlCommand(@"update ProductTypes set UserID=@UserID,
ProductTypeName=@ProductTypeName,ProductGeneralTypeID=@ProductGeneralTypeID,UpdateTime=getdate() where ProductTypeID=@ProductTypeID", SqlConn);
        cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductGeneralTypeID", ProductGeneralTypeID);
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
[WorkID],w.[RegisterTime],w.[WorkTypeID],wt.WorkTypeName,[WorkName],w.Price
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
[WorkID],w.[RegisterTime],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price
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
[WorkID],w.[RegisterTime],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price
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
c.CardID,c.UserID,c.CardNumber,c.InsertTime,c.Updatetime,c.DeleteTime,
g.GardenName,c.GardenID
  FROM Cards c inner join Gardens g on g.GardenID=c.GardenID where c.DeleteTime is null", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCardsByGardenID(int GardenID)
    {  //and c.cardid not in (select cardid from cadres)
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by c.CardID desc) sn,
c.CardID,c.UserID,c.CardNumber,c.InsertTime,c.Updatetime,c.DeleteTime,
g.GardenName,c.GardenID FROM Cards c inner join Gardens g on g.GardenID=c.GardenID 
where c.DeleteTime is null and c.GardenID=@GardenID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("GardenID", GardenID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCardsByID(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [CardID] desc) sn,
[CardID],[UserID],[CardNumber],[InsertTime],[Updatetime],[DeleteTime],GardenID
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
    public Types.ProsesType CardsInsert(int UserID, string CardNumber, int GardenID)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Cards (UserID, CardNumber,GardenID)  Values(@UserID, @CardNumber,@GardenID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
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
    public Types.ProsesType CardsUpdate(int CardID, int UserID, string CardNumber, int GardenID)
    {
        SqlCommand cmd = new SqlCommand(@"update Cards set UserID=@UserID, CardNumber=@CardNumber,GardenID=@GardenID where CardID=@CardID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
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


    //texniki service
    public DataTable GetTechniquesServices()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by t.TechniqueServiceID desc) sn,
t.*,g.GardenName,w.WorkName,p.ProductTypeID,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,
 m1.ModelID modeltexid,m1.ModelName modeltex,b1.BrandID brandtexid,b1.BrandName brandtex,m2.ModelID modelprodid,b2.BrandID brandprodid,
 m2.ModelName modelprod,b2.BrandName modelbrand
FROM [TechniqueService] t 
left join Techniques tc on t.TechniqueID=tc.TechniqueID
left join Models m1 on m1.ModelID=tc.ModelID
left join Brands b1 on m1.BrandID=b1.BrandID

left join Gardens g on t.GardenID=g.GardenID 
left join Works w on t.WorkID=w.WorkID
left join Products p on p.ProductID=t.ProductID
left join Models m2 on m2.ModelID=p.ModelID
left join Brands b2 on m2.BrandID=b2.BrandID

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
t.*,g.GardenName,w.WorkName,p.ProductTypeID,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,
 m1.ModelID modeltexid,m1.ModelName modeltex,b1.BrandID brandtexid,b1.BrandName brandtex,m2.ModelID modelprodid,b2.BrandID brandprodid,
 m2.ModelName modelprod,b2.BrandName modelbrand
FROM [TechniqueService] t 
left join Techniques tc on t.TechniqueID=tc.TechniqueID
left join Models m1 on m1.ModelID=tc.ModelID
left join Brands b1 on m1.BrandID=b1.BrandID

left join Gardens g on t.GardenID=g.GardenID 
left join Works w on t.WorkID=w.WorkID
left join Products p on p.ProductID=t.ProductID
left join Models m2 on m2.ModelID=p.ModelID
left join Brands b2 on m2.BrandID=b2.BrandID

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
t.TechniquesName,b.BrandName, m.ModelName, cp.CompanyName,t.Motor,t.RegisterNumber,t.SerieNumber,t.Passport,t.ProductionYear,t.Birka,ts.TechniqueSituationName,t.GPS,t.GPSLogin,t.GPSPassword,t.BoughtDate,t.Photo,t.TechniqueID,t.ModelID,t.UserID ID,t.BrandID,t.CompanyID,t.TechniqueSituationID
from Techniques t 
left join Users u on t.UserID=u.UserID
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

    public Types.ProsesType CadresInsert(int UserID, int StructureID, int PositionID, int GardenID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string JobEntryDate, string JobExitDate, int CadreTypeID, string RegstrDate)
    {

        SqlCommand cmd = new SqlCommand(@"insert into Cadres (UserID, StructureID, PositionID, GardenID, CardID, Sname, Name, FName, Gender, PassportN, PIN, PhoneNumber, Photo, Email, Address, JobEntryDate, 
JobExitDate, CadreTypeID)  Values(@UserID, @StructureID, @PositionID, @GardenID, @CardID, @Sname, @Name, @FName, @Gender, @PassportN, @PIN, @PhoneNumber, @Photo, @Email, @Address, @JobEntryDate, @JobExitDate, @CadreTypeID)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
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

    public Types.ProsesType CadresUpdate(int CadreID, int UserID, int StructureID, int PositionID, int GardenID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string JobEntryDate, string JobExitDate, int CadreTypeID, string RegstrDate)
    {
        SqlCommand cmd = new SqlCommand(@"update Cadres set UserID=@UserID, StructureID=@StructureID, PositionID=@PositionID,
GardenID=@GardenID, CardID=@CardID,Sname=@Sname, Name=@Name, FName=@FName,Gender=@Gender, PassportN=@PassportN, PIN=@PIN, 
PhoneNumber=@PhoneNumber, Photo=@Photo, Email=@Email, Address=@Address,JobEntryDate=@JobEntryDate, 
JobExitDate=@JobExitDate,CadreTypeID=@CadreTypeID where CadreID=@CadreID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@StructureID", StructureID);
        cmd.Parameters.AddWithValue("@PositionID", PositionID);
        cmd.Parameters.AddWithValue("@GardenID", GardenID);
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

    public Types.ProsesType OperationCadreUpdate(int WorkDoneID, int UserID, int CadreID, int WorkID, int LinesID, int WeatherConditionID, int TreeTypeID, int TariffAgeID, int TreeCount, float Salary, string Notes, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update WorkDone set UserID=@UserID, CadreID=@CadreID,WorkID=@WorkID,LinesID=@LinesID,WeatherConditionID=@WeatherConditionID,TreeTypeID=@TreeTypeID,TariffAgeID=@TariffAgeID,TreeCount=@TreeCount,Salary=@Salary,Notes=@Notes,RegisterTime=@RegisterTime where WorkDoneID=@WorkDoneID", SqlConn);
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
z.ZoneID,z.ZoneName,g.GardenID,g.GardenName,tc.TechniqueID,tc.TechniqueID,tc.TechniquesName+' '+Birka as TechniquesName,tc.BrandID,tc.ModelID,
l.SectorID,sc.ZoneID,z.GardenID 
from TechniquesWorkDone t
left join Companies c on c.CompanyID=t.CompanyID
left join Works s on s.WorkID=t.WorkID
left join Lines l on l.LineID=t.LineID
left join Sectors sc on sc.SectorID=l.SectorID
left join Zones z on z.ZoneID=sc.ZoneID
left join Gardens g on g.GardenID=z.GardenID 
left join Techniques tc on tc.TechniqueID=t.TechniqueID
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

    public Types.ProsesType OperationTechniqueWorkDoneInsert(int UserID, int TechniqueID, int CompanyID, int WorkID, int Odometer, int LineID, int TreeCount, int FactualCount, int OfficialCount, string Notes, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into TechniquesWorkDone (UserID,TechniqueID,CompanyID,WorkID,Odometer,LineID,TreeCount,FactualCount,OfficialCount,Notes,RegisterTime)
                                            Values(@UserID,@TechniqueID,@CompanyID,@WorkID,@Odometer,@LineID,@TreeCount,@FactualCount,@OfficialCount,@Notes,@RegisterTime)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@LineID", LineID);        
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@FactualCount", FactualCount);
        cmd.Parameters.AddWithValue("@OfficialCount", OfficialCount);
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

    public Types.ProsesType OperationTechniqueWorkDoneUpdate(int TechniquesWorkDoneID, int UserID, int TechniqueID, int CompanyID, int WorkID, int Odometer, int LineID, int TreeCount, int FactualCount, int OfficialCount, string Notes, string RegisterTime)
    {
        SqlCommand cmd = new SqlCommand(@"update TechniquesWorkDone set UserID=@UserID, TechniqueID=@TechniqueID,CompanyID=@CompanyID,WorkID=@WorkID,Odometer=@Odometer,LineID=@LineID,TreeCount=@TreeCount,FactualCount=@FactualCount,OfficialCount=@OfficialCount,Notes=@Notes,RegisterTime=@RegisterTime where TechniquesWorkDoneID=@TechniquesWorkDoneID", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        cmd.Parameters.AddWithValue("@WorkID", WorkID);
        cmd.Parameters.AddWithValue("@Odometer", Odometer);
        cmd.Parameters.AddWithValue("@LineID", LineID);
        cmd.Parameters.AddWithValue("@TreeCount", TreeCount);
        cmd.Parameters.AddWithValue("@FactualCount", FactualCount);
        cmd.Parameters.AddWithValue("@OfficialCount", OfficialCount);
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

    public Types.ProsesType OperationWateringSystemsWorkDoneInsert(int UserID, int GardenID, int WateringSystemID, string WateringSystemSize, int UnitMeasurementID, int EntryExitStatus, string Notes, string RegisterTime)
    {

        SqlCommand cmd = new SqlCommand(@"insert into WateringSystemWorkDone (UserID,GardenID,WateringSystemID,WateringSystemSize,UnitMeasurementID,EntryExitStatus,Notes,RegisterTime)
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
        SqlCommand cmd = new SqlCommand(@"update WateringSystemWorkDone set UserID=@UserID, GardenID=@GardenID,WateringSystemID=@WateringSystemID,WateringSystemSize=@WateringSystemSize,UnitMeasurementID=@UnitMeasurementID,EntryExitStatus=@EntryExitStatus,Notes=@Notes,RegisterTime=@RegisterTime where WateringSystemWorkID=@WateringSystemWorkID", SqlConn);
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
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT row_number() over(order by [EntryExitID] desc) sn,
e.*,c2.CardNumber,c1.Sname+' '+c1.Name+' '+c1.FName fullname,case when e.EntryExitStatus =1 then N'Giriş' 
when e.EntryExitStatus =2 then N'Çıxış' else N'Xəta baş verdi!' end EntryExitName from [EntryExit] e 
left join Cadres c1 on e.CadreID=c1.CadreID 
left join Cards c2 on c2.CardID=c1.CardID  where e.DeleteTime is null and c1.DeleteTime is null  and c2.DeleteTime is null ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
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
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT 
row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,
sor.ReasonName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName,b.BrandID,b.BrandName FROM [ProductStockInputOutput] psio 
inner join ProductOperationTypes pot on psio.ProductOperationTypeID=pot.ProductOperationTypeID
inner join StockOperationReasons sor on psio.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID
inner join Products p on psio.ProductID=p.ProductID
inner join Models m on m.ModelID=p.ModelID
inner join brands b on b.BrandID=m.BrandID
inner join UnitMeasurements um on psio.UnitMeasurementID=um.UnitMeasurementID
inner join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID
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
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT 
row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,
sor.ReasonName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName,b.BrandID,b.BrandName FROM [ProductStockInputOutput] psio 
inner join ProductOperationTypes pot on psio.ProductOperationTypeID=pot.ProductOperationTypeID
inner join StockOperationReasons sor on psio.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID
inner join Products p on psio.ProductID=p.ProductID
inner join Models m on m.ModelID=p.ModelID
inner join brands b on b.BrandID=m.BrandID
inner join UnitMeasurements um on psio.UnitMeasurementID=um.UnitMeasurementID
inner join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID
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


    public Types.ProsesType ProductStockInputOutputInsert(int ProductOperationTypeID,
        int StockOperationReasonID, int ProductID, int UnitMeasurementID, string ProductSize,
        string Price, string PriceDiscount, string Amount, string AmountDiscount, string RegisterTime, string Notes)
    {

        SqlCommand cmd = new SqlCommand(@"insert into ProductStockInputOutput 
(UserID,ProductOperationTypeID,StockOperationReasonID,ProductID,UnitMeasurementID,
ProductSize,Price,PriceDiscount,Amount,AmountDiscount,RegisterTime,Notes) values 
(@UserID,@ProductOperationTypeID,@StockOperationReasonID,@ProductID,
@UnitMeasurementID,@ProductSize,@Price,@PriceDiscount,@Amount,@AmountDiscount,@RegisterTime,@Notes)", SqlConn);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        cmd.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@ProductSize", ConvertTypes.ToParseFloat(ProductSize));
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@PriceDiscount", ConvertTypes.ToParseFloat(PriceDiscount));
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@AmountDiscount", ConvertTypes.ToParseFloat(AmountDiscount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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



    public Types.ProsesType ProductStockInputOutputUpdate(int ProductStockInputOutputID,
        int ProductOperationTypeID, int StockOperationReasonID, int ProductID, int UnitMeasurementID,
        string ProductSize, string Price, string PriceDiscount, string Amount,
        string AmountDiscount, string RegisterTime, string Notes)
    {
        SqlCommand cmd = new SqlCommand(@"update ProductStockInputOutput set UserID=@UserID,
ProductOperationTypeID=@ProductOperationTypeID,StockOperationReasonID=@StockOperationReasonID,
ProductID=@ProductID,UnitMeasurementID=@UnitMeasurementID,ProductSize=@ProductSize,
Price=@Price,PriceDiscount=@PriceDiscount,Amount=@Amount,AmountDiscount=@AmountDiscount,
RegisterTime=@RegisterTime,Notes=@Notes,UpdateTime=getdate() where ProductStockInputOutputID=@ProductStockInputOutputID", SqlConn);
        cmd.Parameters.AddWithValue("@ProductStockInputOutputID", ProductStockInputOutputID);
        cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        cmd.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        cmd.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        cmd.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        cmd.Parameters.AddWithValue("@ProductSize", ConvertTypes.ToParseFloat(ProductSize));
        cmd.Parameters.AddWithValue("@Price", ConvertTypes.ToParseFloat(Price));
        cmd.Parameters.AddWithValue("@PriceDiscount", ConvertTypes.ToParseFloat(PriceDiscount));
        cmd.Parameters.AddWithValue("@Amount", ConvertTypes.ToParseFloat(Amount));
        cmd.Parameters.AddWithValue("@AmountDiscount", ConvertTypes.ToParseFloat(AmountDiscount));
        cmd.Parameters.AddWithValue("@RegisterTime", ConvertTypes.ToParseDatetime(RegisterTime));
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













    public DataTable GetProductByModelId(int id)
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
m.DeleteTime is null and u.DeleteTime is null and m.ModelID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
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
 p.* from Techniques p

  inner join Brands b on p.BrandID=b.BrandID
  inner join Models m on p.ModelID=m.ModelID

  where p.DeleteTime is null  and b.DeleteTime is null and 
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





}