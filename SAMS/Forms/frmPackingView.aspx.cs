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


public partial class Forms_frmPackingView : System.Web.UI.Page
{
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    private DataTable dtPacking;
    private static int m_sku_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("packingId");
            ltrlNew.Text = "<a href='frmPacking.aspx?LevelID=" + Request["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'>Add Packing List</a>";
          
            LoadPacking();
            if (Session["reportId"] != null)
            {
                GetPacking(Convert.ToInt64(Session["reportId"].ToString()));
            }
            Session.Remove("reportId");
        }
    }



    private void LoadPacking()
    {
        DataTable dt = oeCtrl.SelectPackingList(Convert.ToInt32(Session["Distributor_id"].ToString()), int.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        if (dt.Rows.Count > 0)
        {
            grdInvoice.DataSource = dt;
            grdInvoice.DataBind();
        }
        else
        {
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
    }

    private void LoadGrid()
    {
        DataTable dt = oeCtrl.SelectPackingList(Constants.IntNullValue, int.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));


        if (dt != null)
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dt.DefaultView.RowFilter = "DistributorName" + " like '%" + "" + "%'";
                    break;
            }

            grdInvoice.DataSource = dt.DefaultView;
            grdInvoice.DataBind();
        }

    }

   

    protected void grdInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "Print")
            {
                GetPacking(Convert.ToInt64(row.Cells[0].Text));
            }
            else if (e.CommandName == "Remove")
            {
                oeCtrl.DeletePackingList(Convert.ToInt64(row.Cells[0].Text));
                LoadPacking();
            }
            else
            {
                Session.Add("packingId", row.Cells[0].Text);

                Response.Redirect("frmPacking.aspx?LevelID=101&LevelType=3&TopID=26");
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();

    }

    private void GetPacking(long sId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(Convert.ToInt32(Session["Distributor_id"].ToString()));

            DataSet ds = null;

            ds = oeCtrl.SelectOrderforView(sId, 2);
            //if (ds.Rows.Count <= 0) return;

            CrpPackingPrinting crpOrder = new CrpPackingPrinting();
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