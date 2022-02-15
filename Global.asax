<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        System.Web.Routing.RouteCollection routes = System.Web.Routing.RouteTable.Routes;
        routes.MapPageRoute("home", "home", "~/Default.aspx");
        routes.MapPageRoute("companies", "companies", "~/Companies.aspx");
        routes.MapPageRoute("garden", "garden", "~/Gardens.aspx");
        routes.MapPageRoute("zones", "zones", "~/Zones.aspx");
        routes.MapPageRoute("sectors", "sectors", "~/Sectors.aspx");
        routes.MapPageRoute("lines", "lines", "~/Lines.aspx");
        routes.MapPageRoute("brands", "brands", "~/Brands.aspx");
        routes.MapPageRoute("models", "models", "~/Models.aspx");
        routes.MapPageRoute("unitmeasurements", "unitmeasurements", "~/UnitMeasurements.aspx");
        routes.MapPageRoute("treetypes", "treetypes", "~/TreeTypes.aspx");
        routes.MapPageRoute("producttypes", "producttypes", "~/ProductTypes.aspx");
        routes.MapPageRoute("works", "works", "~/Works.aspx");
        
        
     
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
