using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

public partial class Forms_rptDailyCollection : System.Web.UI.Page
{

    CustomerDataController CustCtl = new CustomerDataController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();

            LoadData();
            LoadArea();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    private void LoadArea()
    {
      
        DrpRoute.Items.Clear();
        GeoHierarchyController gController = new GeoHierarchyController();
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(DrpRoute, dt, "GEO_ID", "GEO_NAME");

        DataTable dtCustomer = CustCtl.GetCustomerByID(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(DrpCustomer.SelectedValue),1);
        if (dtCustomer.Rows.Count > 0)
        {
            if (dtCustomer.Rows[0]["townId"].ToString() != "")
            {
                DrpRoute.SelectedValue = dtCustomer.Rows[0]["townId"].ToString();
            }
        }
    }
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "customerDetail");
        }
    }
  
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadData();
    }
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCustomer = CustCtl.GetCustomerByID(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(DrpCustomer.SelectedValue),1);
        if (dtCustomer.Rows.Count > 0)
        {
            if (dtCustomer.Rows[0]["townId"].ToString() != "")
            {
                DrpRoute.SelectedValue = dtCustomer.Rows[0]["townId"].ToString();
            }
        }
    }
    private void ShowReport(int p_Report_Type)
    {
      
       
        RptCustomerController RptCustomerCtl = new RptCustomerController();

        DataSet ds;
        ds = RptCustomerCtl.CustomerCollection(int.Parse(drpDistributor.SelectedValue.ToString()), long.Parse(DrpCustomer.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Convert.ToInt32(drpReportType.SelectedValue));

        if (ds.Tables["spDailyCollection"].Rows.Count > 0)
        {
            dvMsg.Visible = false;
            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            SAMSBusinessLayer.Reports.CrpDailyCollection CrpReport = new SAMSBusinessLayer.Reports.CrpDailyCollection();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Type", drpReportType.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_Report_Type);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        { 
            dvMsg.Visible = true;
           
        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}