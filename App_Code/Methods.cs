using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class Methods
{
    public static SqlConnection SqlConn = new SqlConnection(@"Data Source = SQL5109.site4now.net; Initial Catalog = db_a85441_garden; User Id = db_a85441_garden_admin; Password = @Garden123");

    public DataTable User(string Login, string Pass)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Users where Login=@Login \r\nand Password=@Password and DeleteTime is null", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Login", Login);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", Pass);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetUserByid(int userid)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Users where UserID=@userid", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@userid", userid);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetUsers()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT  row_number() \r\nover(order by u.UserID desc) sn,c.Sname+' '+c.Name+' '+c.FName fullname,\r\nus.UserStatusName,g.GardenName,u.* FROM [Users] u\r\ninner join UserStatus us on u.UserStatusID=us.UserStatusID\r\nleft join Gardens g on g.GardenID=u.GardenID\r\nleft join Cadres c on u.cadreid=c.cadreid where u.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType UserInsert(int GardenID, int CadreID, string Login, string Password, int UserStatusID)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Users (GardenID,CadreID,Login,Password,\r\nUserStatusID) values (@GardenID,@CadreID,@Login,@Password,@UserStatusID)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@Login", Login);
        sqlCommand.Parameters.AddWithValue("@Password", Password);
        sqlCommand.Parameters.AddWithValue("@UserStatusID", UserStatusID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType UserUpdate(int id, int GardenID, int CadreID, string Login, string Password, int UserStatusID)
    {
        SqlCommand sqlCommand = new SqlCommand("update Users set \r\nGardenID=@GardenID, CadreID=@CadreID, Login=@Login,Password=@Password,UserStatusID=@UserStatusID  where UserID=@id", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@Login", Login);
        sqlCommand.Parameters.AddWithValue("@Password", Password);
        sqlCommand.Parameters.AddWithValue("@UserStatusID", UserStatusID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteUser(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Users set DeleteTime=GetDate() where UserID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetUsersPermissions()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select m2.SiteMapID,m1.MenuName+' '+m2.MenuName name from (select * from SiteMap s where s.menusubid is null) m1 inner join \r\n(select * from SiteMap s1 where s1.menusubid is not null) m2 on m1.SiteMapID=m2.MenuSubID", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType UserInsertPermissions(int UserID, string[] SiteMapID)
    {
        SqlCommand sqlCommand = new SqlCommand("Delete from PermissionUser Where UserID=@UserID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }

        if (SiteMapID != null && SiteMapID.Length > 1)
        {
            foreach (string value in SiteMapID)
            {
                SqlCommand sqlCommand2 = new SqlCommand("insert into PermissionUser (UserID,SiteMapID) \r\nvalues (@UserID,@SiteMapID)", SqlConn);
                sqlCommand2.Parameters.AddWithValue("@UserID", UserID);
                sqlCommand2.Parameters.AddWithValue("@SiteMapID", value.ToParseInt());
                try
                {
                    sqlCommand2.Connection.Open();
                    sqlCommand2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return Types.ProsesType.Error;
                }
                finally
                {
                    sqlCommand2.Connection.Close();
                    sqlCommand2.Dispose();
                }
            }
        }

        return Types.ProsesType.Succes;
    }

    public DataTable GetCompanies()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by CompanyID desc) sn,\r\nCompanyID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes from Companies c \r\nwhere DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCompanyById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by CompanyID desc) sn,\r\nCompanyID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes from \r\nCompanies c where DeleteTime is null and CompanyID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType CompaniesInsert(string RegisterTime, string CompanyName, string CompanyVoen, string BankAccount, string PhoneNumbers, string Email, string Adress, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Companies \r\n(UserID,RegisterTime,CompanyName,CompanyVoen,BankAccount,PhoneNumbers,Email,Adress,Notes) \r\nvalues (@UserID,@RegisterTime,@CompanyName,@CompanyVoen,@BankAccount,@PhoneNumbers,@Email,@Adress,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@CompanyName", CompanyName);
        sqlCommand.Parameters.AddWithValue("@CompanyVoen", CompanyVoen);
        sqlCommand.Parameters.AddWithValue("@BankAccount", BankAccount);
        sqlCommand.Parameters.AddWithValue("@PhoneNumbers", PhoneNumbers);
        sqlCommand.Parameters.AddWithValue("@Email", Email);
        sqlCommand.Parameters.AddWithValue("@Adress", Adress);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType CompanyUpdate(int CompanyID, string RegisterTime, string CompanyName, string CompanyVoen, string BankAccount, string PhoneNumbers, string Email, string Adress, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Companies set UserID=@UserID,RegisterTime=@RegisterTime,\r\nCompanyName=@CompanyName, CompanyVoen=@CompanyVoen, BankAccount=@BankAccount,\r\nPhoneNumbers=@PhoneNumbers, Email=@Email, Adress=@Adress,Notes=@Notes,UpdateTime=getdate() where CompanyID=@CompanyID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@CompanyName", CompanyName);
        sqlCommand.Parameters.AddWithValue("@CompanyVoen", CompanyVoen);
        sqlCommand.Parameters.AddWithValue("@BankAccount", BankAccount);
        sqlCommand.Parameters.AddWithValue("@PhoneNumbers", PhoneNumbers);
        sqlCommand.Parameters.AddWithValue("@Email", Email);
        sqlCommand.Parameters.AddWithValue("@Adress", Adress);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteCompany(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Companies set deletetime=getdate(),UserID=@UserID where CompanyID=@CompanyID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@CompanyID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetCountries()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by CountryID desc) sn,\r\n* FROM Countries where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCountryByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by CountryID desc) sn,\r\n* FROM Countries where DeleteTime is null and CountryID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType CountryInsert(string CountryName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Countries \r\n(UserID,CountryName) values (@UserID,@CountryName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CountryName", CountryName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType CountryUpdate(int CountryID, string CountryName)
    {
        SqlCommand sqlCommand = new SqlCommand("update Countries set UserID=@UserID,\r\nCountryName=@CountryName,UpdateTime=getdate() where CountryID=@CountryID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CountryName", CountryName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteCountry(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Countries set deletetime=getdate(),\r\nUserID=@UserID where CountryID=@CountryID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@CountryID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetUnitMeasurements()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by UnitMeasurementID desc) sn,\r\n* FROM UnitMeasurements where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetUnitMeasurementByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by UnitMeasurementID desc) sn,\r\n* FROM UnitMeasurements where DeleteTime is null  and UnitMeasurementID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType UnitMeasurementInsert(string UnitMeasurementName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into UnitMeasurements \r\n(UserID,UnitMeasurementName) values (@UserID,@UnitMeasurementName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementName", UnitMeasurementName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType UnitMeasurementUpdate(int UnitMeasurementID, string UnitMeasurementName)
    {
        SqlCommand sqlCommand = new SqlCommand("update UnitMeasurements set UserID=@UserID,\r\nUnitMeasurementName=@UnitMeasurementName,UpdateTime=getdate() where UnitMeasurementID=@UnitMeasurementID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementName", UnitMeasurementName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteUnitMeasurement(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update UnitMeasurements set deletetime=getdate(),\r\nUserID=@UserID where UnitMeasurementID=@UnitMeasurementID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetGardens()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by GardenID desc) sn,\r\nGardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementName,Address,Notes \r\nfrom Gardens g left join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID \r\nwhere  g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetGardenById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by GardenID desc) sn,\r\nGardenID,g.RegisterTime,GardenName,GardenArea,u.UnitMeasurementID,u.UnitMeasurementName,Address,Notes \r\nfrom Gardens g left join UnitMeasurements u on g.UnitMeasurementID=u.UnitMeasurementID \r\nwhere  g.DeleteTime is null and u.DeleteTime is null and GardenID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType GardensInsert(string RegisterTime, string GardenName, string GardenArea, int UnitMeasurementID, string Address, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Gardens \r\n(UserID,RegisterTime,GardenName,GardenArea,UnitMeasurementID,Address,Notes) \r\nvalues (@UserID,@RegisterTime,@GardenName,@GardenArea,@UnitMeasurementID,@Address,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@GardenName", GardenName);
        sqlCommand.Parameters.AddWithValue("@GardenArea", GardenArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Address", Address);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType GardensUpdate(int GardenID, string RegisterTime, string GardenName, string GardenArea, int UnitMeasurementID, string Address, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Gardens set UserID=@UserID,RegisterTime=@RegisterTime,\r\nGardenName=@GardenName, GardenArea=@GardenArea, UnitMeasurementID=@UnitMeasurementID,\r\nAddress=@Address,Notes=@Notes,UpdateTime=getdate() where GardenID=@GardenID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@GardenName", GardenName);
        sqlCommand.Parameters.AddWithValue("@GardenArea", GardenArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Address", Address);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteGarden(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Gardens set deletetime=getdate(),UserID=@UserID where GardenID=@GardenID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@GardenID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetZones()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ZoneID desc) sn,\r\nZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.UnitMeasurementID,u.UnitMeasurementName,z.Notes\r\nFROM Zones z inner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID \r\nwhere z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetZoneById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ZoneID desc) sn,\r\nZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.GardenID,z.UnitMeasurementID,u.UnitMeasurementName,\r\nz.Notes from Zones z  inner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID \r\nwhere z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and z.ZoneID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ZoneInsert(string RegisterTime, int GardenID, string ZoneName, string ZoneArea, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Zones \r\n(UserID,RegisterTime,GardenID,ZoneName,ZoneArea,UnitMeasurementID,Notes) \r\nvalues (@UserID,@RegisterTime,@GardenID,@ZoneName,@ZoneArea,@UnitMeasurementID,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@ZoneName", ZoneName);
        sqlCommand.Parameters.AddWithValue("@ZoneArea", ZoneArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType ZoneUpdate(int ZoneID, string RegisterTime, int GardenID, string ZoneName, string ZoneArea, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Zones set UserID=@UserID,RegisterTime=@RegisterTime,\r\nGardenID=@GardenID, ZoneName=@ZoneName, ZoneArea=@ZoneArea,\r\nUnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() where ZoneID=@ZoneID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ZoneID", ZoneID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@ZoneName", ZoneName);
        sqlCommand.Parameters.AddWithValue("@ZoneArea", ZoneArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteZone(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Zones set deletetime=getdate(),UserID=@UserID where ZoneID=@ZoneID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ZoneID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetZonesByGardenID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ZoneID desc) sn,\r\nZoneID,z.UserID,z.RegisterTime,g.GardenName,ZoneName,ZoneArea,z.UnitMeasurementID,u.UnitMeasurementName,\r\nz.Notes,g.GardenID FROM Zones z inner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on z.UnitMeasurementID=u.UnitMeasurementID \r\nwhere z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and g.GardenID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetSectors()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by SectorID desc) sn,\r\ns.SectorID\r\n      ,s.UserID\r\n      ,s.RegisterTime\r\n      ,SectorName\r\n      ,s.ZoneID\r\n      ,SectorArea\r\n      ,s.UnitMeasurementID\r\n      ,s.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName\r\nFROM Sectors s \r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetSectorById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by SectorID desc) sn,\r\ns.SectorID,s.UserID,s.RegisterTime,SectorName,s.ZoneID,SectorArea,s.UnitMeasurementID,s.Notes,g.GardenID,\r\ng.GardenName,ZoneName,u.UnitMeasurementName FROM Sectors s \r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null \r\nand s.SectorID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType SectorInsert(string RegisterTime, int ZoneID, string SectorName, string SectorArea, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Sectors \r\n(UserID,RegisterTime,ZoneID,SectorName,SectorArea,UnitMeasurementID,Notes) \r\nvalues (@UserID,@RegisterTime,@ZoneID,@SectorName,@SectorArea,@UnitMeasurementID,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@ZoneID", ZoneID);
        sqlCommand.Parameters.AddWithValue("@SectorName", SectorName);
        sqlCommand.Parameters.AddWithValue("@SectorArea", SectorArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType SectorUpdate(int SectorID, string RegisterTime, int ZoneID, string SectorName, string SectorArea, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Sectors set UserID=@UserID,RegisterTime=@RegisterTime,\r\nZoneID=@ZoneID, SectorName=@SectorName, SectorArea=@SectorArea,\r\nUnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() where SectorID=@SectorID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@SectorID", SectorID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@ZoneID", ZoneID);
        sqlCommand.Parameters.AddWithValue("@SectorName", SectorName);
        sqlCommand.Parameters.AddWithValue("@SectorArea", SectorArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteSector(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Sectors set deletetime=getdate(),UserID=@UserID where SectorID=@SectorID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@SectorID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetSectorsByZoneID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by SectorID desc) sn,\r\ns.SectorID\r\n      ,s.UserID\r\n      ,s.RegisterTime\r\n      ,SectorName\r\n      ,s.ZoneID\r\n      ,SectorArea\r\n      ,s.UnitMeasurementID\r\n      ,s.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName\r\nFROM Sectors s \r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on s.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null\r\nand z.ZoneID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetLines()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by LineID desc) sn,\r\nl.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,l.SectorID,\r\nl.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName\r\nFROM Lines l \r\ninner join Sectors s on l.SectorID=s.SectorID\r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and l.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetLineById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by LineID desc) sn,\r\nl.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,z.zoneid,l.SectorID,\r\nl.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName\r\nFROM Lines l \r\ninner join Sectors s on l.SectorID=s.SectorID\r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null \r\nand u.DeleteTime is null and l.DeleteTime is null and l.LineID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetLineBySectorID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by LineID desc) sn,\r\nl.LineID ,l.UserID,l.RegisterTime,l.LineName,SectorName,LineArea,l.SectorID,\r\nl.UnitMeasurementID,TreeCount,l.Notes,g.GardenID,g.GardenName,ZoneName,u.UnitMeasurementName\r\nFROM Lines l \r\ninner join Sectors s on l.SectorID=s.SectorID\r\ninner join Zones z on s.ZoneID=z.ZoneID\r\ninner join Gardens g on g.GardenID=z.GardenID \r\nleft join UnitMeasurements u on l.UnitMeasurementID=u.UnitMeasurementID \r\nwhere s.DeleteTime is null and z.DeleteTime is null and g.DeleteTime is null and u.DeleteTime is null and l.DeleteTime is null and s.SectorID=@ID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ID", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType LineInsert(string RegisterTime, string LineName, string LineArea, int SectorID, int UnitMeasurementID, int TreeCount, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Lines \r\n(UserID,RegisterTime,LineName,LineArea,SectorID,UnitMeasurementID,TreeCount,Notes) \r\nvalues (@UserID,@RegisterTime,@LineName,@LineArea,@SectorID,@UnitMeasurementID,@TreeCount,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineName", LineName);
        sqlCommand.Parameters.AddWithValue("@LineArea", LineArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@SectorID", SectorID);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType LineUpdate(int LineID, string RegisterTime, string LineName, string LineArea, int SectorID, int UnitMeasurementID, int TreeCount, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Lines set UserID=@UserID,RegisterTime=@RegisterTime,\r\nLineName=@LineName,LineArea=@LineArea,SectorID=@SectorID,\r\nUnitMeasurementID=@UnitMeasurementID,TreeCount=@TreeCount\r\n, Notes=@Notes,UpdateTime=getdate() where LineID=@LineID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineName", LineName);
        sqlCommand.Parameters.AddWithValue("@LineArea", LineArea.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@SectorID", SectorID);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteLine(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Lines set deletetime=getdate(),UserID=@UserID \r\nwhere LineID=@LineID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@LineID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTreeSitiuations()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by TreesSitiuationID desc) sn, \r\n * FROM [TreesSitiuation] t where t.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTrees()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by t.TreeID desc) sn, \r\nt.*  FROM [Trees] t where t.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTreesByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by TreeID desc) sn, \r\n* FROM [Trees] where DeleteTime is null and TreeID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType TreeInsert(string TreeName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Trees (UserID,TreeName) values \r\n(@UserID,@TreeName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TreeName", TreeName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TreeUpdate(int TreeID, string TreeName)
    {
        SqlCommand sqlCommand = new SqlCommand("update Trees set UserID=@UserID,\r\nTreeName=@TreeName,UpdateTime=getdate() where TreeID=@TreeID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TreeID", TreeID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TreeName", TreeName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteTree(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Trees set deletetime=getdate(),UserID=@UserID \r\nwhere TreeID=@TreeID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TreeID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTreeTypeByTreeCountryID(int TreeID, int CountryID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nselect row_number() over(order by TreeTypeID desc) sn, \r\ntp.*,c.CountryName,t.TreeName  FROM [TreeTypes] tp left join Countries c on tp.CountryID=c.CountryID\r\nleft join Trees t on tp.TreeID=t.TreeID where  tp.DeleteTime is null and tp.TreeID=@TreeID \r\nand c.CountryID=@CountryID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("TreeID", TreeID);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("CountryID", CountryID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTreeTypes()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by TreeTypeID desc) sn, \r\ntp.*,c.CountryName,t.TreeName  FROM [TreeTypes] tp left join Countries c on tp.CountryID=c.CountryID\r\nleft join Trees t on tp.TreeID=t.TreeID where  tp.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTreeTypesByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by TreeTypeID desc) sn, \r\n* FROM [TreeTypes] where DeleteTime is null and TreeTypeID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType TreeTypeInsert(int CountryID, int TreeID, string TreeTypeName, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into TreeTypes (UserID,CountryID,TreeID,TreeTypeName,\r\nCoefficient) values (@UserID,@CountryID,@TreeID,@TreeTypeName,@Coefficient)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
        sqlCommand.Parameters.AddWithValue("@TreeID", TreeID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TreeTypeUpdate(int TreeTypeID, int CountryID, int TreeID, string TreeTypeName, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("update TreeTypes set UserID=@UserID,CountryID=@CountryID,TreeID=@TreeID,\r\nTreeTypeName=@TreeTypeName,Coefficient=@Coefficient,UpdateTime=getdate() where TreeTypeID=@TreeTypeID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
        sqlCommand.Parameters.AddWithValue("@TreeID", TreeID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeName", TreeTypeName);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteTreeType(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update TreeTypes set deletetime=getdate(),UserID=@UserID \r\nwhere TreeTypeID=@TreeTypeID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProducts()
    {
        DataTable dataTable = new DataTable();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ProductID desc) sn,\r\n       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName\r\n      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName\r\n      ,[Notes] from [Products] p \r\n  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID \r\n \r\n  left join Models m on p.ModelID=m.ModelID\r\n  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID \r\n  where p.DeleteTime is null and pt.DeleteTime is null  and \r\nm.DeleteTime is null and u.DeleteTime is null", SqlConn);
        sqlDataAdapter.Fill(dataTable);
        return dataTable;
    }

    public DataTable GetProductById(int id)
    {
        DataTable dataTable = new DataTable();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ProductID desc) sn,\r\n       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName\r\n      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName\r\n      ,[Notes] from [Products] p \r\n  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID \r\n  left join Models m on p.ModelID=m.ModelID\r\n  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID \r\n  where p.DeleteTime is null and pt.DeleteTime is null and \r\nm.DeleteTime is null and u.DeleteTime is null and p.ProductID=@id", SqlConn);
        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
        sqlDataAdapter.Fill(dataTable);
        return dataTable;
    }

    public Types.ProsesType ProductInsert(string ProductsName, int ProductTypeID, int ModelID, string Code, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Products \r\n(UserID,ProductsName,ProductTypeID,ModelID,Code,UnitMeasurementID,\r\nNotes) values (@UserID,@ProductsName,@ProductTypeID,@ModelID,\r\n@Code,@UnitMeasurementID,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductsName", ProductsName);
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@Code", Code);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType ProductUpdate(int ProductID, string ProductsName, int ProductTypeID, int ModelID, string Code, int UnitMeasurementID, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update Products set UserID=@UserID,\r\nProductsName=@ProductsName, ProductTypeID=@ProductTypeID, ModelID=@ModelID,\r\nCode=@Code,UnitMeasurementID=@UnitMeasurementID,Notes=@Notes,UpdateTime=getdate() \r\nwhere ProductID=@ProductID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductsName", ProductsName);
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@Code", Code);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteProduct(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Products set deletetime=getdate(),UserID=@UserID \r\nwhere ProductID=@ProductID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetModels()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [ModelID] desc) sn,\r\n[ModelID],[ModelName],pt.ProductTypeName FROM [Models] m \r\nleft join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetModelByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [ModelID] desc) sn,\r\n[ModelID],[ModelName],pt.ProductTypeName,m.ProductTypeID FROM [Models] m \r\ninner join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null  and m.ModelID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ModelInsert(int ProductTypeID, string ModelName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Models \r\n(UserID,ProductTypeID,ModelName) values (@UserID,@ProductTypeID,@ModelName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        sqlCommand.Parameters.AddWithValue("@ModelName", ModelName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType ModelUpdate(int ProductTypeID, int ModelID, string ModelName)
    {
        SqlCommand sqlCommand = new SqlCommand("update Models set UserID=@UserID,ProductTypeID=@ProductTypeID,\r\nModelName=@ModelName,UpdateTime=getdate() \r\nwhere ModelID=@ModelID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        sqlCommand.Parameters.AddWithValue("@ModelName", ModelName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteModel(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Models set deletetime=getdate(),UserID=@UserID \r\nwhere ModelID=@ModelID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ModelID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetModelsByProductTypeID(int ProductTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [ModelID] desc) sn,\r\n[ModelID],[ModelName],pt.ProductTypeName FROM [Models] m \r\ninner join ProductTypes pt on m.ProductTypeID=pt.ProductTypeID  where m.DeleteTime is null and pt.ProductTypeID=@ProductTypeID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductTypeID", ProductTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductTypes()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nSELECT row_number() over(order by [ProductTypeID] desc) sn,\r\n[ProductTypeID],[UserID],[ProductTypeName]\r\n  from [ProductTypes] pt  where pt.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductTypeByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [ProductTypeID] desc) sn,\r\n[ProductTypeID],[UserID],[ProductTypeName]\r\n  from [ProductTypes] pt  where pt.DeleteTime is null and ProductTypeID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ProductTypeInsert(string ProductTypeName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into ProductTypes \r\n(UserID,ProductTypeName) values (@UserID,@ProductTypeName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductTypeName", ProductTypeName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType ProductTypeUpdate(int ProductTypeID, string ProductTypeName)
    {
        SqlCommand sqlCommand = new SqlCommand("update ProductTypes set UserID=@UserID,\r\nProductTypeName=@ProductTypeName,UpdateTime=getdate() where ProductTypeID=@ProductTypeID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@ProductTypeName", ProductTypeName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteProductType(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update ProductTypes set deletetime=getdate(),UserID=@UserID \r\nwhere ProductTypeID=@ProductTypeID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductTypeID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetWorks()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by WorkID desc) sn,\r\n[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],w.Price\r\nFROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID \r\nwhere w.DeleteTime is null and wt.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetWorkById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by WorkID desc) sn,\r\n[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price\r\nFROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID \r\nwhere w.DeleteTime is null and wt.DeleteTime is null and w.WorkID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetWorkByWorkTypeId(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by WorkID desc) sn,\r\n[WorkID],w.[WorkTypeID],wt.WorkTypeName,[WorkName],Price\r\nFROM Works w inner join WorkTypes wt on w.WorkTypeID=wt.WorkTypeID \r\nwhere w.DeleteTime is null and wt.DeleteTime is null and w.WorkTypeID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType WorkInsert(int WorkTypeID, string WorkName, string Price)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Works \r\n(UserID,WorkTypeID,WorkName,Price) \r\nvalues (@UserID,@WorkTypeID,@WorkName,@Price)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        sqlCommand.Parameters.AddWithValue("@WorkName", WorkName);
        sqlCommand.Parameters.AddWithValue("@Price", Price);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType WorkUpdate(int WorkID, int WorkTypeID, string WorkName, string Price)
    {
        SqlCommand sqlCommand = new SqlCommand("update Works set UserID=@UserID,\r\nWorkTypeID=@WorkTypeID,WorkName=@WorkName,Price=@Price,UpdateTime=getdate() where WorkID=@WorkID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@WorkTypeID", WorkTypeID);
        sqlCommand.Parameters.AddWithValue("@WorkName", WorkName);
        sqlCommand.Parameters.AddWithValue("@Price", Price);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteWork(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Works set deletetime=getdate(),UserID=@UserID where WorkID=@id;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetWorkTypes()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [WorkTypeID] desc) sn,\r\n[WorkTypeID],[WorkTypeName],[InsertTime],[UpdateTime],[DeleteTime] from WorkTypes  \r\nwhere DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetWorkStatus()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [WorkStatusID] desc) sn,\r\n[WorkStatusID]\r\n      ,[WorkStatusName]\r\n      ,[InsertTime]\r\n      ,[Updatetime]\r\n      ,[DeleteTime] from [WorkStatus]  \r\nwhere DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCadreType()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT  CadreTypeID\r\n      ,CadreTypeName  FROM CadreType", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCards()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by c.CardID desc) sn,\r\nc.* FROM Cards c  where c.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCardsByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [CardID] desc) sn,\r\n*\r\n  FROM [Cards] where DeleteTime is null and CardID=@CardID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("CardID", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteCards(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Cards set deletetime=getdate(),UserID=@UserID where CardID=@id;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType CardsInsert(int UserID, string CardNumber, string CardBarcode)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Cards (UserID,CardNumber,CardBarcode)  \r\nvalues (@UserID,@CardNumber,@CardBarcode)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CardNumber", CardNumber);
        sqlCommand.Parameters.AddWithValue("@CardBarcode", CardBarcode);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType CardsUpdate(int CardID, int UserID, string CardNumber, string CardBarcode)
    {
        SqlCommand sqlCommand = new SqlCommand("update Cards set UserID=@UserID,CardNumber=@CardNumber,\r\nCardBarcode=@CardBarcode where CardID=@CardID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CardNumber", CardNumber);
        sqlCommand.Parameters.AddWithValue("@CardBarcode", CardBarcode);
        sqlCommand.Parameters.AddWithValue("@CardID", CardID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetGenders()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [GenderID]) sn,\r\n[GenderID],[GenderName] FROM [Genders] ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTechniqueSituations()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [TechniqueSituationID] desc) sn,\r\n[TechniqueSituationID] ,[TechniqueSituationName] FROM [TechniqueSituation] where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTechniqueById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Techniques where TechniqueID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType TechniqueInsert(int UserID, int GardenID, int ModelID, string RegisterNumber, string SerieNumber, int Motor, int CompanyID, int TechniqueSituationID, int GPS, string GPSLogin, string GPSPassword, int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, string BoughtDate)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Techniques (GardenID,  ModelID,  RegisterNumber,  SerieNumber, \r\nMotor,  CompanyID,  TechniqueSituationID,  GPS, GPSLogin, GPSPassword,  ProductionYear,  Photo,  Birka,  TechniquesName,  \r\nPassport,  BoughtDate)    Values( @GardenID,  @ModelID,  @RegisterNumber,  @SerieNumber,  @Motor,  @CompanyID,  @TechniqueSituationID,  @GPS, @GPSLogin, @GPSPassword, @ProductionYear,  @Photourl,  @Birka,  @TechniquesName,  @Passport,  @BoughtDate)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        sqlCommand.Parameters.AddWithValue("@SerieNumber", SerieNumber);
        sqlCommand.Parameters.AddWithValue("@Motor", Motor);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        sqlCommand.Parameters.AddWithValue("@GPS", GPS);
        sqlCommand.Parameters.AddWithValue("@GPSLogin", GPSLogin);
        sqlCommand.Parameters.AddWithValue("@GPSPassword", GPSPassword);
        sqlCommand.Parameters.AddWithValue("@ProductionYear", ProductionYear);
        sqlCommand.Parameters.AddWithValue("@Photourl", Photourl);
        sqlCommand.Parameters.AddWithValue("@Birka", Birka);
        sqlCommand.Parameters.AddWithValue("@TechniquesName", TechniquesName);
        sqlCommand.Parameters.AddWithValue("@Passport", Passport);
        sqlCommand.Parameters.AddWithValue("@BoughtDate", BoughtDate.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TechniqueUpdate(int TechniqueID, int UserID, int GardenID, int ModelID, string RegisterNumber, string SerieNumber, int Motor, int CompanyID, int TechniqueSituationID, int GPS, string GPSLogin, string GPSPassword, int ProductionYear, string Photourl, string Birka, string TechniquesName, string Passport, string BoughtDate)
    {
        SqlCommand sqlCommand = new SqlCommand("update Techniques set UserID=@UserID,\r\nGardenID=@GardenID,ModelID=@ModelID,RegisterNumber=@RegisterNumber, SerieNumber=@SerieNumber, \r\nMotor=@Motor,CompanyID=@CompanyID, TechniqueSituationID=@TechniqueSituationID, GPS=@GPS, \r\nGPSLogin=@GPSLogin, GPSPassword=@GPSPassword, ProductionYear=@ProductionYear, Photo=@Photourl, \r\nBirka=@Birka,TechniquesName=@TechniquesName, Passport=@Passport,BoughtDate=@BoughtDate where TechniqueID=@TechniqueID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        sqlCommand.Parameters.AddWithValue("@SerieNumber", SerieNumber);
        sqlCommand.Parameters.AddWithValue("@Motor", Motor);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        sqlCommand.Parameters.AddWithValue("@GPS", GPS);
        sqlCommand.Parameters.AddWithValue("@GPSLogin", GPSLogin);
        sqlCommand.Parameters.AddWithValue("@GPSPassword", GPSPassword);
        sqlCommand.Parameters.AddWithValue("@ProductionYear", ProductionYear);
        sqlCommand.Parameters.AddWithValue("@Photourl", Photourl);
        sqlCommand.Parameters.AddWithValue("@Birka", Birka);
        sqlCommand.Parameters.AddWithValue("@TechniquesName", TechniquesName);
        sqlCommand.Parameters.AddWithValue("@Passport", Passport);
        sqlCommand.Parameters.AddWithValue("BoughtDate", BoughtDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteTechnique(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Techniques set DeleteTime=GetDate() where TechniqueID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTechniquesServices()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by t.TechniqueServiceID desc) sn,\r\nt.*,g.GardenName,w.WorkName,p.ProductTypeID,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,\r\n m1.ModelID modeltexid,m1.ModelName modeltex,m2.ModelID modelprodid, m2.ModelName modelprod\r\nFROM [TechniqueService] t \r\nleft join Techniques tc on t.TechniqueID=tc.TechniqueID\r\nleft join Models m1 on m1.ModelID=tc.ModelID\r\nleft join Gardens g on t.GardenID=g.GardenID \r\nleft join Works w on t.WorkID=w.WorkID\r\nleft join Products p on p.ProductID=t.ProductID\r\nleft join Models m2 on m2.ModelID=p.ModelID\r\nleft join UnitMeasurements u on u.UnitMeasurementID=t.UnitMeasurementID where t.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTechniquesServiceById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by t.TechniqueServiceID desc) sn,\r\nt.*,g.GardenName,w.WorkName,p.ProductID,p.ProductsName,u.UnitMeasurementName,tc.TechniquesName,\r\n m1.ModelID modeltexid,m1.ModelName modeltex,m2.ModelID modelprodid,pt1.ProductTypeID ProductTypeID1,pt2.ProductTypeID ProductTypeID2,m2.ModelName modelprod\r\nFROM [TechniqueService] t \r\nleft join Techniques tc on t.TechniqueID=tc.TechniqueID\r\nleft join Models m1 on m1.ModelID=tc.ModelID\r\nleft join ProductTypes pt1 on m1.ProductTypeID=pt1.ProductTypeID\r\n\r\nleft join Gardens g on t.GardenID=g.GardenID \r\nleft join Works w on t.WorkID=w.WorkID\r\nleft join Products p on p.ProductID=t.ProductID\r\nleft join Models m2 on m2.ModelID=p.ModelID\r\nleft join ProductTypes pt2 on m2.ProductTypeID=pt2.ProductTypeID\r\n\r\nleft join UnitMeasurements u on u.UnitMeasurementID=t.UnitMeasurementID where t.DeleteTime is null \r\nand TechniqueServiceID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType InsertTechniquesService(int TechniqueID, int GardenID, int WorkID, int ProductID, string Price, int ProductSize, int UnitMeasurementID, string Amount, string ServicePrice, int Odometer, string Note, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into TechniqueService \r\n(UserID,TechniqueID,GardenID,WorkID,ProductID,Price,ProductSize,\r\nUnitMeasurementID,Amount,ServicePrice,Odometer,Note,RegisterTime) \r\nValues (@UserID,@TechniqueID,@GardenID,@WorkID,@ProductID,@Price,@ProductSize,\r\n@UnitMeasurementID,@Amount,@ServicePrice,@Odometer,@Note,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@Price", Price.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@ServicePrice", ServicePrice.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Odometer", Odometer);
        sqlCommand.Parameters.AddWithValue("@Note", Note);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType UpdateTechniquesService(int TechniqueServiceID, int TechniqueID, int GardenID, int WorkID, int ProductID, string Price, int ProductSize, int UnitMeasurementID, string Amount, string ServicePrice, int Odometer, string Note, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update TechniqueService set \r\nUserID=@UserID, TechniqueID=@TechniqueID, GardenID=@GardenID, \r\nWorkID=@WorkID, ProductID=@ProductID,Price=@Price,ProductSize=@ProductSize,\r\nUnitMeasurementID=@UnitMeasurementID,Amount=@Amount,ServicePrice=@ServicePrice,\r\nOdometer=@Odometer,Note=@Note,RegisterTime=@RegisterTime \r\nwhere TechniqueServiceID=@TechniqueServiceID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TechniqueServiceID", TechniqueServiceID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@Price", Price.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@ServicePrice", ServicePrice.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Odometer", Odometer);
        sqlCommand.Parameters.AddWithValue("@Note", Note);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTechnique()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() \r\nover(order by t.TechniqueID desc) sn,c.Sname+' '+c.Name UsingUsers,g.GardenID,g.GardenName,\r\nt.TechniquesName, m.ModelName, cp.CompanyName,t.Motor,t.RegisterNumber,t.SerieNumber,t.Passport,t.ProductionYear,t.Birka,ts.TechniqueSituationName,t.GPS,t.GPSLogin,t.GPSPassword,t.BoughtDate,t.Photo,t.TechniqueID,t.ModelID,t.UserID ID,t.CompanyID,t.TechniqueSituationID\r\nfrom Techniques t \r\nleft join Users u on t.UserID=u.UserID\r\nleft join Cadres c on u.CadreID=c.CadreID\r\nleft join Models m on m.ModelID=t.ModelID\r\nleft join Companies cp on cp.CompanyID=t.CompanyID \r\nleft join Gardens g on g.GardenID=t.GardenID \r\nleft join TechniqueSituation ts on ts.TechniqueSituationID=t.TechniqueSituationID \r\nwhere t.DeleteTime is null order by t.UserID desc", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetWateringSystems()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over (order by w.WateringSystemID desc) sn, m.ModelName,w.WateringSystemName, \r\nw.Notes,ts.TechniqueSituationName, w.RegisterTime,w.WateringSystemID,w.ModelID,w.UserID,w.WateringSystemID\r\nfrom WateringSystems w left join Models m on m.ModelID=w.ModelID\r\nleft join TechniqueSituation ts on ts.TechniqueSituationID=w.TechniqueSituationID where w.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteWateringSystems(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update WateringSystems set DeleteTime=GetDate() where WateringSystemID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetWateringSystemsById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from WateringSystems where WateringSystemID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType WateringSystemsInsert(int UserID, int ModelID, string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into WateringSystems \r\n(UserID, ModelID, WateringSystemName, Notes, TechniqueSituationID, RegisterTime) \r\nValues (@UserID, @ModelID, @WateringSystemName, @Notes, @TechniqueSituationID, @RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemName", WateringSystemName);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType WateringSystemsUpdate(int WateringSystemID, int UserID, int ModelID, string WateringSystemName, string Notes, int TechniqueSituationID, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update WateringSystems set \r\nUserID=@UserID,ModelID=@ModelID, WateringSystemName=@WateringSystemName, \r\nNotes=@Notes, TechniqueSituationID=@TechniqueSituationID, RegisterTime=@RegisterTime \r\nwhere WateringSystemID=@WateringSystemID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@ModelID", ModelID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemName", WateringSystemName);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@TechniqueSituationID", TechniqueSituationID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetEmploymentHistoryById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from EmploymentHistory where EmploymentHistoryID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType EmploymentHistoryInsert(int UserID, int StructureID, int CadreID, int CompanyID, int GardenID, int CadreTypeID, int PositionID, string EmploymentNumber, string EntryDate, string ExitDate)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into EmploymentHistory (UserID,StructureID,CadreID,CompanyID,GardenID,CadreTypeID,PositionID,EmploymentNumber,EntryDate,ExitDate)  \r\nValues(@UserID,@StructureID,@CadreID,@CompanyID,@GardenID,@CadreTypeID,@PositionID,@EmploymentNumber,@EntryDate,@ExitDate)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@StructureID", StructureID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
        sqlCommand.Parameters.AddWithValue("@PositionID", PositionID);
        sqlCommand.Parameters.AddWithValue("@EmploymentNumber", EmploymentNumber);
        sqlCommand.Parameters.AddWithValue("@EntryDate", EntryDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@ExitDate", ExitDate.ToParseDatetime());
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType EmploymentHistoryUpdate(int EmploymentHistoryID, int UserID, int StructureID, int CadreID, int CompanyID, int GardenID, int CadreTypeID, int PositionID, string EmploymentNumber, string EntryDate, string ExitDate)
    {
        SqlCommand sqlCommand = new SqlCommand("update EmploymentHistory set UserID=@UserID,StructureID=@StructureID,\r\nCadreID=@CadreID,CompanyID=@CompanyID,GardenID=@GardenID,CadreTypeID=@CadreTypeID,PositionID=@PositionID,\r\nEmploymentNumber=@EmploymentNumber,EntryDate=@EntryDate,ExitDate=@ExitDate \r\nwhere EmploymentHistoryID=@EmploymentHistoryID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@StructureID", StructureID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CadreTypeID", CadreTypeID);
        sqlCommand.Parameters.AddWithValue("@PositionID", PositionID);
        sqlCommand.Parameters.AddWithValue("@EmploymentNumber", EmploymentNumber);
        sqlCommand.Parameters.AddWithValue("@EntryDate", EntryDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@ExitDate", ExitDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@EmploymentHistoryID", EmploymentHistoryID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteEmploymentHistory(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update EmploymentHistory set DeleteTime=GetDate() where EmploymentHistoryID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetEmploymentHistory()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select ROW_NUMBER() over(order by c.EmploymentHistoryID desc) sn,c.UserID,s.StructureID, s.StructureName, k.CardID,\r\n c.CadreID,c.CadreTypeID,ct.CadreTypeName,c.EmploymentHistoryID, k.CardNumber, g.GenderName,gd.GardenName,\r\nc.EntryDate,c.ExitDate,c.EmploymentNumber, p.PositionName, cp.CompanyName,c.CompanyID,\r\ncd.Gender, isnull(k.CardNumber,'')+' '+cd.Sname+' '+ cd.Name+' '+ cd.FName NameDDL\r\nfrom EmploymentHistory c \r\nleft join Structure s on s.StructureID=c.StructureID \r\nleft join Cadres cd on cd.CadreID=c.CadreID\r\nleft join Cards k on cd.CardID=k.CardID\r\nleft join Positions p on c.PositionID=p.PositionID\r\nleft join CadreType ct on ct.CadreTypeID=c.CadreTypeID\r\nleft join Gardens gd on gd.GardenID=c.GardenID\r\nleft join Companies cp on cp.CompanyID=c.CompanyID\r\nleft join Genders g on g.GenderID=cd.Gender where c.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCadresById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Cadres where CadreID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType CadresInsert(int UserID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string RegstrDate)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Cadres (UserID, CardID, Sname, Name, FName, Gender, PassportN, PIN, PhoneNumber, Photo, Email, Address)  \r\nValues(@UserID, @CardID, @Sname, @Name, @FName, @Gender, @PassportN, @PIN, @PhoneNumber, @Photo, @Email, @Address)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CardID", CardID);
        sqlCommand.Parameters.AddWithValue("@Sname", Sname);
        sqlCommand.Parameters.AddWithValue("@Name", Name);
        sqlCommand.Parameters.AddWithValue("@FName", FName);
        sqlCommand.Parameters.AddWithValue("@Gender", Gender);
        sqlCommand.Parameters.AddWithValue("@PassportN", PassportN);
        sqlCommand.Parameters.AddWithValue("@PIN", PIN);
        sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        sqlCommand.Parameters.AddWithValue("@Photo", Photo);
        sqlCommand.Parameters.AddWithValue("@Email", Email);
        sqlCommand.Parameters.AddWithValue("@Address", Address);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType CadresUpdate(int CadreID, int UserID, int CardID, string Sname, string Name, string FName, int Gender, string PassportN, string PIN, string PhoneNumber, string Photo, string Email, string Address, string RegstrDate)
    {
        SqlCommand sqlCommand = new SqlCommand("update Cadres set UserID=@UserID, CardID=@CardID,Sname=@Sname, Name=@Name, FName=@FName,Gender=@Gender, PassportN=@PassportN, PIN=@PIN, \r\nPhoneNumber=@PhoneNumber, Photo=@Photo, Email=@Email, Address=@Address where CadreID=@CadreID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CardID", CardID);
        sqlCommand.Parameters.AddWithValue("@Sname", Sname);
        sqlCommand.Parameters.AddWithValue("@Name", Name);
        sqlCommand.Parameters.AddWithValue("@FName", FName);
        sqlCommand.Parameters.AddWithValue("@Gender", Gender);
        sqlCommand.Parameters.AddWithValue("@PassportN", PassportN);
        sqlCommand.Parameters.AddWithValue("@PIN", PIN);
        sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
        sqlCommand.Parameters.AddWithValue("@Photo", Photo);
        sqlCommand.Parameters.AddWithValue("@Email", Email);
        sqlCommand.Parameters.AddWithValue("@Address", Address);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteCadres(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Cadres set DeleteTime=GetDate() where CadreID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetCadres()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select ROW_NUMBER() over(order by c.CadreID desc) sn, k.CardNumber, \r\nc.Sname, c.Name, c.FName, g.GenderName, c.PassportN, c.PIN, c.[Address], c.PhoneNumber,c.Email,case when c.Photo='' then 'avatar.png' else c.Photo end Photo,\r\nc.RegisterTime, c.CadreID, c.UserID, c.CardID, c.Gender, isnull(k.CardNumber,'')+' '+c.Sname+' '+ c.Name+' '+ c.FName NameDDL\r\nfrom Cadres c \r\nleft join Cards k on c.CardID=k.CardID \r\nleft join Genders g on g.GenderID=c.Gender where c.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetStructure()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by s.StructureSort) sn,s.StructureName, s.StructureSort, s.StructureID from Structure s  where s.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteStructure(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Structure set DeleteTime=GetDate() where StructureID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetStructureById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Structure where StructureID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType StructureInsert(int UserID, string StructureName, float StructureSort)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Structure (UserID, StructureName, StructureSort)  Values(@UserID, @StructureName, @StructureSort)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@StructureName", StructureName);
        sqlCommand.Parameters.AddWithValue("@StructureSort", StructureSort);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType StructureUpdate(int StructureID, int UserID, string StructureName, float StructureSort)
    {
        SqlCommand sqlCommand = new SqlCommand("update Structure set UserID=@UserID, StructureName=@StructureName, StructureSort=@StructureSort where StructureID=@StructureID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@StructureName", StructureName);
        sqlCommand.Parameters.AddWithValue("@StructureSort", StructureSort);
        sqlCommand.Parameters.AddWithValue("@StructureID", StructureID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetPositions()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select  row_number() over(order by p.PositionID) sn,p.PositionName, p.PositionID from Positions p  where p.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeletePosition(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Positions set DeleteTime=GetDate() where PositionID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetPositionById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Positions where PositionID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType PositionInsert(int UserID, string PositionName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Positions (UserID, PositionName)  Values(@UserID, @PositionName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@PositionName", PositionName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType PositionUpdate(int PositionID, int UserID, string PositionName)
    {
        SqlCommand sqlCommand = new SqlCommand("update Positions set UserID=@UserID, PositionName=@PositionName where PositionID=@PositionID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@PositionName", PositionName);
        sqlCommand.Parameters.AddWithValue("@PositionID", PositionID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOperationCadre()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by w.WorkDoneID) sn,\r\nc.Name+' '+c.Sname+' '+c.FName fullname, s.WorkName,w.Salary,\r\ng.GardenName,z.ZoneName,sc.SectorName,l.LineName,w.TreeCount,\r\nw.Notes,w.RegisterTime,w.WorkDoneID,w.UserID,w.LinesID,w.CadreID,w.WorkID,\r\ng.GardenID,z.ZoneID,sc.SectorID,wc.WeatherConditionName+' ('+cast(wc.Coefficient as varchar)+')' as WeatherConditionName1\r\n, tt.TreeTypeName+' ('+cast(tt.Coefficient as varchar)+')' TreeTypeName1,\r\ncast(tta.FirstAge as varchar)+' - '+cast(tta.LastAge as varchar)+' yaş ('+cast(tta.Coefficient as varchar)+')' TreeAgeName1\r\nfrom WorkDone w\r\nleft join Cadres c on c.CadreID=w.CadreID\r\nleft join Works s on s.WorkID=w.WorkID\r\nleft join Lines l on l.LineID=w.LinesID\r\nleft join Sectors sc on sc.SectorID=l.SectorID\r\nleft join Zones z on z.ZoneID=sc.ZoneID\r\nleft join Gardens g on g.GardenID=z.GardenID \r\nleft join WeatherCondition wc on wc.WeatherConditionID=w.WeatherConditionID\r\nleft join TreeTypes tt on tt.TreeTypeID=w.TreeTypeID\r\nleft join TariffTreeAge tta on tta.TariffAgeID=w.TariffAgeID\r\nwhere w.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationCadre(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update WorkDone set DeleteTime=GetDate() where WorkDoneID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOperationCadreByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by w.WorkDoneID) sn,\r\nc.Name+' '+c.Sname+' '+c.FName fullname, s.WorkName,\r\ng.GardenName,z.ZoneName,sc.SectorName,l.LineName,w.TreeCount,\r\nw.Notes,w.RegisterTime,w.WorkDoneID,w.UserID,w.LinesID,w.CadreID,\r\ng.GardenID,z.ZoneID,sc.SectorID,w.WorkID,w.WeatherConditionID,w.TreeTypeID,TariffAgeID,w.Salary\r\nfrom WorkDone w\r\nleft join Cadres c on c.CadreID=w.CadreID\r\nleft join Works s on s.WorkID=w.WorkID\r\nleft join Lines l on l.LineID=w.LinesID\r\nleft join Sectors sc on sc.SectorID=l.SectorID\r\nleft join Zones z on z.ZoneID=sc.ZoneID\r\nleft join Gardens g on g.GardenID=z.GardenID \r\nwhere w.DeleteTime is null and w.WorkDoneID=@WorkDoneID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WorkDoneID", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType OperationCadreInsert(int UserID, int CadreID, int WorkID, int LinesID, int WeatherConditionID, int TreeTypeID, int TariffAgeID, int TreeCount, float Salary, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into WorkDone (UserID,CadreID,WorkID,LinesID,WeatherConditionID,TreeTypeID,TariffAgeID,TreeCount,Salary,Notes,RegisterTime)\r\n                                            Values(@UserID, @CadreID,@WorkID,@LinesID,@WeatherConditionID,@TreeTypeID,@TariffAgeID,@TreeCount,@Salary,@Notes,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@LinesID", LinesID);
        sqlCommand.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        sqlCommand.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Salary", Salary);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OperationCadreUpdate(int WorkDoneID, int UserID, int CadreID, int WorkID, int LinesID, int WeatherConditionID, int TreeTypeID, int TariffAgeID, int TreeCount, float Salary, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update WorkDone set UserID=@UserID, CadreID=@CadreID,WorkID=@WorkID,\r\nLinesID=@LinesID,WeatherConditionID=@WeatherConditionID,TreeTypeID=@TreeTypeID,TariffAgeID=@TariffAgeID,\r\nTreeCount=@TreeCount,Salary=@Salary,Notes=@Notes,RegisterTime=@RegisterTime where WorkDoneID=@WorkDoneID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@LinesID", LinesID);
        sqlCommand.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        sqlCommand.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Salary", Salary);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@WorkDoneID", WorkDoneID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOperationTechniqueWorkDone()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by t.TechniquesWorkDoneID) sn,t.*,c.CompanyName,sc.SectorID,sc.SectorName,\r\nz.ZoneID,z.ZoneName,g.GardenID,g.GardenName,tc.TechniqueID,tc.TechniqueID,tc.TechniquesName+' '+Birka as TechniquesName,s.WorkName,z.ZoneName,g.GardenName,sc.SectorName,l.LineName\r\nfrom TechniquesWorkDone t\r\nleft join Companies c on c.CompanyID=t.CompanyID\r\nleft join Works s on s.WorkID=t.WorkID\r\nleft join Lines l on l.LineID=t.LineID\r\nleft join Sectors sc on sc.SectorID=l.SectorID\r\nleft join Zones z on z.ZoneID=sc.ZoneID\r\nleft join Gardens g on g.GardenID=z.GardenID \r\nleft join Techniques tc on tc.TechniqueID=t.TechniqueID\r\nwhere t.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationTechniqueWorkDone(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update TechniquesWorkDone set DeleteTime=GetDate() where TechniquesWorkDoneID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTechniqueByModelId(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Techniques where ModelID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetOperationTechniqueWorkDoneByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by t.TechniquesWorkDoneID) sn,t.*,c.CompanyName,sc.SectorID,sc.SectorName,\r\nz.ZoneID,z.ZoneName,g.GardenID,g.GardenName,tc.TechniqueID,tc.TechniqueID,tc.TechniquesName+' '+Birka as TechniquesName,\r\ntc.ModelID,m.ModelName,m.ProductTypeID,\r\nl.SectorID,sc.ZoneID,z.GardenID \r\nfrom TechniquesWorkDone t\r\nleft join Companies c on c.CompanyID=t.CompanyID\r\nleft join Works s on s.WorkID=t.WorkID\r\nleft join Lines l on l.LineID=t.LineID\r\nleft join Sectors sc on sc.SectorID=l.SectorID\r\nleft join Zones z on z.ZoneID=sc.ZoneID\r\nleft join Gardens g on g.GardenID=z.GardenID \r\nleft join Techniques tc on tc.TechniqueID=t.TechniqueID\r\nleft join Models m on tc.ModelID=m.ModelID\r\nwhere t.DeleteTime is null and t.TechniquesWorkDoneID=@TechniquesWorkDoneID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TechniquesWorkDoneID", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType OperationTechniqueWorkDoneInsert(int UserID, int TechniqueID, int CompanyID, int WorkID, int Odometer, int LineID, int TreeCount, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into TechniquesWorkDone (UserID,TechniqueID,CompanyID,WorkID,Odometer,LineID,TreeCount,Notes,RegisterTime)\r\n                                            Values(@UserID,@TechniqueID,@CompanyID,@WorkID,@Odometer,@LineID,@TreeCount,@Notes,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@Odometer", Odometer);
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OperationTechniqueWorkDoneUpdate(int TechniquesWorkDoneID, int UserID, int TechniqueID, int CompanyID, int WorkID, int Odometer, int LineID, int TreeCount, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update TechniquesWorkDone set UserID=@UserID, TechniqueID=@TechniqueID,CompanyID=@CompanyID,WorkID=@WorkID,Odometer=@Odometer,LineID=@LineID,TreeCount=@TreeCount,Notes=@Notes,RegisterTime=@RegisterTime where TechniquesWorkDoneID=@TechniquesWorkDoneID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@TechniqueID", TechniqueID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@WorkID", WorkID);
        sqlCommand.Parameters.AddWithValue("@Odometer", Odometer);
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@TechniquesWorkDoneID", TechniquesWorkDoneID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOperationTechniques()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by w.TechniquesWorkDoneID) sn,\r\nt.TechniquesName, s.WorkName,g.GardenName,z.ZonaName,sc.SectorName,\r\nl.LineName,w.Odometer,\r\nw.Note,w.RegstrTime,w.TechniquesWorkDoneID,w.UserID,w.LineID,w.WorkID,\r\ng.GardenID,z.ZoneID,sc.SectorsID\r\nfrom TechniquesWorkDone w\r\nleft join Techniques t on t.TechniqueID=w.TechniqueID\r\nleft join Works s on s.WorkID=w.WorkID\r\nleft join Lines l on l.LineID=w.LineID\r\nleft join Sectors sc on sc.SectorsID=l.SektorID\r\nleft join Zones z on z.ZoneID=sc.ZonaID\r\nleft join Gardens g on g.GardenID=z.GardenID where w.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationTechniques(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update TechniquesWorkDone set DeleteTime=GetDate() where TechniquesWorkDoneID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetTechniquesWorkDoneById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from TechniquesWorkDone where TechniquesWorkDoneID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetOperationWateringSystems()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select ROW_NUMBER() over( order by w.WateringSystemWorkID) sn,\r\nwt.WateringSystemName, g.GardenName,Ws.ProductOperationTypeName, w.WateringSystemSize,\r\num.UnitMeasurementName,w.Notes,w.RegisterTime,w.WateringSystemWorkID,\r\nw.UserID,w.UnitMeasurementID,g.GardenID\r\nfrom WateringSystemWorkDone w\r\nleft join Gardens g on g.GardenID=w.GardenID\r\nleft join WateringSystems wt on wt.WateringSystemID=w.WateringSystemID\r\nleft join ProductOperationTypes Ws on Ws.ProductOperationTypeID=W.EntryExitStatus\r\nleft join UnitMeasurements um on um.UnitMeasurementID=W.UnitMeasurementID\r\nwhere w.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOperationWateringSystems(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update WateringSystemWorkDone set DeleteTime=GetDate() \r\nwhere WateringSystemWorkID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOperationWateringSystemsById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from WateringSystemWorkDone where WateringSystemWorkID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType OperationWateringSystemsWorkDoneInsert(int UserID, int GardenID, int WateringSystemID, string WateringSystemSize, int UnitMeasurementID, int EntryExitStatus, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into WateringSystemWorkDone (UserID,GardenID,WateringSystemID,WateringSystemSize,\r\nUnitMeasurementID,EntryExitStatus,Notes,RegisterTime)\r\n Values(@UserID,@GardenID,@WateringSystemID,@WateringSystemSize,@UnitMeasurementID,@EntryExitStatus,@Notes,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemSize", WateringSystemSize);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OperationWateringSystemsWorkDoneUpdate(int WateringSystemWorkID, int UserID, int GardenID, int WateringSystemID, string WateringSystemSize, int UnitMeasurementID, int EntryExitStatus, string Notes, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update WateringSystemWorkDone set UserID=@UserID, GardenID=@GardenID,\r\nWateringSystemID=@WateringSystemID,WateringSystemSize=@WateringSystemSize,UnitMeasurementID=@UnitMeasurementID,\r\nEntryExitStatus=@EntryExitStatus,Notes=@Notes,RegisterTime=@RegisterTime where WateringSystemWorkID=@WateringSystemWorkID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemID", WateringSystemID);
        sqlCommand.Parameters.AddWithValue("@WateringSystemSize", WateringSystemSize);
        sqlCommand.Parameters.AddWithValue("@UnitMeasurementID", UnitMeasurementID);
        sqlCommand.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@WateringSystemWorkID", WateringSystemWorkID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetWeatherCondition()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [WeatherConditionID] desc) sn,\r\n* FROM [WeatherCondition] where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetWeatherConditionByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [WeatherConditionID] desc) sn,\r\n* FROM [WeatherCondition] where DeleteTime is null and WeatherConditionID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTariffTreeAge()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by [TariffAgeID] desc) sn,\r\n*,cast(FirstAge as varchar)+' - '+cast(LastAge as varchar)+' yaş ('+cast(Coefficient as varchar)+')' TariffAgeName1 FROM [TariffTreeAge] where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTariffTreeAgeByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() \r\nover(order by [TariffAgeID] desc) sn,* FROM [TariffTreeAge] where DeleteTime is null \r\nand TariffAgeID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType TariffTreeAgeInsert(int FirstAge, int LastAge, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into TariffTreeAge \r\n(UserID,FirstAge,LastAge,Coefficient) values (@UserID,@FirstAge,@LastAge,@Coefficient)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@FirstAge", FirstAge);
        sqlCommand.Parameters.AddWithValue("@LastAge", LastAge);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TariffTreeAgeUpdate(int TariffAgeID, int FirstAge, int LastAge, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("update TariffTreeAge set UserID=@UserID,\r\nFirstAge=@FirstAge,LastAge=@LastAge,Coefficient=@Coefficient,\r\nUpdateTime=getdate() where TariffAgeID=@TariffAgeID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TariffAgeID", TariffAgeID);
        sqlCommand.Parameters.AddWithValue("@FirstAge", FirstAge);
        sqlCommand.Parameters.AddWithValue("@LastAge", LastAge);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteTariffTreeAge(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update TariffTreeAge set deletetime=getdate(),\r\nUserID=@UserID where TariffAgeID=@TariffAgeID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@TariffAgeID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType WeatherConditionInsert(string WeatherConditionName, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into WeatherCondition \r\n(UserID,WeatherConditionName,Coefficient) values (@UserID,@WeatherConditionName,@Coefficient)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@WeatherConditionName", WeatherConditionName);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType WeatherConditionUpdate(int WeatherConditionID, string WeatherConditionName, string Coefficient)
    {
        SqlCommand sqlCommand = new SqlCommand("update WeatherCondition set UserID=@UserID,\r\nWeatherConditionName=@WeatherConditionName,Coefficient=@Coefficient,\r\nUpdateTime=getdate() where WeatherConditionID=@WeatherConditionID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@WeatherConditionID", WeatherConditionID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@WeatherConditionName", WeatherConditionName);
        sqlCommand.Parameters.AddWithValue("@Coefficient", Coefficient);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteWeatherCondition(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update WeatherCondition set deletetime=getdate(),\r\nUserID=@UserID where WeatherConditionID=@WeatherConditionID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@WeatherConditionID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetEntryExits()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nselect t.[Bağın adı],t.[İşçinin adı],t.Barkod,t.[Kartın nömrəsi],t.[Tarix], case when t.[sn] % 2 =0 then N'Çıxış' \r\nelse N'Giriş' end [Status],EntryExitID,\r\nrow_number() over( order by t.Tarix desc) [Sıra nömrəsi] from (\r\n\r\nSELECT row_number() over(PARTITION BY c1.Sname,c1.Name,c1.FName order by c1.Sname,c1.Name,c1.FName,e.InsertTime) [sn],\r\nc2.CardNumber [Kartın nömrəsi],c1.Sname+' '+c1.Name+' '+c1.FName [İşçinin adı],e.InsertTime Tarix,e.EntryExitID,g.GardenName [Bağın adı], c2.CardBarcode [Barkod]\r\nfrom [EntryExit] e \r\nleft join Cadres c1 on e.CadreID=c1.CadreID \r\nleft join Cards c2 on c2.CardID=c1.CardID \r\nleft join Users u on u.UserID=e.UserID\r\nleft join Gardens g on u.GardenID=g.GardenID\r\nwhere e.DeleteTime is null and c1.DeleteTime is null  and \r\nc2.DeleteTime is null and u.DeleteTime is null and g.DeleteTime is null\r\n\r\n) t\r\n", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetCadreByCardnumber(string cardnumber)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT  CadreID,c2.CardNumber,c1.Sname+' '+c1.Name+' '+c1.FName fullname \r\nfrom   Cadres c1 left join Cards c2 on c2.CardID=c1.CardID  where c1.DeleteTime is null\r\nand c2.DeleteTime is null and c2.CardNumber=@cardnumber", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("cardnumber", cardnumber);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType EntryExitInsert(int CadreID, int EntryExitStatus)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into EntryExit \r\n(UserID,CadreID,EntryExitStatus) values (@UserID,@CadreID,@EntryExitStatus)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@CadreID", CadreID);
        sqlCommand.Parameters.AddWithValue("@EntryExitStatus", EntryExitStatus);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteEntryExit(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update EntryExit set deletetime=getdate(),UserID=@UserID \r\nwhere EntryExitID=@EntryExitID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@EntryExitID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetUserStatus()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT *\r\n  FROM [UserStatus] where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductOperationTypes()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT *\r\n  FROM [ProductOperationTypes] where DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetStockOperationReasonsByProductOperationTypeID(int ProductOperationTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT *\r\n  FROM [StockOperationReasons] where deletetime is null and ProductOperationTypeID=@ProductOperationTypeID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetInvoiceTransfer(int InvoiceStockTransferID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by InvoiceStockTransferID desc) sn,\r\ninvs.*,Sf.StockName StockFromName,Sto.StockName StockToName,st.InvoiceStatusName,case when invs.InvoiceStockTransferID=" + InvoiceStockTransferID.ToParseStr() + @" then 'btn btn-success' else 'btn btn-primary' end reng,  from InvoiceStockTransfer invs left join Stocks sf on sf.StockID=invs.StockFromID\r\nleft join Stocks sto on sto.StockID=invs.StockToID\r\nleft join InvoiceStatus st on invs.InvoiceStatusID=st.InvoiceStatusID\r\n where invs.InvoiceStatusID=1 and invs.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetInvoiceInputOutput(int ProductOperationTypeID,int InvoiceStockID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(@"select row_number() over(order by InvoiceStockID desc) sn, invs.*,sor.ReasonName,S.StockName,st.InvoiceStatusName,
                   case when invs.InvoiceStockID=" + InvoiceStockID.ToParseStr()+ @" then 'btn btn-success' else 'btn btn-primary' end reng,
(select sum(Amount) cem from ProductStockInputOutput z where z.InvoiceStockID=invs.InvoiceStockID and z.DeleteTime is null) cem,(select sum(AmountDiscount) cem from ProductStockInputOutput z where z.InvoiceStockID=invs.InvoiceStockID and z.DeleteTime is null) cemEndirim from InvoiceStock invs left join 
ProductOperationTypes pot on invs.ProductOperationTypeID=pot.ProductOperationTypeID left join StockOperationReasons sor on invs.StockOperationReasonID=sor.StockOperationReasonID 
and sor.ProductOperationTypeID=pot.ProductOperationTypeID left join Stocks s on s.StockID=invs.StockID left join InvoiceStatus st on invs.InvoiceStatusID=st.InvoiceStatusID 
where invs.DeleteTime is null and invs.InvoiceStatusID=1 and invs.ProductOperationTypeID=@ProductOperationTypeID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetInvoiceStockTransferByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by InvoiceStockTransferID desc) sn,\r\ninvs.*,Sf.StockName StockFromName,Sto.StockName StockToName,st.InvoiceStatusName from InvoiceStockTransfer invs\r\nleft join Stocks sf on sf.StockID=invs.StockFromID\r\nleft join Stocks sto on sto.StockID=invs.StockToID\r\nleft join InvoiceStatus st on invs.InvoiceStatusID=st.InvoiceStatusID\r\n where invs.DeleteTime is null and InvoiceStockTransferID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetInvoiceStockInputOutputByID(int id, int ProductOperationTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by InvoiceStockID desc) sn,\r\ninvs.*,sor.ReasonName,S.StockName,st.InvoiceStatusName from InvoiceStock invs\r\nleft join ProductOperationTypes pot on invs.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join StockOperationReasons sor on invs.StockOperationReasonID=sor.StockOperationReasonID \r\nand sor.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join Stocks s on s.StockID=invs.StockID\r\nleft join InvoiceStatus st on invs.InvoiceStatusID=st.InvoiceStatusID where invs.DeleteTime is null \r\nand InvoiceStockID=@id and invs.ProductOperationTypeID=@ProductOperationTypeID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductStockTransferByInvoiceID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by ProductStockTransferID desc) sn,\r\npsio.*,sf.StockName StockFromName, st.StockName StockToName,\r\npt.ProductTypeID,pt.ProductTypeName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName \r\nFROM ProductStockTransfer psio \r\nleft join InvoiceStockTransfer ist on psio.InvoiceStockTransferID=ist.InvoiceStockTransferID\r\nleft join Products p on psio.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks sf on sf.StockID=ist.StockFromID\r\nleft join Stocks st on st.StockID=ist.StockToID\r\n where psio.DeleteTime is null and psio.InvoiceStockTransferID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductStockInputOutputByInvoiceID(int id, int ProductOperationTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,s.StockName,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,\r\nsor.ReasonName,p.ProductsName,um.UnitMeasurementName,Currency.CurrencyName,m.ModelID,m.ModelName FROM [ProductStockInputOutput] psio \r\nleft join (select * from InvoiceStock where productoperationtypeid=@ProductOperationTypeID)  ist on psio.InvoiceStockID=ist.InvoiceStockID\r\nleft join ProductOperationTypes pot on ist.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join StockOperationReasons sor on ist.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join Products p on psio.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks s on s.StockID=ist.StockID left join Currency on psio.CurrencyID=Currency.CurrencyID where psio.DeleteTime is null and ist.ProductOperationTypeID=@ProductOperationTypeID and ist.InvoiceStockID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetSumProductStockInputOutputByInvoiceID(int id, int ProductOperationTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT sum(ProductSize) ProductSize,sum(Amount) Amount,\r\nsum(AmountDiscount) AmountDiscount FROM [ProductStockInputOutput] psio \r\n where psio.DeleteTime is null and psio.ProductOperationTypeID=@ProductOperationTypeID and psio.InvoiceStockID=@id\r\n group by psio.InvoiceStockID ", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductOperationTypeID", ProductOperationTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType InvoiceStockTransferInsert(int StockFromID, int StockToID, int InvoiceStatusID, string RegisterTime, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into InvoiceStockTransfer \r\n(UserID,StockFromID,StockToID,InvoiceStatusID,RegisterTime,Notes) values \r\n(@UserID,@StockFromID,@StockToID,@InvoiceStatusID,@RegisterTime,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockFromID", StockFromID);
        sqlCommand.Parameters.AddWithValue("@StockToID", StockToID);
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType InvoiceStockTransferUpdate(int InvoiceStockTransferID, int StockFromID, int StockToID, int InvoiceStatusID, string RegisterTime, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update InvoiceStockTransfer set UserID=@UserID,\r\nStockFromID=@StockFromID,StockToID=@StockToID,\r\nInvoiceStatusID=@InvoiceStatusID,\r\nRegisterTime=@RegisterTime,Notes=@Notes,UpdateTime=getdate() where InvoiceStockTransferID=@InvoiceStockTransferID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@InvoiceStockTransferID", InvoiceStockTransferID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockFromID", StockFromID);
        sqlCommand.Parameters.AddWithValue("@StockToID", StockToID);
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType DeleteInvoiceStockTransfer(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update InvoiceStockTransfer set deletetime=getdate(),UserID=@UserID \r\nwhere InvoiceStockTransferID=@InvoiceStockTransferID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@InvoiceStockTransferID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType InvoiceStockInputOutputInsert(int StockID, int ProductOperationTypeID, int StockOperationReasonID, int InvoiceStatusID, string RegisterTime, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into InvoiceStock \r\n(UserID,StockID,ProductOperationTypeID,StockOperationReasonID,InvoiceStatusID,RegisterTime,Notes) values \r\n(@UserID,@StockID,@ProductOperationTypeID,@StockOperationReasonID,@InvoiceStatusID,@RegisterTime,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        sqlCommand.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType InvoiceStockInputOutputUpdate(int InvoiceStockID, int StockID, int ProductOperationTypeID, int StockOperationReasonID, int InvoiceStatusID, string RegisterTime, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("update InvoiceStock set UserID=@UserID,\r\nProductOperationTypeID=@ProductOperationTypeID,StockID=@StockID,StockOperationReasonID=@StockOperationReasonID,\r\nInvoiceStatusID=@InvoiceStatusID,\r\nRegisterTime=@RegisterTime,Notes=@Notes,UpdateTime=getdate() where InvoiceStockID=@InvoiceStockID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@InvoiceStockID", InvoiceStockID);
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@ProductOperationTypeID", ProductOperationTypeID);
        sqlCommand.Parameters.AddWithValue("@StockOperationReasonID", StockOperationReasonID);
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType InvoiceStockInputOutputUpdateOK(int InvoiceStockID, int InvoiceStatusID)
    {
        SqlCommand sqlCommand = new SqlCommand("update InvoiceStock set UserID=@UserID,InvoiceStatusID=@InvoiceStatusID where InvoiceStockID=@InvoiceStockID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@InvoiceStockID", InvoiceStockID);
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }
    public Types.ProsesType DeleteInvoiceStock(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update InvoiceStock set deletetime=getdate(),UserID=@UserID \r\nwhere InvoiceStockID=@InvoiceStockID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@InvoiceStockID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProductStockInputOutputByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,s.StockName,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,\r\nsor.ReasonName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName FROM [ProductStockInputOutput] psio \r\nleft join (select * from InvoiceStock where productoperationtypeid=1)  ist on psio.InvoiceStockID=ist.InvoiceStockID\r\nleft join ProductOperationTypes pot on ist.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join StockOperationReasons sor on ist.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join Products p on psio.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks s on s.StockID=ist.StockID\r\n where psio.DeleteTime is null \r\n and ProductStockInputOutputID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ProductStockInputOutputInsert(int InvoiceStockID, int ProductID, string ProductSize, string Price, string PriceDiscount, string Amount, string AmountDiscount, string Notes, int CurrencyID, string ExchangeRate)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into ProductStockInputOutput \r\n(UserID,InvoiceStockID,ProductID,\r\nProductSize,Price,PriceDiscount,Amount,AmountDiscount,Notes,CurrencyID,ExchangeRate) values \r\n(@UserID,@InvoiceStockID,@ProductID,\r\n@ProductSize,@Price,@PriceDiscount,@Amount,@AmountDiscount,@Notes,@CurrencyID,@ExchangeRate)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@InvoiceStockID", InvoiceStockID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Price", Price.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@PriceDiscount", PriceDiscount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@AmountDiscount", AmountDiscount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@CurrencyID", CurrencyID);
        sqlCommand.Parameters.AddWithValue("@ExchangeRate", ExchangeRate.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType ProductStockInputOutputUpdate(int ProductStockInputOutputID, int InvoiceStockID, int ProductID, string ProductSize, string Price, string PriceDiscount, string Amount, string AmountDiscount, string Notes, int CurrencyID, string ExchangeRate)
    {
        SqlCommand sqlCommand = new SqlCommand("update ProductStockInputOutput set UserID=@UserID,InvoiceStockID=@InvoiceStockID,\r\nProductID=@ProductID,ProductSize=@ProductSize,\r\nPrice=@Price,PriceDiscount=@PriceDiscount,Amount=@Amount,AmountDiscount=@AmountDiscount,\r\nNotes=@Notes,CurrencyID=@CurrencyID,ExchangeRate=@ExchangeRate,UpdateTime=getdate() where ProductStockInputOutputID=@ProductStockInputOutputID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductStockInputOutputID", ProductStockInputOutputID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@InvoiceStockID", InvoiceStockID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Price", Price.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@PriceDiscount", PriceDiscount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@AmountDiscount", AmountDiscount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Parameters.AddWithValue("@CurrencyID", CurrencyID);
        sqlCommand.Parameters.AddWithValue("@ExchangeRate", ExchangeRate.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteProductStockInputOutput(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update ProductStockInputOutput set deletetime=getdate(),UserID=@UserID \r\nwhere ProductStockInputOutputID=@ProductStockInputOutputID;", SqlConn);
        sqlCommand.Parameters.AddWithValue("@ProductStockInputOutputID", id);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProductByModelProductId(int ModelID, int ProductTypeID)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by ProductID desc) sn,\r\n       [ProductID],p.[UserID],[ProductsName],p.[ProductTypeID],pt.ProductTypeName\r\n      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName\r\n      ,[Notes],ProductsName+N', ölçü vahidi:'+isnull(UnitMeasurementName,'') ddlname from [Products] p \r\n  left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID\r\n  left join Models m on p.ModelID=m.ModelID\r\n  left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID \r\n  where p.DeleteTime is null and pt.DeleteTime is null and \r\nm.DeleteTime is null and u.DeleteTime is null and p.ProductTypeID=@ProductTypeID and p.ModelID=@ModelID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ModelID", ModelID);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductTypeID", ProductTypeID);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTechniquesByModelId(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by TechniqueID desc) sn,\r\n p.* from Techniques p  left join Models m on p.ModelID=m.ModelID where p.DeleteTime is null  and \r\n m.DeleteTime is null  and m.ModelID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetExpenses()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() \r\nover(order by OtherExpenseID desc) sn,e.* from OtherExpenses e where e.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetExpenseByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() \r\nover(order by OtherExpenseID desc) sn,e.* from OtherExpenses e where e.DeleteTime is null \r\nand OtherExpenseID=@id ", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType InsertExpense(string OtherExpenseName, string Amount, string Note, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into OtherExpenses \r\n(UserID,OtherExpenseName,Amount,Note,RegisterTime) values \r\n(@UserID,@OtherExpenseName,@Amount,@Note,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@OtherExpenseName", OtherExpenseName);
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Note", Note);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType UpdateExpense(int OtherExpenseID, string OtherExpenseName, string Amount, string Note, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update OtherExpenses set UserID=@UserID,\r\nOtherExpenseName=@OtherExpenseName,Amount=@Amount,RegisterTime=@RegisterTime,Note=@Note,\r\nUpdateTime=getdate() \r\nwhere OtherExpenseID=@OtherExpenseID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@OtherExpenseID", OtherExpenseID);
        sqlCommand.Parameters.AddWithValue("@OtherExpenseName", OtherExpenseName);
        sqlCommand.Parameters.AddWithValue("@Amount", Amount.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Note", Note);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteExpense(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update OtherExpenses set DeleteTime=GetDate() where OtherExpenseID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetSiteMap()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select lm.* from SiteMap lm \r\nwhere  lm.SiteMapID not in (\r\nselect sm.SiteMapID from Users u \r\ninner join PermissionUser p on u.UserID = p.UserID\r\ninner join SiteMap sm on p.SiteMapID = sm.SiteMapID where p.UserID=@UserID)", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("UserID", HttpContext.Current.Session["UserID"].ToParseStr());
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetPermisionByUserID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from PermissionUser p \r\nwhere p.UserID=@UserID", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("UserID", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTreesCounts()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\n\r\nSELECT row_number() \r\nover(order by TreeCountID desc) sn,tc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,ts.TreesSitiuationName,\r\ntt.TreeTypeName,t.TreeName,c.CountryName\r\n  FROM [TreesCount] tc \r\n  left join Lines l on  tc.LineID=l.LineID \r\n  left join Sectors s on s.SectorID=l.SectorID\r\n  left join Zones z on z.ZoneID=s.ZoneID\r\n  left join Gardens g on g.GardenID=z.GardenID \r\n  left join TreesSitiuation ts on tc.TreeSitiuation=ts.TreesSitiuationID\r\n  left join TreeTypes tt on tc.TreeTypeID=tt.TreeTypeID\r\n  left join Countries c on c.CountryID=tt.CountryID\r\n  left join Trees t on t.TreeID=tt.TreeID\r\n  where tc.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetTreesCountsByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\n\r\nSELECT row_number() \r\nover(order by TreeCountID desc) sn,tc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,ts.TreesSitiuationName,\r\ntt.TreeTypeName,t.TreeName,c.CountryName,s.SectorID,g.GardenID,z.ZoneID,c.CountryID,t.TreeID,ts.TreesSitiuationID\r\n  FROM [TreesCount] tc \r\n  left join Lines l on  tc.LineID=l.LineID \r\n  left join Sectors s on s.SectorID=l.SectorID\r\n  left join Zones z on z.ZoneID=s.ZoneID\r\n  left join Gardens g on g.GardenID=z.GardenID \r\n  left join TreesSitiuation ts on tc.TreeSitiuation=ts.TreesSitiuationID\r\n  left join TreeTypes tt on tc.TreeTypeID=tt.TreeTypeID\r\n  left join Countries c on c.CountryID=tt.CountryID\r\n  left join Trees t on t.TreeID=tt.TreeID\r\n  where tc.DeleteTime is null and TreeCountID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType TreeCountInsert(int LineID, int TreeTypeID, int TreeCount, int TreeSitiuation, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into TreesCount \r\n(UserID,RegisterTime,LineID,TreeTypeID,TreeCount,TreeSitiuation) \r\nvalues (@UserID,@RegisterTime,@LineID,@TreeTypeID,@TreeCount,@TreeSitiuation)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@TreeSitiuation", TreeSitiuation);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TreeCountUpdate(int TreeCountID, int LineID, int TreeTypeID, int TreeCount, int TreeSitiuation, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update TreesCount set UserID=@UserID,\r\nRegisterTime=@RegisterTime,LineID=@LineID,TreeTypeID=@TreeTypeID,TreeCount=@TreeCount,\r\nTreeSitiuation=@TreeSitiuation,UpdateTime=getdate() where TreeCountID=@TreeCountID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TreeCountID", TreeCountID);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@TreeTypeID", TreeTypeID);
        sqlCommand.Parameters.AddWithValue("@TreeCount", TreeCount);
        sqlCommand.Parameters.AddWithValue("@TreeSitiuation", TreeSitiuation);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType TreeCountDelete(int TreeCountID)
    {
        SqlCommand sqlCommand = new SqlCommand("update TreesCount set UserID=@UserID,DeleteTime=getdate() where TreeCountID=@TreeCountID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@TreeCountID", TreeCountID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetPoleTypes()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by PoleTypeID desc) sn, \r\n * FROM [PoleTypes] p where p.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetPolesCounts()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nSELECT row_number() over(order by PoleCountID desc) sn,pc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,pt.PoleTypeName\r\n  FROM PolesCount pc \r\n  left join Lines l on  pc.LineID=l.LineID \r\n  left join Sectors s on s.SectorID=l.SectorID\r\n  left join Zones z on z.ZoneID=s.ZoneID\r\n  left join Gardens g on g.GardenID=z.GardenID \r\n  left join PoleTypes pt on pc.PoleTypeID=pt.PoleTypeID\r\n  where pc.DeleteTime is null ", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetPolesCountsByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nSELECT row_number() \r\nover(order by PoleCountID desc) sn,pc.*,l.LineName,s.SectorName,z.ZoneName,g.GardenName,pt.PoleTypeName,\r\ns.SectorID,g.GardenID,z.ZoneID\r\n  FROM PolesCount pc \r\n  left join Lines l on  pc.LineID=l.LineID \r\n  left join Sectors s on s.SectorID=l.SectorID\r\n  left join Zones z on z.ZoneID=s.ZoneID\r\n  left join Gardens g on g.GardenID=z.GardenID \r\n  left join PoleTypes pt on pc.PoleTypeID=pt.PoleTypeID\r\n  where pc.DeleteTime is null and PoleCountID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType PoleCountInsert(int LineID, int PoleTypeID, int PoleCount, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into PolesCount \r\n(UserID,RegisterTime,LineID,PoleTypeID,PoleCount) \r\nvalues (@UserID,@RegisterTime,@LineID,@PoleTypeID,@PoleCount)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@PoleTypeID", PoleTypeID);
        sqlCommand.Parameters.AddWithValue("@PoleCount", PoleCount);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType PoleCountUpdate(int PoleCountID, int LineID, int PoleTypeID, int PoleCount, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update PolesCount set UserID=@UserID,\r\nRegisterTime=@RegisterTime,LineID=@LineID,PoleTypeID=@PoleTypeID,PoleCount=@PoleCount,\r\nUpdateTime=getdate() where PoleCountID=@PoleCountID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@LineID", LineID);
        sqlCommand.Parameters.AddWithValue("@PoleTypeID", PoleTypeID);
        sqlCommand.Parameters.AddWithValue("@PoleCount", PoleCount);
        sqlCommand.Parameters.AddWithValue("@PoleCountID", PoleCountID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType PoleCountDelete(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("update PolesCount set UserID=@UserID,DeleteTime=getdate() where PoleCountID=@id", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProductStock()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by ps.StockID desc) sn,ps.ProductID,s.StockID,s.StockName,\r\npt.ProductTypeID,pt.ProductTypeName,\r\np.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName\r\n      ,[productsizesum]\r\n  FROM [dbo].[vProductStock] ps \r\nleft join Products p on ps.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks s on s.StockID=ps.StockID", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetProductStockByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * from ProductStock where DeleteTime is null and ProductStockID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ProductStockInsertTransfer(int InvoiceStockTransferID, int UserID, int ProductID, string ProductSize, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("Insert into ProductStockTransfer (UserID,InvoiceStockTransferID,ProductID,\r\nProductSize,RegisterTime) \r\nValues (@UserID,@InvoiceStockTransferID,@ProductID,@ProductSize,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@InvoiceStockTransferID", InvoiceStockTransferID);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType ProductStockUpdateTransfer(int InvoiceStockTransferID, int ProductStockTransferID, int UserID, int ProductID, string ProductSize, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update ProductStockTransfer set UserID=@UserID,\r\nInvoiceStockTransferID=@InvoiceStockTransferID,ProductID=@ProductID,ProductSize=@ProductSize,RegisterTime=@RegisterTime,\r\nUpdateTime=getdate() where ProductStockTransferID=@ProductStockTransferID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@InvoiceStockTransferID", InvoiceStockTransferID);
        sqlCommand.Parameters.AddWithValue("@ProductStockTransferID", ProductStockTransferID);
        sqlCommand.Parameters.AddWithValue("@UserID", UserID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteGardenInformation(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update GardenInformations set DeleteTime=GetDate() where GardenInformationID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType GardenInformationInsert(int GardenID, int CompanyID, string Adress, string Area, string RegisterNumber, string RegistryNumber, string XCoordinate, string YCoordinate, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into GardenInformations (UserID,GardenID,CompanyID,Adress,Area,RegisterNumber,\r\nRegistryNumber,XCoordinate,YCoordinate,RegisterTime)  Values(@UserID, @GardenID,@CompanyID,@Adress,@Area,@RegisterNumber,\r\n@RegistryNumber,@XCoordinate,@YCoordinate,@RegisterTime)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@Adress", Adress);
        sqlCommand.Parameters.AddWithValue("@Area", Area.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        sqlCommand.Parameters.AddWithValue("@RegistryNumber", RegistryNumber);
        sqlCommand.Parameters.AddWithValue("@XCoordinate", XCoordinate);
        sqlCommand.Parameters.AddWithValue("@YCoordinate", YCoordinate);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType GardenInformationUpdate(int GardenInformationID, int GardenID, int CompanyID, string Adress, string Area, string RegisterNumber, string RegistryNumber, string XCoordinate, string YCoordinate, string RegisterTime)
    {
        SqlCommand sqlCommand = new SqlCommand("update GardenInformations set UserID=@UserID, GardenID=@GardenID,CompanyID=@CompanyID,\r\nAdress=@Adress,Area=@Area,RegisterNumber=@RegisterNumber,RegistryNumber=@RegistryNumber,XCoordinate=@XCoordinate,YCoordinate=@YCoordinate,\r\nRegisterTime=@RegisterTime where GardenInformationID=@GardenInformationID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@GardenInformationID", GardenInformationID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
        sqlCommand.Parameters.AddWithValue("@Adress", Adress);
        sqlCommand.Parameters.AddWithValue("@Area", Area.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterNumber", RegisterNumber);
        sqlCommand.Parameters.AddWithValue("@RegistryNumber", RegistryNumber);
        sqlCommand.Parameters.AddWithValue("@XCoordinate", XCoordinate);
        sqlCommand.Parameters.AddWithValue("@YCoordinate", YCoordinate);
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetGardenInformations()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT  row_number() over(order by gi.GardenInformationID desc) sn,\r\ngi.*,g.GardenName,c.CompanyName FROM GardenInformations gi left join Gardens g on gi.GardenID=g.GardenID\r\n  left join Companies c on gi.CompanyID=c.CompanyID where gi.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetGardenInformationByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT  row_number() over(order by gi.GardenInformationID desc) sn,\r\ngi.*,g.GardenName,c.CompanyName FROM GardenInformations gi left join Gardens g on gi.GardenID=g.GardenID\r\n  left join Companies c on gi.CompanyID=c.CompanyID where gi.DeleteTime is null and GardenInformationID=@id ", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetStocks()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by s.StockID) sn,\r\nStockID,StockName,g.GardenName from Stocks s left join Gardens g on s.GardenID=g.GardenID where s.DeleteTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetStockById(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by s.StockID) sn,\r\nStockID,StockName,g.GardenName,s.GardenID from Stocks s left join Gardens g on s.GardenID=g.GardenID \r\nwhere s.DeleteTime is null and s.StockID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType StockInsert(int GardenID, string StockName)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into Stocks (UserID,GardenID,StockName) \r\nValues(@UserID, @GardenID,@StockName)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@StockName", StockName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType StockUpdate(int StockID, int GardenID, string StockName)
    {
        SqlCommand sqlCommand = new SqlCommand("update Stocks set UserID=@UserID,GardenID=@GardenID,StockName=@StockName\r\nwhere StockID=@StockID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@GardenID", GardenID);
        sqlCommand.Parameters.AddWithValue("@StockName", StockName);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType DeleteStock(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update Stocks set DeleteTime=GetDate() where StockID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetInvoiceStatus()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT InvoiceStatusID\r\n      ,InvoiceStatusName\r\n  FROM InvoiceStatus", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetOrderInvoice()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select ROW_NUMBER() over(order by o.OrderInvoiceID desc) sn,o.OrderInvoiceID,o.UserID,o.InvoiceDate, o.StockID,o.InvoiceStatusID,\r\ns.StockName,i.InvoiceStatusName\r\nfrom OrderInvoice o\r\nleft join Stocks s on s.StockID=o.StockID \r\nleft join InvoiceStatus i on i.InvoiceStatusID=o.InvoiceStatusID\r\n where o.DeteletTime is null", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetOrderInvoiceByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select ROW_NUMBER() over(order by o.OrderInvoiceID desc) sn,o.UserID,o.InvoiceDate, o.StockID,o.InvoiceStatusID,\r\ns.StockName,i.InvoiceStatusName\r\nfrom OrderInvoice o\r\nleft join Stocks s on s.StockID=o.StockID \r\nleft join InvoiceStatus i on i.InvoiceStatusID=o.InvoiceStatusID\r\n where o.DeteletTime is null and o.OrderInvoiceID=@p", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@p", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOrderInvoice(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update OrderInvoice set DeleteTime=GetDate() where OrderInvoiceID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OrderInvoiceInsert(int StockID, string InvoiceDate, int InvoiceStatusID)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into OrderInvoice (UserID, InvoiceDate, StockID, InvoiceStatusID) \r\nValues(@UserID, @InvoiceDate, @StockID, @InvoiceStatusID)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@InvoiceDate", InvoiceDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OrderInvoiceUpdate(int OrderInvoiceID, int StockID, string InvoiceDate, int InvoiceStatusID)
    {
        SqlCommand sqlCommand = new SqlCommand("update OrderInvoice set UserID=@UserID,StockID=@StockID,InvoiceDate=@InvoiceDate,InvoiceStatusID=@InvoiceStatusID\r\nwhere OrderInvoiceID=@OrderInvoiceID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@InvoiceDate", InvoiceDate.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@InvoiceStatusID", InvoiceStatusID);
        sqlCommand.Parameters.AddWithValue("@OrderInvoiceID", OrderInvoiceID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetOrderProductByIDInvoice(int idinvoice)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select row_number() over(order by op.OrderProductID desc) sn,\r\n       p.[ProductsName],p.[ProductTypeID],pt.ProductTypeName\r\n      ,p.[ModelID],m.ModelName,[Code],p.[UnitMeasurementID] ,u.UnitMeasurementName,\r\n\t  op.UserID,op.OrderInvoiceID, op.ProductID,op.ProductSize,op.OrderProductID\r\n       from OrderProduct op left join[Products] p on op.ProductID=p.ProductID\r\n left join ProductTypes pt on p.ProductTypeID=pt.ProductTypeID\r\n left join Models m on p.ModelID=m.ModelID\r\n left join UnitMeasurements u on p.UnitMeasurementID=u.UnitMeasurementID\r\n where op.DeleteTime is null and op.OrderInvoiceID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", idinvoice);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable GetOrderProductByID(int id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select op.ProductID,op.ProductSize,p.ModelID,p.ProductTypeID from \r\nOrderProduct op inner join Products p on p.ProductID=op.ProductID\r\n where op.DeleteTime is null and op.OrderProductID=@id", SqlConn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType DeleteOrderProduct(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update OrderProduct set DeleteTime=GetDate() where OrderProductID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OrderProductInsert(int ProductID, int OrderInvoiceID, string ProductSize)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into OrderProduct (UserID, OrderInvoiceID, ProductID, ProductSize) \r\nValues(@UserID, @OrderInvoiceID, @ProductID, @ProductSize)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@OrderInvoiceID", OrderInvoiceID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public Types.ProsesType OrderProductUpdate(int OrderProductID, int ProductID, int OrderInvoiceID, string ProductSize)
    {
        SqlCommand sqlCommand = new SqlCommand("update OrderProduct set UserID=@UserID,ProductID=@ProductID,OrderInvoiceID=@OrderInvoiceID,ProductSize=@ProductSize\r\nwhere OrderProductID=@OrderProductID", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@OrderInvoiceID", OrderInvoiceID);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@OrderProductID", OrderProductID);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProductTransferByID(int id)
    {
        DataTable dataTable = new DataTable();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("\r\nSELECT row_number() over(order by pst.ProductStockTransferID desc) sn,\r\npst.*,p.ProductsName,m.ModelName,um.UnitMeasurementName,pt.ProductTypeName,pt.ProductTypeID,m.ModelID,\r\ns.StockName StockFromName,s1.StockName StockToName\r\n  FROM ProductStockTransfer pst\r\nleft join InvoiceStockTransfer ist on pst.InvoiceStockTransferID=ist.InvoiceStockTransferID\r\nleft join Products p on pst.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks s on s.StockID=ist.StockFromID\r\nleft join Stocks s1 on s1.StockID=ist.StockToID where pst.DeleteTime is null and\r\npst.ProductStockTransferID=@id", SqlConn);
        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("id", id);
        sqlDataAdapter.Fill(dataTable);
        return dataTable;
    }

    public Types.ProsesType DeleteProductTransfer(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update ProductStockTransfer set DeleteTime=GetDate() where ProductStockTransferID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }

    public DataTable GetProductStockOutput()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() over(order by ProductStockInputOutputID desc) sn,psio.*,s.StockName,pot.ProductOperationTypeName,pt.ProductTypeID,pt.ProductTypeName,\r\nsor.ReasonName,p.ProductsName,um.UnitMeasurementName,m.ModelID,m.ModelName FROM [ProductStockInputOutput] psio \r\nleft join ProductOperationTypes pot on psio.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join StockOperationReasons sor on psio.StockOperationReasonID=sor.StockOperationReasonID and sor.ProductOperationTypeID=pot.ProductOperationTypeID\r\nleft join Products p on psio.ProductID=p.ProductID\r\nleft join Models m on m.ModelID=p.ModelID\r\nleft join UnitMeasurements um on p.UnitMeasurementID=um.UnitMeasurementID\r\nleft join ProductTypes pt on pt.ProductTypeID=p.ProductTypeID\r\nleft join Stocks s on s.StockID=psio.StockID\r\n where psio.DeleteTime is null and psio.ProductOperationTypeID=2\r\n", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Types.ProsesType ProductStockOutputInsert(int StockID, int ProductID, string ProductSize, string RegisterTime, string Notes)
    {
        SqlCommand sqlCommand = new SqlCommand("insert into ProductStockInputOutput \r\n(UserID,StockID,ProductOperationTypeID,StockOperationReasonID,ProductID,\r\nProductSize,RegisterTime,Notes) values \r\n(@UserID,@StockID,@ProductOperationTypeID,@StockOperationReasonID,@ProductID,\r\n@ProductSize,@RegisterTime,@Notes)", SqlConn);
        sqlCommand.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToParseStr());
        sqlCommand.Parameters.AddWithValue("@StockID", StockID);
        sqlCommand.Parameters.AddWithValue("@ProductOperationTypeID", 2);
        sqlCommand.Parameters.AddWithValue("@StockOperationReasonID", 4);
        sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
        sqlCommand.Parameters.AddWithValue("@ProductSize", ProductSize.ToParseFloat());
        sqlCommand.Parameters.AddWithValue("@RegisterTime", RegisterTime.ToParseDatetime());
        sqlCommand.Parameters.AddWithValue("@Notes", Notes);
        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        return Types.ProsesType.Succes;
    }

    public Types.ProsesType DeleteProductOutput(int id)
    {
        SqlCommand sqlCommand = new SqlCommand("Update ProductStockInputOutput set DeleteTime=GetDate() where ProductStockInputOutputID=@id ", SqlConn);
        sqlCommand.Parameters.AddWithValue("@id", id);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            return Types.ProsesType.Succes;
        }
        catch (Exception)
        {
            return Types.ProsesType.Error;
        }
        finally
        {
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }
    public DataTable GetCurrency()
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT row_number() \r\nover(order by CurrencyID) sn,* from Currency", SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataTable GetCurrencyBYID(string id)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * from Currency where CurrencyID=" + id, SqlConn);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception)
        {
            return null;
        }
    }

}
#if false // Decompilation log
'33' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\mscorlib.dll'
------------------
Resolve: 'System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Web.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.dll'
------------------
Resolve: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Data.dll'
------------------
Resolve: 'System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Configuration.dll'
------------------
Resolve: 'System.Web.ApplicationServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Found single assembly: 'System.Web.ApplicationServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Web.ApplicationServices.dll'
#endif
