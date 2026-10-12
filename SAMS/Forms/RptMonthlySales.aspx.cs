using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For  Customer Monthly Reports
/// </summary>
public partial class Forms_RptMonthlySales : System.Web.UI.Page
{
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
            this.LoadChannelType();
            this.LoadCategory();
            this.LoadSKU();

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

        //DataTable dt4 = dt.Clone();
        //DataRow[] dr1 = dt.Select("SUBZONE_ID = 5");

        //if (dr1.Length > 0)
        //{
        //    for (int k = 0; k < dr1.Length; k++)
        //    {
        //        dt4.ImportRow(dr1[k]);
        //    }
        //}

       // clsWebFormUtil.FillDropDownList(this.ddlStore, dt4, 0, 2, true);
        
        
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    
    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);

    }

    /// <summary>
    /// Loads Categoreis To Category Combo
    /// </summary>
    private void LoadCategory()
    {
        DrpCategory.Items.Clear();
   
        SkuHierarchyController mSkuHieController = new SkuHierarchyController();
        //DataTable dtDist = mSkuHieController.SelectDTSkuHierarchy01(2, int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DataTable dtCatagory = mSkuHieController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);

        DrpCategory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpCategory, dtCatagory, 0, 3);
    }

   

    /// <summary>
    /// Loads Sale Forces, Categories And SKUS
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategory();
        this.LoadSKU();
    }

    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSKU()
    {
        SkuController mSKUController = new SkuController();

        DataTable m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpCategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        
        this.DrpSKU .Items.Clear();
        this.DrpSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpSKU, m_SKUDt, "SKU_ID", "skuDetail");
    }

    /// <summary>
    /// Loads SKUS
    /// </summary>
    protected void DrpCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKU();
    }
    
    /// <summary>
    /// Shows  Customer Monthly Reports Either in PDF Or in Excel
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();

        DataControl dc = new DataControl();
        DataSet ds = RptCustomerCtl.SelectCustomerMonthlySales(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpCategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(drpChannelType.SelectedValue.ToString()), DrpReportType.SelectedIndex, Convert.ToInt32(DrpSKU.SelectedValue), Constants.IntNullValue);

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

       CrpCustomerMonthlySales CrpReport = new CrpCustomerMonthlySales();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
        CrpReport.SetParameterValue("ReportType", "Customer Monthly " + DrpReportType.SelectedItem.Text + " Report");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("OrderBooker","All");
        CrpReport.SetParameterValue("SKU", DrpSKU.SelectedItem.Text);
        CrpReport.SetParameterValue("Category", DrpCategory.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Report_Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows  Customer Monthly Reports in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows  Customer Monthly Reports in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}
