using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Reports;

public partial class Forms_rptOrderVsDispatch : System.Web.UI.Page
{

    CustomerDataController CustCtl = new CustomerDataController();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadData();

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

  
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "customerDetail");
        }
    }

   
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

   

    private void ShowReport(int p_Report_Type)
    {
        DataControl dc = new DataControl();
        DocumentPrintController DPrint = new DocumentPrintController();
        RptInventoryController RptInventCtl = new RptInventoryController();


        DataSet ds = RptInventCtl.SelectOrderVsDispatch(DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(drpDistributor.SelectedValue.ToString())
            , int.Parse(Session["UserId"].ToString()), int.Parse(DrpCustomer.SelectedValue), Convert.ToInt32(rdbReportType.SelectedValue));


        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        

        if (rdbReportType.SelectedValue == "0")
        {
            CrpOrderVsDispatch CrpReport = new CrpOrderVsDispatch();

            CrpReport.SetDataSource(ds);


            CrpReport.Refresh();

            CrpReport.SetParameterValue("fromdate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("todate", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            Session.Add("CrpReport", CrpReport);
        }
        else
        {

            CrpOrderVsDispatchDateWise CrpReport = new CrpOrderVsDispatchDateWise();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("fromdate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("todate", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            Session.Add("CrpReport", CrpReport);
        }
      
        Session.Add("ReportType", p_Report_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
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