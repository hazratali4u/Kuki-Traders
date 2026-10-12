using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_frmPrincipalInvoice : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadRoute();
            this.LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }
        
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        var mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlStore, mDt, 0, 1, true);
        var dController = new DistributorController();
        var dt3 = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        var dt2 = dt3.Clone();
        var dr = dt3.Select("SUBZONE_ID = 5");

        if (dr.Length > 0)
        {
            foreach (var t in dr)
            {
                dt2.ImportRow(t);
            }
        }

        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt2, 0, 2, true);
    }
        
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        if (drpDistributor.Items.Count > 0)
        {
            

            ddlTown.Items.Clear();
            var gController = new GeoHierarchyController();
            var dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
            ddlTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlTown, dt, 0, 1);
        }
        else
        {
            ddlTown.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 )
        {
            var mController = new CustomerDataController();
            var dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }

    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
    }
    
    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="pReportType">Type</param>
    private void ShowReport(int pReportType)
    {
        var pCustomerType = Constants.IntNullValue;
        if (rblCustomerType.SelectedValue != "-1")
        {
            pCustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
        }

        var rptSaleCtl = new RptSaleController();
        var dPrint = new DocumentPrintController();
        DataSet ds = null;

        DataTable dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        if (dt.Rows.Count > 0)
        {


            // mean sale order
            var dc = new DataControl();
            ds = rptSaleCtl.SelectDocumentforPrint5(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 3, Constants.LongNullValue, pCustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(ddlTown.SelectedValue), 0);


            ReportDocument crpReport = new CrpPrintDocumentPrincipal();

            var subReport = crpReport.OpenSubreport("SubReportReplacement");
            var subReport2 = crpReport.OpenSubreport("SubReportDiscount");
            var subReport3 = crpReport.OpenSubreport("SubReportSalesTax");
            var subReport4 = crpReport.OpenSubreport("SubReportUnloading");
            var subReport5 = crpReport.OpenSubreport("SubReportFreeSku");
            crpReport.SetDataSource(ds);
            subReport.SetDataSource(ds);
            subReport2.SetDataSource(ds);
            subReport3.SetDataSource(ds);
            subReport4.SetDataSource(ds);
            subReport5.SetDataSource(ds);
            crpReport.Refresh();

            if (rbtSortOrder.SelectedIndex == 0)
            {
                crpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
            }
            else
            {
                crpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder;
            }

            crpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("NtnNumber", "NTN :" + dt.Rows[0]["NTN_NO"].ToString());
            crpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpReport.SetParameterValue("BranchAddress", "Email: " + dt.Rows[0]["ADDRESS2"].ToString());
            crpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
           // CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
            crpReport.SetParameterValue("DISTRIBUTOR_NAME", ddlStore.SelectedItem .Text );
            crpReport.SetParameterValue("CONTACT_NUMBER", "Ph: " + dt.Rows[0]["CONTACT_NUMBER"].ToString());
            crpReport.SetParameterValue("PrintType", "0");
            this.Session.Add("CrpReport", crpReport);
            this.Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            var cstype = this.GetType();
            var cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }

    }
  
    

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
