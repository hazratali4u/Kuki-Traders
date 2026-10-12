using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;


public partial class Forms_rptPurchaseVsSaleActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadCategories();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
           // txtFromDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           // txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtFromMonth.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtToMonth.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");

            txtFromMonth.Attributes.Add("readonly","readonly");
            
            txtToMonth.Attributes.Add("readonly","readonly");

        }
    }

    private void LoadCategories()
    {
        LstCategory.Items.Clear();

        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectSKUCategories(Constants.SKUCategory, true);
        clsWebFormUtil.FillListBox(LstCategory, dt, 0, 3, false);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
       // drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int p_Report_Type)
    {
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
       
            DateTime dtFromMonth = DateTime.Parse(txtFromMonth.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtToMonth.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);
        
       

        string Catagories_IDs = null;
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        for (int i = 0; i < LstCategory.Items.Count; i++)
        {
            if (LstCategory.Items[i].Selected == true)
            {
                Catagories_IDs += LstCategory.Items[i].Value.ToString() + ",";
            }
        }
        if (Catagories_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select Technology.');", true);
            return;
        }
        int source = Convert.ToInt32(ddlSource.SelectedValue);
        if (ddlSource.SelectedValue == "-1")
        {
            source = Constants.IntNullValue;
        }
        DataSet ds = RptInventoryCtl.SelectPurhcaseVsSale(dtTo, dtFrom, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Catagories_IDs, source, ddlSortBy.SelectedValue, ddlOrderBy.SelectedValue);

        SAMSBusinessLayer.Reports.CrpSaleVsPurchase CrpReport = new SAMSBusinessLayer.Reports.CrpSaleVsPurchase();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("source", ddlSource.SelectedItem.Text);
        CrpReport.SetParameterValue("fromdate", txtFromMonth.Text);
        CrpReport.SetParameterValue("todate", txtToMonth.Text);
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