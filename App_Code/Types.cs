using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Types
{
    public enum ProsesType
    {
        Succes = 1,
        Error = 0
    }
    public string PageType(int typeid)
    {
        string typename = "";
        if (typeid == 1)
        {
            typename = "TeElmiShura";
        }
        else if (typeid == 2)
        {
            typename = "TeElmiMuessise";
        }
        else if (typeid == 3)
        {
            typename = "TeElmSaheleri";
        }
        else if (typeid == 4)
        {
            typename = "TeIxtisasIndex";
        }
        else if (typeid == 5)
        {
            typename = "TeElmiDerece";
        }
        else if (typeid == 6)
        {
            typename = "TeElmiAdlar";
        }
        else if (typeid == 7)
        {
            typename = "TeTehsilFormasi";
        }
        else
        {
            typename = "";
        }
        return typename;
    }


}