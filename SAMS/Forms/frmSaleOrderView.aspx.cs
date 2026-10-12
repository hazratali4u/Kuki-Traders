using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSBusinessLayer.Reports;

public partial class Forms_frmSaleOrderView : System.Web.UI.Page
{

    readonly OrderEntryController oeCtrl = new OrderEntryController();
   
    private DataTable m_SKUDt;
    private DataTable dtInvoice;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("orderId");
            Session.Remove("invoiceId");
            LoadData();
            LoadInvoice();
            LoadGrid();
            //if (Session["reportId"] != null)
            //{
            //    GetInvoice(Convert.ToInt64(Session["reportId"].ToString()));
            //}
            //Session.Remove("reportId");
        }
    }

    private void LoadData()
    {
       // m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DataTable m_SKUDt = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 1, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
   
        Session.Add("m_SKUDt", m_SKUDt);
    }

    private void LoadInvoice()
    {
        DataTable dtInvoice = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 2, int.Parse(Session["UserID"].ToString()), Constants.DateNullValue);

        Session.Add("dtInvoice", dtInvoice);

        grdInvoice.DataSource = dtInvoice;
        grdInvoice.DataBind();
    }
    
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)Session["m_SKUDt"];
        if (m_SKUDt != null)
        {

            switch (ddSearchType.SelectedIndex)
            {

                case 1:
                    m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    m_SKUDt.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdSaleOrder.DataSource = m_SKUDt.DefaultView;
            grdSaleOrder.DataBind();
        }


        dtInvoice = (DataTable)Session["dtInvoice"];

        if (dtInvoice != null)
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dtInvoice.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdInvoice.DataSource = dtInvoice.DefaultView;
            grdInvoice.DataBind();
        }
        
    }

    
    protected void grdSaleOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Session.Add("orderId", grdSaleOrder.Rows[e.NewEditIndex].Cells[0].Text);

        //Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID=34&LevelType=3&TopID=26");
        Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID="+ Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString()+"&TopID=26");
        
    }
    protected void grdSaleOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       // int m_sku_id = int.Parse(grdSaleOrder.Rows[e.RowIndex].Cells[0].Text);
        this.LoadData();
        this.LoadGrid();
    }

    protected void grdInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Session.Add("invoiceId", grdInvoice.Rows[e.NewEditIndex].Cells[1].Text);

        Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID="+ Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString()+"&TopID=26");
    }

    protected void grdInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // int m_sku_id = int.Parse(grdSaleOrder.Rows[e.RowIndex].Cells[0].Text);
        this.LoadData();
        this.LoadGrid();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
        
    }

    private void GetInvoice(long sId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(Convert.ToInt32(Session["Distributor_id"].ToString()));

            DataSet ds = null;

            ds = oeCtrl.SelectOrderforView(sId, 1);
            //if (ds.Rows.Count <= 0) return;

            CrpInvoicePrinting2 crpOrder = new CrpInvoicePrinting2();
            crpOrder.SetDataSource(ds);
            crpOrder.Refresh();

            crpOrder.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
            crpOrder.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpOrder.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //---------------------------------------------------------------------------------\\

            //---------------------------------------------------------------------------------\\


            Session.Add("CrpReport", crpOrder);
            Session.Add("ReportType", 0);

            //const string url = "'Default.aspx'";
            //Response.Write("<script>window.open(" + url + ",'_blank');</script>");
            Session.Add("CrpReport", crpOrder);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);


        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}