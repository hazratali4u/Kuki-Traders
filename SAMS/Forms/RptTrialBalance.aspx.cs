using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For  Trial Balance Report
/// </summary>
public partial class Forms_RptTrialBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadAccount();
            //this.LoadAccountDetail();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Main Account Heads To Main Account Combo
    /// </summary>
    private void LoadAccount()
    {
        AccountHeadController MController = new AccountHeadController();
        DataTable dt = MController.SelectAccountHead(Constants.AC_MainTypeId, Constants.LongNullValue);
        this.DrpMainAccount.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpMainAccount , dt, 0, 10);   
    }

    /// <summary>
    /// Loads Account Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        DataView dv = new DataView(dtHead);
        dv.Sort = "ACCOUNT_DETAIL";
        dtHead = dv.ToTable(); 
        clsWebFormUtil.FillDropDownList(this.ddlFromAccount, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL",true);
        clsWebFormUtil.FillDropDownList(this.ddlToAccount, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
        this.Session.Add("dtHead", dtHead);
    }

    /// <summary>
    /// Shows Trial Balance in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {

        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataControl dc = new DataControl();
        ReportDocument CrpReport = new ReportDocument();

        DataSet  ds = RptAccountCtl.TrialBalance(Constants.IntNullValue,int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(DrpMainAccount.SelectedValue.ToString()),    
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),int.Parse(DrpLevel.SelectedValue.ToString()),ddlFromAccount.SelectedItem.Text.Substring(0,10) ,ddlToAccount.SelectedItem.Text.Substring(0,10) ,Convert.ToInt32(rdbPostType.SelectedValue));
        
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
      
        if (drpTrialLevel.SelectedValue == "Six")
        {
          CrpReport = new CrpTrialBalance();
        }
        else {
             CrpReport = new CrpTrialBalance2();
        
        }

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "All");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        
    }

    /// <summary>
    /// Shows Trial Balance in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataControl dc = new DataControl();

        DataSet ds = RptAccountCtl.TrialBalance(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpMainAccount.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLevel.SelectedValue.ToString()), ddlFromAccount.SelectedItem.Text.Substring(0, 10), ddlToAccount.SelectedItem.Text.Substring(0, 10), Convert.ToInt32(rdbPostType.SelectedValue));
        
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        SAMSBusinessLayer.Reports.CrpTrialBalance CrpReport = new SAMSBusinessLayer.Reports.CrpTrialBalance();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "All");
        
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

    /// <summary>
    /// Checks All Account Heads In Account Head ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbSelectAll.Checked == true)
        {
            ddlToAccount.Items.Add(new ListItem("9999999999","0"));
            ddlFromAccount.Items.Add(new ListItem("0000000000", "0"));
            ddlToAccount.Enabled = false;
            ddlFromAccount.Enabled = false;
        }
        else
        {
            this.LoadAccountDetail();
            ddlToAccount.Enabled = true;
            ddlFromAccount.Enabled = true;
        }
    }
}
