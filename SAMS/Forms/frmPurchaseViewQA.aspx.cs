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
using System.Drawing;

public partial class Forms_frmPurchaseViewQA : System.Web.UI.Page
{
    readonly  PurchaseController _purchaseCtrl= new PurchaseController();

    private DataTable m_SKUDt;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ltrlNew.Text = "<a href='frmSaleReturn.aspx?LevelID=" + Request["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26'>Add Sale Return</a>";
            Session.Remove("PurchaseQAId");
            Session.Remove("VendId");

            LoadData();
            LoadGrid();
        }
    }
    private void LoadData()
    {
        DataTable m_SKUDt = _purchaseCtrl.SelectPurchaseDocumentNo(2, Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.LongNullValue, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CURRENTWORKDATE"]), 2);

        Session.Add("m_SKUDt", m_SKUDt);
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
                case 3:
                    m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    m_SKUDt.DefaultView.RowFilter = "VENDOR_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdSaleOrder.DataSource = m_SKUDt.DefaultView;
            grdSaleOrder.DataBind();
        }
    }


    protected void grdSaleOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            int index = gvr.RowIndex;

            if (e.CommandName == "Edt")
            {
                Session.Add("PurchaseQAId", grdSaleOrder.Rows[index].Cells[0].Text);
                Session.Add("VendId", grdSaleOrder.Rows[index].Cells[2].Text);
                Response.Redirect("frmPurchaseQA.aspx?LevelID=39&LevelType=3&TopID=27");
            }
           
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
        }

    }
    protected void grdSaleOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text.ToString() != "" && e.Row.Cells[6].Text.ToString() != "&nbsp;")
                {
                    e.Row.BackColor = Color.PaleGreen;
                }
            }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();

    }
}