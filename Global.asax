<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        System.Web.Routing.RouteCollection routes = System.Web.Routing.RouteTable.Routes;
        routes.MapPageRoute("home", "home", "~/Default.aspx");
        routes.MapPageRoute("products", "products", "~/Products.aspx");
        routes.MapPageRoute("companies", "companies", "~/Companies.aspx");
        routes.MapPageRoute("garden", "garden", "~/Gardens.aspx");
        routes.MapPageRoute("zones", "zones", "~/Zones.aspx");
        routes.MapPageRoute("sectors", "sectors", "~/Sectors.aspx");
        routes.MapPageRoute("lines", "lines", "~/Lines.aspx");
    
        routes.MapPageRoute("models", "models", "~/Models.aspx");
        routes.MapPageRoute("unitmeasurements", "unitmeasurements", "~/UnitMeasurements.aspx");
        routes.MapPageRoute("treetypes", "treetypes", "~/TreeTypes.aspx");
        routes.MapPageRoute("producttypes", "producttypes", "~/ProductTypes.aspx");
        routes.MapPageRoute("works", "works", "~/Works.aspx");
        routes.MapPageRoute("cadres", "cadres", "~/Cadres.aspx");
        routes.MapPageRoute("positions", "positions", "~/Positions.aspx");
        routes.MapPageRoute("structure", "structure", "~/Structure.aspx");
        routes.MapPageRoute("technique", "technique", "~/Technique.aspx");
        routes.MapPageRoute("wateringsystems", "wateringsystems", "~/WateringSystems.aspx");
        routes.MapPageRoute("operationwateringsystems", "operationwateringsystems", "~/OperationWateringSystems.aspx");
        routes.MapPageRoute("operationtechniques", "operationtechniques", "~/OperationTechniques.aspx");
        routes.MapPageRoute("operationcadres", "operationcadres", "~/OperationCadres.aspx");
        routes.MapPageRoute("operationstockinput", "operationstockinput", "~/OperationStockInput.aspx");
        routes.MapPageRoute("operationstocktransfer", "operationstocktransfer", "~/OperationStockTransfer.aspx");
        routes.MapPageRoute("operationtechniqueservices", "operationtechniqueservices", "~/OperationTechniqueServices.aspx");
        routes.MapPageRoute("otherexpenses", "otherexpenses", "~/OtherExpenses.aspx");
        routes.MapPageRoute("employmenthistory", "employmenthistory", "~/EmploymentHistory.aspx");
        routes.MapPageRoute("weathercondition", "weathercondition", "~/WeatherCondition.aspx");
        routes.MapPageRoute("treeage", "treeage", "~/Treeage.aspx");
        routes.MapPageRoute("cards", "cards", "~/Cards.aspx");
        routes.MapPageRoute("users", "users", "~/Users.aspx");
        routes.MapPageRoute("login", "login", "~/Login.aspx");
        routes.MapPageRoute("reportexitentry", "reportexitentry", "~/ReportExitEntry.aspx");
        routes.MapPageRoute("countries", "countries", "~/Countries.aspx");
        routes.MapPageRoute("trees", "trees", "~/Trees.aspx");
        routes.MapPageRoute("treescount", "treescount", "~/TreesCount.aspx");
        routes.MapPageRoute("orders", "orders", "~/Orders.aspx");
        routes.MapPageRoute("gardeninformation", "gardeninformation", "~/GardenInformation.aspx");
        routes.MapPageRoute("stock", "stock", "~/Stock.aspx");
        routes.MapPageRoute("productsexit", "productsexit", "~/OperationProductsExit.aspx");
        routes.MapPageRoute("polescount", "polescount", "~/PolesCount.aspx");
        routes.MapPageRoute("productstocks", "productstocks", "~/ProductStocks.aspx");

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
