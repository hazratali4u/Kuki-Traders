using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;

public partial class Forms_RptGeoHierarchy : System.Web.UI.Page
{

    readonly GeoHierarchyController gController = new GeoHierarchyController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadZone();
            LoadRegion();
            LoadDistrict();
            
        }
    }



    protected void LoadZone()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        
        drpZone.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
       
        clsWebFormUtil.FillDropDownList(drpZone, dt, 0, 4);
    }

    protected void LoadRegion()
    {

        drpRegion.Items.Clear();

        if (drpZone.Items.Count > 0)
        {
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpZone.SelectedValue), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            
            drpRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
           
            clsWebFormUtil.FillDropDownList(drpRegion, dt, 0, 4);
        }


    }
    protected void LoadDistrict()
    {

        drpDistrict.Items.Clear();
        if (drpRegion.Items.Count > 0)
        {
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpRegion.SelectedValue), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            drpDistrict.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpDistrict, dt, 0, 4);
        }


    }

    protected void drpZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRegion();
    }
    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict();
    }

    private void ShowReport(int p_Report_Type)
    {
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();

        DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));

        GeoHierarchyController RptGeoHierarchyCtl = new GeoHierarchyController();
        DataSet ds=null;
        ds = RptGeoHierarchyCtl.SelectGeoHierarchy(int.Parse(drpZone.SelectedValue.ToString()), int.Parse(drpRegion.SelectedValue.ToString()), int.Parse(drpDistrict.SelectedValue.ToString()));


        SAMSBusinessLayer.Reports.CrpGeoHierarchy CrpReport = new SAMSBusinessLayer.Reports.CrpGeoHierarchy();

        CrpReport.SetDataSource(ds);

        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
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