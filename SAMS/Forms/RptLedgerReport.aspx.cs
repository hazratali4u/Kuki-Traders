using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport : System.Web.UI.Page
{

    static string opType = "CR";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadAccountDetail();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

   
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

 
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        if (drpDistributor.SelectedValue == Constants.IntNullValue.ToString())
        {
           // DataTable dtHead = mAccountController.SelectAccountHead(Constants.IntNullValue, Constants.LongNullValue, 0);
            DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME",true);
        }
        else
        {
            DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, Constants.LongNullValue, Convert.ToInt32(drpDistributor.SelectedValue));
            clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME",true);
        }
    }

   
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        DataControl dc = new DataControl();
        ds = RptAccountCtl.GeneralLedger_View(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),Convert.ToInt32(rdbPostType.SelectedValue));

             DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

             decimal OpeningValue = RptAccountCtl.GeneralLedgerOpening(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Convert.ToInt32(rdbPostType.SelectedValue));

             if (OpeningValue > 0)
             {
                 opType = "DR";
             }
             else if (OpeningValue==0) 
             {

                 opType = "";
             }

             SAMSBusinessLayer.Reports.CrpLedgerView CrpReport = new SAMSBusinessLayer.Reports.CrpLedgerView();

             ReportDocument subReport = CrpReport.OpenSubreport("PostCheques");

             ReportDocument subReport2 = CrpReport.OpenSubreport("PostCheques2");

             CrpReport.SetDataSource(ds);
             subReport.SetDataSource(ds);
             subReport2.SetDataSource(ds);
             
             CrpReport.Refresh();

             CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
             CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
             CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
             CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
             CrpReport.SetParameterValue("Principal", "All");
             CrpReport.SetParameterValue("opType",opType);
             CrpReport.SetParameterValue("OpeningValue", OpeningValue.ToString());
            
             this.Session.Add("CrpReport", CrpReport);
             this.Session.Add("ReportType", 0);
             string url = "'Default.aspx'";
             string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
             Type cstype = this.GetType();
             ClientScriptManager cs = Page.ClientScript;
             cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

  
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        ds = RptAccountCtl.GeneralLedger_View(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                   DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),Convert.ToInt32(rdbPostType.SelectedValue));

        decimal OpeningValue = RptAccountCtl.GeneralLedgerOpening(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),Convert.ToInt32(rdbPostType.SelectedValue));

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        SAMSBusinessLayer.Reports.CrpLedgerView CrpReport = new SAMSBusinessLayer.Reports.CrpLedgerView();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "All");
        CrpReport.SetParameterValue("OpeningValue", OpeningValue.ToString());
        CrpReport.SetParameterValue("opType", opType);

        string path = SAMSCommonLayer.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountDetail();
    }
}
