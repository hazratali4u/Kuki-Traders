using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Town Wise Sales Report
/// </summary>
public partial class Forms_RptTownWiseSales : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();

            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        DrpPrincipal.Items.Clear();
        var pController = new SKUPriceDetailController();
        var mDt = pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, mDt, 0, 1, false); 
        
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));
        DataTable dt4 = dt.Clone();
        DataRow[] dr1 = dt.Select("SUBZONE_ID = 5");

        if (dr1.Length > 0)
        {
            for (int k = 0; k < dr1.Length; k++)
            {
                dt4.ImportRow(dr1[k]);
            }
        }
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt4, 0, 2);
    }

    /// <summary>
    /// Shows Town Wise Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        var dPrint = new DocumentPrintController();
        var rptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        var dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        var dc = new DataControl();
        ds = rptCustomerCtl.SelectTownWiseSales(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbtType.SelectedIndex);

        var crpReport = new CrpTownWiseSales();
        
            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");

            this.Session.Add("ReportType", 0);
            this.Session.Add("CrpReport", crpReport);
        
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = this.GetType();
        var cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Town Wise Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        var dPrint = new DocumentPrintController();
        var rptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        var dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

       
        ds = rptCustomerCtl.SelectTownWiseSales(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbtType.SelectedIndex);

        using (var crpReport = new CrpTownWiseSales())
        {
            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");

            this.Session.Add("ReportType", 1);
            this.Session.Add("CrpReport", crpReport);
        }
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = this.GetType();
        var cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}
