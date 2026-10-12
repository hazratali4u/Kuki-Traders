using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer.Reports;

public partial class Forms_rptOutGoingPayment : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadAccountDetail();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }


    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
    //    this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }


    private void LoadAccountDetail()
    {
        SAMSCommonLayer.Classes.Configuration.GetAccountHead();

        AccountHeadController mAccountController = new AccountHeadController();
       
            //DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, long.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType), Convert.ToInt32(drpDistributor.SelectedValue));
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.IntNullValue, long.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType), 0);
          
            clsWebFormUtil.FillListBox(LstAccount, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);
    }

    private void ShowReport(int ReportType)
    {
        
        string Accounts_IDs = null;

        for (int i = 0; i < LstAccount.Items.Count; i++)
        {
            if (LstAccount.Items[i].Selected == true)
            {
                Accounts_IDs += LstAccount.Items[i].Value.ToString() + ",";
            }
        }
        if (Accounts_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select Account.');", true);

            return;
        }

        DataSet ds = null;

        RptAccountController RptAccountCtl = new RptAccountController();

        ds = RptAccountCtl.Outgoing_Payment(Accounts_IDs, int.Parse(drpDistributor.SelectedValue.ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0);

        if (ds.Tables["UspOutGoingPaymentReport"].Rows.Count > 0)
        {
            dvError.Visible = false;

            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            CrpOutgoingPayment CrpReport = new CrpOutgoingPayment();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);


            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", ReportType);

            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            dvError.Visible = true;
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
        //DocumentPrintController DPrint = new DocumentPrintController();
        //RptAccountController RptAccountCtl = new RptAccountController();

        //DataSet ds = null;

        //ds = RptAccountCtl.GeneralLedger_View(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
        //           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0);

        //decimal OpeningValue = RptAccountCtl.GeneralLedgerOpening(Constants.IntNullValue, Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
        //          DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0);

        //DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        //SAMSBusinessLayer.Reports.CrpLedgerView CrpReport = new SAMSBusinessLayer.Reports.CrpLedgerView();
        //CrpReport.SetDataSource(ds);
        //CrpReport.Refresh();

        //CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        //CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        //CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        //CrpReport.SetParameterValue("Principal", "All");
        //CrpReport.SetParameterValue("OpeningValue", OpeningValue.ToString());
        //CrpReport.SetParameterValue("opType", opType);

        //string path = SAMSCommonLayer.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        //CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        //CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        //System.IO.FileInfo file = new System.IO.FileInfo(path);

        //if (file.Exists)
        //{
        //    Response.Clear();

        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

        //    Response.AddHeader("Content-Length", file.Length.ToString());

        //    Response.ContentType = "application/octet-stream";

        //    Response.WriteFile(file.FullName);

        //    Response.End();

        //}
        //else
        //{
        //    Response.Write("This file does not exist.");
       // }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountDetail();
    }
}