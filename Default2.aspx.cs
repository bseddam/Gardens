using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    SerialPort _serialPort;
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] portnames = SerialPort.GetPortNames();

        foreach (var item in portnames)
        {

        }
    }
    void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        Thread.Sleep(500);
        String data = _serialPort.ReadLine();
        //this.BeginInvoke(new SetTextDeleg(si_DataReceived), new Object[] { data });
    }

    private void si_DataReceived(String data)
    {
        TextBox1.Text = data.Trim().ToString();
    }
}