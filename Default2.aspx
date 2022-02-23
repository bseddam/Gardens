<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript">
        function textchange() {
            var txtFirstNo = document.getElementById('txtFirstNo').value.toString();
            if (txtFirstNo.length > 4) {
                document.getElementById('txtResult').value = txtFirstNo;
            }
        }
    </script>
        <div style="border:1px solid gray;width: 450px; height:300px">
        <h2>Add two textbox values without pressing anybuttons</h2>
        <input type="text" id="txtFirstNo" placeholder="pleaseenterFirst Number" onkeyup="textchange()" />
        
            <br />
        <div style="padding-top:10px">
            Result:
            <input type="text" id="txtResult" />
        </div></div>
       
    </form>
</body>
</html>
