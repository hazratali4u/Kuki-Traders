using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;

/// <summary>
/// Form For Customer Ledger Report
/// </summary>
public partial class Forms_RptCustomerLedger : System.Web.UI.Page
{
    CustomerDataController CustCtl = new CustomerDataController();
    public static string opType = "CR";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadCustomer();
           Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    

    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 )
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, 2);

            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "customerDetail", true);
        }
    }

    private decimal LoadCustomerOpBalance()
    {
        if (drpDistributor.Items.Count > 0 && DrpCustomer.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomerOpBalance(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), Constants.IntNullValue);

            if (Convert.ToDecimal(dt.Rows[0][0]) < 0)
            {
                opType = "DR";
            }
            else if (Convert.ToDecimal(dt.Rows[0][0]) == 0)
            {
                opType = "";
            }

            return decimal.Parse(dt.Rows[0][0].ToString());

        }
        return 0;
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    

    private void ShowReport(int p_Report_Type)
    {

        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();

        var customer = DrpCustomer.SelectedItem.Text.Split('[');

        DataSet ds = null;
        {
            DataControl dc = new DataControl();
            ds = RptCustomerCtl.CustomerLedger_View2(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            SAMSBusinessLayer.Reports.CrpCustomerLedger2 CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerLedger2();

            ReportDocument subReport = CrpReport.OpenSubreport("SubReport");

            CrpReport.SetDataSource(ds);
            subReport.SetDataSource(ds);

            CrpReport.Refresh();

            CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Town_Name", customer[1].Trim(']'));
            CrpReport.SetParameterValue("Customer", customer[0]);
            CrpReport.SetParameterValue("Op_Balance", LoadCustomerOpBalance());
            CrpReport.SetParameterValue("Op_Type", opType);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_Report_Type);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
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
