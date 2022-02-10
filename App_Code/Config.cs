using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

public class Config
{
    //Adminmi ?
    public static bool Adminmi
    {
        get
        {
            return ConvertString(HttpContext.Current.Session["AdminLogin"]).Length > 0;
        }
    }
    public static string Slug(string x)
    {
        x = HtmlRemoval.StripTagsRegex(x);
        x = x.ToLower().Replace("<", "");
        x = x.Replace(">", "");
        x = x.Replace("(", "");
        x = x.Replace(")", "");
        x = x.Replace("`", "");
        x = x.Replace("'", "");
        x = x.Replace("\"", "");
        x = x.Replace("?", "");
        x = x.Replace("!", "");
        x = x.Replace("/", "");
        x = x.Replace("\\", "");
        x = x.Replace("&", "");
        x = x.Replace("#", "");
        x = x.Replace("~", "");
        x = x.Replace("%", "");

        x = x.Replace(" ", "-");
        x = x.Replace("&nbsp;", "-");

        x = x.Replace("ə", "e");
        x = x.Replace("ü", "u");
        x = x.Replace("ç", "ch");
        x = x.Replace("ö", "o");
        x = x.Replace("ğ", "g");
        x = x.Replace("ş", "s");
        x = x.Replace("ı", "i");
        x = x.Replace(",", "");
        x = x.Replace(".", "");
        x = x.Replace(":", "");
        x = x.Replace("”", "");
        x = x.Replace("“", "");

        x = x.Trim('.');
        x = x.Trim();

        int length = x.Length;

        if (length > 100)
        {
            length = 100;
        }
        return x.Substring(0, length);
    }
    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
    public static string AdminLang
    {
        get
        {
            return ConvertString(HttpContext.Current.Session["AdminLogin"]);
        }
    }

    //Bunun lazım olan bütün səhifələrin ilk Page_Load -na qoyuruq. 
    //Lazım olan bütün setting əmməliyyatlarını bura əlavə et.
    public static void PageSettings()
    {
        HttpContext.Current.Response.Cache.SetNoStore(); //Templəri bağlayırıq...
        HttpContext.Current.Session.Timeout = 10080;
        HttpContext.Current.Server.ScriptTimeout = 9999; //Əgər yüklənmə gecikərsə maksimum gözləmə saniyəsi.
        System.Threading.Thread.Sleep(0);
    }

    //Language
    public static string Lang
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["Lang"] == null) return "az";
            return (Config.ConvertString(HttpContext.Current.Request.Cookies["Lang"].Value) == "en" ? "en" : "az");
        }
    }
    public static string getLangSession
    {
        get
        {
            string lang = HttpContext.Current.Session["lang"].ToParseStr();
            return lang.Length == 0 ? "az" : lang;
        }
    }
    public static string getLang(Page p)
    {
        string lang = p.RouteData.Values["lang"].ToParseStr();
        HttpContext.Current.Session["lang"] = lang;
        return lang.Length == 0 ? "az" : lang;
    }
   
    
    public static string Cs(object s)
    {
        if (s == null) s = "";
        return Convert.ToString(s);
    }
    //Get WebConfig.config App Key
    public static string GetAppSetting(string KeyName)
    {
        return ConvertString(ConfigurationManager.AppSettings[KeyName]);
    }

    //Fileupload-da gələn şəkilin ölçüsünü kəsir (100x??px).
    public static Unit PicturesSizeSplit(string s)
    {
        try
        {
            int i = Convert.ToInt16(s.Substring(0, 3).Trim().Trim('x'));
            if (i < 220)
                return Unit.Pixel(i);
            else return Unit.Pixel(220);
        }
        catch { return Unit.Pixel(220); }
    }

    //Açar yaradaq.
    public static string Key(int say)
    {
        Random ran = new Random();
        string veri = "QAZWXEDCRFVTGBYHNUJMKP123456789";
        string acar = "";
        for (int i = 1; i <= say; i++)
        {
            acar += veri.Substring(ran.Next(veri.Length - 1), 1);
        }
        return acar.Trim();
    }

    //Set title to url (clear latin and special simvols)
    public static string ClrTitle(string Title)
    {
        Title = Title.ToLower().Trim().Trim('-').Trim('.').Trim();
        Title = Title.Replace("   ", " ");
        Title = Title.Replace("  ", " ");
        Title = Title.Replace(" ", "-");
        Title = Title.Replace(",", "-");
        Title = Title.Replace("\"", "");

        Title = Title.Replace("---", "-");
        Title = Title.Replace("--", "-");
        Title = Title.Replace("#", "");
        Title = Title.Replace("&", "");

        //No Latin
        Title = Title.Replace("ü", "u");
        Title = Title.Replace("ı", "i");
        Title = Title.Replace("ö", "o");
        Title = Title.Replace("ğ", "g");
        Title = Title.Replace("ə", "e");
        Title = Title.Replace("ç", "ch");
        Title = Title.Replace("ş", "sh");

        return Title;
    }

    public static string ClearInjection(string x)
    {
        x = x.Replace("<", "&lt;");
        x = x.Replace(">", "&gt;");
        x = x.Replace("`", "");
        x = x.Replace("'", "");
        x = x.Replace("\"", "");
        x = x.Replace("?", "");
        x = x.Replace("!", "");
        x = x.Replace("/", "");
        x = x.Replace("\\", "");
        x = x.Replace("&", "");
        x = x.Replace("#", "");
        x = x.Replace("~", "");
        x = x.Replace("%", "");
        x = x.Trim('.');
        x = x.Trim();
        return x;
    }

    //ConvertString.
    public static string ConvertString(object s)
    {
        if (s == null) s = "";
        return Convert.ToString(s);
    }

    //Numaric testi.
    public static bool IsNumeric(string s)
    {
        if (s == null) return false;
        if (s.Length < 1) return false;
        for (int i = 0; i < s.Length; i++)
        {
            if ("0123456789".IndexOf(s.Substring(i, 1)) < 0) { return false; }
        }
        return true;
    }

    //MessagesBox.
    public static void MsgBox(string mstx, Page P)
    {
        P.ClientScript.RegisterClientScriptBlock(P.GetType(), "PopupScript", "window.focus(); alert('" + mstx + "');", true);
    }

    //Sha1  - özəl
    public static string Sha1(string Pwd)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        string str = "Raqif{0}Seddam";
        str = String.Format(str, Pwd);
        byte[] buffer = new byte[str.Length];
        buffer = System.Text.Encoding.UTF8.GetBytes(str);
        result = sha.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }

    //Səhifəni yönləndirək:
    public static void Rd(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl);
        HttpContext.Current.Response.End();
        return;
    }

    //Menu sort #-den sonrasin goturur
    public static string SplitDiez(object x)
    {
        string v = ConvertString(x);
        try { v = ConvertString(v.Split('#').GetValue(1)); }
        catch { }
        return v;
    }

    //Tarix yaradaq istifadəçilər üçün:
    public static string Date(string x)
    {
        //Əgər boş gələrsə hər ikisidə return edilir.
        string tarix = "dd.MM.yyyy HH:mm";
        if (x.Length > 1) tarix = x;
        if (x == "t") tarix = "dd.MM.yyyy";
        if (x == "z") tarix = "HH:mm:ss";
        tarix = DateTime.Now.AddHours(2).ToString(tarix);
        return tarix;
    }
}
