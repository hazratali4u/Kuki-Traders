
using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Reports;
public partial class Forms_rptFundPosition : System.Web.UI.Page
{
     CustomerDataController CustCtl = new CustomerDataController();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
           
            LoadAccountHead();
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


    private void LoadAccountHead()
    {
        LstCategory.Items.Clear();

        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();

       
            DataTable dtCash = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommonLayer.Classes.Configuration.CashDefaultType));
           
            DataTable dtBanks = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType));
           

        DataTable merged = new DataTable();
        merged.Merge(dtCash);
        merged.Merge(dtBanks);

        clsWebFormUtil.FillListBox(LstCategory, merged, 0, 4, false);
    }



    private void ShowReport(int p_Report_Type)
    {
        string Catagories_IDs = null;
        
        for (int i = 0; i < LstCategory.Items.Count; i++)
        {
            if (LstCategory.Items[i].Selected == true)
            {
                Catagories_IDs += LstCategory.Items[i].Value.ToString() + ",";
            }
        }
        if (Catagories_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select Account Head.');", true);
            return;
        }

        DataControl dc = new DataControl();
       
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        
        DataSet ds;
        ds = RptAccountCtl.FundPosition(int.Parse(drpDistributor.SelectedValue.ToString()), Catagories_IDs, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 00:00:00"));

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        CrpFundPosition CrpReport = new CrpFundPosition();

        CrpReport.SetDataSource(ds);
        
        CrpReport.Refresh();

        CrpReport.SetParameterValue("date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("ToDate", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("companyName", dt.Rows[0]["COMPANY_NAME"].ToString());
       
        Session.Add("CrpReport", CrpReport);
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
